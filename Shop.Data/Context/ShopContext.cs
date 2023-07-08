using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities.Ticket;
using Shop.Domain.Models.Account;

namespace Shop.Data.Context
{
    public class ShopContext:DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {
            
        }

        #region account

        public DbSet<User> Users { get; set; }

        #endregion

        #region ticket

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketMessage> TicketMessages { get; set; }

        #endregion

        #region on model creating

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            //modelBuilder.Entity<TicketMessage>()
            //    .HasOne(t => t.Sender)
            //    .WithMany(s => s.TicketMessages)
            //    .HasForeignKey(s=> s.SenderId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<TicketMessage>()
            //    .HasOne(t => t.Ticket)
            //    .WithMany(s => s.TicketMessages)
            //    .HasForeignKey(s => s.TicketId)
            //    .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

        #endregion

    }
}
