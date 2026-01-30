using RestaurantApp.BLL.Dtos.OrderDtos;

namespace RestaurantApp.BLL.Interfaces
{
    public interface IOrderService
    {
        Task AddOrderAsync(OrderCreateDto orderCreateDto);
        Task<List<OrderReturnDto>> GetAllOrdersAsync();
        Task<OrderDetailReturnDto> GetOrderByNumberAsync(string number);
        Task<List<OrderReturnDto>> GetOrdersByDateAsync(DateTime date);
        Task<List<OrderReturnDto>> GetOrdersByDateIntervalAsync(DateTime startDate, DateTime endDate);
        Task<List<OrderReturnDto>> GetOrdersByPriceIntervalAsync(decimal minPrice, decimal maxPrice);
        Task RemoveOrderAsync(string number);
    }
}