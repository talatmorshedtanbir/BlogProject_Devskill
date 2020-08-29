using Autofac;
using BlogProject_Devskill.Membership.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Web.Areas.Admin.Models.AdminControl
{
    public class AdminInformationModel : AdminBaseModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime Created { get; set; }
        private readonly IApplicationUserService _applicationUserService;

        public AdminInformationModel()
        {
            _applicationUserService = Startup.AutofacContainer.Resolve<IApplicationUserService>();
        }
        public AdminInformationModel(IApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }
        public async Task LoadByIdAsync(Guid id)
        {
            var user = await _applicationUserService.GetByIdAsync(id);
            this.Id = user.Id;
            this.Email = user.Email;
            this.PhoneNumber = user.PhoneNumber;
            this.ImageUrl = user.ImageUrl;
            this.FullName = user.FullName;
            this.Address = user.Address;
            this.Gender = user.Gender;
            this.DateOfBirth = user.DateOfBirth;
            this.EmailConfirmed = user.EmailConfirmed;
            this.IsBlocked = user.IsBlocked;
            this.Created = user.Created;

        }
    }
}
