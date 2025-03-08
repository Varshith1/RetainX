using Microsoft.ML;
using RetainX.Application.Interfaces;

namespace RetainX.Application
{
    public class ModelService : IModelService
    {
        private ITransformer _trainedModel;
        private readonly IModelTrainingService _modelTrainingService;

        public ModelService(IModelTrainingService modelTrainingService)
        {
            _modelTrainingService = modelTrainingService;
        }

        public ITransformer GetTrainedModel()
        {
            // If the model is null, we train a new one and return it
            if (_trainedModel == null)
            {
                    _trainedModel = _modelTrainingService.TrainModel();
            }

            return _trainedModel;
        }

        // Optional: Clear the model if needed (e.g., when refreshing or updating it)
        public void ClearModel()
        {
            _trainedModel = null;
        }
    }
}
