# Tables 📊

## 👥 Users

| Column 🏷️   | Type 🔠   | Description 📝              |
|--------------|----------|----------------------------|
| 🆔 UserId    | UUID     | 🔑 Primary Key             |
| 👤 Nama      | VARCHAR  | 🏷️ Full Name              |
| 📧 Email     | VARCHAR  | ✉️ User email              |
| 🔒 Password  | VARCHAR  | 🔑 Hashed password         |
| 🕒 CreateAt  | DATETIME | 🆕 Creation timestamp      |
| 🕒 UpdateAt  | DATETIME | 🔄 Last update timestamp   |
| 🕒 DeleteAt  | DATETIME | 🗑️ Soft delete timestamp  |

## 🎦 Studios

| Column 🏷️     | Type 🔠   | Description 📝                |
|----------------|----------|--------------------------------|
| 🆔 StudioId    | UUID     | 🔑 Primary Key               |
| 🎦 NamaStudio | VARCHAR  | 🏷️ Studio Name              |
| 🪑 Kapasitas   | INT      | 🪑 Seat capacity             |
| 🏗️ Fasilitas  | VARCHAR  | 🔧 Additional facilities     |
| 🕒 CreateAt    | DATETIME | 🆕 Creation timestamp        |
| 🕒 UpdateAt    | DATETIME | 🔄 Last update timestamp     |
| 🕒 DeleteAt    | DATETIME | 🗑️ Soft delete timestamp    |

## 🎬 Movies

| Column 🏷️     | Type 🔠   | Description 📝                |
|----------------|----------|--------------------------------|
| 🆔 FilmId      | UUID     | 🔑 Primary Key               |
| 🎬 Judul      | VARCHAR  | 🎭 Movie Title               |
| 🎭 Genre      | VARCHAR  | 🎞️ Genre                     |
| ⏳ Durasi     | INT      | ⏳ Duration in minutes        |
| 📝 Deskripsi  | TEXT     | 📝 Movie description          |
| 🕒 CreateAt   | DATETIME | 🆕 Creation timestamp         |
| 🕒 UpdateAt   | DATETIME | 🔄 Last update timestamp      |
| 🕒 DeleteAt   | DATETIME | 🗑️ Soft delete timestamp     |

## 📅 Schedules

| Column 🏷️     | Type 🔠   | Description 📝                |
|----------------|----------|--------------------------------|
| 🆔 JadwalId    | UUID     | 🔑 Primary Key               |
| 🎦 StudioId    | UUID     | 🔗 Foreign Key to Studios    |
| 🎬 FilmId      | UUID     | 🔗 Foreign Key to Movies     |
| 📅 Tanggal     | DATE     | 📆 Show date                 |
| ⏰ WaktuMulai  | TIME     | 🎬 Start time                |
| ⏰ WaktuSelesai| TIME     | 🎬 End time                  |
| 💰 HargaTiket  | DECIMAL  | 💵 Ticket price              |
| 🕒 CreateAt    | DATETIME | 🆕 Creation timestamp        |
| 🕒 UpdateAt    | DATETIME | 🔄 Last update timestamp     |
| 🕒 DeleteAt    | DATETIME | 🗑️ Soft delete timestamp    |

## 🎟️ Tickets

| Column 🏷️     | Type 🔠   | Description 📝                |
|----------------|----------|--------------------------------|
| 🆔 TiketId     | UUID     | 🔑 Primary Key               |
| 📅 JadwalId    | UUID     | 🔗 Foreign Key to Schedules  |
| 👤 UserId      | UUID     | 🔗 Foreign Key to Users      |
| 💺 NomorKursi  | VARCHAR  | 🪑 Assigned seat number      |
| 🏷️ Status     | ENUM     | ✅ Terkonfirmasi, ❌ Dibatalkan, ⏳ Expired |
| 🕒 CreateAt    | DATETIME | 🆕 Creation timestamp        |
| 🕒 UpdateAt    | DATETIME | 🔄 Last update timestamp     |
| 🕒 DeleteAt    | DATETIME | 🗑️ Soft delete timestamp    |

## 💳 Transactions

| Column 🏷️     | Type 🔠   | Description 📝                |
|----------------|----------|--------------------------------|
| 🆔 TransaksiId | UUID     | 🔑 Primary Key               |
| 🎟️ TiketId    | UUID     | 🔗 Foreign Key to Tickets    |
| 💵 StatusPembayaran | ENUM | ✅ Berhasil, ❌ Gagal, ⏳ Pending |
| 💳 MetodePembayaran | ENUM | 💳 KartuKredit, 📲 EWallet, 🏦 TransferBank |
| 🕒 CreateAt    | DATETIME | 🆕 Creation timestamp        |
| 🕒 UpdateAt    | DATETIME | 🔄 Last update timestamp     |
| 🕒 DeleteAt    | DATETIME | 🗑️ Soft delete timestamp    |
