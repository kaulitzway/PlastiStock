from sqlalchemy.ext.asyncio import AsyncSession
from sqlalchemy import select, or_
from app.models.models import Solicitud
from typing import List, Optional

class SolicitudRepository:
    def __init__(self, db: AsyncSession):
        self.db = db

    async def get_all(self) -> List[Solicitud]:
        result = await self.db.execute(select(Solicitud))
        return result.scalars().all()

    async def get_by_id(self, solicitud_id: int) -> Optional[Solicitud]:
        result = await self.db.execute(select(Solicitud).where(Solicitud.Id == solicitud_id))
        return result.scalar_one_or_none()

    async def get_by_usuario(self, usuario_id: int) -> List[Solicitud]:
        result = await self.db.execute(
            select(Solicitud).where(
                or_(Solicitud.UsuarioSolicitanteId == usuario_id, Solicitud.UsuarioAfectadoId == usuario_id)
            )
        )
        return result.scalars().all()

    async def get_by_estado(self, estado: str) -> List[Solicitud]:
        result = await self.db.execute(select(Solicitud).where(Solicitud.Estado == estado))
        return result.scalars().all()

    async def create(self, solicitud_data: dict) -> Solicitud:
        solicitud = Solicitud(**solicitud_data)
        self.db.add(solicitud)
        await self.db.commit()
        await self.db.refresh(solicitud)
        return solicitud

    async def update(self, solicitud_id: int, solicitud_data: dict) -> Optional[Solicitud]:
        solicitud = await self.get_by_id(solicitud_id)
        if not solicitud:
            return None
        for key, value in solicitud_data.items():
            setattr(solicitud, key, value)
        await self.db.commit()
        await self.db.refresh(solicitud)
        return solicitud

    async def delete(self, solicitud_id: int) -> bool:
        solicitud = await self.get_by_id(solicitud_id)
        if not solicitud:
            return False
        await self.db.delete(solicitud)
        await self.db.commit()
        return True
