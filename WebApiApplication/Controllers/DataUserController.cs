using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiApplication.Controllers
{
    [ApiController]
    [Route("user/[controller]")]
    public class DataUserController : ControllerBase
    {
        private readonly ILogger<DataUser> _logger;

        public DataUserController(ILogger<DataUser> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IEnumerable<DataUser> Get(DataUser user)
        {
            var rnd = new Random();
            return Enumerable.Range(1, 3).Select(index => new DataUser
            {
                Name = user.Name,
                SurName = user.SurName,
                Age = rnd.Next(18, 25)
            })
            .ToArray();
        }
        [HttpPost]
        public async Task<ActionResult<DataUser>> Post(DataUser user)
        {
            if(user.Name == null)
            {
                return BadRequest(404); 
            }
            return  Ok();
        }
    }
}
