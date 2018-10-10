using System;
using Microsoft.EntityFrameworkCore;

namespace SharePass.Models
{
    public class PassView
    {
        public string Password;
        public string Salt;
        public string Link;
        public DateTime TimeCreated;
    }

        public class SharePassContext : DbContext
    {
        public SharePassContext(DbContextOptions<SharePassContext> options)
            : base(options)
        {
        }

        public DbSet<PassView> Passwords { get; set; }
    }
}