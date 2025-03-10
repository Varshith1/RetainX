using Microsoft.AspNetCore.Mvc;
using RetainX.Application.Interfaces;
using RetainX.Core;

namespace RetainX.WebAPI.Controllers
{
    [Route("api/retainx/Features")]
    public class FeatureSelectionController : Controller
    {
        private readonly IFeatureService _featureService;

        public FeatureSelectionController(IFeatureService featureService)
        {
            _featureService = featureService;
        }
        // POST endpoint to receive the feature weights and store them
        [HttpPost]
        [Route("SaveFeatureWeights")]
        public async Task SaveFeatureWeights([FromBody] List<FeatureWeightDetails> featureWeights)
        {
            _featureService.UpdateFeatures(featureWeights);
        }
    }
}
