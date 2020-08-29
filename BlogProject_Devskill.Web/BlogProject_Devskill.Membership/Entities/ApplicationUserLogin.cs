using System;
using Microsoft.AspNetCore.Identity;

namespace BlogProject_Devskill.Membership.Entities
{
    public class ApplicationUserLogin
        : IdentityUserLogin<Guid>
    {
        public ApplicationUserLogin()
            : base()
        {

        }
    }
}
