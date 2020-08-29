using BlogProject_Devskill.Framework.Entities;
using BlogProject_Devskill.Framework.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Web.Areas.User.Models
{
    public class ListModel
    {
        public IList<BlogPost> Blogs { get; set; }
        public Pager Pager { get; set; }
        public string Category { get; set; } // posts by category
        public PostListType PostListType { get; set; }
    }
}
