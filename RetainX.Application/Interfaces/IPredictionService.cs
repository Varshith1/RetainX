using RetainX.Core;

namespace RetainX.Application.Interfaces
{
    public interface IPredictionService
    {
        EmployeeAttritionPredicition Predict(int employeeId);

    }
}
