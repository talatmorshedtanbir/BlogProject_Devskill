using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject_Devskill.Membership.Extensions
{
    public class DuplicationException : Exception
    {
        public DuplicationException(string name)
            : base($"{name} already exists.")
        {
        }
    }
}
