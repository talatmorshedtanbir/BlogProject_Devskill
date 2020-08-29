using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        Task SaveChangesAsync();
    }
}
