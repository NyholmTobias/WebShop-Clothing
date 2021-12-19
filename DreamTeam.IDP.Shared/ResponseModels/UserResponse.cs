using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamTeam.IDP.Shared.ResponseModels
{
    public class UserResponse : IdentityUser
    {
        public Guid UserID { get; set; }
        public bool Success { get; set; }

    }
}
