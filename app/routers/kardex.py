from fastapi import APIRouter, Depends, HTTPException
from sqlalchemy.ext.asyncio import AsyncSession
from typing import List
from app.database import get_db
from app.schemas.schemas import KardexResponse
from app.repositories.kardex_repository import KardexRepository
from app.services.jwt_service import get_current_user

router = APIRouter(prefix="/api/Kardex", tags=["Kardex"])

@router.get("", response_model=List[KardexResponse])
async def get_all(
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    return await KardexRepository(db).get_all()

@router.get("/producto/{producto_id}", response_model=List[KardexResponse])
async def get_by_producto(
    producto_id: int,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    return await KardexRepository(db).get_by_producto(producto_id)

@router.get("/{id}", response_model=KardexResponse)
async def get_by_id(
    id: int,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    kardex = await KardexRepository(db).get_by_id(id)
    if not kardex:
        raise HTTPException(status_code=404, detail="Registro no encontrado")
    return kardex
