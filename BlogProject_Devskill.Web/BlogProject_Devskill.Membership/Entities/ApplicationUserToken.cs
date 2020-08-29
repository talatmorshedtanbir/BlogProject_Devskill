using System;
using Microsoft.AspNetCore.Identity;

namespace BlogProject_Devskill.Membership.Entities
{
    public class ApplicationUserToken
        : IdentityUserToken<Guid>
    {
        public ApplicationUserToken()
            : base()
        {

        }
    }
}
