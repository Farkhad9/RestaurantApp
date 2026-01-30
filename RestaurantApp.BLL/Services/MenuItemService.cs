using AutoMapper;
using AutoMapper.QueryableExtensions;
using RestaurantApp.BLL.Dtos.MenuItemDtos;
using RestaurantApp.DAL.Concretes;
using RestaurantApp.DAL.Interfaces;
using RestaurantApp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using RestaurantApp.BLL.Interfaces;

namespace RestaurantApp.BLL.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IRepository<MenuItem> _menuItemRepo;
        private readonly IMapper _mapper;

        public MenuItemService(IRepository<MenuItem> menuItemRepo, IMapper mapper)
        {
            _menuItemRepo = menuItemRepo;
            _mapper = mapper;
        }

        public async Task AddMenuItemAsync(MenuItemCreateDto menuItemCreateDto)
        {
            if (await _menuItemRepo.IsExistAsync(m => m.Name.ToLower() == menuItemCreateDto.Name.ToLower()))
                throw new Exception($"MenuItem с названием '{menuItemCreateDto.Name}' уже существует");

            var allMenuItems = await _menuItemRepo.GetAll().ToListAsync();
            var maxNumber = allMenuItems.Any()
                ? allMenuItems.Max(m => int.Parse(m.Number.Substring(1)))
                : 0;
            var newNumber = $"M{(maxNumber + 1):D3}";

            var menuItem = _mapper.Map<MenuItem>(menuItemCreateDto);
            menuItem.Number = newNumber;
            await _menuItemRepo.AddAsync(menuItem);
            await _menuItemRepo.SaveChangeAsync();
        }

        public async Task RemoveMenuItemAsync(string number)
        {
            var menuItem = await _menuItemRepo.GetAll(false, m => m.Number == number)
                .FirstOrDefaultAsync();

            if (menuItem == null)
                throw new Exception($"MenuItem с номером '{number}' не найден");
            _menuItemRepo.Delete(menuItem);
            await _menuItemRepo.SaveChangeAsync();
        }

        public async Task EditMenuItemAsync(MenuItemUpdateDto menuItemUpdateDto)
        {
            var menuItem = await _menuItemRepo.GetByIdAsync(menuItemUpdateDto.Id);

            if (menuItem == null)
                throw new Exception($"MenuItem с ID {menuItemUpdateDto.Id} не найден");

            if (await _menuItemRepo.IsExistAsync(m =>
                m.Name.ToLower() == menuItemUpdateDto.Name.ToLower() && m.Id != menuItemUpdateDto.Id))
                throw new Exception($"MenuItem с названием '{menuItemUpdateDto.Name}' уже существует");

            menuItem.Name = menuItemUpdateDto.Name;
            menuItem.Price = menuItemUpdateDto.Price;
            await _menuItemRepo.SaveChangeAsync();
        }

        public async Task<List<MenuItemReturnDto>> GetAllMenuItemsAsync()
        {
            var menuItems = await _menuItemRepo.GetAll(isTracking: false, filter: null)
                .ProjectTo<MenuItemReturnDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return menuItems;
        }

        public async Task<List<MenuItemReturnDto>> GetMenuItemsByCategoryAsync(string category)
        {
            var menuItems = await _menuItemRepo.GetAll(isTracking: false, filter: m => m.Category.ToLower() == category.ToLower())
                .ProjectTo<MenuItemReturnDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return menuItems;
        }

        public async Task<List<MenuItemReturnDto>> GetMenuItemsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            if (minPrice < 0 || maxPrice < 0)
                throw new Exception("Цена не может быть отрицательной");

            if (minPrice > maxPrice)
                throw new Exception("Минимальная цена не может быть больше максимальной");

            var menuItems = await _menuItemRepo.GetAll(isTracking: false, filter: m => m.Price >= minPrice && m.Price <= maxPrice)
                .ProjectTo<MenuItemReturnDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return menuItems;
        }

        public async Task<List<MenuItemReturnDto>> SearchMenuItemsAsync(string searchValue)
        {
            if (string.IsNullOrWhiteSpace(searchValue))
                throw new Exception("Значение поиска не может быть пустым");

            var menuItems = await _menuItemRepo.GetAll(isTracking: false, filter: m => m.Name.ToLower().Contains(searchValue.ToLower()))
                .ProjectTo<MenuItemReturnDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return menuItems;
        }
    }
}

