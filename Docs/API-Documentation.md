# API Gateway Routes 🚀

| Method    | Endpoint         | Description 📜                          | Query Params 🔍                               |
|-----------|------------------|-----------------------------------------|-----------------------------------------------|
| 🔵 POST   | /auth/login      | 🔐 Login user & return JWT token       | -                                             |
| 🟢 POST   | /auth/register   | 📝 Register new user                   | -                                             |
| 🔍 GET    | /movies          | 🎬 Get all movies                      | `🎭 genre` (optional), `🔢 limit` (optional)  |
| 🟢 POST   | /movies          | ➕ Add new movie (Admin only)          | -                                             |
| 🟡 PUT    | /movies/{id}     | 📝 Update movie details (Admin only)   | -                                             |
| 🔍 GET    | /schedule        | 📆 Get movie schedules                 | `📅 date` (optional), `🎦 studio` (optional), `🎬 filmId` (optional) |
| 🟢 POST   | /schedule        | 📅 Create movie schedule (Admin only)  | -                                             |
| 🟡 PUT    | /schedule/{id}   | 📝 Update movie schedule (Admin only)  | -                                             |
| ❌ DELETE | /schedule/{id}   | 🗑️ Delete movie schedule (Admin only)  | -                                             |
| 🔍 GET    | /ticket          | 🎟️ Get user tickets                    | `👤 userId` (optional), `📌 status` (optional)|
| 🟢 POST   | /ticket          | 🎟️ Book a ticket                       | -                                             |
| 🟡 PUT    | /ticket/{id}     | 📝 Update ticket status (Admin only)   | -                                             |
| 🔍 GET    | /transaction     | 💳 Get transactions                    | `📌 status` (optional), `👤 userId` (optional)|
| 🟢 POST   | /transaction     | 💰 Process transaction                 | -                                             |
| 🟡 PUT    | /transaction/{id}| 📝 Update transaction details (Admin only) | -                                             |
| 🔍 GET    | /notifications   | 🔔 Get notifications                   | `📩 type` (optional), `📌 unread` (optional)  |
