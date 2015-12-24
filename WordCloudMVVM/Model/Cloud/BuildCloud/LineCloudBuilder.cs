﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using WordCloudMVVM.Model.Cloud.BuildCloud.Intersection;

namespace WordCloudMVVM.Model.Cloud.BuildCloud
{
    public class LineCloudBuilder : ICloudBuilder
    {
        private readonly IIntersectionChecker mIntersectionChecher;

        public LineCloudBuilder(IIntersectionChecker intersectionChecher)
        {
            mIntersectionChecher = intersectionChecher;
        }

        public Dictionary<WordStyle, Geometry> BuildWordsGeometry(IEnumerable<WordStyle> words, int imageWidth, int imageHeight, int maxFont)
        {
            var sortWords = words.OrderByDescending(word => word.FontSize);

            Dictionary<WordStyle, Geometry> geometryWords = new Dictionary<WordStyle, Geometry>();

            using (IEnumerator<WordStyle> enumeratorWords = sortWords.GetEnumerator())
            {
                List<Geometry> prewLineGeometry = new List<Geometry>();
                for (double coordY = 0; coordY < imageHeight;)
                {
                    List<Geometry> currentLineGeometry = new List<Geometry>();
                    for (double coordX = 0; coordX < imageWidth && enumeratorWords.MoveNext();)
                    {
                        WordStyle word = enumeratorWords.Current;
                        Geometry geometryWord = GeometryExtended.GetWordGeometry(word, new Point(coordX, coordY));

                        for (double offset = 1; mIntersectionChecher.CheckIntersection(geometryWord, prewLineGeometry); offset++)
                            geometryWord = GeometryExtended.GetWordGeometry(word, new Point(coordX, coordY + offset));

                        geometryWords.Add(word, geometryWord);
                        currentLineGeometry.Add(geometryWord);

                        coordX = geometryWord.GetGeometryRight() + 1;
                    }
                    coordY = currentLineGeometry.Max(geometry => geometry.GetGeometryDown());
                    prewLineGeometry = currentLineGeometry.ToList();
                }
            }

            return geometryWords;
        }
    }
}
