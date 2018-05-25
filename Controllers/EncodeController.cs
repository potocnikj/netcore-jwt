using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Services;

namespace netcore_jwt.Controllers
{
    [Route("v1/[controller]")]
    public class EncodeController : BaseController
    {
        private readonly JwtService _svc;
        
        public EncodeController(JwtService svc)
        {
            _svc = svc;
        }
        
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_svc.Encode(HttpContext.Request.Query["jwt"].ToString()));
        }
    }
}
