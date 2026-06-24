# M_ID_Blog - Clean Architecture Blog Uygulaması

Bu depo, **Clean Architecture (Temiz Mimari)** prensipleri kullanılarak .NET üzerinde geliştirilmiş, kurumsal standartlara uygun gelişmiş bir Blog uygulaması barındırmaktadır. Proje; modülerlik, sürdürülebilirlik ve ölçeklenebilirlik hedeflenerek, SOLID tasarım ilkelerine, Bağımlılık Enjeksiyonuna (Dependency Injection) ve modern tasarım kalıplarına sıkı sıkıya bağlı kalınarak tasarlanmıştır.

---

## 📋 İçindekiler
- [Mimari Genel Bakış](#-mimari-genel-bakış)
- [Proje Klasör Yapısı](#-proje-klasör-yapısı)
- [Teknoloji Yığını ve Kütüphaneler](#-teknoloji-yığını-ve-kütüphaneler)
- [Veritabanı ve Yapılandırma](#-veritabanı-ve-yapılandırma)
- [Kurulum ve Çalıştırma](#-kurulum-ve-çalıştırma)

---

## 🏗️ Mimari Genel Bakış

Uygulama, **Clean Architecture (Soğan Mimarisi)** yaklaşımını uygulamaktadır. Buradaki temel amaç, sorumlulukların net bir şekilde ayrılması (Separation of Concerns) ve iş mantığının (Business Logic) dış çerçevelerden, veritabanlarından veya arayüz bağımlılıklarından tamamen bağımsız kalmasını sağlamaktır.

### Katman Yapısı:
1. **Domain / Entities Katmanı (`M_ID_Blog.Entities`):** Kurumsal iş kurallarını, çekirdek nesneleri ve yapısal sabitleri içerir. Sıfır dış bağımlılığa sahiptir.
2. **Application / Business Katmanı (`M_ID_Blog.Business`):** İş mantığını, servis arayüzlerini, DTO'ları (Veri Transfer Nesneleri) ve doğrulama kurallarını barındırır.
3. **DataAccess / Infrastructure Katmanı (`M_ID_Blog.DataAccess`):** Verilerin kalıcı hale getirilmesini, Entity Framework Core DbContext yapılandırmalarını, veritabanı göçlerini (Migrations) ve Repository (Depo) tasarımlarını yönetir.
4. **Web / Presentation Katmanı (`M_ID_Blog.Web`):** Uygulamanın giriş noktasıdır (ASP.NET Core Web Uygulaması). HTTP isteklerini, yönlendirmeleri (Routing) ve kullanıcı arayüzü bileşenlerini yönetir.

---

## 📁 Proje Klasör Yapısı

Kod tabanı, mantıksal katmanlara göre oldukça düzenli bir şekilde organize edilmiştir:

| Proje Adı | Katman Tipi | Sorumluluk Alanı |
| :--- | :--- | :--- |
| **M_ID_Blog.Entities** | Çekirdek / Domain | Veritabanı varlık modelleri (Entities) ve veri şeması konfigürasyonları. |
| **M_ID_Blog.Business** | Çekirdek / İş Mantığı | İş kuralları, validasyonlar, somut servis implementasyonları ve DTO eşlemeleri. |
| **M_ID_Blog.DataAccess** | Altyapı / Veri Erişimi | Entity Framework Core DbContext, soyut/somut repository sınıfları ve fiziksel veritabanı operasyonları. |
| **M_ID_Blog.Web** | Sunum / Presentation | Kontrolcüler (Controllers), Görünümler (Views), API uç noktaları, middleware yapılandırmaları ve uygulamanın ayağa kalktığı yer. |

---

## 🛠️ Teknoloji Yığını ve Kütüphaneler

- **Framework:** .NET 8.0 / .NET Core C#
- **ORM:** Entity Framework Core (Code-First yaklaşımı)
- **Veritabanı Desteği:** MS SQL Server / PostgreSQL uyumluluğu
- **Tasarım İlkeleri:** SOLID, Repository Pattern, Dependency Injection

---

## 🚀 Kurulum ve Çalıştırma

Projeyi yerel geliştirme ortamınızda çalıştırmak için aşağıdaki adımları takip edebilirsiniz:

### Gereksinimler
- Bilgisayarınızda **.NET SDK 8.0** veya üzeri sürümün kurulu olması
- **Visual Studio 2022** veya JetBrains Rider IDE
- Çalışır durumda bir **MS SQL Server** örneği

### Adımlar
1. Projeyi yerel dizininize klonlayın.
2. `M_ID_Blog.sln` dosyasının bulunduğu klasöre gidin ve çözümü IDE'niz ile açın.
3. `M_ID_Blog.Web` projesinin içinde yer alan `appsettings.json` dosyasındaki veritabanı bağlantı adresini (`ConnectionStrings`) kendi yerel SQL Server ayarlarınıza göre güncelleyin.
4. Paket Yöneticisi Konsolunu (Package Manager Console) veya terminalinizi açarak veritabanı tablolarını oluşturmak için şu migrasyon komutunu çalıştırın:
```bash
   dotnet ef database update --project M_ID_Blog.DataAccess --startup-project M_ID_Blog.Web

   ---

## 📬 İletişim

Projeyle ilgili sorularınız, iş birliği teklifleriniz veya geri bildirimleriniz için benimle aşağıdaki kanallar üzerinden iletişime geçebilirsiniz:

- **Ad Soyad:** Muhammet İslam Demir
- **LinkedIn:** [Muhammet İslam Demir]
- **GitHub:** [@Mislamdemir44](https://github.com/Mislamdemir44)
