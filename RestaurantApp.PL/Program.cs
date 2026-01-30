using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestaurantApp.BLL.Interfaces;
using RestaurantApp.BLL.Profiles;
using RestaurantApp.BLL.Services;
using RestaurantApp.DAL.Concretes;
using RestaurantApp.DAL.Data;
using RestaurantApp.DAL.Interfaces;
using System;
using System.Threading.Tasks;

var serviceCollection = new ServiceCollection();

serviceCollection.AddDbContext<RestaurantAppDbContext>(options =>
    options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=RestaurantAppDB;Trusted_Connection=True;TrustServerCertificate=True;"));
serviceCollection.AddLogging();

serviceCollection.AddAutoMapper(options =>
{
    options.AddProfile<MapperProfile>();
});

serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
serviceCollection.AddScoped<IMenuItemService, MenuItemService>();
serviceCollection.AddScoped<IOrderService, OrderService>();

var serviceProvider = serviceCollection.BuildServiceProvider();

var menuItemService = serviceProvider.GetService<IMenuItemService>();
var orderService = serviceProvider.GetService<IOrderService>();

Console.OutputEncoding = System.Text.Encoding.UTF8;

while (true)
{
    Console.Clear();
    Console.WriteLine("╔════════════════════════════════════════╗");
    Console.WriteLine("║  Restaurant Management System          ║");
    Console.WriteLine("╚════════════════════════════════════════╝");
    Console.WriteLine();
    Console.WriteLine("1 - Menu uzerinde emeliyyat aparmaq");
    Console.WriteLine("2 - Sifarisler uzerinde emeliyyat aparmaq");
    Console.WriteLine("0 - Sistemden cixmaq");
    Console.WriteLine();
    Console.Write("Seciminiz: ");

    var mainChoice = Console.ReadLine()?.Trim();

    switch (mainChoice)
    {
        case "1":
            await MenuItemOperations(menuItemService);
            break;
        case "2":
            await OrderOperations(orderService, menuItemService);
            break;
        case "0":
            Console.WriteLine("\nSistemden cixilir...");
            return;
        default:
            Console.WriteLine("\nYanlish secim! Zehmet olmasa 0, 1 ve ya 2 daxil edin.");
            Console.WriteLine("Davam etmek ucun Enter basin...");
            Console.ReadLine();
            break;
    }
}

static async Task MenuItemOperations(IMenuItemService menuItemService)
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║         Menu Items                     ║");
        Console.WriteLine("╚════════════════════════════════════════╝");
        Console.WriteLine();
        Console.WriteLine("1 - Yeni item elave et");
        Console.WriteLine("2 - Item uzerinde duzelis et");
        Console.WriteLine("3 - Item sil");
        Console.WriteLine("4 - Butun Item-lari goster");
        Console.WriteLine("5 - Categoriyasina gore menu item-lari goster");
        Console.WriteLine("6 - Qiymet araligina gore menu item-lar goster");
        Console.WriteLine("7 - Menu item-lar arasinda ada gore axtaris et");
        Console.WriteLine("0 - Evvelki menuya qayit");
        Console.WriteLine();
        Console.Write("Seciminiz: ");

        var choice = Console.ReadLine()?.Trim();

        switch (choice)
        {
            case "1":
                await AddMenuItem(menuItemService);
                break;
            case "2":
                await EditMenuItem(menuItemService);
                break;
            case "3":
                await RemoveMenuItem(menuItemService);
                break;
            case "4":
                await ShowAllMenuItems(menuItemService);
                Console.WriteLine("\nDavam etmek ucun Enter basin...");
                Console.ReadLine();
                break;
            case "5":
                await ShowMenuItemsByCategory(menuItemService);
                Console.WriteLine("\nDavam etmek ucun Enter basin...");
                Console.ReadLine();
                break;
            case "6":
                await ShowMenuItemsByPriceRange(menuItemService);
                Console.WriteLine("\nDavam etmek ucun Enter basin...");
                Console.ReadLine();
                break;
            case "7":
                await SearchMenuItems(menuItemService);
                Console.WriteLine("\nDavam etmek ucun Enter basin...");
                Console.ReadLine();
                break;
            case "0":
                return;
            default:
                Console.WriteLine("\nYanlish secim! Zehmet olmasa 0-7 arasi reqem daxil edin.");
                Console.WriteLine("\nDavam etmek ucun Enter basin...");
                Console.ReadLine();
                break;
        }
    }
}

