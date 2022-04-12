using Contracts.Sentiment;
using GroupDocs.Classification;
using GroupDocs.Classification.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.SentimentForTweetWith100CharsOrLessClassUnitTests
{
    internal class FakeSentimentClassifier : ISentimentClassifier
    {
        public ClassificationResponse Classify(string text, int bestClassesCount, Taxonomy taxonomy)
        {
            var classificationResponse = new ClassificationResponse
            {
                BestClassName = "Positive",
                BestClassProbability = 0.7254f,
            };

            return classificationResponse;
        }

        public float PositiveProbability(string text)
        {
            return 0.9f;
        }
    }
}
