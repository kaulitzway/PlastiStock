from fastapi import APIRouter, Depends, HTTPException, status
from sqlalchemy.ext.asyncio import AsyncSession
from typing import List
from app.database import get_db
from app.schemas.schemas import SolicitudCreate, SolicitudUpdate, SolicitudResponse
from app.repositories.solicitud_repository import SolicitudRepository
from app.services.jwt_service import get_current_user, require_role

router = APIRouter(prefix="/api/Solicitudes", tags=["Solicitudes"])

@router.get("", response_model=List[SolicitudResponse])
async def get_all(
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    return await SolicitudRepository(db).get_all()

@router.get("/usuario/{usuario_id}", response_model=List[SolicitudResponse])
async def get_by_usuario(
    usuario_id: int,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    return await SolicitudRepository(db).get_by_usuario(usuario_id)

@router.get("/{id}", response_model=SolicitudResponse)
async def get_by_id(
    id: int,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    solicitud = await SolicitudRepository(db).get_by_id(id)
    if not solicitud:
        raise HTTPException(status_code=404, detail="Solicitud no encontrada")
    return solicitud

@router.post("", response_model=SolicitudResponse, status_code=status.HTTP_201_CREATED)
async def create(
    solicitud: SolicitudCreate,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    return await SolicitudRepository(db).create(solicitud.model_dump())

@router.put("/{id}", response_model=SolicitudResponse)
async def update(
    id: int,
    solicitud: SolicitudUpdate,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador"]))
):
    updated = await SolicitudRepository(db).update(id, solicitud.model_dump())
    if not updated:
        raise HTTPException(status_code=404, detail="Solicitud no encontrada")
    return updated

@router.delete("/{id}")
async def delete(
    id: int,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador"]))
):
    deleted = await SolicitudRepository(db).delete(id)
    if not deleted:
        raise HTTPException(status_code=404, detail="Solicitud no encontrada")
    return {"message": "Solicitud eliminada correctamente"}
