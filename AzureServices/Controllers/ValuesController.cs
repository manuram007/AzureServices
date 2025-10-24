using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzureServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly string _value;
        private readonly IConfiguration _configuration;
        public ValuesController(IConfiguration configuration)
        {
            _configuration = configuration;
            _value = _configuration.GetValue<string>("Greetings") ?? "";
        }
        [HttpGet]
        public async Task<IActionResult> GetMessage()
        {
            return Ok(new { result=_value });
        }
    }
}
