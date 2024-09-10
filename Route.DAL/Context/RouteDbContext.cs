using Microsoft.EntityFrameworkCore;
using Route.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.DAL.Context
{
    public class RouteDbContext : DbContext
    {
        public RouteDbContext(DbContextOptions<RouteDbContext> options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
