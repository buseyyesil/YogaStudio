# 🧘‍♀️ YogaStudio

> ASP.NET Core 8 MVC + Web API ile geliştirilmiş tam kapsamlı yoga stüdyo yönetim sistemi

!\[.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square\&logo=dotnet)
!\[ASP.NET Core](https://img.shields.io/badge/ASP.NET\_Core-MVC-blue?style=flat-square)
!\[Entity Framework](https://img.shields.io/badge/Entity\_Framework-Core-green?style=flat-square)
!\[MSSQL](https://img.shields.io/badge/MSSQL-Server-CC2927?style=flat-square\&logo=microsoftsqlserver)
!\[Bootstrap](https://img.shields.io/badge/Bootstrap-4-7952B3?style=flat-square\&logo=bootstrap)

\---

## 📸 Ekran Görüntüleri

### 🏠 Anasayfa

!\[Anasayfa](screenshots/home.png)

### 📅 Program Sayfası

!\[Program](screenshots/schedule.png)

### 🛠️ Admin Dashboard

!\[Admin Dashboard](screenshots/admin.png)

### 👤 Kullanıcı Paneli

!\[Kullanıcı Paneli](screenshots/profile.png)

### 📝 Blog Detay

!\[Blog](screenshots/blog.png)

\---

## 🏗️ Mimari

Proje **N-Tier Architecture** prensiplerine uygun olarak 5 katmana ayrılmıştır:

```
YogaStudio
├── YogaStudio.Entity      → Domain modelleri (Entity sınıfları)
├── YogaStudio.Data        → AppDbContext, Migrations
├── YogaStudio.Business    → İş katmanı
├── YogaStudio.API         → RESTful Web API (port: 7276)
└── YogaStudio.UI          → ASP.NET Core MVC (port: 7154)
```

API ve UI projeleri birbirinden tamamen bağımsız çalışmakta, **HttpClient** aracılığıyla haberleşmektedir.

\---

## ✨ Özellikler

### ⚙️ Admin Paneli

* Ders, eğitmen, paket yönetimi (tam CRUD)
* Her derse Zoom linki ve kapasite atama
* Rezervasyon takibi ve yönetimi
* Blog yazısı oluşturma ve düzenleme
* Müşteri yorumları yönetimi
* İletişim mesajları (okundu/sil, modal görüntüleme)
* Kullanıcı yönetimi
* Chart.js ile aylık rezervasyon ve ders dağılımı grafikleri

### 👤 Kullanıcı Paneli

* Kayıt \& giriş (Session tabanlı kimlik doğrulama)
* Haftalık ders programı görüntüleme
* Kontenjan takibi ile rezervasyon oluşturma ve iptal
* Zoom linki ile online derse katılım
* Üyelik paketi satın alma ve kalan ders hakkı takibi
* Profil düzenleme (kullanıcı adı ve email güncelleme)
* Blog yorumu yapma

### 🌟 Öne Çıkan Özellikler

* 📅 Haftalık takvim görünümü ile ders programı
* 🎯 Hedefine göre kişiselleştirilmiş program önerisi (zayıflama, sıkılaşma, hamilelik, stres)
* 🎥 Online ders desteği (Zoom entegrasyonu)
* 💬 SweetAlert2 ile şık bildirim sistemi
* 📱 WhatsApp entegrasyonu
* 📝 Blog detay ve yorum sistemi
* 🌍 Tam Türkçe arayüz

\---

## 🛠️ Kullanılan Teknolojiler

|Teknoloji|Açıklama|
|-|-|
|ASP.NET Core 8 MVC|Frontend (Razor Views)|
|ASP.NET Core 8 Web API|Backend RESTful API|
|Entity Framework Core|ORM (Code-First)|
|MSSQL Server|Veritabanı|
|HttpClient|API-UI haberleşmesi|
|Session Authentication|Kimlik doğrulama|
|Chart.js|Dashboard grafikleri|
|SweetAlert2|Bildirim sistemi|
|Breeze Bootstrap Admin|Admin panel teması|
|Yogalax Bootstrap|UI teması|
|Bootstrap 4|CSS Framework|
|jQuery|JavaScript kütüphanesi|
|Swagger / OpenAPI|API dokümantasyonu|

\---

## 📁 Proje Yapısı

```
YogaStudio/
├── YogaStudio.Entity/
│   └── Entities/
│       ├── Lesson.cs
│       ├── Trainer.cs
│       ├── Package.cs
│       ├── Reservation.cs
│       ├── User.cs
│       ├── BlogPost.cs
│       ├── BlogComment.cs
│       ├── Testimony.cs
│       ├── UserPackage.cs
│       └── ContactMessage.cs
├── YogaStudio.Data/
│   ├── AppDbContext.cs
│   └── Migrations/
├── YogaStudio.API/
│   ├── Controllers/
│   └── DTOs/
└── YogaStudio.UI/
    ├── Controllers/
    ├── Models/
    ├── Services/
    │   └── ApiService.cs
    └── Views/
```

\---

## 

