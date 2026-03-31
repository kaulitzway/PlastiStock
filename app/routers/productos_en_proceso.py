from fastapi import APIRouter, Depends, HTTPException, status
from sqlalchemy.ext.asyncio import AsyncSession
from typing import List
from app.database import get_db
from app.schemas.schemas import ProductoEnProcesoCreate, ProductoEnProcesoUpdate, ProductoEnProcesoResponse
from app.repositories.producto_en_proceso_repository import ProductoEnProcesoRepository
from app.services.jwt_service import get_current_user, require_role

router = APIRouter(prefix="/api/ProductosEnProceso", tags=["Productos en Proceso"])

@router.get("", response_model=List[ProductoEnProcesoResponse])
async def get_all(
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    return await ProductoEnProcesoRepository(db).get_all()

@router.get("/{id}", response_model=ProductoEnProcesoResponse)
async def get_by_id(
    id: int,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    producto = await ProductoEnProcesoRepository(db).get_by_id(id)
    if not producto:
        raise HTTPException(status_code=404, detail="Producto en proceso no encontrado")
    return producto

@router.post("", response_model=ProductoEnProcesoResponse, status_code=status.HTTP_201_CREATED)
async def create(
    producto: ProductoEnProcesoCreate,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador", "Supervisor"]))
):
    return await ProductoEnProcesoRepository(db).create(producto.model_dump())

@router.put("/{id}", response_model=ProductoEnProcesoResponse)
async def update(
    id: int,
    producto: ProductoEnProcesoUpdate,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador", "Supervisor"]))
):
    updated = await ProductoEnProcesoRepository(db).update(id, producto.model_dump())
    if not updated:
        raise HTTPException(status_code=404, detail="Producto en proceso no encontrado")
    return updated

@router.delete("/{id}")
async def delete(
    id: int,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador"]))
):
    deleted = await ProductoEnProcesoRepository(db).delete(id)
    if not deleted:
        raise HTTPException(status_code=404, detail="Producto en proceso no encontrado")
    return {"message": "Producto en proceso eliminado correctamente"}
