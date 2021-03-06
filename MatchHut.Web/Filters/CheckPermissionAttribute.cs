using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MatchHut.Web.Filters
{
    public class CheckPermissionAttribute : ActionFilterAttribute
    {
        private readonly Permission _permission;
        private readonly IHttpContextAccessor _contextAccessor;
        private HttpContext _context => _contextAccessor.HttpContext;

        public CheckPermissionAttribute(Permission permission, IHttpContextAccessor contextAccessor)
        {
            _permission = permission;
            _contextAccessor = contextAccessor;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (_context != null)
            {
                if (_context.User != null)
                {
                    var hasClaim = _context.User.HasClaim(c => c.Value == _permission.ToString().ToLowerInvariant());

                    if (!hasClaim)
                    {
                        context.Result = new UnauthorizedResult();
                    }
                }
            }

            base.OnActionExecuting(context);
        }
    }

    public enum Permission
    {
        User,
        Config,
        Company
    }
}