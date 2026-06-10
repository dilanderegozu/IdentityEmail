using IdentityEmail.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace IdentityEmail.Context
{
    public class EmailContext : IdentityDbContext<AppUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=Project2IdentityDb; Integrated Security=True; TrustServerCertificate=True;");
        }
        public DbSet<MessageCategory> MessageCategories { get; set; }
        public DbSet<UserMessage> UserMessages { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserMessage>().HasOne(c=>c.Category).WithMany(c=>c.Messages).HasForeignKey(c=>c.CategoryId).OnDelete(DeleteBehavior.ClientSetNull);
            builder.Entity<UserMessage>().HasOne(c=>c.Sender).WithMany(c=>c.SendMessages).HasForeignKey(c => c.SenderId).OnDelete(DeleteBehavior.ClientSetNull);
            builder.Entity<UserMessage>().HasOne(c=>c.Receiver).WithMany(c=>c.ReceivedMessages).HasForeignKey(c => c.ReceiverId).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
