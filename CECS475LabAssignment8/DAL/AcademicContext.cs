using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CECS475LabAssignment8.Models;
using Microsoft.EntityFrameworkCore;

namespace CECS475LabAssignment8.DAL
{
    public class AcademicContext : DbContext
    {
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public AcademicContext(DbContextOptions<AcademicContext> options): base(options)
        {
            Database.EnsureCreated();
        }


    }

}
