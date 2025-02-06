using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace JhipsterSampleApplication.Client.Test.Helpers;

public class MockAuthorizationService : IAuthorizationService
{
    public List<(ClaimsPrincipal user, object resource, IEnumerable<IAuthorizationRequirement> requirements)> AuthorizeCalls { get; }
        = new List<(ClaimsPrincipal user, object resource, IEnumerable<IAuthorizationRequirement> requirements)>();

    public Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object resource, IEnumerable<IAuthorizationRequirement> requirements)
    {
        AuthorizeCalls.Add((user, resource, requirements));

        return Task.FromResult(user.Identities.Any() ? AuthorizationResult.Success() : AuthorizationResult.Failed());
    }

    public Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object resource, string policyName)
        => throw new NotImplementedException();
}
