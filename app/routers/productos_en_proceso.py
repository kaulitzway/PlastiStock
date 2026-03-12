from fastapi import APIRouter, Depends, HTTPException, status
from sqlalchemy.orm import Session
from typing import List
from app.database import get_db
from app.schemas.schemas import ProductoEnProcesoCreate, ProductoEnProcesoUpdate, ProductoEnProcesoResponse
from app.repositories.producto_en_proceso_repository import ProductoEnProcesoRepository
from app.services.jwt_service import get_current_user, require_role

router = APIRouter(prefix="/api/ProductosEnProceso", tags=["Productos en Proceso"])

@router.get("", response_model=List[ProductoEnProcesoResponse])
async def get_all_productos_en_proceso(
    db: Session = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    repo = ProductoEnProcesoRepository(db)
    return repo.get_all()

@router.get("/{id}", response_model=ProductoEnProcesoResponse)
async def get_producto_en_proceso_by_id(
    id: int,
    db: Session = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    repo = ProductoEnProcesoRepository(db)
    producto = repo.get_by_id(id)
    if not producto:
        raise HTTPException(status_code=404, detail="Producto en proceso no encontrado")
    return producto

@router.post("", response_model=ProductoEnProcesoResponse, status_code=status.HTTP_201_CREATED)
async def create_producto_en_proceso(
    producto: ProductoEnProcesoCreate,
    db: Session = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador", "Supervisor"]))
):
    repo = ProductoEnProcesoRepository(db)
    return repo.create(producto.model_dump())

@router.put("/{id}", response_model=ProductoEnProcesoResponse)
async def update_producto_en_proceso(
    id: int,
    producto: ProductoEnProcesoUpdate,
    db: Session = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador", "Supervisor"]))
):
    repo = ProductoEnProcesoRepository(db)
    updated = repo.update(id, producto.model_dump())
    if not updated:
        raise HTTPException(status_code=404, detail="Producto en proceso no encontrado")
    return updated

@router.delete("/{id}")
async def delete_producto_en_proceso(
    id: int,
    db: Session = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador"]))
):
    repo = ProductoEnProcesoRepository(db)
    deleted = repo.delete(id)
    if not deleted:
        raise HTTPException(status_code=404, detail="Producto en proceso no encontrado")
    return {"message": "Producto en proceso eliminado correctamente"}
