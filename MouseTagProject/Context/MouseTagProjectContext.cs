using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MouseTagProject.Models;

namespace MouseTagProject.Context
{
    public class MouseTagProjectContext : IdentityDbContext
    {
        public MouseTagProjectContext(DbContextOptions options) : base(options) { }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<UserDate> UserDates { get; set; }
    }
}