static async Task OrderOperations(IOrderService orderService, IMenuItemService menuItemService)
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║           Orders                       ║");
        Console.WriteLine("╚════════════════════════════════════════╝");
        Console.WriteLine();
        Console.WriteLine("1 - Yeni sifaris elave etmek");
        Console.WriteLine("2 - Sifarisin legvi");
        Console.WriteLine("3 - Butun sifarislerin ekrana cixarilmasi");
        Console.WriteLine("4 - Verilen tarix araligina gore sifarislerin gosterilmesi");
        Console.WriteLine("5 - Verilen mebleg araligina gore sifarislerin gosterilmesi");
        Console.WriteLine("6 - Verilmis bir tarixde olan sifarislerin gosterilmesi");
        Console.WriteLine("7 - Verilmis nomreye esasen hemin nomreli sifarisin melumatlarinin gosterilmesi");
        Console.WriteLine("0 - Evvelki menuya qayit");
        Console.WriteLine();
        Console.Write("Seciminiz: ");

        var choice = Console.ReadLine()?.Trim();

        switch (choice)
        {
            case "1":
                await AddOrder(orderService, menuItemService);
                break;
            case "2":
                await RemoveOrder(orderService);
                break;
            case "3":
                await ShowAllOrders(orderService);
                Console.WriteLine("\nDavam etmek ucun Enter basin...");
                Console.ReadLine();
                break;
            case "4":
                await ShowOrdersByDateInterval(orderService);
                Console.WriteLine("\nDavam etmek ucun Enter basin...");
                Console.ReadLine();
                break;
            case "5":
                await ShowOrdersByPriceInterval(orderService);
                Console.WriteLine("\nDavam etmek ucun Enter basin...");
                Console.ReadLine();
                break;
            case "6":
                await ShowOrdersByDate(orderService);
                Console.WriteLine("\nDavam etmek ucun Enter basin...");
                Console.ReadLine();
                break;
            case "7":
                await ShowOrderByNumber(orderService);
                Console.WriteLine("\nDavam etmek ucun Enter basin...");
                Console.ReadLine();
                break;
            case "0":
                return;
            default:
                Console.WriteLine("\nYanlish secim! Zehmet olmasa 0-7 arasi reqem daxil edin.");
                Console.WriteLine("\nDavam etmek ucun Enter basin...");
                Console.ReadLine();
                break;
        }
    }
}

static string ReadNonEmptyString(string prompt, bool allowNumbers = false)
{
    while (true)
    {
        Console.Write(prompt);
        var input = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Bu sahe bos ola bilmez! Zehmet olmasa metn daxil edin.");
            continue;
        }

        if (!allowNumbers && decimal.TryParse(input, out _))
        {
            Console.WriteLine("Ad yalniz reqemlerden ibaret ola bilmez! Zehmet olmasa metn daxil edin.");
            continue;
        }

        return input;
    }
}

static decimal ReadDecimal(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        var input = Console.ReadLine()?.Trim();

        if (decimal.TryParse(input, out decimal result) && result >= 0)
        {
            return result;
        }

        Console.WriteLine("Yanlish deyer! Zehmet olmasa musbet reqem daxil edin (meselen: 10.5)");
    }
}

static int ReadInt(string prompt, int min = 1)
{
    while (true)
    {
        Console.Write(prompt);
        var input = Console.ReadLine()?.Trim();

        if (int.TryParse(input, out int result) && result >= min)
        {
            return result;
        }

        Console.WriteLine($"Yanlish deyer! Zehmet olmasa {min}-den boyuk tam reqem daxil edin.");
    }
}

static DateTime ReadDate(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        var input = Console.ReadLine()?.Trim();

        if (DateTime.TryParseExact(input, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime result))
        {
            return result;
        }

        Console.WriteLine("Yanlish tarix formati! Zehmet olmasa dd.MM.yyyy formatinda daxil edin (meselen: 25.01.2024)");
    }
}

static string ReadCategory()
{
    var validCategories = new[] { "Sup", "Ana yemek", "Desert", "Icki" };

    while (true)
    {
        Console.WriteLine("Movcud kateqoriyalar: Sup, Ana yemek, Desert, Icki");
        Console.Write("Kateqoriya: ");
        var input = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Kateqoriya bos ola bilmez!");
            continue;
        }

        var found = validCategories.FirstOrDefault(c =>
            c.Equals(input, StringComparison.OrdinalIgnoreCase));

        if (found != null)
        {
            return found;
        }

        Console.WriteLine("Yanlish kateqoriya! Zehmet olmasa yuxaridaki kateqoriyalardan birini secin.");
    }
}

