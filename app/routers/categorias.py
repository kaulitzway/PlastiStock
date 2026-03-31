from fastapi import APIRouter, Depends, HTTPException, status
from sqlalchemy.ext.asyncio import AsyncSession
from typing import List
from app.database import get_db
from app.schemas.schemas import CategoriaCreate, CategoriaResponse
from app.repositories.categoria_repository import CategoriaRepository
from app.services.jwt_service import get_current_user, require_role

router = APIRouter(prefix="/api/Categorias", tags=["Categorías"])

@router.get("", response_model=List[CategoriaResponse])
async def get_all_categorias(
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    return await CategoriaRepository(db).get_all()

@router.get("/{id}", response_model=CategoriaResponse)
async def get_categoria_by_id(
    id: int,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    categoria = await CategoriaRepository(db).get_by_id(id)
    if not categoria:
        raise HTTPException(status_code=404, detail="Categoría no encontrada")
    return categoria

@router.post("", response_model=CategoriaResponse, status_code=status.HTTP_201_CREATED)
async def create_categoria(
    categoria: CategoriaCreate,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador"]))
):
    return await CategoriaRepository(db).create(categoria.model_dump())

@router.put("/{id}", response_model=CategoriaResponse)
async def update_categoria(
    id: int,
    categoria: CategoriaCreate,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador"]))
):
    updated = await CategoriaRepository(db).update(id, categoria.model_dump())
    if not updated:
        raise HTTPException(status_code=404, detail="Categoría no encontrada")
    return updated

@router.delete("/{id}")
async def delete_categoria(
    id: int,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador"]))
):
    deleted = await CategoriaRepository(db).delete(id)
    if not deleted:
        raise HTTPException(status_code=404, detail="Categoría no encontrada")
    return {"message": "Categoría eliminada correctamente"}
