# 🚀 Inicio Rápido - PlastiStock API

## ⚡ Configuración en 5 minutos

### 1️⃣ Instalar Python

Descarga Python 3.10+ desde [python.org](https://www.python.org/downloads/)

Verifica la instalación:
```bash
python --version
```

### 2️⃣ Clonar el repositorio

```bash
git clone <tu-repositorio>
cd plastistok-api-python
```

### 3️⃣ Crear entorno virtual

**Windows:**
```bash
python -m venv venv
venv\Scripts\activate
```

**Linux/Mac:**
```bash
python3 -m venv venv
source venv/bin/activate
```

### 4️⃣ Instalar dependencias

```bash
pip install -r requirements.txt
```

### 5️⃣ Configurar base de datos

Edita el archivo `.env`:

```env
DB_SERVER=DESKTOP-9001S9E
DB_NAME=PlastiStockDB
DB_TRUSTED_CONNECTION=yes
JWT_SECRET_KEY=A9D3F8C1E4B7A2D9F1C6E3B8D7A4C9F2A1B6D8E4F3C7A9D1F5B2E7C4A8D3F6
```

### 6️⃣ Ejecutar la aplicación

```bash
python run.py
```

O directamente con uvicorn:
```bash
uvicorn app.main:app --reload
```

### 7️⃣ Probar la API

Abre tu navegador en:
- **API**: http://localhost:8000
- **Documentación**: http://localhost:8000/docs
- **Health Check**: http://localhost:8000/health

## 🧪 Probar el Login

### Usando la documentación interactiva (Swagger)

1. Ve a http://localhost:8000/docs
2. Busca el endpoint `POST /api/InicioSesion/login`
3. Haz clic en "Try it out"
4. Ingresa:
```json
{
  "Correo": "admin@plastistok.com",
  "Contrasena": "tu_contraseña"
}
```
5. Haz clic en "Execute"
6. Copia el token de la respuesta

### Usando curl

```bash
curl -X POST "http://localhost:8000/api/InicioSesion/login" \
  -H "Content-Type: application/json" \
  -d '{
    "Correo": "admin@plastistok.com",
    "Contrasena": "tu_contraseña"
  }'
```

### Usando Postman

1. Método: `POST`
2. URL: `http://localhost:8000/api/InicioSesion/login`
3. Body (JSON):
```json
{
  "Correo": "admin@plastistok.com",
  "Contrasena": "tu_contraseña"
}
```

## 🔐 Usar el Token

Una vez que tengas el token, agrégalo en los headers de tus peticiones:

```
Authorization: Bearer tu_token_aqui
```

### Ejemplo con curl:
```bash
curl -X GET "http://localhost:8000/api/Usuarios" \
  -H "Authorization: Bearer tu_token_aqui"
```

## 📋 Endpoints Disponibles

| Método | Endpoint | Descripción | Auth |
|--------|----------|-------------|------|
| POST | `/api/InicioSesion/login` | Iniciar sesión | No |
| GET | `/api/Usuarios` | Listar usuarios | Sí |
| GET | `/api/Usuarios/{id}` | Ver usuario | Sí |
| POST | `/api/Usuarios/crear` | Crear usuario | Admin |
| PUT | `/api/Usuarios/{id}` | Actualizar usuario | Admin |
| DELETE | `/api/Usuarios/{id}` | Eliminar usuario | Admin |
| GET | `/api/Productos` | Listar productos | Sí |
| GET | `/api/Productos/{id}` | Ver producto | Sí |
| POST | `/api/Productos` | Crear producto | Admin/Supervisor |
| PUT | `/api/Productos/{id}` | Actualizar producto | Admin/Supervisor |
| DELETE | `/api/Productos/{id}` | Eliminar producto | Admin |

## ❓ Problemas Comunes

### Error: "No module named 'app'"
```bash
# Asegúrate de estar en la carpeta raíz del proyecto
cd plastistok-api-python
python run.py
```

### Error: "Cannot connect to SQL Server"
- Verifica que SQL Server esté corriendo
- Revisa la configuración en `.env`
- Asegúrate de que el nombre del servidor sea correcto

### Error: "Port 8000 already in use"
```bash
# Usa otro puerto
uvicorn app.main:app --reload --port 8001
```

### El token no funciona
- Verifica que estés usando `Bearer` antes del token
- Asegúrate de que el token no haya expirado (2 horas)
- Revisa que la clave JWT en `.env` sea la correcta

## 🎯 Siguiente Paso

Lee el archivo `README.md` para información más detallada sobre la arquitectura y desarrollo.
