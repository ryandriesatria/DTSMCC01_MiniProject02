using Microsoft.EntityFrameworkCore;
using MiniProject02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniProject02.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Products> Products { get; set; }
        public DbSet<Masters> Masters { get; set; }
        public DbSet<Details> Details{ get; set; }
        public DbSet<Employee> Employee{ get; set; }
        public DbSet<User> User{ get; set; }
        public DbSet<Role> Role{ get; set; }
        public DbSet<UserRole> UserRole{ get; set; }




    }
}
