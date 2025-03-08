using Microsoft.ML.Data;

namespace RetainX.Core
{
    public class EmployeeStats
    {
        public float WorkHours { get; set; }
        public bool ManagerLeft { get; set; }
        public int PeerLeftCount { get; set; }
        public float PulseScoreChange { get; set; }
        public string Transcript { get; set; }
        public bool IsSalarySatisfied { get; set; }

        [ColumnName("Label")]
        public bool Attrition { get; set; }
    }
}
