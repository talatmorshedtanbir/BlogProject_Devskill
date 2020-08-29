using System;
using Microsoft.AspNetCore.Identity;

namespace BlogProject_Devskill.Membership.Entities
{
    public class ApplicationRoleClaim
        : IdentityRoleClaim<Guid>
    {
        public ApplicationRoleClaim()
            : base()
        {

        }
    }
}
