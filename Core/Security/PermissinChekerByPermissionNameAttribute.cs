using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Core.Security
{
    public class PermissionCheckerByPermissionNameAttribute(string permissionName) : AuthorizeAttribute, IAuthorizationFilter
    {
        private IPermissionService? _permissionService;
        private readonly string _permissionName = permissionName;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _permissionService = (IPermissionService)context.HttpContext.RequestServices.GetService(typeof(IPermissionService))!;

            if (context.HttpContext.User.Identity!.IsAuthenticated)
            {
                string username = context.HttpContext.User.Identity!.Name!;
                if (string.IsNullOrEmpty(username))
                {
                    context.Result = new RedirectResult("/AccessDenied");
                }
                if (!_permissionService.CheckPermissionByName(_permissionName!, username))
                {

                    context.Result = new RedirectResult("/AccessDenied");
                }
            }
            else
            {
                context.Result = new RedirectResult("/Login");
            }
        }


    }
}
