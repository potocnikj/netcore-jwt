using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Services;

namespace netcore_jwt.Controllers
{
    [Route("v1/[controller]")]
    public class DecodeController : BaseController
    {
        private readonly JwtService _svc;
        
        public DecodeController(JwtService svc)
        {
            _svc = svc;
        }

        [HttpGet]
        public ActionResult Decode(string jwt) // query string works by default
        {
            return Ok(_svc.Decode(jwt));
        }

        [HttpPost]
        public ActionResult DecodeFromBody([FromBody]JwtRequestDTO jwt) // This how you would bind from post body. Although doesn't make sense in this case. Just use query string
        {
            return Ok(_svc.Decode(jwt.Token));
        }
    }
}
