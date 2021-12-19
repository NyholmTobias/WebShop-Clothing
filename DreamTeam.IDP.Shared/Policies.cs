using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamTeam.IDP.Shared
{
    public static class Policies
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";

        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder() //Lägg till alla nya claims här för att Admin ska ha tillgång till allt
                .RequireAuthenticatedUser()
                .RequireClaim("admin")
                .RequireClaim("customer")
                .Build();
        }

        public static AuthorizationPolicy CustomerPolicy()
        {
            return new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireClaim("customer")
                .Build();
        }
    }

   
}
