using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        protected ApiBaseController()
        {
        }

        [NonAction]
        protected ActionResult CustomResponse(GenericResponse genericResponse)
        {
            return genericResponse.OutputType switch
            {
                EOutputType.Success => OkResponse(genericResponse),
                EOutputType.InvalidInput => BadRequestResponse(genericResponse.Message),
                EOutputType.BusinessValidation => Conflict(genericResponse.Message),
                EOutputType.NotFound => NotFound(),
                EOutputType.IntegrationError => BadRequestResponse(genericResponse.Message),
                _ => StatusCode(500, genericResponse.Message),
            };
        }

        private ActionResult OkResponse(GenericResponse genericResponse)
        {
            return Ok(new
            {
                success = genericResponse.Success,
                data = genericResponse.Data,
                message = genericResponse.Message
            });
        }

        private ActionResult BadRequestResponse(string message = null)
        {
            return BadRequest(new
            {
                success = false,
                message = message
            });
        }
    }
}