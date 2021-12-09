using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Controllers.Helpers
{
    public static class ContextHelper
    {
        /// <summary>
        /// When passed "User" object inside ControllerBase subclass, returns
        /// the value at "sub" in the jwt; for this app that is the userId
        /// of the logged in user.
        /// e.g: ContextHelper.GetSubFromClaimsPrincipalJWT(User) => \<userId\>
        /// </summary>
        /// <param name="claimsPrincipal"></param>
        /// <returns>userId of authenticated user or -1 if null/does not exist</returns>
        public static int GetSubFromClaimsPrincipalJWT(System.Security.Claims.ClaimsPrincipal claimsPrincipal)
        {
            int? userId = int.Parse(claimsPrincipal.FindFirst("sub")?.Value);
            return userId == null ? -1 : userId.Value;
        }
    }
}
