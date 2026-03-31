from sqlalchemy.ext.asyncio import AsyncSession
from sqlalchemy import select
from app.models.models import Usuario
from app.services.jwt_service import hash_password
from typing import List, Optional

class UsuarioRepository:
    def __init__(self, db: AsyncSession):
        self.db = db

    async def get_all(self) -> List[Usuario]:
        result = await self.db.execute(select(Usuario))
        return result.scalars().all()

    async def get_by_id(self, usuario_id: int) -> Optional[Usuario]:
        result = await self.db.execute(select(Usuario).where(Usuario.Id == usuario_id))
        return result.scalar_one_or_none()

    async def get_by_email(self, email: str) -> Optional[Usuario]:
        result = await self.db.execute(select(Usuario).where(Usuario.Correo == email))
        return result.scalar_one_or_none()

    async def create(self, usuario_data: dict) -> Usuario:
        usuario_data["Contraseña"] = hash_password(usuario_data["Contraseña"])
        usuario = Usuario(**usuario_data)
        self.db.add(usuario)
        await self.db.commit()
        await self.db.refresh(usuario)
        return usuario

    async def update(self, usuario_id: int, usuario_data: dict) -> Optional[Usuario]:
        usuario = await self.get_by_id(usuario_id)
        if not usuario:
            return None
        if "Contraseña" in usuario_data and usuario_data["Contraseña"]:
            usuario_data["Contraseña"] = hash_password(usuario_data["Contraseña"])
        for key, value in usuario_data.items():
            if value is not None:
                setattr(usuario, key, value)
        await self.db.commit()
        await self.db.refresh(usuario)
        return usuario

    async def delete(self, usuario_id: int) -> bool:
        usuario = await self.get_by_id(usuario_id)
        if not usuario:
            return False
        await self.db.delete(usuario)
        await self.db.commit()
        return True
