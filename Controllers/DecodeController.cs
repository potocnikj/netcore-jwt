using System;
using Microsoft.AspNetCore.Mvc;

using Services;

namespace netcore_jwt.Controllers
{
    [Route("v1/[controller]")]
    public class DecodeController : Controller
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