static bool ReadYesNo(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        var input = Console.ReadLine()?.Trim().ToLower();

        if (input == "b" || input == "beli")
            return true;
        if (input == "x" || input == "xeyr")
            return false;

        Console.WriteLine("Zehmet olmasa 'b' (beli) ve ya 'x' (xeyr) daxil edin.");
    }
}


static async Task AddMenuItem(IMenuItemService menuItemService)
{
    while (true)
    {
        try
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║    Yeni MenuItem Elave Et              ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();

            var name = ReadNonEmptyString("Ad: ");
            var price = ReadDecimal("Qiymet: ");
            var category = ReadCategory();

            var dto = new RestaurantApp.BLL.Dtos.MenuItemDtos.MenuItemCreateDto
            {
                Name = name,
                Price = price,
                Category = category
            };

            await menuItemService.AddMenuItemAsync(dto);
            Console.WriteLine("\n✓ MenuItem ugurla elave edildi!");
            Console.WriteLine("\nDavam etmek ucun Enter basin...");
            Console.ReadLine();
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n✗ Xeta: {ex.Message}");
            if (!ReadYesNo("\nYeniden cehd etmek isteyirsiniz? (b/x): "))
            {
                return;
            }
        }
    }
}

static async Task EditMenuItem(IMenuItemService menuItemService)
{
    while (true)
    {
        try
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║      MenuItem Duzelis Et               ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();

            await ShowAllMenuItems(menuItemService);
            Console.WriteLine();

            var id = ReadInt("Duzelis etmek istediyiniz MenuItem ID-si: ");
            var name = ReadNonEmptyString("Yeni ad: ");
            var price = ReadDecimal("Yeni qiymet: ");

            var dto = new RestaurantApp.BLL.Dtos.MenuItemDtos.MenuItemUpdateDto
            {
                Id = id,
                Name = name,
                Price = price
            };

            await menuItemService.EditMenuItemAsync(dto);
            Console.WriteLine("\n✓ MenuItem ugurla yenilendi!");
            Console.WriteLine("\nDavam etmek ucun Enter basin...");
            Console.ReadLine();
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n✗ Xeta: {ex.Message}");
            if (!ReadYesNo("\nYeniden cehd etmek isteyirsiniz? (b/x): "))
            {
                return;
            }
        }
    }
}

static async Task RemoveMenuItem(IMenuItemService menuItemService)
{
    while (true)
    {
        try
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║         MenuItem Sil                   ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();

            await ShowAllMenuItems(menuItemService);
            Console.WriteLine();

            var number = ReadNonEmptyString("Silmek istediyiniz MenuItem nomresi (meselen: M001): ", allowNumbers: true);

            if (!number.StartsWith("M", StringComparison.OrdinalIgnoreCase) || number.Length != 4)
            {
                Console.WriteLine("\n✗ Nomre duzgun formatda deyil! Format: M001, M002, və s.");
                if (!ReadYesNo("\nYeniden cehd etmek isteyirsiniz? (b/x): "))
                {
                    return;
                }
                continue;
            }

            await menuItemService.RemoveMenuItemAsync(number);
            Console.WriteLine("\n✓ MenuItem ugurla silindi!");
            Console.WriteLine("\nDavam etmek ucun Enter basin...");
            Console.ReadLine();
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n✗ Xeta: {ex.Message}");
            if (!ReadYesNo("\nYeniden cehd etmek isteyirsiniz? (b/x): "))
            {
                return;
            }
        }
    }
}

static async Task ShowAllMenuItems(IMenuItemService menuItemService)
{
    try
    {
        var items = await menuItemService.GetAllMenuItemsAsync();

        if (items == null || !items.Any())
        {
            Console.WriteLine("Hec bir MenuItem tapilmadi.");
            return;
        }

        Console.WriteLine($"{"ID",-5} {"Nomre",-10} {"Ad",-25} {"Qiymet",-12} {"Kateqoriya",-15}");
        Console.WriteLine(new string('-', 75));

        foreach (var item in items)
        {
            Console.WriteLine($"{item.Id,-5} {item.Number,-10} {item.Name,-25} {item.Price,-12:C} {item.Category,-15}");
        }

        Console.WriteLine(new string('-', 75));
        Console.WriteLine($"Cemi: {items.Count} mehsul");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\n✗ Xeta: {ex.Message}");
    }
}

