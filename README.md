# Article Automation Project


## Console Application Geliştirmeleri
Hedef web sitesine gidip bütün makaleleri (500+) MSSQL veritabanına kaydedip, resimlerini indiren otomasyon sistemi. Belirlenen dosya yollarını uygun olarak indirme işlemini gerçekleştirir, indirilen resimlerin dosya yollarını ilgili makalelerin "url" kısmına kayıt eder.

- Dosya işlemleri kullanıldı
- Multithread için C# Paralel kullanıldı
- HTML Parse için HTMLAgility Pack Nuget kullanıldı
  - [HTMLAgility Pack](https://html-agility-pack.net/)
- Database için MS SQL kullanıldı
- Data katmanı için Entity Framework kullanıldı
  - [Entity Framework NuGet](https://www.nuget.org/packages/EntityFramework/)
- Makaleleri PDF haline getirmek için iTextSharp Nuget'i kullanıldı
  - [iTextSharp NuGet](https://www.nuget.org/packages/iTextSharp/5.5.13.1)
    

## Windows Application Geliştirmeleri
Console otomasyonu ile kaydedilen makalelerin yönetildiği Windows Forms desktop uygulaması. DataGridView üzerinde tüm kayıtlar gösterilir, istenen satıra tıklanarak bilgisi görülebilir ve seçili makale PDF formatına dönüştürülerek kaydedilebilir.

- EF Core ile makale ve kategorilerin gösterimi windows form üzerinde geliştirildi
  - [EF Core NuGet](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/)
- iTextSharp ile istenilen kategorinin PDF dosyası oluşturuldu
    

## Asp.Net Core MVC Geliştirmeleri
Console otomasyonu ile kaydı yapılmış makaleleri web sitesi üzerinde gösteren, MVC mimarisi ile geliştirilmiş Asp.Net Core projesi. Makaleler kategorilendirilip, sayfalandırılarak; filtreleme ve detay gösterme işlemleri yapılabilir.
    
- Model View Controller uygulama tasarımında EF ile Web Sitesi geliştirildi
- Bu web sitesinde Kategori gösterimi, makale gösterimi ve makale detay gösterimi yapıldı
- Makale gösterimlerinde sayfalama algoritması geliştirildi ve MVC component kullanıldı
  - [Asp.Net Core](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-2.2)
