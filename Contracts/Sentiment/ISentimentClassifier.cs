using GroupDocs.Classification;
using GroupDocs.Classification.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Sentiment
{
    public interface ISentimentClassifier
    {
        ClassificationResponse Classify(string text, int bestClassesCount = 1, Taxonomy taxonomy = Taxonomy.Sentiment);
        float PositiveProbability(string text);
    }
}
