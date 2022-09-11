using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Account.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsHistoryController : ControllerBase
    {
        // GET: api/<OperationsHistoryController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

    }
}
