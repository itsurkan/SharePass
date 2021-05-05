using Microsoft.EntityFrameworkCore;

namespace SharePass.Models
{
    public class SharePassContext : DbContext
    {
        public SharePassContext(DbContextOptions<SharePassContext> options)
            : base(options)
        {
        }

        public DbSet<PassModel> Passwords { get; set; }
        public DbSet<SaltModel> Salt { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PassModel>()
                .HasOne(a => a.Salt)
                .WithOne(b => b.EncryptedPass)
                .HasForeignKey<PassModel>(b => b.SaltId);

        }
    }
}