using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject_Devskill.Membership.Services
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
        bool IsAuthenticated { get; }
    }
}
