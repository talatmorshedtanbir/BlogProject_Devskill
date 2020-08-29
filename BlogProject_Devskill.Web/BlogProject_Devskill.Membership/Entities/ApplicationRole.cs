﻿using BlogProject_Devskill.Membership.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace BlogProject_Devskill.Membership.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public EnumApplicationRoleStatus Status { get; set; }

        public Guid? CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public IList<ApplicationUserRole> UserRoles { get; set; }

        public ApplicationRole()
            : base()
        {
            this.IsActive = true;
            this.IsDeleted = false;
            this.UserRoles = new List<ApplicationUserRole>();
        }

        public ApplicationRole(string roleName)
            : base(roleName)
        {
            this.IsActive = true;
            this.IsDeleted = false;
            this.UserRoles = new List<ApplicationUserRole>();
        }
    }
}
