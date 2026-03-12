# 🧪 Cómo Probar que Todo Funciona

## Paso 1: Verificar la Instalación

### Instalar dependencias
```bash
pip install -r requirements.txt
```

### Ejecutar script de verificación
```bash
python test_conexion.py
```

Este script verifica:
- ✅ Que todos los módulos se importan correctamente
- ✅ Que las clases están bien definidas
- ✅ Que JWT funciona
- ✅ Conexión a base de datos (si está configurada)

---

## Paso 2: Iniciar la Aplicación

```bash
python run.py
```

O directamente:
```bash
uvicorn app.main:app --reload
```

Deberías ver algo como:
```
INFO:     Uvicorn running on http://0.0.0.0:8000 (Press CTRL+C to quit)
INFO:     Started reloader process
INFO:     Started server process
INFO:     Waiting for application startup.
INFO:     Application startup complete.
```

---

## Paso 3: Probar en el Navegador

### Documentación Interactiva (Swagger)
Abre: **http://localhost:8000/docs**

Aquí verás TODOS los endpoints disponibles y podrás probarlos directamente.

### Health Check
Abre: **http://localhost:8000/health**

Deberías ver:
```json
{"status": "ok"}
```

### Root
Abre: **http://localhost:8000/**

Deberías ver:
```json
{
  "message": "PlastiStock API",
  "version": "1.0.0",
  "docs": "/docs"
}
```

---

## Paso 4: Probar Login (Sin Base de Datos)

Si tu base de datos aún no está lista, puedes probar que los endpoints responden:

### Usando Swagger (http://localhost:8000/docs)

1. Busca el endpoint `POST /api/InicioSesion/login`
2. Haz clic en "Try it out"
3. Ingresa cualquier dato:
```json
{
  "Correo": "test@test.com",
  "Contrasena": "test123"
}
```
4. Haz clic en "Execute"

Verás un error 401 (normal, porque el usuario no existe), pero significa que el endpoint funciona.

### Usando curl

```bash
curl -X POST "http://localhost:8000/api/InicioSesion/login" \
  -H "Content-Type: application/json" \
  -d '{"Correo": "test@test.com", "Contrasena": "test123"}'
```

---

## Paso 5: Verificar Todos los Endpoints

