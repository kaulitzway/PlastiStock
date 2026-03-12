from fastapi import APIRouter, Depends, HTTPException, status
from sqlalchemy.orm import Session
from typing import List
from app.database import get_db
from app.schemas.schemas import SalidaInventarioCreate, SalidaInventarioResponse
from app.repositories.salida_inventario_repository import SalidaInventarioRepository
from app.services.jwt_service import get_current_user, require_role

router = APIRouter(prefix="/api/SalidasInventario", tags=["Salidas de Inventario"])

@router.get("", response_model=List[SalidaInventarioResponse])
async def get_all_salidas(
    db: Session = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    repo = SalidaInventarioRepository(db)
    return repo.get_all()

@router.get("/{id}", response_model=SalidaInventarioResponse)
async def get_salida_by_id(
    id: int,
    db: Session = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    repo = SalidaInventarioRepository(db)
    salida = repo.get_by_id(id)
    if not salida:
        raise HTTPException(status_code=404, detail="Salida no encontrada")
    return salida

@router.post("", response_model=SalidaInventarioResponse, status_code=status.HTTP_201_CREATED)
async def create_salida(
    salida: SalidaInventarioCreate,
    db: Session = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador", "Supervisor"]))
):
    repo = SalidaInventarioRepository(db)
    try:
        return repo.create(salida.model_dump())
    except ValueError as e:
        raise HTTPException(status_code=400, detail=str(e))

@router.delete("/{id}")
async def delete_salida(
    id: int,
    db: Session = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador"]))
):
    repo = SalidaInventarioRepository(db)
    deleted = repo.delete(id)
    if not deleted:
        raise HTTPException(status_code=404, detail="Salida no encontrada")
    return {"message": "Salida eliminada correctamente"}
