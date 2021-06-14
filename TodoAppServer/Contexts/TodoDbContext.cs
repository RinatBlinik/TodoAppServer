using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAppServer.Models.Entities;

namespace TodoAppServer.Contexts
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {

        }

        public DbSet<TodoItem> Items { get; set; }
        public DbSet<TodoList> Lists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Specific
            //modelBuilder.ApplyConfiguration<Contact>(new ContactConfiguration());

            // Automatic Discovery off all Configurations in Assembly            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TodoItem).Assembly);
        }

    }
}
