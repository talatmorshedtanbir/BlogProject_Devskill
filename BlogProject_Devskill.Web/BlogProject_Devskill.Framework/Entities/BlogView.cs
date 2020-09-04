using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogProject_Devskill.Framework.Entities
{
    public class PostModel
    {
        public BlogPost Blog { get; set; }
        public BlogPost Post { get; set; }
        public BlogPost Older { get; set; }
        public BlogPost Newer { get; set; }
    }
    public class ListModel
    {
        public BlogPost Blog { get; set; }
        public Category Category { get; set; } // posts by category

        public IEnumerable<BlogPost> Posts { get; set; }
        public Pager Pager { get; set; }

        public PostListType PostListType { get; set; }
    }
    public class PageListModel
    {
        public IEnumerable<BlogPost> Posts { get; set; }
        public Pager Pager { get; set; }
    }
    public class PostItem
    {
        public int Id { get; set; }
        [Required]
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        [Required]
        [StringLength(160)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string CoverImageUrl { get; set; }
        [Required]
        public bool Draft { get; set; }
        public DateTime EditingTime { get; set; }
        public DateTime PublicationTime { get; set; }
        public IList<Category> Categories { get; set; }
        public IList<BlogCategory> BlogCategories { get; set; }
        public bool UseAdminInfo { get; set; }
    }
    public enum PostListType
    {
        Blog, Category, Author, Search
    }
    public class PostListFilter
    {
        HttpRequest _req;

        public PostListFilter(HttpRequest request)
        {
            _req = request;
        }

        public string Page
        {
            get
            {
                return string.IsNullOrEmpty(_req.Query["page"])
                    ? "" : _req.Query["page"].ToString();
            }
        }
        public string Status
        {
            get
            {
                return string.IsNullOrEmpty(_req.Query["status"])
                    ? "A" : _req.Query["status"].ToString();
            }
        }
        public string Search
        {
            get
            {
                return string.IsNullOrEmpty(_req.Query["search"])
                    ? "" : _req.Query["search"].ToString();
            }
        }
        public string Qstring
        {
            get
            {
                var q = "";
                if (!string.IsNullOrEmpty(Status)) q += $"&status={Status}";
                if (!string.IsNullOrEmpty(Search)) q += $"&search={Search}";
                return q;
            }
        }

        public string IsChecked(string status)
        {
            return status == Status ? "checked" : "";
        }
    }
}
