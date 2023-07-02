using Microsoft.EntityFrameworkCore;
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

    }
}
