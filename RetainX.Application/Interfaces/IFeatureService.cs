using RetainX.Core;

namespace RetainX.Application.Interfaces
{
    public interface IFeatureService
    {

        void UpdateFeatures(List<FeatureWeightDetails> features);

        List<FeatureWeightDetails> GetFeatures();
    }
}
