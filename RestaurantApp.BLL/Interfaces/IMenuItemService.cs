using RestaurantApp.BLL.Dtos.MenuItemDtos;

namespace RestaurantApp.BLL.Interfaces
{
    public interface IMenuItemService
    {
        Task AddMenuItemAsync(MenuItemCreateDto menuItemCreateDto);
        Task EditMenuItemAsync(MenuItemUpdateDto menuItemUpdateDto);
        Task<List<MenuItemReturnDto>> GetAllMenuItemsAsync();
        Task<List<MenuItemReturnDto>> GetMenuItemsByCategoryAsync(string category);
        Task<List<MenuItemReturnDto>> GetMenuItemsByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task RemoveMenuItemAsync(string number);
        Task<List<MenuItemReturnDto>> SearchMenuItemsAsync(string searchValue);
    }
}