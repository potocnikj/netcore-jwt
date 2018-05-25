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
        public ActionResult Get()
        {
            return Ok(_svc.Decode(HttpContext.Request.Query["jwt"].ToString()));
        }
    }
}
