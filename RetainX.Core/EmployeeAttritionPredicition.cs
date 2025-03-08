namespace RetainX.Core
{
    public class EmployeeAttritionPredicition
    {
        public string Name { get; set; }
        public int EmployeeId { get; set; }
        public string Department { get; set; }

        public bool Attrition { get; set; }
        public float RiskScore { get; set; }
    }
}