En Swagger (http://localhost:8000/docs) verás:

### Autenticación
- ✅ POST /api/InicioSesion/login

### Usuarios
- ✅ GET /api/Usuarios
- ✅ GET /api/Usuarios/{id}
- ✅ POST /api/Usuarios/crear
- ✅ PUT /api/Usuarios/{id}
- ✅ DELETE /api/Usuarios/{id}

### Productos
- ✅ GET /api/Productos
- ✅ GET /api/Productos/{id}
- ✅ POST /api/Productos
- ✅ PUT /api/Productos/{id}
- ✅ DELETE /api/Productos/{id}

### Categorías
- ✅ GET /api/Categorias
- ✅ GET /api/Categorias/{id}
- ✅ POST /api/Categorias
- ✅ PUT /api/Categorias/{id}
- ✅ DELETE /api/Categorias/{id}

### Proveedores
- ✅ GET /api/Proveedores
- ✅ GET /api/Proveedores/{id}
- ✅ POST /api/Proveedores
- ✅ PUT /api/Proveedores/{id}
- ✅ DELETE /api/Proveedores/{id}

### Materias Primas
- ✅ GET /api/MateriasPrimas
- ✅ GET /api/MateriasPrimas/{id}
- ✅ POST /api/MateriasPrimas
- ✅ PUT /api/MateriasPrimas/{id}
- ✅ DELETE /api/MateriasPrimas/{id}

### Productos en Proceso
- ✅ GET /api/ProductosEnProceso
- ✅ GET /api/ProductosEnProceso/{id}
- ✅ POST /api/ProductosEnProceso
- ✅ PUT /api/ProductosEnProceso/{id}
- ✅ DELETE /api/ProductosEnProceso/{id}

### Productos Terminados
- ✅ GET /api/ProductosTerminados
- ✅ GET /api/ProductosTerminados/{id}
- ✅ POST /api/ProductosTerminados
- ✅ PUT /api/ProductosTerminados/{id}
- ✅ DELETE /api/ProductosTerminados/{id}

### Entradas de Inventario
- ✅ GET /api/EntradasInventario
- ✅ GET /api/EntradasInventario/{id}
- ✅ POST /api/EntradasInventario
- ✅ DELETE /api/EntradasInventario/{id}

### Salidas de Inventario
- ✅ GET /api/SalidasInventario
- ✅ GET /api/SalidasInventario/{id}
- ✅ POST /api/SalidasInventario
- ✅ DELETE /api/SalidasInventario/{id}

### Kardex
- ✅ GET /api/Kardex
- ✅ GET /api/Kardex/{id}
- ✅ GET /api/Kardex/producto/{producto_id}

### Órdenes de Producción
- ✅ GET /api/OrdenesProduccion
- ✅ GET /api/OrdenesProduccion/{id}
- ✅ POST /api/OrdenesProduccion
- ✅ PUT /api/OrdenesProduccion/{id}
- ✅ DELETE /api/OrdenesProduccion/{id}

### Mermas
- ✅ GET /api/Mermas
- ✅ GET /api/Mermas/{id}
- ✅ GET /api/Mermas/producto/{producto_id}
- ✅ POST /api/Mermas
- ✅ DELETE /api/Mermas/{id}

### Solicitudes
- ✅ GET /api/Solicitudes
- ✅ GET /api/Solicitudes/{id}
- ✅ GET /api/Solicitudes/usuario/{usuario_id}
- ✅ POST /api/Solicitudes
- ✅ PUT /api/Solicitudes/{id}
- ✅ DELETE /api/Solicitudes/{id}

---

## 🎯 Resumen de Archivos Creados

### Modelos (1 archivo, 15 clases)
- `app/models/models.py` - Todos los modelos de base de datos

### Repositorios (13 archivos)
- `app/repositories/usuario_repository.py`
- `app/repositories/producto_repository.py`
- `app/repositories/categoria_repository.py`
- `app/repositories/proveedor_repository.py`
- `app/repositories/materia_prima_repository.py`
- `app/repositories/producto_en_proceso_repository.py`
- `app/repositories/producto_terminado_repository.py`
- `app/repositories/entrada_inventario_repository.py`
- `app/repositories/salida_inventario_repository.py`
- `app/repositories/kardex_repository.py`
- `app/repositories/orden_produccion_repository.py`
- `app/repositories/merma_repository.py`
- `app/repositories/solicitud_repository.py`

### Routers (14 archivos)
- `app/routers/auth.py`
- `app/routers/usuarios.py`
- `app/routers/productos.py`
- `app/routers/categorias.py`
- `app/routers/proveedores.py`
- `app/routers/materias_primas.py`
- `app/routers/productos_en_proceso.py`
- `app/routers/productos_terminados.py`
- `app/routers/entradas_inventario.py`
- `app/routers/salidas_inventario.py`
- `app/routers/kardex.py`
- `app/routers/ordenes_produccion.py`
- `app/routers/mermas.py`
- `app/routers/solicitudes.py`

### Schemas (1 archivo con todos los DTOs)
- `app/schemas/schemas.py`

### Servicios
- `app/services/jwt_service.py`

---

## ✅ Checklist de Verificación

- [ ] `python test_conexion.py` pasa todas las pruebas
- [ ] `python run.py` inicia sin errores
- [ ] http://localhost:8000 responde
- [ ] http://localhost:8000/docs muestra Swagger
- [ ] Se ven todos los endpoints en Swagger
- [ ] Los endpoints responden (aunque sea con error de auth)

---

## 🐛 Solución de Problemas

### Error: "No module named 'app'"
```bash
# Asegúrate de estar en la carpeta raíz
cd plastistok-api-python
python run.py
```

### Error: "ModuleNotFoundError: No module named 'fastapi'"
```bash
pip install -r requirements.txt
```

### Error: "Cannot connect to database"
- Es normal si aún no configuraste la base de datos
- La API funcionará, solo no podrás hacer operaciones reales
- Configura `.env` con tus datos de SQL Server

### Puerto 8000 ocupado
```bash
# Usa otro puerto
uvicorn app.main:app --reload --port 8001
```

---

## 🎉 ¡Listo!

Si todos los pasos funcionan, tu migración está completa y funcionando correctamente.
