using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MKTFY.Auth.Services
{
    public sealed class ProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<User> _userClaimsPrincipalFactory;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ProfileService(
            UserManager<User> userMgr,
            RoleManager<IdentityRole> roleMgr,
            IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory)
        {
            _userManager = userMgr;
            _roleManager = roleMgr;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            // Get the user
            string sub = context.Subject.GetSubjectId();
            User user = await _userManager.FindByIdAsync(sub);

            // Get the list of already assigned claims
            ClaimsPrincipal userClaims = await _userClaimsPrincipalFactory.CreateAsync(user);
            List<Claim> claims = userClaims.Claims.ToList();
            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();

            // Get the user's roles
            IList<string> roles = await _userManager.GetRolesAsync(user);
            foreach (var roleName in roles)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, roleName));  // Add the role to the token claims
                IdentityRole role = await _roleManager.FindByNameAsync(roleName);
                if (role != null)
                    claims.AddRange(await _roleManager.GetClaimsAsync(role));  // Roles can have a set of claims associated to them directly
            }

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            string sub = context.Subject.GetSubjectId();
            User user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
