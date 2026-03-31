from fastapi import APIRouter, Depends, HTTPException, status
from sqlalchemy.ext.asyncio import AsyncSession
from typing import List
from app.database import get_db
from app.schemas.schemas import MermaCreate, MermaResponse
from app.repositories.merma_repository import MermaRepository
from app.services.jwt_service import get_current_user, require_role

router = APIRouter(prefix="/api/Mermas", tags=["Mermas"])

@router.get("", response_model=List[MermaResponse])
async def get_all(
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    return await MermaRepository(db).get_all()

@router.get("/producto/{producto_id}", response_model=List[MermaResponse])
async def get_by_producto(
    producto_id: int,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    return await MermaRepository(db).get_by_producto(producto_id)

@router.get("/{id}", response_model=MermaResponse)
async def get_by_id(
    id: int,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    merma = await MermaRepository(db).get_by_id(id)
    if not merma:
        raise HTTPException(status_code=404, detail="Merma no encontrada")
    return merma

@router.post("", response_model=MermaResponse, status_code=status.HTTP_201_CREATED)
async def create(
    merma: MermaCreate,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador", "Supervisor"]))
):
    return await MermaRepository(db).create(merma.model_dump())

@router.delete("/{id}")
async def delete(
    id: int,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador"]))
):
    deleted = await MermaRepository(db).delete(id)
    if not deleted:
        raise HTTPException(status_code=404, detail="Merma no encontrada")
    return {"message": "Merma eliminada correctamente"}
