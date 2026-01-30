# 🍽️ Restaurant Management System

Restoran idarəetmə sistemi - menyu və sifarişləri idarə etmək üçün console əsaslı C# .NET tətbiqi.

## 📋 Haqqında

Bu layihə restoran menyu elementlərini və sifarişləri idarə etmək üçün nəzərdə tutulmuş tam funksional bir idarəetmə sistemidir. Entity Framework Core və Repository Pattern istifadə edərək hazırlanmışdır.

## ✨ Əsas Funksiyalar

### 🍕 Menu Items İdarəetməsi
- ✅ Yeni menyu elementi əlavə etmək
- ✅ Mövcud elementləri redaktə etmək
- ✅ Elementləri silmək
- ✅ Bütün elementləri görmək
- ✅ Kateqoriyaya görə axtarış
- ✅ Qiymət aralığına görə filtrlənmə
- ✅ Ada görə axtarış

### 📦 Sifarişlər İdarəetməsi
- ✅ Yeni sifariş yaratmaq
- ✅ Sifarişi ləğv etmək
- ✅ Bütün sifarişləri görmək
- ✅ Tarix aralığına görə axtarış
- ✅ Məbləğ aralığına görə axtarış
- ✅ Konkret tarixdə olan sifarişlər
- ✅ Nömrəyə görə ətraflı məlumat

## 🏗️ Arxitektura

Layihə 3-səviyyəli arxitektura üzərində qurulmuşdur:

```
RestaurantApp/
├── RestaurantApp.BLL/          # Business Logic Layer
│   ├── Dtos/                   # Data Transfer Objects
│   ├── Interfaces/             # Service interfeyslər
│   ├── Services/               # Business məntiqi
│   └── Profiles/               # AutoMapper konfiqurasiyaları
│
├── RestaurantApp.DAL/          # Data Access Layer
│   ├── Models/                 # Entity modellər
│   ├── Data/                   # DbContext və konfiqurasiyalar
│   ├── Interfaces/             # Repository interfeyslər
│   └── Concretes/              # Repository implementasiyaları
│
└── RestaurantApp.UI/           # Console Application
    └── Program.cs              # İstifadəçi interfeysi
```

## 🛠️ Texnologiyalar

- **C# 10+**
- **.NET 6/7/8**
- **Entity Framework Core**
- **SQL Server**
- **AutoMapper**
- **Dependency Injection**

## 📦 Quraşdırma

### Tələblər

- .NET SDK 6.0 və ya daha yuxarı
- SQL Server (LocalDB və ya tam versiya)
- Visual Studio 2022 / VS Code / Rider

### Addımlar

1. **Repository-ni klonlayın**
```bash
git clone https://github.com/yourusername/RestaurantApp.git
cd RestaurantApp
```

2. **Connection string-i konfiqurasiya edin**

`Program.cs` faylında connection string-i öz SQL Server konfiqurasiyasına uyğunlaşdırın:

```csharp
serviceCollection.AddDbContext<RestaurantAppDbContext>(options =>
    options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=RestaurantAppDB;Trusted_Connection=True;TrustServerCertificate=True;"));
```

3. **Verilənlər bazasını yaradın**

Package Manager Console-da:
```bash
Add-Migration InitialCreate
Update-Database
```

və ya CLI-də:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

4. **Tətbiqi işə salın**
```bash
dotnet run
```

## 💾 Verilənlər Bazası Strukturu

### MenuItem
```csharp
Id: int (Primary Key)
Number: string (M001, M002...)
Name: string (Unikal)
Price: decimal
Category: string (Sup, Ana yemek, Desert, Icki)
```

### Order
```csharp
Id: int (Primary Key)
Number: string (O001, O002...)
Date: DateTime
TotalAmount: decimal
OrderItems: List<OrderItem>
```

### OrderItem
```csharp
Id: int (Primary Key)
Number: string (O001-01, O001-02...)
MenuItemId: int (Foreign Key)
OrderId: int (Foreign Key)
Count: int
```

## 🎮 İstifadə

### Menyu Əməliyyatları

```
1 - Menu üzərində əməliyyat aparmaq
    1 - Yeni item əlavə et
    2 - Item üzərində düzəliş et
    3 - Item sil
    4 - Bütün Item-ları göstər
    5 - Kateqoriyasına görə menu item-ları göstər
    6 - Qiymət aralığına görə menu item-lar göstər
    7 - Menu item-lar arasında ada görə axtarış et
    0 - Əvvəlki menyu ya qayıt
```

### Sifariş Əməliyyatları

```
2 - Sifarişlər üzərində əməliyyat aparmaq
    1 - Yeni sifariş əlavə etmək
    2 - Sifarişin ləğvi
    3 - Bütün sifarişlərin ekrana çıxarılması
    4 - Verilən tarix aralığına görə sifarişlərin göstərilməsi
    5 - Verilən məbləğ aralığına görə sifarişlərin göstərilməsi
    6 - Verilmiş bir tarixdə olan sifarişlərin göstərilməsi
    7 - Verilmiş nömrəyə əsasən həmin nömrəli sifarişin məlumatlarının göstərilməsi
    0 - Əvvəlki menüya qayıt
```

## 🎯 Design Patterns

- **Repository Pattern** - məlumat bazası əməliyyatlarının abstraksiyası
- **Dependency Injection** - asılılıqların idarəsi
- **DTO Pattern** - layer-lər arasında məlumat transferi
- **AutoMapper** - obyekt mapping

## ⚙️ Validasiya

Sistem aşağıdakı validasiyaları həyata keçirir:

- ✅ Boş dəyərlərin yoxlanması
- ✅ Mənfi qiymətlərin qarşısının alınması
- ✅ Dublikat adların yoxlanması
- ✅ Format validasiyası (nömrələr üçün)
- ✅ Tarix intervallarının yoxlanması
- ✅ MenuItem mövcudluğunun yoxlanması

## 🐛 Xəta İdarəetməsi

- Try-catch blokları ilə tam xəta idarəetməsi
- İstifadəçi dostu xəta mesajları
- Xəta zamanı yenidən cəhd imkanı
- Input validasiyası ilə səhvlərin qarşısının alınması

## 📝 Nümunə Data

Sistem ilkin olaraq 10 menyu elementi ilə gəlir:

| Kateqoriya | Məhsullar |
|------------|-----------|
| Sup | Piti, Dovga |
| Ana yemek | Kebab, Lyulya-kebab, Plov |
| Desert | Pakhlava, Shekerbura |
| İçki | Çay, Ayran, Sherbet |

## 🤝 Töhfə

Pull request-lər xoş qarşılanır! Böyük dəyişikliklər üçün əvvəlcə issue açaraq nəyi dəyişdirmək istədiyinizi müzakirə edin.

## 📄 Lisenziya

[MIT](https://choosealicense.com/licenses/mit/)

## 👨‍💻 Müəllif

**Sizin Adınız**
- GitHub: [@Farkhad9](https://github.com/Farkhad9)
- Email: farkhad0491@gmail.com

## 🙏 Təşəkkürlər

- Entity Framework Core komandasına
- AutoMapper kitabxanası tərtibatçılarına
- .NET Community-ə

---

⭐ Layihə bəyəndinizsə, ulduz verməyi unutmayın!
