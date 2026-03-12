from fastapi import APIRouter, Depends, HTTPException, status
from sqlalchemy.orm import Session
from typing import List
from app.database import get_db
from app.schemas.schemas import ProductoTerminadoCreate, ProductoTerminadoUpdate, ProductoTerminadoResponse
from app.repositories.producto_terminado_repository import ProductoTerminadoRepository
from app.services.jwt_service import get_current_user, require_role

router = APIRouter(prefix="/api/ProductosTerminados", tags=["Productos Terminados"])

@router.get("", response_model=List[ProductoTerminadoResponse])
async def get_all_productos_terminados(
    db: Session = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    repo = ProductoTerminadoRepository(db)
    return repo.get_all()

@router.get("/{id}", response_model=ProductoTerminadoResponse)
async def get_producto_terminado_by_id(
    id: int,
    db: Session = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    repo = ProductoTerminadoRepository(db)
    producto = repo.get_by_id(id)
    if not producto:
        raise HTTPException(status_code=404, detail="Producto terminado no encontrado")
    return producto

@router.post("", response_model=ProductoTerminadoResponse, status_code=status.HTTP_201_CREATED)
async def create_producto_terminado(
    producto: ProductoTerminadoCreate,
    db: Session = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador", "Supervisor"]))
):
    repo = ProductoTerminadoRepository(db)
    return repo.create(producto.model_dump())

@router.put("/{id}", response_model=ProductoTerminadoResponse)
async def update_producto_terminado(
    id: int,
    producto: ProductoTerminadoUpdate,
    db: Session = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador", "Supervisor"]))
):
    repo = ProductoTerminadoRepository(db)
    updated = repo.update(id, producto.model_dump())
    if not updated:
        raise HTTPException(status_code=404, detail="Producto terminado no encontrado")
    return updated

@router.delete("/{id}")
async def delete_producto_terminado(
    id: int,
    db: Session = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador"]))
):
    repo = ProductoTerminadoRepository(db)
    deleted = repo.delete(id)
    if not deleted:
        raise HTTPException(status_code=404, detail="Producto terminado no encontrado")
    return {"message": "Producto terminado eliminado correctamente"}
