using AutoMapper;
using AutoMapper.QueryableExtensions;
using RestaurantApp.BLL.Dtos.OrderDtos;
using RestaurantApp.DAL.Interfaces;
using RestaurantApp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using RestaurantApp.BLL.Interfaces;

namespace RestaurantApp.BLL.Services
{
    public class OrderService : IOrderService
    {

        private readonly IRepository<Order> _orderRepo;
        private readonly IRepository<MenuItem> _menuItemRepo;
        private readonly IRepository<OrderItem> _orderItemRepo;
        private readonly IMapper _mapper;

        public OrderService(
            IRepository<Order> orderRepo,
            IRepository<MenuItem> menuItemRepo,
            IRepository<OrderItem> orderItemRepo,
            IMapper mapper)
        {
            _orderRepo = orderRepo;
            _menuItemRepo = menuItemRepo;
            _orderItemRepo = orderItemRepo;
            _mapper = mapper;
        }

        public async Task AddOrderAsync(OrderCreateDto orderCreateDto)
        {
            if (orderCreateDto.OrderItems == null || !orderCreateDto.OrderItems.Any())
                throw new Exception("Заказ должен содержать хотя бы один элемент");

            foreach (var item in orderCreateDto.OrderItems)
            {
                if (!await _menuItemRepo.IsExistAsync(m => m.Id == item.MenuItemId))
                    throw new Exception($"MenuItem с ID {item.MenuItemId} не найден");

                if (item.Count <= 0)
                    throw new Exception("Количество должно быть больше 0");
            }

            var allOrders = await _orderRepo.GetAll(filter: null).ToListAsync();
            var maxNumber = allOrders.Any()
                ? allOrders.Max(o => int.Parse(o.Number.Substring(1)))
                : 0;
            var newNumber = $"O{(maxNumber + 1):D3}";

            var order = new Order
            {
                Number = newNumber,
                Date = DateTime.Now,
                OrderItems = new List<OrderItem>()
            };

            decimal totalAmount = 0;
            int orderItemNumber = 1;

            foreach (var itemDto in orderCreateDto.OrderItems)
            {
                var menuItem = await _menuItemRepo.GetByIdAsync(itemDto.MenuItemId);

                var orderItem = new OrderItem
                {
                    Number = $"{newNumber}-{orderItemNumber:D2}",
                    MenuItemId = itemDto.MenuItemId,
                    Count = itemDto.Count
                };

                order.OrderItems.Add(orderItem);
                totalAmount += menuItem.Price * itemDto.Count;
                orderItemNumber++;
            }

            order.TotalAmount = totalAmount;

            await _orderRepo.AddAsync(order);
            await _orderRepo.SaveChangeAsync();
        }

        public async Task RemoveOrderAsync(string number)
        {
            var order = await _orderRepo.GetAll(isTracking: true, filter: o => o.Number == number)
                .FirstOrDefaultAsync();

            if (order == null)
                throw new Exception($"Заказ с номером '{number}' не найден");

            _orderRepo.Delete(order);
            await _orderRepo.SaveChangeAsync();
        }


        public async Task<List<OrderReturnDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepo.GetAll(isTracking: false, filter: null, "OrderItems")
                .ProjectTo<OrderReturnDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return orders;
        }

        public async Task<List<OrderReturnDto>> GetOrdersByDateIntervalAsync(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                throw new Exception("Начальная дата не может быть больше конечной");

            var orders = await _orderRepo.GetAll(
                isTracking: false,
                filter: o => o.Date.Date >= startDate.Date && o.Date.Date <= endDate.Date,
                "OrderItems")
                .ProjectTo<OrderReturnDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return orders;
        }

        public async Task<List<OrderReturnDto>> GetOrdersByDateAsync(DateTime date)
        {
            var orders = await _orderRepo.GetAll(
                isTracking: false,
                filter: o => o.Date.Date == date.Date,
                "OrderItems")
                .ProjectTo<OrderReturnDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return orders;
        }

        public async Task<List<OrderReturnDto>> GetOrdersByPriceIntervalAsync(decimal minPrice, decimal maxPrice)
        {
            if (minPrice < 0 || maxPrice < 0)
                throw new Exception("Цена не может быть отрицательной");

            if (minPrice > maxPrice)
                throw new Exception("Минимальная цена не может быть больше максимальной");

            var orders = await _orderRepo.GetAll(
                isTracking: false,
                filter: o => o.TotalAmount >= minPrice && o.TotalAmount <= maxPrice,
                "OrderItems")
                .ProjectTo<OrderReturnDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return orders;
        }

        public async Task<OrderDetailReturnDto> GetOrderByNumberAsync(string number)
        {
            var order = await _orderRepo.GetAll(
                isTracking: false,
                filter: o => o.Number == number,
                "OrderItems", "OrderItems.MenuItem")
                .FirstOrDefaultAsync();

            if (order == null)
                throw new Exception($"Заказ с номером '{number}' не найден");

            var orderDetailDto = _mapper.Map<OrderDetailReturnDto>(order);
            return orderDetailDto;
        }
    }
}