static async Task ShowMenuItemsByCategory(IMenuItemService menuItemService)
{
    while (true)
    {
        try
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║   Kateqoriyaya gore Menu Items         ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();

            var category = ReadCategory();

            var items = await menuItemService.GetMenuItemsByCategoryAsync(category);

            Console.WriteLine();
            if (items == null || !items.Any())
            {
                Console.WriteLine($"'{category}' kateqoriyasinda hec bir mehsul tapilmadi.");
                return;
            }

            Console.WriteLine($"{"ID",-5} {"Nomre",-10} {"Ad",-25} {"Qiymet",-12} {"Kateqoriya",-15}");
            Console.WriteLine(new string('-', 75));

            foreach (var item in items)
            {
                Console.WriteLine($"{item.Id,-5} {item.Number,-10} {item.Name,-25} {item.Price,-12:C} {item.Category,-15}");
            }

            Console.WriteLine(new string('-', 75));
            Console.WriteLine($"Cemi: {items.Count} mehsul");
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n✗ Xeta: {ex.Message}");
            if (!ReadYesNo("\nYeniden cehd etmek isteyirsiniz? (b/x): "))
            {
                return;
            }
        }
    }
}

static async Task ShowMenuItemsByPriceRange(IMenuItemService menuItemService)
{
    while (true)
    {
        try
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║  Qiymet Araligina gore Menu Items      ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();

            var minPrice = ReadDecimal("Minimum qiymet: ");
            var maxPrice = ReadDecimal("Maximum qiymet: ");

            var items = await menuItemService.GetMenuItemsByPriceRangeAsync(minPrice, maxPrice);

            Console.WriteLine();
            if (items == null || !items.Any())
            {
                Console.WriteLine($"{minPrice:C} - {maxPrice:C} araliginda hec bir mehsul tapilmadi.");
                return;
            }

            Console.WriteLine($"{"ID",-5} {"Nomre",-10} {"Ad",-25} {"Qiymet",-12} {"Kateqoriya",-15}");
            Console.WriteLine(new string('-', 75));

            foreach (var item in items)
            {
                Console.WriteLine($"{item.Id,-5} {item.Number,-10} {item.Name,-25} {item.Price,-12:C} {item.Category,-15}");
            }

            Console.WriteLine(new string('-', 75));
            Console.WriteLine($"Cemi: {items.Count} mehsul");
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n✗ Xeta: {ex.Message}");
            if (!ReadYesNo("\nYeniden cehd etmek isteyirsiniz? (b/x): "))
            {
                return;
            }
        }
    }
}

static async Task SearchMenuItems(IMenuItemService menuItemService)
{
    while (true)
    {
        try
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║       Menu Items Axtarish              ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();

            var searchValue = ReadNonEmptyString("Axtarish (ad): ");

            var items = await menuItemService.SearchMenuItemsAsync(searchValue);

            Console.WriteLine();
            if (items == null || !items.Any())
            {
                Console.WriteLine($"'{searchValue}' uzre hec bir mehsul tapilmadi.");
                return;
            }

            Console.WriteLine($"{"ID",-5} {"Nomre",-10} {"Ad",-25} {"Qiymet",-12} {"Kateqoriya",-15}");
            Console.WriteLine(new string('-', 75));

            foreach (var item in items)
            {
                Console.WriteLine($"{item.Id,-5} {item.Number,-10} {item.Name,-25} {item.Price,-12:C} {item.Category,-15}");
            }

            Console.WriteLine(new string('-', 75));
            Console.WriteLine($"Cemi: {items.Count} mehsul");
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n✗ Xeta: {ex.Message}");
            if (!ReadYesNo("\nYeniden cehd etmek isteyirsiniz? (b/x): "))
            {
                return;
            }
        }
    }
}



