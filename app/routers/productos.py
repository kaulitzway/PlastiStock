from fastapi import APIRouter, Depends, HTTPException, status
from sqlalchemy.orm import Session
from typing import List
from app.database import get_db
from app.schemas.schemas import ProductoCreate, ProductoUpdate, ProductoResponse
from app.repositories.producto_repository import ProductoRepository
from app.services.jwt_service import get_current_user, require_role

router = APIRouter(prefix="/api/Productos", tags=["Productos"])

@router.get("", response_model=List[ProductoResponse])
async def get_all_productos(
    db: Session = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    repo = ProductoRepository(db)
    return repo.get_all()

@router.get("/{id}", response_model=ProductoResponse)
async def get_producto_by_id(
    id: int,
    db: Session = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    repo = ProductoRepository(db)
    producto = repo.get_by_id(id)
    if not producto:
        raise HTTPException(status_code=404, detail="Producto no encontrado")
    return producto

@router.post("", response_model=ProductoResponse, status_code=status.HTTP_201_CREATED)
async def create_producto(
    producto: ProductoCreate,
    db: Session = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador", "Supervisor"]))
):
    repo = ProductoRepository(db)
    return repo.create(producto.model_dump())

@router.put("/{id}", response_model=ProductoResponse)
async def update_producto(
    id: int,
    producto: ProductoUpdate,
    db: Session = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador", "Supervisor"]))
):
    repo = ProductoRepository(db)
    updated = repo.update(id, producto.model_dump())
    if not updated:
        raise HTTPException(status_code=404, detail="Producto no encontrado")
    return updated

@router.delete("/{id}")
async def delete_producto(
    id: int,
    db: Session = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador"]))
):
    repo = ProductoRepository(db)
    deleted = repo.delete(id)
    if not deleted:
        raise HTTPException(status_code=404, detail="Producto no encontrado")
    return {"message": "Producto eliminado correctamente"}
