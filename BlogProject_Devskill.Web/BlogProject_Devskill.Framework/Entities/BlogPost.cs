using BlogProject_Devskill.Data;
using BlogProject_Devskill.Membership.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;

namespace BlogProject_Devskill.Framework.Entities
{
    public class BlogPost : IEntity<int>
    {
        [Required]
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorImageUrl { get; set; }

        [Required]
        [StringLength(160)]
        public string Title { get; set; }

        [Required]
        [StringLength(450)]
        public string Description { get; set; }

        [Required]
        public string CoverImageUrl { get; set; }
        [Required]
        public bool Draft { get; set; }
        public bool UseAdminInfo { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastEditTime { get; set; }
        public DateTime PublishTime { get; set; }
        public IList<Comment> Comments { get; set; }
        public IList<BlogCategory> BlogCategories { get; set; }

        public BlogPost()
        {
            this.Comments = new List<Comment>();
            this.BlogCategories = new List<BlogCategory>();
        }
    }
}
