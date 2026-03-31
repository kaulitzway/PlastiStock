from fastapi import APIRouter, Depends, HTTPException, status
from sqlalchemy.ext.asyncio import AsyncSession
from typing import List
from app.database import get_db
from app.schemas.schemas import UsuarioCreate, UsuarioUpdate, UsuarioResponse
from app.repositories.usuario_repository import UsuarioRepository
from app.services.jwt_service import get_current_user, require_role

router = APIRouter(prefix="/api/Usuarios", tags=["Usuarios"])

@router.get("", response_model=List[UsuarioResponse])
async def get_all_usuarios(
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    return await UsuarioRepository(db).get_all()

@router.get("/{id}", response_model=UsuarioResponse)
async def get_usuario_by_id(
    id: int,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    usuario = await UsuarioRepository(db).get_by_id(id)
    if not usuario:
        raise HTTPException(status_code=404, detail="Usuario no encontrado")
    return usuario

@router.post("/crear", response_model=UsuarioResponse, status_code=status.HTTP_201_CREATED)
async def create_usuario(
    usuario: UsuarioCreate,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador"]))
):
    return await UsuarioRepository(db).create(usuario.model_dump())

@router.put("/{id}", response_model=UsuarioResponse)
async def update_usuario(
    id: int,
    usuario: UsuarioUpdate,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador"]))
):
    updated = await UsuarioRepository(db).update(id, usuario.model_dump(exclude_unset=True))
    if not updated:
        raise HTTPException(status_code=404, detail="Usuario no encontrado")
    return updated

@router.delete("/{id}")
async def delete_usuario(
    id: int,
    db: AsyncSession = Depends(get_db),
    current_user: dict = Depends(require_role(["Administrador"]))
):
    deleted = await UsuarioRepository(db).delete(id)
    if not deleted:
        raise HTTPException(status_code=404, detail="Usuario no encontrado")
    return {"message": "Usuario eliminado correctamente"}
