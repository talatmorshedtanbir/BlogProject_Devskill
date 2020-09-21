using BlogProject_Devskill.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject_Devskill.Framework.Entities
{
    public class BlogCategory : IEntity<int>
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; }
    }
}
