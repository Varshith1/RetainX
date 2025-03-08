using Newtonsoft.Json;
using RetainX.Application.Interfaces;
using RetainX.Core;

namespace RetainX.Application
{
    public class FeatureService : IFeatureService
    {
        private readonly string _filePath = "C:\\Users\\varshith.k\\source\\repos\\RetainX\\RetainX.Core\\Data\\feature_weights.json";

        public List<FeatureWeightDetails> GetFeatures()
        {
            if (!File.Exists(_filePath))
            {
                throw new FileNotFoundException("Training data file not found.", _filePath);
            }

            var jsonContent = File.ReadAllText(_filePath);

            return JsonConvert.DeserializeObject<List<FeatureWeightDetails>>(jsonContent);
        }

        public async void UpdateFeatures(List<FeatureWeightDetails> features)
        {
            if (features == null || features.Count == 0)
            {
                throw new Exception("Need to have atleast one feature");
            }
            var json = JsonConvert.SerializeObject(features, Formatting.Indented);

            // Write the JSON to a file
            await System.IO.File.WriteAllTextAsync(_filePath, json);
        }
    }
}
