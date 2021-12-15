using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Capstone.Models;
using Capstone.DAO;


namespace Capstone.Controllers
{
    public class Controller : ControllerBase
    {
        protected bool isAuthorized(int accountId)
        {
            bool isAuthorized = false;

            System.Security.Claims.ClaimsPrincipal Principal = HttpContext.User;
            if (Principal?.Claims != null)
            {
                IEnumerable<System.Security.Claims.Claim> AccountIdClaim = Principal.Claims.Where(x => x.Type == "sub");
                isAuthorized = AccountIdClaim.Any() ? AccountIdClaim.ToArray()[0].Value == accountId.ToString() : false;
            }

            return isAuthorized;
        }
    }


}
