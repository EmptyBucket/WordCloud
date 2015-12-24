using System.IO;
using System.Text;

namespace WordCloudMVVM.Model.Read
{
    public class TXTReader : ITextReader
    {
        private readonly Encoding mEncoding;

        public string ReadAll(Stream stream)
        {
            using (StreamReader reader = new StreamReader(stream, mEncoding))
                return reader.ReadToEnd();
        }

        public string ReadAll(string path) =>
            File.ReadAllText(path);

        public TXTReader(Encoding encoding)
        {
            mEncoding = encoding;
        }

        public TXTReader()
        {
            mEncoding = Encoding.ASCII;
        }
    }
}
