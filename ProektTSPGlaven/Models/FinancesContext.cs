using Microsoft.EntityFrameworkCore;

namespace ProektTSPGlaven.Models
{
    public class FinancesContext:DbContext
    {
        public DbSet<User> users { get; set; }

        public FinancesContext(DbContextOptions options):base(options) {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
               .HasOne(a => a.User)
               .WithMany(u => u.Accounts)
               .HasForeignKey(a => a.userID);

            modelBuilder.Entity<TransactionEntity>()
                .HasOne(t => t.account)
                .WithMany(a => a.Transactions)
                .HasForeignKey(t => t.accountID);
        }
    }
}
