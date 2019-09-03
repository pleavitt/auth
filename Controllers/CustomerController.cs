using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace webapplication.Controllers
{

    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "John Doe", "Jane Doe" };
        }

    }
}
