using Microsoft.AspNetCore.Mvc;
using netcore_jwt.DTO;

namespace netcore_jwt.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        public override OkObjectResult Ok(object value) => base.Ok(ResponseDTO.Create(value));
    }
}