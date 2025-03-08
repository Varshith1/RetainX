using Microsoft.ML.Data;

namespace RetainX.Core
{
    public class AttritionPrediction
    {
        [ColumnName("PredictedLabel")]
        public bool Attrition { get; set; }
        public float Probability { get; set; }
        public float Score { get; set; }
    }
}
