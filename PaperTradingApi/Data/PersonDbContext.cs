using Microsoft.EntityFrameworkCore;
using PaperTrading.Data;
using PaperTrading.Models;
using PaperTradingApi.Models;

namespace PaperTradingApi.Data
{
    public class PersonDbContext:DbContext
    {
        public PersonDbContext(DbContextOptions<PersonDbContext> options) : base(options)
        {


        }
        public DbSet<UserDetails> UserDetail { get; set; }
        public DbSet<UserOrders> UserOrder { get; set; }
        public DbSet<StockDetails> StockDetail { get; set; }
        public DbSet<UserAllOrders> UserAllOrders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserOrders>()
                .HasKey(c => new { c.UserName, c.Timestamp });
            modelBuilder.Entity<UserDetails>()
                .HasKey(c => new { c.UserName });
            modelBuilder.Entity<UserAllOrders>()
                .HasKey(c => new { c.UserName, c.Timestamp });
            modelBuilder.Entity<StockDetails>().HasNoKey().ToView("StockDetails");

        }
    }
}
