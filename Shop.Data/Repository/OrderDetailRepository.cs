using Microsoft.EntityFrameworkCore;
using Shop.Data.Context;
using Shop.Domain.Interfaces;
using Shop.Domain.Models.Order;

namespace Shop.Data.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {

        #region construtor

        private readonly ShopContext _context;

        public OrderDetailRepository(ShopContext context)
        {
            _context = context;
        }

        #endregion

        public async Task AddOrderDetail(OrderDetail orderDetail)
        {
            orderDetail.CreateDate = DateTime.Now;
            await _context.OrderDetails.AddAsync(orderDetail);
            await SaveChange();
        }

        public Task<List<OrderDetail>> GetAllOrderDetailByOrderId(int orderId)
        {
            return _context.OrderDetails.Where(o => o.OrderId == orderId).ToListAsync();
        }

        public async Task<OrderDetail> GetOrderDetailById(int id)
        {
            return await _context.OrderDetails.FindAsync(id);
        }

        public async Task SaveChange()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderDetail(OrderDetail orderDetail)
        {
            _context.OrderDetails.Update(orderDetail);
            await SaveChange();
        }
    }
}
