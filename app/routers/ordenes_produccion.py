from fastapi import APIRouter, Depends, HTTPException, status
from sqlalchemy.orm import Session
from typing import List
from app.database import get_db
from app.schemas.schemas import OrdenProduccionCreate, OrdenProduccionUpdate, OrdenProduccionResponse
from app.repositories.orden_produccion_repository import OrdenProduccionRepository
from app.services.jwt_service import get_current_user, require_role

router = APIRouter(prefix="/api/OrdenesProduccion", tags=["Órdenes de Producción"])

@router.get("", response_model=List[OrdenProduccionResponse])
async def get_all_ordenes(
    db: Session = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    repo = OrdenProduccionRepository(db)
    return repo.get_all()

@router.get("/{id}", response_model=OrdenProduccionResponse)
async def get_orden_by_id(
    id: int,
    db: Session = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    repo = OrdenProduccionRepository(db)
    orden = repo.get_by_id(id)
    if not orden:
        raise HTTPException(status_code=404, detail="Orden no encontrada")
    return orden

@router.post("", response_model=OrdenProduccionResponse, status_code=status.HTTP_201_CREATED)
async def create_orden(
    orden: OrdenProduccionCreate,
    db: Session = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador", "Supervisor"]))
):
    repo = OrdenProduccionRepository(db)
    return repo.create(orden.model_dump())

@router.put("/{id}", response_model=OrdenProduccionResponse)
async def update_orden(
    id: int,
    orden: OrdenProduccionUpdate,
    db: Session = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador", "Supervisor"]))
):
    repo = OrdenProduccionRepository(db)
    updated = repo.update(id, orden.model_dump())
    if not updated:
        raise HTTPException(status_code=404, detail="Orden no encontrada")
    return updated

@router.delete("/{id}")
async def delete_orden(
    id: int,
    db: Session = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador"]))
):
    repo = OrdenProduccionRepository(db)
    deleted = repo.delete(id)
    if not deleted:
        raise HTTPException(status_code=404, detail="Orden no encontrada")
    return {"message": "Orden eliminada correctamente"}
