using Shop.Domain.Models.Order;

namespace Shop.Domain.Interfaces
{
    public interface IOrderDetailRepository
    {
        Task<List<OrderDetail>> GetAllOrderDetailByOrderId(int orderId);
        Task<OrderDetail> GetOrderDetailById(int id);
        Task AddOrderDetail(OrderDetail orderDetail);
        Task UpdateOrderDetail(OrderDetail orderDetail);
        Task SaveChange();
    }
}
