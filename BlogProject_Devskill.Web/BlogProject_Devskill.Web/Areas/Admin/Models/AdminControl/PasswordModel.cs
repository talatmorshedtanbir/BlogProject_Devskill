using BlogProject_Devskill.Membership.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Web.Areas.Admin.Models.AdminControl
{
    public class PasswordModel : AdminControlBaseModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = "Do not match with new password")]
        public string ConfirmNewPassword { get; set; }

        public PasswordModel() : base() { }
        public PasswordModel(ICurrentUserService currentUserService, IApplicationUserService applicationuserService)
            : base(currentUserService, applicationuserService) { }

        internal async Task<bool> ChangePasswordAsync()
        {
            var result = await _applicationuserService.ChangePasswordAsync(_currentUserService.UserId, CurrentPassword, NewPassword);
            return result;
        }
    }
}
