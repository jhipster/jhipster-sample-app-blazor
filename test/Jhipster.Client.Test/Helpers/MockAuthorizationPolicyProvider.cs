using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Jhipster.Client.Test.Helpers
{
    public class MockAuthorizationPolicyProvider : IAuthorizationPolicyProvider
    {
        private readonly AuthorizationOptions options = new AuthorizationOptions();

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
            => Task.FromResult(options.DefaultPolicy);

        public Task<AuthorizationPolicy> GetFallbackPolicyAsync()
            => Task.FromResult(options.FallbackPolicy);

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName) => Task.FromResult(
            new AuthorizationPolicy(new[]
                {
                    new TestPolicyRequirement { PolicyName = policyName }
                },
                new[] { $"TestScheme:{policyName}" }));
    }
}
