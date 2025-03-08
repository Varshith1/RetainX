using Microsoft.ML;

namespace RetainX.Application.Interfaces
{
    public interface IModelTrainingService
    {
        ITransformer TrainModel();
    }
}
