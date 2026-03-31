from fastapi import APIRouter, Depends, HTTPException, status
from sqlalchemy.ext.asyncio import AsyncSession
from typing import List
from app.database import get_db
from app.schemas.schemas import OrdenProduccionCreate, OrdenProduccionUpdate, OrdenProduccionResponse
from app.repositories.orden_produccion_repository import OrdenProduccionRepository
from app.services.jwt_service import get_current_user, require_role

router = APIRouter(prefix="/api/OrdenesProduccion", tags=["Órdenes de Producción"])

@router.get("", response_model=List[OrdenProduccionResponse])
async def get_all(
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    return await OrdenProduccionRepository(db).get_all()

@router.get("/{id}", response_model=OrdenProduccionResponse)
async def get_by_id(
    id: int,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    orden = await OrdenProduccionRepository(db).get_by_id(id)
    if not orden:
        raise HTTPException(status_code=404, detail="Orden no encontrada")
    return orden

@router.post("", response_model=OrdenProduccionResponse, status_code=status.HTTP_201_CREATED)
async def create(
    orden: OrdenProduccionCreate,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador", "Supervisor"]))
):
    return await OrdenProduccionRepository(db).create(orden.model_dump())

@router.put("/{id}", response_model=OrdenProduccionResponse)
async def update(
    id: int,
    orden: OrdenProduccionUpdate,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador", "Supervisor"]))
):
    updated = await OrdenProduccionRepository(db).update(id, orden.model_dump())
    if not updated:
        raise HTTPException(status_code=404, detail="Orden no encontrada")
    return updated

@router.delete("/{id}")
async def delete(
    id: int,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador"]))
):
    deleted = await OrdenProduccionRepository(db).delete(id)
    if not deleted:
        raise HTTPException(status_code=404, detail="Orden no encontrada")
    return {"message": "Orden eliminada correctamente"}
