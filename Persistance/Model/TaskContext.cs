using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Persistance.Extensions;
using Task.Domain;

namespace Persistance.Models
{
    public partial class TaskContext : DbContext
    {
        public TaskContext()
        {
        }

        public TaskContext(DbContextOptions<TaskContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }
       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseEntityTypeConfiguration<TaskContext>();
        }
    }
}