static async Task AddOrder(IOrderService orderService, IMenuItemService menuItemService)
{
    while (true)
    {
        try
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║      Yeni Sifaris Elave Et             ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();

            await ShowAllMenuItems(menuItemService);
            Console.WriteLine();

            var orderItems = new System.Collections.Generic.List<RestaurantApp.BLL.Dtos.OrderDtos.OrderItemCreateDto>();

            while (true)
            {
                var menuItemId = ReadInt("MenuItem ID: ");
                var count = ReadInt("Say: ", 1);

                orderItems.Add(new RestaurantApp.BLL.Dtos.OrderDtos.OrderItemCreateDto
                {
                    MenuItemId = menuItemId,
                    Count = count
                });

                if (!ReadYesNo("Daha mehsul elave etmek isteyirsiniz? (b/x): "))
                    break;
            }

            if (!orderItems.Any())
            {
                Console.WriteLine("\n✗ Sifaris hec olmasa bir mehsul olmalidir!");
                if (!ReadYesNo("\nYeniden cehd etmek isteyirsiniz? (b/x): "))
                {
                    return;
                }
                continue;
            }

            var dto = new RestaurantApp.BLL.Dtos.OrderDtos.OrderCreateDto
            {
                OrderItems = orderItems
            };

            await orderService.AddOrderAsync(dto);
            Console.WriteLine("\n✓ Sifaris ugurla elave edildi!");
            Console.WriteLine("\nDavam etmek ucun Enter basin...");
            Console.ReadLine();
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n✗ Xeta: {ex.Message}");
            if (!ReadYesNo("\nYeniden cehd etmek isteyirsiniz? (b/x): "))
            {
                return;
            }
        }
    }
}

static async Task RemoveOrder(IOrderService orderService)
{
    while (true)
    {
        try
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║         Sifaris Legvi                  ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();

            await ShowAllOrders(orderService);
            Console.WriteLine();

            var number = ReadNonEmptyString("Legv etmek istediyiniz sifaris nomresi (meselen: O001): ", allowNumbers: true);

            if (!number.StartsWith("O", StringComparison.OrdinalIgnoreCase) || number.Length != 4)
            {
                Console.WriteLine("\n✗ Nomre duzgun formatda deyil! Format: O001, O002, və s.");
                if (!ReadYesNo("\nYeniden cehd etmek isteyirsiniz? (b/x): "))
                {
                    return;
                }
                continue;
            }

            await orderService.RemoveOrderAsync(number);
            Console.WriteLine("\n✓ Sifaris ugurla legv edildi!");
            Console.WriteLine("\nDavam etmek ucun Enter basin...");
            Console.ReadLine();
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n✗ Xeta: {ex.Message}");
            if (!ReadYesNo("\nYeniden cehd etmek isteyirsiniz? (b/x): "))
            {
                return;
            }
        }
    }
}

static async Task ShowAllOrders(IOrderService orderService)
{
    try
    {
        var orders = await orderService.GetAllOrdersAsync();

        if (orders == null || !orders.Any())
        {
            Console.WriteLine("Hec bir sifaris tapilmadi.");
            return;
        }

        Console.WriteLine($"{"ID",-5} {"Nomre",-10} {"Mebleg",-15} {"Item Sayi",-12} {"Tarix",-20}");
        Console.WriteLine(new string('-', 75));

        foreach (var order in orders)
        {
            Console.WriteLine($"{order.Id,-5} {order.Number,-10} {order.TotalAmount,-15:C} {order.MenuItemCount,-12} {order.Date,-20:dd.MM.yyyy HH:mm}");
        }

        Console.WriteLine(new string('-', 75));
        Console.WriteLine($"Cemi: {orders.Count} sifaris");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\n✗ Xeta: {ex.Message}");
    }
}

static async Task ShowOrdersByDateInterval(IOrderService orderService)
{
    while (true)
    {
        try
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║  Tarix Araligina gore Sifarisler       ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();

            var startDate = ReadDate("Baslangic tarixi (dd.MM.yyyy): ");
            var endDate = ReadDate("Bitme tarixi (dd.MM.yyyy): ");

            var orders = await orderService.GetOrdersByDateIntervalAsync(startDate, endDate);

            Console.WriteLine();
            if (orders == null || !orders.Any())
            {
                Console.WriteLine($"{startDate:dd.MM.yyyy} - {endDate:dd.MM.yyyy} araliginda hec bir sifaris tapilmadi.");
                return;
            }

            Console.WriteLine($"{"ID",-5} {"Nomre",-10} {"Mebleg",-15} {"Item Sayi",-12} {"Tarix",-20}");
            Console.WriteLine(new string('-', 75));

            foreach (var order in orders)
            {
                Console.WriteLine($"{order.Id,-5} {order.Number,-10} {order.TotalAmount,-15:C} {order.MenuItemCount,-12} {order.Date,-20:dd.MM.yyyy HH:mm}");
            }

            Console.WriteLine(new string('-', 75));
            Console.WriteLine($"Cemi: {orders.Count} sifaris");
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n✗ Xeta: {ex.Message}");
            if (!ReadYesNo("\nYeniden cehd etmek isteyirsiniz? (b/x): "))
            {
                return;
            }
        }
    }
}

