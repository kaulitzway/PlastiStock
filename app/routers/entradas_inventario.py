from fastapi import APIRouter, Depends, HTTPException, status
from sqlalchemy.ext.asyncio import AsyncSession
from typing import List
from app.database import get_db
from app.schemas.schemas import EntradaInventarioCreate, EntradaInventarioResponse
from app.repositories.entrada_inventario_repository import EntradaInventarioRepository
from app.services.jwt_service import get_current_user, require_role

router = APIRouter(prefix="/api/EntradasInventario", tags=["Entradas de Inventario"])

@router.get("", response_model=List[EntradaInventarioResponse])
async def get_all(
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    return await EntradaInventarioRepository(db).get_all()

@router.get("/{id}", response_model=EntradaInventarioResponse)
async def get_by_id(
    id: int,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    entrada = await EntradaInventarioRepository(db).get_by_id(id)
    if not entrada:
        raise HTTPException(status_code=404, detail="Entrada no encontrada")
    return entrada

@router.post("", response_model=EntradaInventarioResponse, status_code=status.HTTP_201_CREATED)
async def create(
    entrada: EntradaInventarioCreate,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador", "Supervisor"]))
):
    return await EntradaInventarioRepository(db).create(entrada.model_dump())

@router.delete("/{id}")
async def delete(
    id: int,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador"]))
):
    deleted = await EntradaInventarioRepository(db).delete(id)
    if not deleted:
        raise HTTPException(status_code=404, detail="Entrada no encontrada")
    return {"message": "Entrada eliminada correctamente"}
