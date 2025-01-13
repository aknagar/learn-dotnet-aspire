using System.Diagnostics.CodeAnalysis;
using Api.Authorization;
using AuthorizationPolicy = Api.Authorization.AuthorizationPolicy;

namespace eShopLite.Api.Routes;

public static class RouteGroupBuilderExtensions
{
    public static RouteGroupBuilder MapUserGroup(this IEndpointRouteBuilder endpoints,
        [StringSyntax("Route")] string prefix, string? groupTagName = null,
        string[]? extraRequiredPolicies = null)
    {
        extraRequiredPolicies ??= [];

        var group = endpoints.MapGroup(prefix).WithGroupName("User");
            //.RequireAuthorization([AuthorizationPolicy.User, ..extraRequiredPolicies]);

        if (groupTagName != null)
            group.WithTags(groupTagName);

        //group.WithOpenApi();

        return group;
    }
    
    public static RouteGroupBuilder MapAdminGroup(this IEndpointRouteBuilder endpoints,
        [StringSyntax("Route")] string prefix, string? groupTagName = null,
        string[]? extraRequiredPolicies = null)
    {
        extraRequiredPolicies ??= [];

        var group = endpoints.MapGroup(prefix).WithGroupName("Admin");
           // .RequireAuthorization([AuthorizationPolicy.Admin, ..extraRequiredPolicies]);

        if (groupTagName != null)
            group.WithTags(groupTagName);

        //group.WithOpenApi();

        return group;
    }
}
