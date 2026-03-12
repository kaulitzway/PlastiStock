from fastapi import APIRouter, Depends, HTTPException, status
from sqlalchemy.orm import Session
from typing import List
from app.database import get_db
from app.schemas.schemas import ProveedorCreate, ProveedorUpdate, ProveedorResponse
from app.repositories.proveedor_repository import ProveedorRepository
from app.services.jwt_service import get_current_user, require_role

router = APIRouter(prefix="/api/Proveedores", tags=["Proveedores"])

@router.get("", response_model=List[ProveedorResponse])
async def get_all_proveedores(
    db: Session = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    repo = ProveedorRepository(db)
    return repo.get_all()

@router.get("/{id}", response_model=ProveedorResponse)
async def get_proveedor_by_id(
    id: int,
    db: Session = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    repo = ProveedorRepository(db)
    proveedor = repo.get_by_id(id)
    if not proveedor:
        raise HTTPException(status_code=404, detail="Proveedor no encontrado")
    return proveedor

@router.post("", response_model=ProveedorResponse, status_code=status.HTTP_201_CREATED)
async def create_proveedor(
    proveedor: ProveedorCreate,
    db: Session = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador", "Supervisor"]))
):
    repo = ProveedorRepository(db)
    return repo.create(proveedor.model_dump())

@router.put("/{id}", response_model=ProveedorResponse)
async def update_proveedor(
    id: int,
    proveedor: ProveedorUpdate,
    db: Session = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador", "Supervisor"]))
):
    repo = ProveedorRepository(db)
    updated = repo.update(id, proveedor.model_dump())
    if not updated:
        raise HTTPException(status_code=404, detail="Proveedor no encontrado")
    return updated

@router.delete("/{id}")
async def delete_proveedor(
    id: int,
    db: Session = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador"]))
):
    repo = ProveedorRepository(db)
    deleted = repo.delete(id)
    if not deleted:
        raise HTTPException(status_code=404, detail="Proveedor no encontrado")
    return {"message": "Proveedor eliminado correctamente"}
