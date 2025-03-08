using Microsoft.ML;

namespace RetainX.Application.Interfaces
{
    public interface IModelService
    {
        ITransformer GetTrainedModel();
    }
}
