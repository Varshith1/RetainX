namespace RetainX.Core
{
    public class EmployeeInfo
    {
        public string Name { get; set; }
        public int EmployeeId { get; set; }
        public string Department { get; set; }
        public int WorkHours { get; set; }
        public bool ManagerLeft { get; set; }
        public int PeerLeftCount { get; set; }
        public int PulseScoreChange { get; set; }
        public string Transcript { get; set; }
        public bool IsSalarySatisfied { get; set; }
        public bool Attrition { get; set; }
    }
}
