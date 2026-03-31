from fastapi import FastAPI
from fastapi.middleware.cors import CORSMiddleware
from app.config import settings
from app.database import engine, Base

# Importa los modelos ANTES de crear las tablas
from app.models import models

from app.routers import (
    auth_router,
    usuarios_router,
    productos_router,
    categorias_router,
    proveedores_router,
    materias_primas_router,
    productos_en_proceso_router,
    productos_terminados_router,
    entradas_inventario_router,
    salidas_inventario_router,
    kardex_router,
    ordenes_produccion_router,
    mermas_router,
    solicitudes_router
)

# 1. Primero se crea app
app = FastAPI(
    title=settings.APP_NAME,
    version=settings.APP_VERSION,
    description="API REST para PlastiStock - Sistema de gestión de inventario"
)

# 2. Luego el middleware
app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

# 3. Luego el evento startup
@app.on_event("startup")
async def startup():
    async with engine.begin() as conn:
        await conn.run_sync(Base.metadata.create_all)

# 4. Luego los routers
app.include_router(auth_router)
app.include_router(usuarios_router)
app.include_router(productos_router)
app.include_router(categorias_router)
app.include_router(proveedores_router)
app.include_router(materias_primas_router)
app.include_router(productos_en_proceso_router)
app.include_router(productos_terminados_router)
app.include_router(entradas_inventario_router)
app.include_router(salidas_inventario_router)
app.include_router(kardex_router)
app.include_router(ordenes_produccion_router)
app.include_router(mermas_router)
app.include_router(solicitudes_router)

@app.get("/")
async def root():
    return {
        "message": "PlastiStock API",
        "version": settings.APP_VERSION,
        "docs": "/docs"
    }

@app.get("/health")
async def health_check():
    return {"status": "ok"}