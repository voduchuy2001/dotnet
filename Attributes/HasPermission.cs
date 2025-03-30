using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Api.Repositories.Interfaces;
using Api.Enums;

namespace Api.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class HasPermissionAttribute : Attribute, IAuthorizationFilter
{
    private readonly List<string> _permissions;
    private readonly AllOrAnyEnum _mode;

    public HasPermissionAttribute(string permissions, AllOrAnyEnum mode = AllOrAnyEnum.Any)
    {
        _permissions = permissions.Split(",").Select(p => p.Trim()).ToList();
        _mode = mode;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var authRepository = context.HttpContext.RequestServices.GetRequiredService<IAuthRepository>();

        var email = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(email))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var user = authRepository.GetAuthenticatedUser(email).Result;
        if (user is null)
        {
            context.Result = new ForbidResult();
            return;
        }

        var userPermissions = user.Roles
            .SelectMany(role => role.Permissions)
            .Select(permission => permission.Name)
            .Distinct()
            .ToList();

        bool hasAccess = _mode == AllOrAnyEnum.All
            ? _permissions.All(permission => userPermissions.Contains(permission))
            : _permissions.Any(permission => userPermissions.Contains(permission));

        if (!hasAccess)
        {
            context.Result = new ForbidResult();
            return;
        }
    }
}