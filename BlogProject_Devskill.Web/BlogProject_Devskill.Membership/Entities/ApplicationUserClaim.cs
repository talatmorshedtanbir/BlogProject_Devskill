using System;
using Microsoft.AspNetCore.Identity;

namespace BlogProject_Devskill.Membership.Entities
{
    public class ApplicationUserClaim
        : IdentityUserClaim<Guid>
    {
        public ApplicationUserClaim()
            : base()
        {

        }
    }
}
