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
    }
}
