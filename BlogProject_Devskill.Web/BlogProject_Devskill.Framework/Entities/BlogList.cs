using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject_Devskill.Framework.Entities
{
    public class BlogList
    {
        public BlogPost Blog { get; set; }
        public string Category { get; set; } // posts by category

        public IEnumerable<BlogPost> Blogs { get; set; }
        public Pager Pager { get; set; }
    }
}
