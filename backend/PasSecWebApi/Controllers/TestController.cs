using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PasSecWebApi.Controllers
{
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Route("api/test")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Test()
        {
            return NoContent();
        }

        [HttpGet]
        [Authorize]
        [Route("api/test-auth")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult TestAuth()
        {
            return NoContent();
        }
    }
}
