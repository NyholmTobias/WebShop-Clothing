using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamTeam.IDP.Shared.RequestModels
{
    public class UserRequest : IdentityUser
    {
        public Guid UserID { get; set; }
        public string ClaimName { get; set; }

        public string ClaimValue { get; set; }

        public string Password { get; set; }

        public List<IdentityUserClaim<string>> Claims { get; set; }
    }
}
