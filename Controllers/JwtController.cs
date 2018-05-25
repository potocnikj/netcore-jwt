using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Services;

namespace netcore_jwt.Controllers
{
  [Route("v1")]
  public class JwtController : BaseController
  {
    private readonly JwtService jwtService;

    public JwtController(JwtService jwtService)
    {
      this.jwtService = jwtService;
    }

    [HttpGet("decode")]
    public IActionResult Decode(string token)
    {
      var result = this.jwtService.Decode(token);
      return Ok(result);
    }

    [HttpGet("encode")]
    public IActionResult Encode(string data)
    {
      var result = this.jwtService.Encode(data);
      return Ok(result);
    }
  }
}
