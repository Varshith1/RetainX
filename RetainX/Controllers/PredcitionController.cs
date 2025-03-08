using Microsoft.AspNetCore.Mvc;
using RetainX.Application;
using RetainX.Application.Interfaces;
using RetainX.Core;

namespace RetainX.WebAPI.Controllers
{
    [Route("api/retainx")]
    public class PredcitionController : Controller
    {
        private readonly IPredictionService _predictionService;

        // Constructor to inject PredictionService
        public PredcitionController(IPredictionService predictionService)
        {
            _predictionService = predictionService;
        }

        // POST endpoint to predict attrition based on employee information
        [HttpGet("predictAttrition/{employeeId:int}")]
        public ActionResult<EmployeeAttritionPredicition> PredictAttrition( int employeeId)
        {

            // Make prediction using PredictionService
            var predictionResult = _predictionService.Predict(employeeId);

            if (predictionResult == null)
            {
                return StatusCode(500, "Error during prediction.");
            }

            // Return the prediction result
            return Ok(predictionResult);
        }
    }
}
