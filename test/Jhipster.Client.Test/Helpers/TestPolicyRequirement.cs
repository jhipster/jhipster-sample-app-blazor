using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace Jhipster.Client.Test.Helpers
{
    public class TestPolicyRequirement : IAuthorizationRequirement
    {
        public string PolicyName { get; set; }
    }
}
