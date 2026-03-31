from sqlalchemy.ext.asyncio import AsyncSession
from sqlalchemy import select
from app.models.models import Kardex
from typing import List, Optional

class KardexRepository:
    def __init__(self, db: AsyncSession):
        self.db = db

    async def get_all(self) -> List[Kardex]:
        result = await self.db.execute(select(Kardex).order_by(Kardex.Fecha.desc()))
        return result.scalars().all()

    async def get_by_id(self, kardex_id: int) -> Optional[Kardex]:
        result = await self.db.execute(select(Kardex).where(Kardex.Id == kardex_id))
        return result.scalar_one_or_none()

    async def get_by_producto(self, producto_id: int) -> List[Kardex]:
        result = await self.db.execute(
            select(Kardex).where(Kardex.ProductoId == producto_id).order_by(Kardex.Fecha.desc())
        )
        return result.scalars().all()

    async def create(self, kardex_data: dict) -> Kardex:
        kardex = Kardex(**kardex_data)
        self.db.add(kardex)
        await self.db.commit()
        await self.db.refresh(kardex)
        return kardex

    async def delete(self, kardex_id: int) -> bool:
        kardex = await self.get_by_id(kardex_id)
        if not kardex:
            return False
        await self.db.delete(kardex)
        await self.db.commit()
        return True
