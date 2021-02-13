using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskTracker.IdentityServer.Models;

namespace TaskTracker.IdentityServer
{
    public class UserValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserValidator(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            // a static set of username, passwords
            // for demonstration
            // in production scenarios, we can inject a
            // repository or DbContext via DependencyInjection
            // into the constructor
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var username = context.UserName;
            var password = context.Password;

            var result = _signInManager.PasswordSignInAsync(username, password, false, lockoutOnFailure: true).Result;
            if (result.Succeeded)
            {
                var user = _userManager.FindByNameAsync(username).Result;
                if (user != null)
                {
                    // context set to success
                    context.Result = new GrantValidationResult(
                    subject: username,
                    authenticationMethod: "custom",
                    claims: new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier, username)
                    }
                );
                    return Task.FromResult(0);
                }
            }
            // context set to Failure        
            context.Result = new GrantValidationResult(
                    TokenRequestErrors.UnauthorizedClient, "Invalid Crdentials");

            return Task.FromResult(0);
        }
    }
}
