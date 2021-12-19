

using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DreamTeam.IDP.Shared.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Guid UserID { get; set; }
        public List<IdentityUserClaim<string>> IdentityUserClaims { get; set; }
    }
}
