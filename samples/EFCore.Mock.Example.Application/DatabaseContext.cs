using System;
using EFCore.Mock.Example.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Mock.Example.Application
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<Blog> Blogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            }
        }
    }
}