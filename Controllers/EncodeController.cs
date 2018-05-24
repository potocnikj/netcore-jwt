using System;
using Microsoft.AspNetCore.Mvc;

using Services;

namespace netcore_jwt.Controllers
{
    [Route("v1/[controller]")]
    public class EncodeController : Controller
    {
        private readonly JwtService _svc;
        
        public EncodeController(JwtService svc)
        {
            _svc = svc;
        }
        
        [HttpGet]
        public string Get()
        {
            return this._svc.Encode(HttpContext.Request.Query["jwt"].ToString());
        }
    }
}
