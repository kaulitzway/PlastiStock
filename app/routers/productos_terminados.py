from fastapi import APIRouter, Depends, HTTPException, status
from sqlalchemy.ext.asyncio import AsyncSession
from typing import List
from app.database import get_db
from app.schemas.schemas import ProductoTerminadoCreate, ProductoTerminadoUpdate, ProductoTerminadoResponse
from app.repositories.producto_terminado_repository import ProductoTerminadoRepository
from app.services.jwt_service import get_current_user, require_role

router = APIRouter(prefix="/api/ProductosTerminados", tags=["Productos Terminados"])

@router.get("", response_model=List[ProductoTerminadoResponse])
async def get_all(
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    return await ProductoTerminadoRepository(db).get_all()

@router.get("/{id}", response_model=ProductoTerminadoResponse)
async def get_by_id(
    id: int,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    producto = await ProductoTerminadoRepository(db).get_by_id(id)
    if not producto:
        raise HTTPException(status_code=404, detail="Producto terminado no encontrado")
    return producto

@router.post("", response_model=ProductoTerminadoResponse, status_code=status.HTTP_201_CREATED)
async def create(
    producto: ProductoTerminadoCreate,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador", "Supervisor"]))
):
    return await ProductoTerminadoRepository(db).create(producto.model_dump())

@router.put("/{id}", response_model=ProductoTerminadoResponse)
async def update(
    id: int,
    producto: ProductoTerminadoUpdate,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador", "Supervisor"]))
):
    updated = await ProductoTerminadoRepository(db).update(id, producto.model_dump())
    if not updated:
        raise HTTPException(status_code=404, detail="Producto terminado no encontrado")
    return updated

@router.delete("/{id}")
async def delete(
    id: int,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador"]))
):
    deleted = await ProductoTerminadoRepository(db).delete(id)
    if not deleted:
        raise HTTPException(status_code=404, detail="Producto terminado no encontrado")
    return {"message": "Producto terminado eliminado correctamente"}
