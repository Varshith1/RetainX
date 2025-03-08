using AutoMapper;
using Microsoft.ML;
using Newtonsoft.Json;
using RetainX.Application.Interfaces;
using RetainX.Core;

namespace RetainX.Application
{
    public class PredictionService : IPredictionService
    {
        private readonly IModelService _modelService;
        private readonly IFeatureService _featureService;
        private readonly ITransformer _model;
        private readonly IMapper _mapper;

        public PredictionService(IModelService modelService, IFeatureService featureService, IMapper mapper)
        {
            _modelService = modelService;
            _model = _modelService.GetTrainedModel(); // Get the model (ensures it's trained)
            _mapper = mapper;
            _featureService = featureService;
        }
        public EmployeeAttritionPredicition Predict(int employeeId)
        {
            MLContext mlContext = new MLContext();

            // Fetch employee details (use appropriate method to fetch the employee data)
            var employee = this.GetEmployeeDetails(employeeId);

            if (employee == null)
            {
                throw new NullReferenceException("Employee not found");
            }

            // Map employee data to EmployeeStats
            var employeeStats = _mapper.Map<EmployeeStats>(employee);

            // Create a prediction engine for the trained model
            var predictor = mlContext.Model.CreatePredictionEngine<EmployeeStats, AttritionPrediction>(_model);

            // Predict using the selected features
            var predictionResult = predictor.Predict(employeeStats);

            return new EmployeeAttritionPredicition
            {
                EmployeeId = employeeId,
                Name = employee.Name,
                Department = employee.Department,
                Attrition = predictionResult.Attrition,
                RiskScore = predictionResult.Probability * 100,
            };

        }

        private EmployeeInfo GetEmployeeDetails(int employeeId)
        {
            var filePath = ("C:\\Users\\varshith.k\\source\\repos\\RetainX\\RetainX.Core\\Data\\EmployeeData.json");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Training data file not found.", filePath);
            }

            var jsonContent = File.ReadAllText(filePath);

            // Deserialize the JSON data into a list of EmployeeInfo
            var employeeList = JsonConvert.DeserializeObject<List<EmployeeInfo>>(jsonContent);

            // Find the employee by employeeId
            var employee = employeeList.FirstOrDefault(e => e.EmployeeId == employeeId);

            // Return the employee details if found, otherwise return null
            return employee;
        }
    }
}
