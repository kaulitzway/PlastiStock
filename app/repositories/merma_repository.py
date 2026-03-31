from sqlalchemy.ext.asyncio import AsyncSession
from sqlalchemy import select
from app.models.models import Merma, Producto, Kardex
from typing import List, Optional

class MermaRepository:
    def __init__(self, db: AsyncSession):
        self.db = db

    async def get_all(self) -> List[Merma]:
        result = await self.db.execute(select(Merma).order_by(Merma.Fecha.desc()))
        return result.scalars().all()

    async def get_by_id(self, merma_id: int) -> Optional[Merma]:
        result = await self.db.execute(select(Merma).where(Merma.Id == merma_id))
        return result.scalar_one_or_none()

    async def get_by_producto(self, producto_id: int) -> List[Merma]:
        result = await self.db.execute(select(Merma).where(Merma.ProductoId == producto_id))
        return result.scalars().all()

    async def create(self, merma_data: dict) -> Merma:
        merma = Merma(**merma_data)
        self.db.add(merma)

        result = await self.db.execute(select(Producto).where(Producto.Id == merma_data["ProductoId"]))
        producto = result.scalar_one_or_none()
        if producto:
            producto.Stock -= merma_data["Cantidad"]
            kardex = Kardex(
                ProductoId=merma_data["ProductoId"],
                TipoMovimiento="Merma",
                Cantidad=merma_data["Cantidad"]
            )
            self.db.add(kardex)

        await self.db.commit()
        await self.db.refresh(merma)
        return merma

    async def delete(self, merma_id: int) -> bool:
        merma = await self.get_by_id(merma_id)
        if not merma:
            return False
        await self.db.delete(merma)
        await self.db.commit()
        return True
