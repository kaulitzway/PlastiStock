# 🏭 PlastiStock API - Python/FastAPI

API REST para el sistema de gestión de inventario PlastiStock, migrada desde ASP.NET Core a Python con FastAPI.

[![Python](https://img.shields.io/badge/Python-3.10+-blue.svg)](https://www.python.org/)
[![FastAPI](https://img.shields.io/badge/FastAPI-0.115.0-green.svg)](https://fastapi.tiangolo.com/)
[![License](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

> 📖 **Inicio Rápido**: Lee [INICIO_RAPIDO.md](INICIO_RAPIDO.md) para empezar en 5 minutos
> 
> 📝 **Migración**: Lee [MIGRACION.md](MIGRACION.md) para entender los cambios desde .NET

## Tecnologías

- **FastAPI**: Framework web moderno y rápido
- **SQLAlchemy**: ORM para manejo de base de datos
- **PyMSSQL**: Conector para SQL Server
- **JWT**: Autenticación con tokens
- **BCrypt**: Encriptación de contraseñas
- **Pydantic**: Validación de datos

## Requisitos Previos

- Python 3.10 o superior
- SQL Server (la misma base de datos que usabas)
- pip (gestor de paquetes de Python)

## Instalación

### 1. Clonar el repositorio

```bash
git clone <tu-repositorio>
cd plastistok-api
```

### 2. Crear entorno virtual

```bash
python -m venv venv
```

### 3. Activar entorno virtual

**Windows:**
```bash
venv\Scripts\activate
```

**Linux/Mac:**
```bash
source venv/bin/activate
```

### 4. Instalar dependencias

```bash
pip install -r requirements.txt
```

### 5. Configurar variables de entorno

Edita el archivo `.env` con tu configuración de base de datos:

```env
DB_SERVER=DESKTOP-9001S9E
DB_NAME=PlastiStockDB
DB_TRUSTED_CONNECTION=yes
JWT_SECRET_KEY=tu_clave_secreta_aqui
```

## Ejecutar la aplicación

```bash
uvicorn app.main:app --reload --host 0.0.0.0 --port 8000
```

La API estará disponible en: `http://localhost:8000`

Documentación interactiva: `http://localhost:8000/docs`

## Endpoints Principales

### Autenticación
- `POST /api/InicioSesion/login` - Iniciar sesión

### Usuarios
- `GET /api/Usuarios` - Listar usuarios
- `GET /api/Usuarios/{id}` - Obtener usuario
- `POST /api/Usuarios/crear` - Crear usuario (Admin)
- `PUT /api/Usuarios/{id}` - Actualizar usuario (Admin)
- `DELETE /api/Usuarios/{id}` - Eliminar usuario (Admin)

### Productos
- `GET /api/Productos` - Listar productos
- `GET /api/Productos/{id}` - Obtener producto
- `POST /api/Productos` - Crear producto (Admin/Supervisor)
- `PUT /api/Productos/{id}` - Actualizar producto (Admin/Supervisor)
- `DELETE /api/Productos/{id}` - Eliminar producto (Admin)

## Estructura del Proyecto

```
app/
├── models/          # Modelos de base de datos (SQLAlchemy)
├── schemas/         # Esquemas de validación (Pydantic)
├── repositories/    # Capa de acceso a datos
├── routers/         # Endpoints de la API
├── services/        # Lógica de negocio (JWT, etc.)
├── config.py        # Configuración
├── database.py      # Conexión a BD
└── main.py          # Aplicación principal
```

## Diferencias con .NET

- **Controllers → Routers**: Los controladores ahora son routers de FastAPI
- **Entity Framework → SQLAlchemy**: ORM equivalente
- **Data Annotations → Pydantic**: Validación de modelos
- **Dependency Injection**: Nativo en FastAPI con `Depends()`
- **JWT**: Usando `python-jose` en lugar de Microsoft.IdentityModel

## Despliegue

Para producción, usa un servidor ASGI como Gunicorn:

```bash
pip install gunicorn
gunicorn app.main:app -w 4 -k uvicorn.workers.UvicornWorker
```

## 📚 Documentación Adicional

- [INICIO_RAPIDO.md](INICIO_RAPIDO.md) - Guía de inicio rápido
- [MIGRACION.md](MIGRACION.md) - Documentación de migración desde .NET
- [GUIA_GITHUB.md](GUIA_GITHUB.md) - Cómo subir el proyecto a GitHub

## 🤝 Contribuir

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 📄 Licencia

Este proyecto está bajo la Licencia MIT.

## 👥 Autor

PlastiStock Team
