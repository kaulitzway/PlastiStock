# 📡 Documentación de Endpoints - PlastiStock API

## 🔐 Autenticación

### Login
- **POST** `/api/InicioSesion/login`
  - Body: `{ "Correo": "email@example.com", "Contrasena": "password" }`
  - Response: `{ "success": true, "token": "jwt_token", "message": "..." }`
  - Auth: No requerida

---

## 👥 Usuarios

- **GET** `/api/Usuarios` - Listar todos los usuarios (Auth: Cualquier usuario)
- **GET** `/api/Usuarios/{id}` - Obtener usuario por ID (Auth: Cualquier usuario)
- **POST** `/api/Usuarios/crear` - Crear usuario (Auth: Administrador)
- **PUT** `/api/Usuarios/{id}` - Actualizar usuario (Auth: Administrador)
- **DELETE** `/api/Usuarios/{id}` - Eliminar usuario (Auth: Administrador)

---

## 📦 Productos

- **GET** `/api/Productos` - Listar productos (Auth: Cualquier usuario)
- **GET** `/api/Productos/{id}` - Obtener producto (Auth: Cualquier usuario)
- **POST** `/api/Productos` - Crear producto (Auth: Admin/Supervisor)
- **PUT** `/api/Productos/{id}` - Actualizar producto (Auth: Admin/Supervisor)
- **DELETE** `/api/Productos/{id}` - Eliminar producto (Auth: Administrador)

---

## 🏷️ Categorías

- **GET** `/api/Categorias` - Listar categorías (Auth: Cualquier usuario)
- **GET** `/api/Categorias/{id}` - Obtener categoría (Auth: Cualquier usuario)
- **POST** `/api/Categorias` - Crear categoría (Auth: Administrador)
- **PUT** `/api/Categorias/{id}` - Actualizar categoría (Auth: Administrador)
- **DELETE** `/api/Categorias/{id}` - Eliminar categoría (Auth: Administrador)

---

## 🏭 Proveedores

- **GET** `/api/Proveedores` - Listar proveedores (Auth: Cualquier usuario)
- **GET** `/api/Proveedores/{id}` - Obtener proveedor (Auth: Cualquier usuario)
- **POST** `/api/Proveedores` - Crear proveedor (Auth: Admin/Supervisor)
- **PUT** `/api/Proveedores/{id}` - Actualizar proveedor (Auth: Admin/Supervisor)
- **DELETE** `/api/Proveedores/{id}` - Eliminar proveedor (Auth: Administrador)

---

## 🧱 Materias Primas

- **GET** `/api/MateriasPrimas` - Listar materias primas (Auth: Cualquier usuario)
- **GET** `/api/MateriasPrimas/{id}` - Obtener materia prima (Auth: Cualquier usuario)
- **POST** `/api/MateriasPrimas` - Crear materia prima (Auth: Admin/Supervisor)
- **PUT** `/api/MateriasPrimas/{id}` - Actualizar materia prima (Auth: Admin/Supervisor)
- **DELETE** `/api/MateriasPrimas/{id}` - Eliminar materia prima (Auth: Administrador)

---

## ⚙️ Productos en Proceso

- **GET** `/api/ProductosEnProceso` - Listar productos en proceso (Auth: Cualquier usuario)
- **GET** `/api/ProductosEnProceso/{id}` - Obtener producto en proceso (Auth: Cualquier usuario)
- **POST** `/api/ProductosEnProceso` - Crear producto en proceso (Auth: Admin/Supervisor)
- **PUT** `/api/ProductosEnProceso/{id}` - Actualizar producto en proceso (Auth: Admin/Supervisor)
- **DELETE** `/api/ProductosEnProceso/{id}` - Eliminar producto en proceso (Auth: Administrador)

---

## ✅ Productos Terminados

- **GET** `/api/ProductosTerminados` - Listar productos terminados (Auth: Cualquier usuario)
- **GET** `/api/ProductosTerminados/{id}` - Obtener producto terminado (Auth: Cualquier usuario)
- **POST** `/api/ProductosTerminados` - Crear producto terminado (Auth: Admin/Supervisor)
- **PUT** `/api/ProductosTerminados/{id}` - Actualizar producto terminado (Auth: Admin/Supervisor)
- **DELETE** `/api/ProductosTerminados/{id}` - Eliminar producto terminado (Auth: Administrador)

---

## 📥 Entradas de Inventario

- **GET** `/api/EntradasInventario` - Listar entradas (Auth: Cualquier usuario)
- **GET** `/api/EntradasInventario/{id}` - Obtener entrada (Auth: Cualquier usuario)
- **POST** `/api/EntradasInventario` - Registrar entrada (Auth: Admin/Supervisor)
  - Actualiza automáticamente el stock del producto
  - Registra movimiento en Kardex
- **DELETE** `/api/EntradasInventario/{id}` - Eliminar entrada (Auth: Administrador)

---

## 📤 Salidas de Inventario

