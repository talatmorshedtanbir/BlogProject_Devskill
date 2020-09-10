using BlogProject_Devskill.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogProject_Devskill.Framework.Entities
{
    public class Comment : IEntity<int>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Description { get; set; }
        public int BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; }
        [Required]
        public bool IsAuthorized { get; set; }
        public DateTime CommentTime { get; set; }
    }
}
