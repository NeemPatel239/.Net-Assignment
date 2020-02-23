using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AssignmentMusic.Models;

namespace AssignmentMusic.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<AssignmentMusic.Models.MusicProducts> MusicProducts { get; set; }
        public DbSet<AssignmentMusic.Models.Company> Company { get; set; }
    }
}