- **GET** `/api/SalidasInventario` - Listar salidas (Auth: Cualquier usuario)
- **GET** `/api/SalidasInventario/{id}` - Obtener salida (Auth: Cualquier usuario)
- **POST** `/api/SalidasInventario` - Registrar salida (Auth: Admin/Supervisor)
  - Verifica stock disponible
  - Actualiza automáticamente el stock del producto
  - Registra movimiento en Kardex
- **DELETE** `/api/SalidasInventario/{id}` - Eliminar salida (Auth: Administrador)

---

## 📊 Kardex

- **GET** `/api/Kardex` - Listar todos los movimientos (Auth: Cualquier usuario)
- **GET** `/api/Kardex/{id}` - Obtener movimiento (Auth: Cualquier usuario)
- **GET** `/api/Kardex/producto/{producto_id}` - Historial de un producto (Auth: Cualquier usuario)

---

## 📋 Órdenes de Producción

- **GET** `/api/OrdenesProduccion` - Listar órdenes (Auth: Cualquier usuario)
- **GET** `/api/OrdenesProduccion/{id}` - Obtener orden (Auth: Cualquier usuario)
- **POST** `/api/OrdenesProduccion` - Crear orden (Auth: Admin/Supervisor)
- **PUT** `/api/OrdenesProduccion/{id}` - Actualizar orden (Auth: Admin/Supervisor)
- **DELETE** `/api/OrdenesProduccion/{id}` - Eliminar orden (Auth: Administrador)

---

## ⚠️ Mermas

- **GET** `/api/Mermas` - Listar mermas (Auth: Cualquier usuario)
- **GET** `/api/Mermas/{id}` - Obtener merma (Auth: Cualquier usuario)
- **GET** `/api/Mermas/producto/{producto_id}` - Mermas de un producto (Auth: Cualquier usuario)
- **POST** `/api/Mermas` - Registrar merma (Auth: Admin/Supervisor)
  - Reduce automáticamente el stock del producto
  - Registra movimiento en Kardex
- **DELETE** `/api/Mermas/{id}` - Eliminar merma (Auth: Administrador)

---

## 📝 Solicitudes

- **GET** `/api/Solicitudes` - Listar solicitudes (Auth: Cualquier usuario)
- **GET** `/api/Solicitudes/{id}` - Obtener solicitud (Auth: Cualquier usuario)
- **GET** `/api/Solicitudes/usuario/{usuario_id}` - Solicitudes de un usuario (Auth: Cualquier usuario)
- **POST** `/api/Solicitudes` - Crear solicitud (Auth: Cualquier usuario)
- **PUT** `/api/Solicitudes/{id}` - Actualizar solicitud (Auth: Administrador)
- **DELETE** `/api/Solicitudes/{id}` - Eliminar solicitud (Auth: Administrador)

---

## 🔑 Roles de Usuario

- **Administrador**: Acceso completo a todos los endpoints
- **Supervisor**: Puede crear/editar productos, materias primas, órdenes, etc.
- **Usuario**: Solo lectura (GET)

---

## 📖 Documentación Interactiva

Una vez que la API esté corriendo, puedes acceder a:

- **Swagger UI**: http://localhost:8000/docs
- **ReDoc**: http://localhost:8000/redoc

Estas interfaces te permiten:
- Ver todos los endpoints disponibles
- Probar las peticiones directamente desde el navegador
- Ver los esquemas de datos (request/response)
- Autenticarte con JWT

---

## 🧪 Ejemplo de Uso Completo

### 1. Login
```bash
curl -X POST "http://localhost:8000/api/InicioSesion/login" \
  -H "Content-Type: application/json" \
  -d '{"Correo": "admin@plastistok.com", "Contrasena": "password123"}'
```

Response:
```json
{
  "success": true,
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "message": "Inicio de sesión exitoso"
}
```

### 2. Usar el Token
```bash
curl -X GET "http://localhost:8000/api/Productos" \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
```

### 3. Crear un Producto
```bash
curl -X POST "http://localhost:8000/api/Productos" \
  -H "Authorization: Bearer tu_token" \
  -H "Content-Type: application/json" \
  -d '{
    "Nombre": "Producto Ejemplo",
    "Descripcion": "Descripción del producto",
    "CodigoInterno": "PROD001",
    "PrecioUnitario": 100.50,
    "Stock": 50,
    "CategoriaId": 1
  }'
```

### 4. Registrar Entrada de Inventario
```bash
curl -X POST "http://localhost:8000/api/EntradasInventario" \
  -H "Authorization: Bearer tu_token" \
  -H "Content-Type: application/json" \
  -d '{
    "ProductoId": 1,
    "Cantidad": 100
  }'
```

---

## 💡 Notas Importantes

1. **Todos los endpoints (excepto login) requieren autenticación JWT**
2. **El token expira en 2 horas**
3. **Los movimientos de inventario (entradas/salidas/mermas) actualizan automáticamente el stock**
4. **Todos los movimientos se registran en el Kardex**
5. **Las fechas se manejan automáticamente (UTC)**
