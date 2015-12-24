﻿using System.Text.RegularExpressions;

namespace WordCloudMVVM.Model.WordParse.Clean
{
    public class TextCleaner : ICleaner
    {
        public string Clean(string text)
        {
            string punctuation = "’'\\[\\],(){}⟨⟩<>:‒…!.\\‐\\-?„“«»“”‘’‹›;1234567890_\\-+=\\/|@#$%^&*\"\r\n\t";
            string punctuationPattern = $"[{punctuation}]";
            Regex punctuationReg = new Regex(punctuationPattern);

            string lotSpacePattern = " {2,}";
            Regex lotSpaceReg = new Regex(lotSpacePattern);

            string cleanPunctuationText = punctuationReg.Replace(text, " ");
            string cleanText = lotSpaceReg.Replace(cleanPunctuationText, " ");

            return cleanText;
        }
    }
}
