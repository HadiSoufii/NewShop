using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities.Ticket;
using Shop.Domain.Models.Account;
using Shop.Domain.Models.Permissions;
using Shop.Domain.Models.Product;

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

        #region product

        public DbSet<Product> Products { get; set; } 
        public DbSet<ProductCategory> ProductCategories { get; set; } 
        public DbSet<ProductDiscount> ProductDiscounts { get; set; } 
        public DbSet<ProductGallery> ProductGalleries { get; set; }

        #endregion

        #region reole permission

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        #endregion

        #region on model creating

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }

        #endregion

    }
}
