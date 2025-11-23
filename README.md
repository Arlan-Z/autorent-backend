# 🚗 Autorent — Backend API

**Простой backend-сервис для управления автомобилями и бронированиями.
Построен на Clean Architecture, ASP.NET Core, PostgreSQL и JWT.**

---

## 📌 Основные возможности

### 🔐 Авторизация

* Регистрация пользователя
* Логин
* JWT (access token)
* Доступ к защищённым маршрутам

### 🚙 Машины (Cars)

* Просмотр списка машин
* Получение автомобиля по ID
* Создание, редактирование и удаление машин
* Сидеры начальных данных (для DEV среды)

### 📅 Бронирования (Bookings)

* Создание бронирования автомобиля
* Проверка доступности автомобиля
* Просмотр своих бронирований
* Просмотр одного бронирования
* Изменение статуса:

  * pending
  * confirmed
  * completed
  * canceled

### 📐 Чистая архитектура

Проект разделён на слои:

```
Domain
Application
Infrastructure
API
```

---

# 🏗 Архитектура проекта

```
Autorent.Domain
    Entities/
    Enums/

Autorent.Application
    DTO/
    Interfaces/
    Services (если нужны use cases)

Autorent.Infrastructure
    Persistence/
        ApplicationDbContext.cs
        Seeders/
            CarSeeder.cs
            SeedData.cs
    Services/
        AuthService.cs
        CarService.cs
        BookingService.cs

Autorent.Api
    Controllers/
    Program.cs
```

**Domain** — только сущности и enum’ы
**Application** — DTO, интерфейсы, бизнес-логика
**Infrastructure** — EF Core, PostgreSQL, реализации сервисов
**API** — контроллеры, JWT, DI

---

# 🗄 Требования

* .NET 8
* Docker + Docker Compose
* PostgreSQL 16
* JWT Secret Key в `.env`

---

# ⚙️ Запуск проекта

### 1. Клонировать репозиторий

```bash
git clone https://github.com/<your-repo>/autorent-backend.git
cd autorent-backend
```

---

### 2. Создать `.env` на основе `.env.example`

```bash
cp .env.example .env
```

Пример:

```
ConnectionStrings__DefaultConnection=Host=localhost;Port=5432;Database=autorent_db;Username=postgres;Password=postgres
Jwt__Key=YOUR_LONG_SECRET_KEY_32+chars
```

⚠️ **Jwt__Key должен быть минимум 32 символа!**

---

### 3. Запустить базу данных

```bash
git clone https://github.com/Arlan-Z/autorent-infrastructure
cd autorent-infrastructure/docker/postgres
docker compose up -d
```

---

### 4. Запустить backend API

```bash
dotnet run --project Autorent.Api
```

API будет доступен на:

```
http://localhost:5219
```
---

# 🌱 Сидеры

В режиме **Development** автоматически заполняются:

* тестовые машины (3 шт.)

Файл сидеров:

```
Infrastructure/Persistence/Seeders/
```

---

# 🔐 Авторизация

Используется JWT Bearer.

### Авторизация по токену:

```
Authorization: Bearer <token>
```

---

# 🚙 API: Машины

### Получить список машин

```
GET /api/cars
```

### Получить машину по ID

```
GET /api/cars/{id}
```

### Создать машину

```
POST /api/cars
```

```json
{
  "brand": "BMW",
  "model": "X5",
  "year": 2020,
  "priceHour": 20,
  "priceDay": 150,
  "imageUrl": "https://img.jpg"
}
```

---

# 📅 API: Бронирования

### Проверить доступность машины

```
GET /api/booking/available?carId=1&start=2025-02-21T10:00:00Z&end=2025-02-21T14:00:00Z
```

### Создать бронирование

```
POST /api/booking
```

```json
{
  "carId": 1,
  "startDate": "2025-02-21T10:00:00Z",
  "endDate": "2025-02-21T15:00:00Z"
}
```

### Мои бронирования

```
GET /api/booking/my
```

### Отменить бронирование

```
POST /api/booking/{id}/cancel
```

### Подтвердить бронирование

```
POST /api/booking/{id}/confirm
```

### Завершить бронирование

```
POST /api/booking/{id}/complete
```

---
