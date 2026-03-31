from sqlalchemy.ext.asyncio import AsyncSession
from sqlalchemy import select
from app.models.models import OrdenProduccion
from typing import List, Optional

class OrdenProduccionRepository:
    def __init__(self, db: AsyncSession):
        self.db = db

    async def get_all(self) -> List[OrdenProduccion]:
        result = await self.db.execute(select(OrdenProduccion).order_by(OrdenProduccion.FechaCreacion.desc()))
        return result.scalars().all()

    async def get_by_id(self, orden_id: int) -> Optional[OrdenProduccion]:
        result = await self.db.execute(select(OrdenProduccion).where(OrdenProduccion.Id == orden_id))
        return result.scalar_one_or_none()

    async def get_by_estado(self, estado: str) -> List[OrdenProduccion]:
        result = await self.db.execute(select(OrdenProduccion).where(OrdenProduccion.Estado == estado))
        return result.scalars().all()

    async def create(self, orden_data: dict) -> OrdenProduccion:
        orden = OrdenProduccion(**orden_data)
        self.db.add(orden)
        await self.db.commit()
        await self.db.refresh(orden)
        return orden

    async def update(self, orden_id: int, orden_data: dict) -> Optional[OrdenProduccion]:
        orden = await self.get_by_id(orden_id)
        if not orden:
            return None
        for key, value in orden_data.items():
            setattr(orden, key, value)
        await self.db.commit()
        await self.db.refresh(orden)
        return orden

    async def delete(self, orden_id: int) -> bool:
        orden = await self.get_by_id(orden_id)
        if not orden:
            return False
        await self.db.delete(orden)
        await self.db.commit()
        return True
