# Sistem Informasi Bioskop - .NET Microservices

## 📌 Deskripsi Proyek

Sistem informasi bioskop ini dikembangkan menggunakan arsitektur **microservices** untuk mengelola **pemesanan tiket bioskop**, **jadwal film**, **transaksi pembayaran**, dan **manajemen studio** secara efisien. Proyek ini dirancang untuk menangani skenario nyata dalam operasional bioskop.

## 📚 Tech Stack

- **Backend:** .NET Core 8
- **ORM:** Entity Framework Core
- **Database:** Microsoft SQL Server (MsSQL), Redis
- **Message Broker:** NATS
- **API Gateway:** Ocelot
- **Containerization:** Docker 

## 🔧 Prerequisite

Sebelum memulai pengembangan, pastikan Anda telah memenuhi semua persyaratan berikut:

1. **Instal .NET SDK 8.0** [(Download)](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
2. **Instal SQL Server** dan konfigurasikan sesuai kebutuhan.
3. **Instal Visual Studio / VS Code** sebagai IDE:
   - [VS Code](https://code.visualstudio.com/)
   - Ekstensi yang dibutuhkan:
     - **C# extension**
     - **.NET Extension Pack**

## 🎭 Aktor dalam Sistem

- **Admin Bioskop**: Bertanggung jawab untuk mengelola studio, film, jadwal, dan transaksi.
- **Pengguna**: Dapat melihat jadwal film dan membeli tiket.

## 📌 Studi Kasus

Sistem ini dirancang untuk bioskop modern yang memiliki banyak studio dan berbagai jenis film yang ditayangkan setiap hari. Sistem ini membantu **admin** dalam mengelola operasional bioskop dan **pelanggan** dalam memesan tiket secara online.

## 📂 Struktur Microservices

```
📂 cinema-microservices
│── 📂 gateway (API Gateway menggunakan Ocelot)
│ ├── 📄 OcelotConfig.json
│ ├── 📄 Program.cs
│ ├── 📄 appsettings.json
│ ├── 📂 Extensions
│ │ ├── 📄 ServiceExtensions.cs
│ ├── 📂 Middleware
│ │ ├── 📄 ExceptionMiddleware.cs
│
│── 📂 services (Berisi semua microservices yang digunakan)
│ │
│ ├── 📂 IdentityService (Autentikasi dan Manajemen Pengguna - OAuth, JWT)
│ │ ├── 📂 API
│ │ │ ├── 📄 Program.cs
│ │ │ ├── 📂 Controllers
│ │ │ │ ├── 📄 AuthController.cs
│ │ ├── 📂 Application
│ │ │ ├── 📂 DTOs
│ │ │ │ ├── 📄 LoginRequest.cs
│ │ │ │ ├── 📄 RegisterRequest.cs
│ │ │ ├── 📂 Handlers
│ │ │ │ ├── 📄 LoginHandler.cs
│ │ │ │ ├── 📄 RegisterHandler.cs
│ │ │ ├── 📂 Interfaces
│ │ │ │ ├── 📄 IAuthService.cs
│ │ ├── 📂 Domain
│ │ │ ├── 📄 User.cs
│ │ ├── 📂 Infrastructure
│ │ │ ├── 📂 Persistence
│ │ │ │ ├── 📄 AuthDbContext.cs
│ │ │ │ ├── 📄 AuthRepository.cs
│ │ │ ├── 📂 Security
│ │ │ │ ├── 📄 JwtService.cs
│
│ ├── 📂 StudioService (Manajemen Studio Bioskop)
│ │ ├── 📂 API
│ │ │ ├── 📄 Program.cs
│ │ │ ├── 📂 Controllers
│ │ │ │ ├── 📄 StudioController.cs
│ │ ├── 📂 Application
│ │ │ ├── 📂 DTOs
│ │ │ │ ├── 📄 StudioRequest.cs
│ │ │ ├── 📂 Handlers
│ │ │ │ ├── 📄 CreateStudioHandler.cs
│ │ │ │ ├── 📄 GetStudiosHandler.cs
│ │ │ ├── 📂 Interfaces
│ │ │ │ ├── 📄 IStudioRepository.cs
│ │ ├── 📂 Domain
│ │ │ ├── 📄 Studio.cs
│ │ ├── 📂 Infrastructure
│ │ │ ├── 📂 Persistence
│ │ │ │ ├── 📄 StudioDbContext.cs
│ │ │ │ ├── 📄 StudioRepository.cs
│
│ ├── 📂 MovieService (Manajemen Film)
│ │ ├── 📂 API
│ │ │ ├── 📄 Program.cs
│ │ │ ├── 📂 Controllers
│ │ │ │ ├── 📄 MovieController.cs
│ │ ├── 📂 Application
│ │ │ ├── 📂 DTOs
│ │ │ │ ├── 📄 MovieRequest.cs
│ │ │ ├── 📂 Events
│ │ │ │ ├── 📄 MovieCreatedEvent.cs
│ │ │ │ ├── 📄 MovieUpdatedEvent.cs
│ │ │ │ ├── 📄 MovieDeletedEvent.cs
│ │ │ ├── 📂 Handlers
│ │ │ │ ├── 📄 CreateMovieHandler.cs
│ │ │ │ ├── 📄 UpdateMovieHandler.cs
│ │ │ ├── 📂 Interfaces
│ │ │ │ ├── 📄 IMovieRepository.cs
│ │ ├── 📂 Domain
│ │ │ ├── 📄 Movie.cs
│ │ ├── 📂 Infrastructure
│ │ │ ├── 📂 Persistence
│ │ │ │ ├── 📄 MovieDbContext.cs
│ │ │ │ ├── 📄 MovieRepository.cs
│
│ ├── 📂 ScheduleService (Jadwal Film)
│ │ ├── [struktur folder seperti yang sudah kamu buat sebelumnya]
│
│ ├── 📂 TicketService (Pemesanan Tiket)
│ │ ├── 📂 API
│ │ │ ├── 📄 Program.cs
│ │ │ ├── 📂 Controllers
│ │ │ │ ├── 📄 TicketController.cs
│ │ ├── 📂 Application
│ │ │ ├── 📂 DTOs
│ │ │ │ ├── 📄 TicketRequest.cs
│ │ │ ├── 📂 Handlers
│ │ │ │ ├── 📄 CreateTicketHandler.cs
│ │ │ │ ├── 📄 GetTicketHandler.cs
│ │ │ ├── 📂 Interfaces
│ │ │ │ ├── 📄 ITicketRepository.cs
│ │ ├── 📂 Domain
│ │ │ ├── 📄 Ticket.cs
│ │ ├── 📂 Infrastructure
│ │ │ ├── 📂 Persistence
│ │ │ │ ├── 📄 TicketDbContext.cs
│ │ │ │ ├── 📄 TicketRepository.cs
│
│ ├── 📂 TransactionService (Pembayaran dan Transaksi)
│ │ ├── 📂 API
│ │ ├── 📂 Application
│ │ ├── 📂 Domain
│ │ ├── 📂 Infrastructure
│
│── 📂 deployments (Skrip Deployment)
│ ├── 📂 docker
│ │ ├── 📄 docker-compose.yml
│ ├── 📂 kubernetes
│ │ ├── 📄 deployment.yaml
│
│── 📂 docs (Dokumentasi API & Sistem)
│ ├── 📄 api-specs.yaml
│ ├── 📄 architecture.md
│ ├── 📄 messaging.md
│ ├── 📄 database-schema.md
```

### 1️⃣ **API Gateway** (`/gateway`)
- Menggunakan **Ocelot** sebagai reverse proxy untuk merutekan permintaan ke layanan yang sesuai.
- Middleware global untuk menangani **Exception Handling**.

### 2️⃣ **IdentityService** (`/services/IdentityService`)
- Mengelola **registrasi, login, JWT authentication, dan OAuth**.

### 3️⃣ **StudioService** (`/services/StudioService`)
- Mengelola **studio bioskop**, termasuk **nama, kapasitas, dan fasilitas**.

### 4️⃣ **MovieService** (`/services/MovieService`)
- Mengelola **film**, termasuk **judul, genre, durasi, dan deskripsi**.

### 5️⃣ **ScheduleService** (`/services/ScheduleService`)
- Mengatur **jadwal film** dan memastikan **film tersedia sebelum dijadwalkan**.

### 6️⃣ **TicketService** (`/services/TicketService`)
- Mengelola **pemesanan tiket**, nomor kursi, dan **status tiket (reserved, paid, canceled)**.

### 7️⃣ **TransactionService** (`/services/TransactionService`)
- Mengelola **proses pembayaran tiket** dan memastikan pembayaran berhasil.

## Aktor
- **Admin Bioskop**: Bertanggung jawab untuk mengelola semua studio, film, jadwal, dan transaksi. Admin memiliki akses penuh terhadap semua data yang ada dalam sistem.
- **Pengguna**: Pengguna umum yang ingin membeli tiket dan melihat jadwal film yang sedang tayang. Pengguna hanya memiliki akses untuk melihat film yang tersedia dan melakukan transaksi pembelian tiket.

## Deskripsi Kasus
Di sebuah bioskop modern yang memiliki banyak studio dan berbagai jenis film yang ditayangkan setiap hari, dibutuhkan sebuah sistem informasi untuk memudahkan pengelolaan operasionalnya. Admin bioskop harus bisa mengelola data studio, film yang ditayangkan, jadwal tayang, dan transaksi penjualan tiket secara efisien. Pengguna sistem (pelanggan) harus dapat melihat jadwal film, memilih kursi, dan membeli tiket dengan cepat.

## Tujuan
Menciptakan sebuah Microservice API yang akan digunakan oleh admin dan pengguna untuk melakukan berbagai aktivitas yang berhubungan dengan bioskop, seperti manajemen studio, jadwal tayang, manajemen film, dan transaksi tiket.

## Data Utama
- **Studio Bioskop**: Tempat di mana film ditayangkan. Setiap studio memiliki nama, kapasitas, dan fasilitas tambahan.
- **Film**: Data film yang akan ditayangkan di bioskop, termasuk judul, genre, durasi, dan deskripsi.
- **Jadwal Film**: Jadwal tayang dari masing-masing film di setiap studio, termasuk waktu mulai, waktu selesai, dan harga tiket.
- **Tiket**: Tiket yang dibeli oleh pengguna, mencakup informasi tentang jadwal film, nomor kursi, dan status pembayaran.
- **Transaksi**: Rekaman transaksi pembelian tiket oleh pengguna, termasuk metode pembayaran dan status pembayaran.


##  Fitur Utama

- **Manajemen Studio**: CRUD untuk studio bioskop.
- **Manajemen Film**: CRUD untuk daftar film.
- **Manajemen Jadwal**: CRUD untuk jadwal tayang film.
- **Pemesanan Tiket**: Memilih kursi dan melakukan reservasi tiket.
- **Manajemen Transaksi**: Pembayaran tiket dan pengelolaan status transaksi.

## 📌 Tabel Rute API

### 🛠 **AuthController**
| HTTP Method | Route               | Action      | Deskripsi                    |
|------------|-------------------|------------|----------------------------|
| POST       | /api/auth/register | Register   | Mendaftar pengguna baru.   |
| POST       | /api/auth/login    | Login      | Masuk pengguna & token.    |
| POST       | /api/auth/refresh  | Refresh    | Memperbarui token autentikasi. |
| DELETE     | /api/auth/logout   | Logout     | Keluar dari pengguna.      |

### 👤 **UserController**
| HTTP Method | Route                   | Action         | Deskripsi                        |
|------------|------------------------|---------------|--------------------------------|
| GET       | /api/users/profile      | GetProfile    | Mendapatkan profil pengguna.  |
| PUT       | /api/users/profile      | UpdateProfile | Memperbarui profil pengguna. |
| PUT       | /api/users/change-password | ChangePassword | Mengubah kata sandi pengguna. |

### 🎟 **TicketController**
| HTTP Method | Route             | Action       | Deskripsi                           |
|------------|----------------|-------------|----------------------------------|
| GET       | /api/tickets   | GetTickets  | Mendapatkan daftar tiket pengguna. |
| POST      | /api/tickets   | Create      | Membuat tiket baru.                 |
| GET       | /api/tickets/{ticketId} | GetTicketDetails | Mendapatkan detail tiket tertentu. |

### 💰 **TransactionController**
| HTTP Method | Route                       | Action               | Deskripsi                           |
|------------|---------------------------|--------------------|----------------------------------|
| GET       | /api/transactions         | GetTransactions   | Mendapatkan daftar transaksi.  |
| POST      | /api/transactions         | Create           | Membuat transaksi baru.       |
| GET       | /api/transactions/{id}    | GetDetailTransactions | Mendapatkan detail transaksi tertentu. |
| PUT       | /api/transactions/{id}/payment | PayTransaction | Membayar transaksi tertentu. |

### 🎬 **MovieController**
| HTTP Method | Route             | Action  | Deskripsi                     |
|------------|----------------|--------|----------------------------|
| GET       | /api/movies   | GetMovies | Mendapatkan daftar film.    |
| POST      | /api/movies   | Create    | Menambahkan film baru.      |
| PUT       | /api/movies/{id} | Update    | Memperbarui film tertentu.  |
| DELETE    | /api/movies/{id} | Delete    | Menghapus film tertentu.    |

### ⏳ **ScheduleController**
| HTTP Method | Route               | Action  | Deskripsi                   |
|------------|----------------|--------|--------------------------|
| GET       | /api/schedules | GetSchedules | Mendapatkan daftar jadwal. |
| POST      | /api/schedules | Create    | Membuat jadwal baru.      |
| PUT       | /api/schedules/{id} | Update    | Memperbarui jadwal tertentu. |
| DELETE    | /api/schedules/{id} | Delete    | Menghapus jadwal tertentu. |

### 🎭 **StudioController**
| HTTP Method | Route             | Action  | Deskripsi                    |
|------------|----------------|--------|----------------------------|
| GET       | /api/studios   | GetStudios | Mendapatkan daftar studio.  |
| POST      | /api/studios   | Create    | Menambahkan studio baru.   |
| PUT       | /api/studios/{id} | Update    | Memperbarui studio tertentu. |

## 🚀 Deployment

Gunakan **Docker Compose** untuk menjalankan seluruh layanan:
```sh
docker-compose up -d
```

### Menjalankan Gateway API
```sh
cd Gateways/OcelotApigateway
dotnet run
```

### Menjalankan Service Secara Individual
Buka terminal baru untuk menjalankan setiap service yang dipilih.
```sh
cd Services/{service yang dipilih}
dotnet run
```


## 📜 Lisensi
MIT License. Silakan gunakan dan kembangkan lebih lanjut.

---

🚀 **Sistem informasi bioskop ini dirancang untuk skala besar dengan arsitektur modular yang siap produksi!**




