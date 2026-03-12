from fastapi import APIRouter, Depends, HTTPException
from sqlalchemy.orm import Session
from typing import List
from app.database import get_db
from app.schemas.schemas import KardexResponse
from app.repositories.kardex_repository import KardexRepository
from app.services.jwt_service import get_current_user

router = APIRouter(prefix="/api/Kardex", tags=["Kardex"])

@router.get("", response_model=List[KardexResponse])
async def get_all_kardex(
    db: Session = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    repo = KardexRepository(db)
    return repo.get_all()

@router.get("/{id}", response_model=KardexResponse)
async def get_kardex_by_id(
    id: int,
    db: Session = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    repo = KardexRepository(db)
    kardex = repo.get_by_id(id)
    if not kardex:
        raise HTTPException(status_code=404, detail="Registro no encontrado")
    return kardex

@router.get("/producto/{producto_id}", response_model=List[KardexResponse])
async def get_kardex_by_producto(
    producto_id: int,
    db: Session = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    repo = KardexRepository(db)
    return repo.get_by_producto(producto_id)
