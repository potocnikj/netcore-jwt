
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using netcore_jwt.DTO;
using Services;
using System.Text;

namespace netcore_jwt.Middleware
{
  public class JwtMiddleware
  {
    private readonly RequestDelegate next;
    private static readonly JsonSerializer serializer = new JsonSerializer()
    {
      ContractResolver = new CamelCasePropertyNamesContractResolver()
    };

    private static readonly UTF8Encoding encoding = new UTF8Encoding(false);
    private static readonly PathString encodePathV1 = new PathString("/v1/encode");
    private static readonly PathString decodePathV1 = new PathString("/v1/decode");

    public JwtMiddleware(RequestDelegate next)
    {
      this.next = next;
    }

    public Task Invoke(HttpContext context, JwtService service)
    {
      if (context.Request.Path.StartsWithSegments(encodePathV1, StringComparison.Ordinal))
      {
        var data = context.Request.Query["jwt"];
        var token = service.Encode(data);

        var result = ResponseDTO.Create(token);
        using (var sw = new StreamWriter(context.Response.Body, encoding))
        {
          serializer.Serialize(sw, result);
        }
      }
      if (context.Request.Path.StartsWithSegments(decodePathV1, StringComparison.Ordinal))
      {
        var token = context.Request.Query["jwt"];
        var data = service.Decode(token);

        var result = ResponseDTO.Create(data);
        
        using (var sw = new StreamWriter(context.Response.Body, encoding))
        {
          serializer.Serialize(sw, result);
        }
      }
      return Task.CompletedTask;
    }
  }
}