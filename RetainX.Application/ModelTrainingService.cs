using Microsoft.ML;
using Microsoft.ML.Data;
using Newtonsoft.Json;
using RetainX.Application.Interfaces;
using RetainX.Core;

namespace RetainX.Application
{
    public class ModelTrainingService : IModelTrainingService
    {
        private readonly IFeatureService _featureService;
        public ModelTrainingService(IFeatureService featureService)
        {
            _featureService = featureService;
        }
        public ITransformer TrainModel()
        {
            var mlContext = new MLContext();

            // Load the training data from the JSON file and map it to EmployeeStats
            var trainingData = LoadData("trainingdata.json");

            var selectedFeatures = _featureService.GetFeatures();

            var trainDataView = mlContext.Data.LoadFromEnumerable(trainingData);


            IEstimator<ITransformer> pipeline = mlContext.Transforms.CopyColumns("Label", "Label");

            var transcriptFeature = selectedFeatures.FirstOrDefault(f => f.Feature == "Transcript");
            // Text feature extraction
            if (transcriptFeature != null)
            {
                pipeline = pipeline.Append(mlContext.Transforms.Text.FeaturizeText("TranscriptFeaturized", "Transcript"));
                double weight = transcriptFeature.Weight;

                // Remove "Transcript" from the list
                selectedFeatures.Remove(transcriptFeature);

                // Add "TranscriptFeaturized" with the same weight
                selectedFeatures.Add(new FeatureWeightDetails { Feature = "TranscriptFeaturized", Weight = weight });
            }

            // Convert boolean fields properly
            if (selectedFeatures.Any(f => f.Feature == "ManagerLeft"))
                pipeline = pipeline.Append(mlContext.Transforms.Conversion.ConvertType("ManagerLeft", outputKind: DataKind.Single));

            if (selectedFeatures.Any(f => f.Feature == "IsSalarySatisfied"))
                pipeline = pipeline.Append(mlContext.Transforms.Conversion.ConvertType("IsSalarySatisfied", outputKind: DataKind.Single));

            if (selectedFeatures.Any(f => f.Feature == "PeerLeftCount"))
                pipeline = pipeline.Append(mlContext.Transforms.Conversion.ConvertType("PeerLeftCount", outputKind: DataKind.Single));

            // Concatenate selected features
            var features = selectedFeatures.Select(feature => feature.Feature).ToArray();
            pipeline = pipeline.Append(mlContext.Transforms.Concatenate("Features", features));

            // Convert label to boolean (for classification)
            pipeline = pipeline.Append(mlContext.Transforms.Conversion.ConvertType("Label", outputKind: DataKind.Boolean));

            // Append ML Algorithm
            pipeline = pipeline.Append(mlContext.BinaryClassification.Trainers.LbfgsLogisticRegression(labelColumnName: "Label", featureColumnName: "Features"));

            // **Step 4: Train Model**
            var model = pipeline.Fit(trainDataView);

            var testData = this.LoadData("testingdata.json");
            var testDataView = mlContext.Data.LoadFromEnumerable(testData);
            var predictions = model.Transform(testDataView);
            var metrics = mlContext.BinaryClassification.Evaluate(predictions, labelColumnName: "Label", scoreColumnName: "Score", predictedLabelColumnName: "PredictedLabel");

            Console.WriteLine($"\nModel Evaluation:");
            Console.WriteLine($"Accuracy: {metrics.Accuracy:P2}");
            Console.WriteLine($"AUC: {metrics.AreaUnderRocCurve:P2}");
            Console.WriteLine($"F1 Score: {metrics.F1Score:P2}");

            return model;
        }

        private List<EmployeeStats> LoadData(string fileName)
        {
            var filePath = $@"C:\Users\varshith.k\source\repos\RetainX\RetainX.Core\Data\{fileName}";

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Training data file not found.", filePath);
            }

            var jsonContent = File.ReadAllText(filePath);
            var trainingData = JsonConvert.DeserializeObject<List<EmployeeStats>>(jsonContent);

            if (trainingData == null)
            {
                throw new InvalidDataException("Failed to deserialize training data.");
            }

            return trainingData;
        }
    }
}
