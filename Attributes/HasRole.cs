using System.Security.Claims;
using Api.Enums;
using Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class HasRole: Attribute, IAsyncAuthorizationFilter
{
    private readonly List<string> _requiredRoles;
    private readonly AllOrAnyEnum _mode;

    public HasRole(string requiredRoles, AllOrAnyEnum mode = AllOrAnyEnum.Any)
    {
        _requiredRoles = requiredRoles.Split(",").Select(r => r.Trim()).ToList();
        _mode = mode;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var authRepository = context.HttpContext.RequestServices.GetRequiredService<IAuthRepository>();

        var email = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(email))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var user = await authRepository.GetAuthenticatedUser(email);
        if (user == null)
        {
            context.Result = new ForbidResult();
            return;
        }

        var roles = user.Roles.Select(role => role.Name).ToList();

        bool hasAccess = _mode == AllOrAnyEnum.All
            ? _requiredRoles.All(role => roles.Contains(role)) 
            : _requiredRoles.Any(role => roles.Contains(role));

        if (!hasAccess)
        {
            context.Result = new ForbidResult();
            return;
        }
    }
}