static async Task ShowOrdersByPriceInterval(IOrderService orderService)
{
    while (true)
    {
        try
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║  Mebleg Araligina gore Sifarisler      ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();

            var minPrice = ReadDecimal("Minimum mebleg: ");
            var maxPrice = ReadDecimal("Maximum mebleg: ");

            var orders = await orderService.GetOrdersByPriceIntervalAsync(minPrice, maxPrice);

            Console.WriteLine();
            if (orders == null || !orders.Any())
            {
                Console.WriteLine($"{minPrice:C} - {maxPrice:C} araliginda hec bir sifaris tapilmadi.");
                return;
            }

            Console.WriteLine($"{"ID",-5} {"Nomre",-10} {"Mebleg",-15} {"Item Sayi",-12} {"Tarix",-20}");
            Console.WriteLine(new string('-', 75));

            foreach (var order in orders)
            {
                Console.WriteLine($"{order.Id,-5} {order.Number,-10} {order.TotalAmount,-15:C} {order.MenuItemCount,-12} {order.Date,-20:dd.MM.yyyy HH:mm}");
            }

            Console.WriteLine(new string('-', 75));
            Console.WriteLine($"Cemi: {orders.Count} sifaris");
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n✗ Xeta: {ex.Message}");
            if (!ReadYesNo("\nYeniden cehd etmek isteyirsiniz? (b/x): "))
            {
                return;
            }
        }
    }
}

static async Task ShowOrdersByDate(IOrderService orderService)
{
    while (true)
    {
        try
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║  Verilmis Tarixde Olan Sifarisler      ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();

            var date = ReadDate("Tarix (dd.MM.yyyy): ");

            var orders = await orderService.GetOrdersByDateAsync(date);

            Console.WriteLine();
            if (orders == null || !orders.Any())
            {
                Console.WriteLine($"{date:dd.MM.yyyy} tarixinde hec bir sifaris tapilmadi.");
                return;
            }

            Console.WriteLine($"{"ID",-5} {"Nomre",-10} {"Mebleg",-15} {"Item Sayi",-12} {"Tarix",-20}");
            Console.WriteLine(new string('-', 75));

            foreach (var order in orders)
            {
                Console.WriteLine($"{order.Id,-5} {order.Number,-10} {order.TotalAmount,-15:C} {order.MenuItemCount,-12} {order.Date,-20:dd.MM.yyyy HH:mm}");
            }

            Console.WriteLine(new string('-', 75));
            Console.WriteLine($"Cemi: {orders.Count} sifaris");
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n✗ Xeta: {ex.Message}");
            if (!ReadYesNo("\nYeniden cehd etmek isteyirsiniz? (b/x): "))
            {
                return;
            }
        }
    }
}

static async Task ShowOrderByNumber(IOrderService orderService)
{
    while (true)
    {
        try
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║       Sifaris Melumatlari              ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine();

            var number = ReadNonEmptyString("Sifaris nomresi (meselen: O001): ", allowNumbers: true);

            if (!number.StartsWith("O", StringComparison.OrdinalIgnoreCase) || number.Length != 4)
            {
                Console.WriteLine("\n✗ Nomre duzgun formatda deyil! Format: O001, O002, və s.");
                if (!ReadYesNo("\nYeniden cehd etmek isteyirsiniz? (b/x): "))
                {
                    return;
                }
                continue;
            }

            var order = await orderService.GetOrderByNumberAsync(number);

            Console.WriteLine();
            Console.WriteLine($"Nomre: {order.Number}");
            Console.WriteLine($"Tarix: {order.Date:dd.MM.yyyy HH:mm}");
            Console.WriteLine($"Umumi mebleg: {order.TotalAmount:C}");
            Console.WriteLine($"Umumi item sayi: {order.MenuItemCount}");
            Console.WriteLine();
            Console.WriteLine("Sifaris Items:");
            Console.WriteLine($"{"Nomre",-15} {"Ad",-30} {"Say",-10} {"Qiymet",-12}");
            Console.WriteLine(new string('-', 75));

            foreach (var item in order.OrderItems)
            {
                Console.WriteLine($"{item.MenuItemNumber,-15} {item.MenuItemName,-30} {item.Count,-10} {item.Price,-12:C}");
            }

            Console.WriteLine(new string('-', 75));
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n✗ Xeta: {ex.Message}");
            if (!ReadYesNo("\nYeniden cehd etmek isteyirsiniz? (b/x): "))
            {
                return;
            }
        }
    }
}