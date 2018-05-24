using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace netcore_jwt.Controllers
{
    [Route("v1/[controller]")]
    public class DecodeController : Controller
    {
        [HttpGet]
        public string Get()
        {
            var decodedBytes = Convert.FromBase64String(HttpContext.Request.Query["jwt"].ToString());
            
            return System.Text.Encoding.Unicode.GetString(decodedBytes);
        }
     
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
    }
}
