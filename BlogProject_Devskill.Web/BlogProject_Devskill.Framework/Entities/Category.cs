using BlogProject_Devskill.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject_Devskill.Framework.Entities
{
    public class Category : IEntity<int>
    {
        public string Name { get; set; }
        public int PostCount { get; set; }
        public bool IsChecked { get; set; }
        public IList<BlogCategory> BlogCategories { get; set; }
        public Category()
        {
            this.BlogCategories = new List<BlogCategory>();
        }
    }
}
