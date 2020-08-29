using BlogProject_Devskill.Framework.MenuItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Web.Areas.Admin.Models
{
    public class MenuModel
    {
        public IList<MenuItem> MenuItems { get; set; }
    }
}
