from fastapi import APIRouter, Depends, HTTPException, status
from sqlalchemy.orm import Session
from typing import List
from app.database import get_db
from app.schemas.schemas import SolicitudCreate, SolicitudUpdate, SolicitudResponse
from app.repositories.solicitud_repository import SolicitudRepository
from app.services.jwt_service import get_current_user, require_role

router = APIRouter(prefix="/api/Solicitudes", tags=["Solicitudes"])

@router.get("", response_model=List[SolicitudResponse])
async def get_all_solicitudes(
    db: Session = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    repo = SolicitudRepository(db)
    return repo.get_all()

@router.get("/{id}", response_model=SolicitudResponse)
async def get_solicitud_by_id(
    id: int,
    db: Session = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    repo = SolicitudRepository(db)
    solicitud = repo.get_by_id(id)
    if not solicitud:
        raise HTTPException(status_code=404, detail="Solicitud no encontrada")
    return solicitud

@router.get("/usuario/{usuario_id}", response_model=List[SolicitudResponse])
async def get_solicitudes_by_usuario(
    usuario_id: int,
    db: Session = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    repo = SolicitudRepository(db)
    return repo.get_by_usuario(usuario_id)

@router.post("", response_model=SolicitudResponse, status_code=status.HTTP_201_CREATED)
async def create_solicitud(
    solicitud: SolicitudCreate,
    db: Session = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    repo = SolicitudRepository(db)
    return repo.create(solicitud.model_dump())

@router.put("/{id}", response_model=SolicitudResponse)
async def update_solicitud(
    id: int,
    solicitud: SolicitudUpdate,
    db: Session = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador"]))
):
    repo = SolicitudRepository(db)
    updated = repo.update(id, solicitud.model_dump())
    if not updated:
        raise HTTPException(status_code=404, detail="Solicitud no encontrada")
    return updated

@router.delete("/{id}")
async def delete_solicitud(
    id: int,
    db: Session = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador"]))
):
    repo = SolicitudRepository(db)
    deleted = repo.delete(id)
    if not deleted:
        raise HTTPException(status_code=404, detail="Solicitud no encontrada")
    return {"message": "Solicitud eliminada correctamente"}
