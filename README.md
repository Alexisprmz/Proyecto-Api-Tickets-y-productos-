# API Tickets y Productos - Backend .NET

Sistema de gestión de tickets de compra y productos desarrollado con **ASP.NET Core 8.0**. Permite autenticación JWT, CRUD completo de productos (rol admin) y generación/consulta de tickets con cálculo automático de IVA 16%.[file:9]

##  Video Demostrativo

[![Video Demo Tickets API](https://img.youtube.com/vi/ijvgWhjKp6I/maxresdefault.jpg)](https://www.youtube.com/watch?v=ijvgWhjKp6I)

>  **[Ver video en YouTube](https://www.youtube.com/watch?v=ijvgWhjKp6I)**

En este video se demuestra el funcionamiento de los **7 endpoints principales y algorítmicos** del sistema:[file:9]

| # | Endpoint | Descripción |
|---|----------|-------------|
| 1 | `POST /api/Auth/login` | Autenticación JWT admin/Admin123 |[file:2]
| 2 | `GET /api/Products` | Listado completo de productos (rol admin) |[file:3]
| 3 | `POST /api/Products` | Crear producto con validación FluentValidation |[file:3]
| 4 | `PUT /api/Products/{id}` | Actualizar producto (JSON mejorado) |[file:3]
| 5 | `GET /api/Products/{id}` | Detalle específico de producto |[file:3]
| 6 | `POST /api/Tickets` | **Generar ticket** (validación stock, IVA 16%, código único) |[file:1]
| 7 | `GET /api/Tickets` + `GET /api/Tickets/{id}` | Historial y detalle completo |[file:1]

---

## Colección de Postman

**Enlace directo a la colección completa:**
>  **[Abrir en Postman](https://postman.co/workspace/My-Workspace~e46b459a-2e61-474a-8b45-89b2559699d5/collection/41483485-69bc0c22-7c1f-4c37-84e9-3173ba0ef8df?action=share&creator=41483485)**

La colección incluye **todos los endpoints** organizados.
## Docker Compose (Método Recomendado)

### docker-compose.yml

**Ejecutar:** `docker-compose up -d`
**URLs:** `http://localhost:5000` | `http://localhost:5000/swagger`

---

##  Tecnologías Utilizadas

| Tecnología | Versión | Uso |
|------------|---------|-----|
| ASP.NET Core | 8.0 | API REST |[file:21]
| Entity Framework Core | 8.0 | ORM SQL Server |[file:8]
| FluentValidation | 11.0+ | Validación DTOs |[file:3]
| JWT Bearer | 8.0 | Autenticación |[file:17]
| Swagger | 6.0+ | Documentación |[file:21]
| Docker | 3.8+ | Contenedores |[file:9]

---

## Requisitos Previos
1. .NET SDK 8.0+
dotnet --version

2. Docker Desktop
docker --version

3. Git (para clonar)
git --version


---

## 🚀 Instalación Manual (Sin Docker)
1. Clonar repositorio
git clone TU_REPOSITORIO_URL
cd tu-proyecto

2. Restaurar dependencias
dotnet restore

3. Configurar appsettings.json
ConnectionStrings → Tu SQL Server local
JwtSettings → Clave secreta

5. Migraciones
dotnet ef migrations add InitialCreate
dotnet ef database update


---

## Ejecución

### Con Docker (Recomendado)
docker-compose up -d
docker-compose logs -f


### Manual
dotnet run

**URLs (ver puerto en consola):**
API: http://localhost:{PUERTO}
Swagger: http://localhost:{PUERTO}/swagger


**Admin:** `admin` / `Admin123` [file:2]

---

## Autenticación JWT

**Flujo:**
1. `POST /api/Auth/login` → `{ "token": "..." }`
2. `Authorization: Bearer {token}` en headers

**Login demo:**
{
"username": "admin",
"password": "Admin123"
}

[file:2]

---

## Endpoints Principales

###  `POST /api/Auth/login`
{ "username": "admin", "password": "Admin123" }
**Response:** `{ "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..." }` [file:2]

### `GET /api/Products`
**Headers:** `Authorization: Bearer {token}`
**Response:** `[{ "id": 1, "nombre": "Hamburguesa", "precio": 89.50, "stock": 100 }]` [file:3]

### `POST /api/Products`
{ "nombre": "Chetos", "precio": 20.50, "stock": 100 }

[file:3]

### `PUT /api/Products/{id}`
**Response mejorada:**
{
"message": "Producto actualizado",
"product": { "id": 1, "nombre": "Doritos", ... }
}

[file:3]

### `POST /api/Tickets` **(Algorítmico)**
[
{ "productoId": 1, "cantidad": 2 },
{ "productoId": 2, "cantidad": 1 }
]

**Algoritmo:** Stock ↓ → Subtotal + IVA 16% → Código `TKT-XXXX`
{
"message": "Ticket generado exitosamente",
"ticket": {
"id": 1, "codigo": "TKT-12345678",
"subtotal": 200.00, "iva": 32.00, "total": 232.00
}
}

[file:1]

### 🔒 `GET /api/Tickets` | `GET /api/Tickets/{id}`
**Historial y detalles completos** [file:1]

---

## Modelos de Datos

| Entidad | Campos Principales |
|---------|-------------------|
| **Product** | `Id`, `Nombre`, `Precio`, `Stock` [file:22] |
| **Ticket** | `Id`, `Codigo`, `Fecha`, `Subtotal`, `IVA`, `Total`, `Estado` [file:12] |
| **TicketDetalle** | `TicketId`, `ProductoId`, `Cantidad`, `Precio`, `Total` [file:11] |

---

## Errores Comunes

| Código | Causa | Solución |
|--------|-------|----------|
| **401** | Token inválido | `POST /api/Auth/login` |
| **403** | Sin rol admin | Usar `admin/Admin123` |
| **404** | ID no existe | Verificar en `/api/Products` |
| **400** | Validación | Revisar JSON (nombre, precio > 0, stock ≥ 0) |

---


---

## Autores

**Alexis Armando Peralta Ramirez** 

---









