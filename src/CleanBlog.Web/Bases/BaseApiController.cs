using CleanBlog.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CleanBlog.Web.Bases
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public abstract class BaseApiController : ControllerBase
    {
        protected ActionResult SendOk([ActionResultObjectValue] object value) =>
            value switch
            {
                ApiResponse => base.Ok(value),
                _ => base.Ok(new ApiResponse(value)),
            };

        protected ActionResult SendBadRequest([ActionResultObjectValue] object error) =>
            error switch
            {
                ApiResponse => base.BadRequest(error),
                string => base.BadRequest(new ApiResponse((string)error)),
                IEnumerable<string> => base.BadRequest(new ApiResponse(errors: (IList<string>)error)),
                _ => base.BadRequest(new ApiResponse("bad request."))
            };

        protected ActionResult SendNotFound([ActionResultObjectValue] object value) =>
            value switch
            {
                ApiResponse => base.NotFound(value),
                string => base.NotFound(new ApiResponse((string)value)),
                _ => base.NotFound(new ApiResponse("data not found."))
            };

        protected ActionResult SendCreated(string uri, [ActionResultObjectValue] object value) =>
            value switch
            {
                ApiResponse => base.Created(uri, value),
                _ => base.Created(uri, new ApiResponse(value))
            };

        protected ActionResult SendCreatedAtAction(string actionName, [ActionResultObjectValue] object value) =>
            value switch
            {
                ApiResponse => base.CreatedAtAction(actionName, value),
                _ => base.CreatedAtAction(actionName, new ApiResponse(value)),
            };

        protected ActionResult SendNoContent()
        {
            return base.NoContent();
        }
    }
}