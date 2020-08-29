﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly DbContext _dbContext;

        public UnitOfWork(DbContext dbContext) => _dbContext = dbContext;

        public void Dispose() => _dbContext?.Dispose();
        public void Save() => _dbContext?.SaveChanges();
        public Task SaveChangesAsync() => _dbContext?.SaveChangesAsync();
    }
}
