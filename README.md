# HatchWatch - Film ve Dizi Takip Sistemi

Bu proje, **BTS304 - Veritabanı Yönetim Sistemleri II** dersi final ödevi kapsamında geliştirilmiş demo seviyesinde bir film ve dizi takip sistemidir. Amaç gerçek bir Netflix klonu yapmak değil; MySQL üzerinde tablo tasarımı, stored procedure, function, trigger kullanımı ve .NET 8 ile N-katmanlı mimariyi göstermektir.

HatchWatch ile içerikler listelenebilir, yeni film/dizi kayıtları eklenebilir, izleme listesi oluşturulabilir, kullanıcı puanları takip edilebilir ve bazı istatistikler veritabanı function'ları üzerinden gösterilebilir.

## Projenin Amacı

Projenin temel amacı, veritabanı yönetim sistemlerinde ileri konuları uygulamalı olarak göstermektir:

- MySQL üzerinde ilişkisel veritabanı tasarımı
- Primary key, foreign key, unique, check ve default constraint kullanımı
- Her ana tablo için stored procedure ile CRUD işlemleri
- Kullanıcı tanımlı function kullanımı
- Trigger ile iş kuralı kontrolü
- ASP.NET Core Razor Pages ile N-katmanlı mimari

Uygulamada veri akışı şu şekilde ilerler:

```text
HatchWatch.UI -> HatchWatch.BL -> HatchWatch.DAL -> Stored Procedure / Function -> MySQL
```

## Kullanılan Teknolojiler

- .NET 8
- ASP.NET Core Razor Pages
- MySQL 8.4
- MySqlConnector
- Bootstrap
- HTML / CSS
- N-katmanlı mimari

## Proje Dosya Yapısı

Proje, aşağıdaki ana klasörlerden oluşur:

- **HatchWatch.UI**: Razor Pages arayüz katmanıdır. Ana sayfa, keşfet, izleme listesi ve yönetim ekranları burada yer alır.
- **HatchWatch.BL**: İş katmanıdır. UI ile DAL arasında ara katman görevi görür.
- **HatchWatch.DAL**: Veritabanı erişim katmanıdır. Stored procedure ve function çağrıları burada yapılır.
- **HatchWatch.Entities**: Entity sınıflarını içerir.
- **hatchwatchdb.sql**: Veritabanı tablolarını, stored procedure'leri, function'ları ve trigger'ları oluşturan SQL dosyasıdır.
- **HatchWatch.sln**: Visual Studio / .NET solution dosyasıdır.

## Veritabanı Yapısı

Veritabanı adı:

```sql
hatchwatch_db
```

Projede kullanılan tablolar:

- `users`
- `contents`
- `genres`
- `platforms`
- `content_genres`
- `content_platforms`
- `watchlist`

## Geliştirilen Veritabanı Özellikleri

### Stored Procedure

CRUD işlemleri doğrudan SQL komutları ile değil, stored procedure çağrıları ile yapılır. Projede kullanılan procedure grupları:

- `sp_users_select_all`, `sp_users_insert`, `sp_users_update`, `sp_users_delete`
- `sp_contents_select_all`, `sp_contents_insert`, `sp_contents_update`, `sp_contents_delete`
- `sp_genres_select_all`, `sp_genres_insert`, `sp_genres_update`, `sp_genres_delete`
- `sp_platforms_select_all`, `sp_platforms_insert`, `sp_platforms_update`, `sp_platforms_delete`
- `sp_content_genres_select_all`, `sp_content_genres_insert`, `sp_content_genres_update`, `sp_content_genres_delete`
- `sp_content_platforms_select_all`, `sp_content_platforms_insert`, `sp_content_platforms_update`, `sp_content_platforms_delete`
- `sp_watchlist_select_all`, `sp_watchlist_insert`, `sp_watchlist_update`, `sp_watchlist_delete`

### Function

Projede iki adet kullanıcı tanımlı function vardır:

- `fn_user_watched_count(p_user_id)`: Kullanıcının kaç içeriği izlediğini döndürür.
- `fn_content_average_rating(p_content_id)`: Bir içeriğe verilen kullanıcı puanlarının ortalamasını döndürür.

### Trigger

Projede iki adet trigger vardır:

- `trg_watchlist_rating_required`: Kullanıcı bir içeriği `İzlendi` olarak işaretlerse puan girmesini zorunlu tutar.
- `trg_watchlist_update_date`: İzleme listesi güncellendiğinde `updated_at` alanını otomatik günceller.

## Kurulum ve Çalıştırma

### Gereksinimler

- .NET 8 SDK
- MySQL 8.4
- Visual Studio Code veya Visual Studio

### 1. Projeyi Klonlayın

```bash
git clone <repo-url>
cd HatchWatch
```

### 2. Veritabanını Oluşturun

MySQL üzerinde proje kökündeki SQL dosyasını çalıştırın:

```bash
mysql -u root -p < hatchwatchdb.sql
```

Bu dosya `hatchwatch_db` veritabanını ve gerekli tabloları, stored procedure'leri, function'ları ve trigger'ları oluşturur.

### 3. Veritabanı Bağlantısını Kontrol Edin

Bağlantı bilgisi `HatchWatch.DAL/VeritabaniBaglanti.cs` dosyasındadır:

```csharp
Server=localhost;Port=3306;Database=hatchwatch_db;Uid=root;Pwd=MYSQL_SIFRESI;
```

Yerel çalıştırmada şifreyi kod içine yazmadan ortam değişkeni ile verebilirsiniz:

```powershell
$env:HATCHWATCH_MYSQL_PASSWORD="MYSQL_SIFRENIZ"
dotnet run --project HatchWatch.UI
```

### 4. Projeyi Derleyin

```bash
dotnet build HatchWatch.sln
```

### 5. Uygulamayı Çalıştırın

```bash
dotnet run --project HatchWatch.UI
```

Uygulama çalıştıktan sonra tarayıcıdan açın:

```text
http://localhost:5000
```

veya terminalde gösterilen localhost adresini kullanın.

## Uygulama Ekranları

Uygulamada bulunan temel ekranlar:

- **Ana Sayfa**: Projenin genel tanıtımı ve özet bilgileri
- **Keşfet**: Film ve dizilerin kart görünümünde listelenmesi
- **İzleme Listem**: Kullanıcının izleme durumlarını ve puanlarını yönetmesi
- **Kullanıcılar**: Demo kullanıcılarının listelenmesi
- **İçerik Yönetimi**: İçerik ekleme, güncelleme ve silme işlemleri
- **Tür Yönetimi**: Tür ekleme, güncelleme ve silme işlemleri
- **Platform Yönetimi**: Platform ekleme, güncelleme ve silme işlemleri

## Notlar

- Proje demo amaçlıdır; login/auth sistemi eklenmemiştir.
- Yönetim ekranları ödev videosunda CRUD ve stored procedure akışını gösterebilmek için açık bırakılmıştır.
- CRUD işlemleri DAL katmanında stored procedure çağrıları ile yapılır.
- Function çağrıları yalnızca gerekli istatistiklerin gösterilmesi için kullanılır.

## Geliştirici

Ahmet Faruk UYSAL

BTS304 - Veritabanı Yönetim Sistemleri II
