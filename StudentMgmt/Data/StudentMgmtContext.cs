using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentMgmt.Models;

namespace StudentMgmt.Data
{
    public class StudentMgmtContext : DbContext
    {
        public StudentMgmtContext (DbContextOptions<StudentMgmtContext> options)
            : base(options)
        {
        }

        public DbSet<StudentMgmt.Models.Student> Student { get; set; }

        public DbSet<StudentMgmt.Models.Course> Course { get; set; }

        public DbSet<StudentMgmt.Models.Enrol> Enrol { get; set; }
    }
}
