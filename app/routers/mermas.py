from fastapi import APIRouter, Depends, HTTPException, status
from sqlalchemy.orm import Session
from typing import List
from app.database import get_db
from app.schemas.schemas import MermaCreate, MermaResponse
from app.repositories.merma_repository import MermaRepository
from app.services.jwt_service import get_current_user, require_role

router = APIRouter(prefix="/api/Mermas", tags=["Mermas"])

@router.get("", response_model=List[MermaResponse])
async def get_all_mermas(
    db: Session = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    repo = MermaRepository(db)
    return repo.get_all()

@router.get("/{id}", response_model=MermaResponse)
async def get_merma_by_id(
    id: int,
    db: Session = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    repo = MermaRepository(db)
    merma = repo.get_by_id(id)
    if not merma:
        raise HTTPException(status_code=404, detail="Merma no encontrada")
    return merma

@router.get("/producto/{producto_id}", response_model=List[MermaResponse])
async def get_mermas_by_producto(
    producto_id: int,
    db: Session = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    repo = MermaRepository(db)
    return repo.get_by_producto(producto_id)

@router.post("", response_model=MermaResponse, status_code=status.HTTP_201_CREATED)
async def create_merma(
    merma: MermaCreate,
    db: Session = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador", "Supervisor"]))
):
    repo = MermaRepository(db)
    return repo.create(merma.model_dump())

@router.delete("/{id}")
async def delete_merma(
    id: int,
    db: Session = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador"]))
):
    repo = MermaRepository(db)
    deleted = repo.delete(id)
    if not deleted:
        raise HTTPException(status_code=404, detail="Merma no encontrada")
    return {"message": "Merma eliminada correctamente"}
