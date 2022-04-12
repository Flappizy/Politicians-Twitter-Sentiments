using Contracts.Sentiment;
using GroupDocs.Classification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizLogic.AppClasses
{
    public class SentimentClassifierWrappedClass : SentimentClassifier, ISentimentClassifier
    {
        public SentimentClassifierWrappedClass() : base()
        {
        }
    }
}
