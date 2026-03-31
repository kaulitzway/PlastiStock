from sqlalchemy.ext.asyncio import AsyncSession
from sqlalchemy import select
from app.models.models import ProductoEnProceso
from typing import List, Optional

class ProductoEnProcesoRepository:
    def __init__(self, db: AsyncSession):
        self.db = db

    async def get_all(self) -> List[ProductoEnProceso]:
        result = await self.db.execute(select(ProductoEnProceso))
        return result.scalars().all()

    async def get_by_id(self, producto_id: int) -> Optional[ProductoEnProceso]:
        result = await self.db.execute(select(ProductoEnProceso).where(ProductoEnProceso.Id == producto_id))
        return result.scalar_one_or_none()

    async def create(self, producto_data: dict) -> ProductoEnProceso:
        producto = ProductoEnProceso(**producto_data)
        self.db.add(producto)
        await self.db.commit()
        await self.db.refresh(producto)
        return producto

    async def update(self, producto_id: int, producto_data: dict) -> Optional[ProductoEnProceso]:
        producto = await self.get_by_id(producto_id)
        if not producto:
            return None
        for key, value in producto_data.items():
            setattr(producto, key, value)
        await self.db.commit()
        await self.db.refresh(producto)
        return producto

    async def delete(self, producto_id: int) -> bool:
        producto = await self.get_by_id(producto_id)
        if not producto:
            return False
        await self.db.delete(producto)
        await self.db.commit()
        return True
