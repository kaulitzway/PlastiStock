from fastapi import APIRouter, Depends, HTTPException, status
from sqlalchemy.ext.asyncio import AsyncSession
from typing import List
from app.database import get_db
from app.schemas.schemas import MateriaPrimaCreate, MateriaPrimaResponse
from app.repositories.materia_prima_repository import MateriaPrimaRepository
from app.services.jwt_service import get_current_user, require_role

router = APIRouter(prefix="/api/MateriasPrimas", tags=["Materias Primas"])

@router.get("", response_model=List[MateriaPrimaResponse])
async def get_all_materias(
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    return await MateriaPrimaRepository(db).get_all()

@router.get("/{id}", response_model=MateriaPrimaResponse)
async def get_materia_by_id(
    id: int,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    materia = await MateriaPrimaRepository(db).get_by_id(id)
    if not materia:
        raise HTTPException(status_code=404, detail="Materia prima no encontrada")
    return materia

@router.post("", response_model=MateriaPrimaResponse, status_code=status.HTTP_201_CREATED)
async def create_materia(
    materia: MateriaPrimaCreate,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador", "Supervisor"]))
):
    return await MateriaPrimaRepository(db).create(materia.model_dump())

@router.put("/{id}", response_model=MateriaPrimaResponse)
async def update_materia(
    id: int,
    materia: MateriaPrimaCreate,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador", "Supervisor"]))
):
    updated = await MateriaPrimaRepository(db).update(id, materia.model_dump())
    if not updated:
        raise HTTPException(status_code=404, detail="Materia prima no encontrada")
    return updated

@router.delete("/{id}")
async def delete_materia(
    id: int,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador"]))
):
    deleted = await MateriaPrimaRepository(db).delete(id)
    if not deleted:
        raise HTTPException(status_code=404, detail="Materia prima no encontrada")
    return {"message": "Materia prima eliminada correctamente"}
