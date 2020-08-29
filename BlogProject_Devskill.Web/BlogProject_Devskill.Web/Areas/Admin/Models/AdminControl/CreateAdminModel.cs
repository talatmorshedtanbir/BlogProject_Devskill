using BlogProject_Devskill.Membership.Constants;
using BlogProject_Devskill.Membership.Entities;
using BlogProject_Devskill.Membership.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Web.Areas.Admin.Models.AdminControl
{
    public class CreateAdminModel : AdminControlBaseModel
    {
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Date Of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMMM, yyyy}")]
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }

        public string ImageUrl { get; set; }
        public CreateAdminModel() : base() { }

        public CreateAdminModel(IApplicationUserService applicationUserService) : base(applicationUserService) { }


        public async Task CreateAdminAsync()
        {
            var user = new ApplicationUser
            {
                FullName = this.FullName,
                UserName = this.Email,
                Email = this.Email,
                PhoneNumber = this.PhoneNumber,
                Gender = this.Gender,
                DateOfBirth = this.DateOfBirth,
                Address = this.Address,
                EmailConfirmed = true,
                Created = DateTime.Now
                
            };

            await _applicationUserService.AddAsync(user, ConstantsValue.UserRoleName.Admin, "Defau1tp@ss");
        }
    }
}
