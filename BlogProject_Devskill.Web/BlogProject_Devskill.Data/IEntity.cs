using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject_Devskill.Data
{
    public abstract class IEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}
