using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RouxAcademy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouxAcademy.Data
{
    public class StudentDataContext : IdentityDbContext
    {
        public StudentDataContext(DbContextOptions<StudentDataContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<UniGrad> uniGrads { get; set; }
 
    }
}
