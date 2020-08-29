using Autofac;
using BlogProject_Devskill.Membership.Entities;
using BlogProject_Devskill.Membership.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Web.Areas.Admin.Models.AdminControl
{
    public class EditAdminModel : AdminBaseModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Display(Name = "Date Of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMMM, yyyy}")]
        public DateTime? DateOfBirth { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile ProfileImage { get; set; }

        private readonly IApplicationUserService _applicationUserService;

        public EditAdminModel()
        {
            _applicationUserService = Startup.AutofacContainer.Resolve<IApplicationUserService>();
        }

        public EditAdminModel(IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }

        public async Task LoadByIdAsync(Guid id)
        {
            var user = await _applicationUserService.GetByIdAsync(id);
            this.Id = user.Id;
            this.FullName = user.FullName;
            this.Email = user.Email;
            this.ImageUrl = user.ImageUrl;
            this.DateOfBirth = user.DateOfBirth;
            this.PhoneNumber = user.PhoneNumber;
            this.Address = user.Address;
            this.Gender = user.Gender;
            this.ProfileImage = null;
        }

        public async Task<string> ResetPasswordAsync(Guid id,string pass)
        {
            var name = await _applicationUserService.ResetPasswordAsync(id, pass);
            return name;
        }

        public async Task UpdateAsync()
        {
            var user = new ApplicationUser
            {
                Id = this.Id,
                FullName = this.FullName,
                UserName = this.Email,
                Email = this.Email,
                ImageUrl = this.ImageUrl,
                PhoneNumber = this.PhoneNumber,
                Gender = this.Gender,
                DateOfBirth = this.DateOfBirth,
                Address = this.Address
            };

            await _applicationUserService.UpdateAsync(user);
        }
    }
}
