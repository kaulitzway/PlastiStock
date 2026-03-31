from sqlalchemy.ext.asyncio import AsyncSession
from sqlalchemy import select
from app.models.models import Categoria
from typing import List, Optional

class CategoriaRepository:
    def __init__(self, db: AsyncSession):
        self.db = db

    async def get_all(self) -> List[Categoria]:
        result = await self.db.execute(select(Categoria))
        return result.scalars().all()

    async def get_by_id(self, categoria_id: int) -> Optional[Categoria]:
        result = await self.db.execute(select(Categoria).where(Categoria.Id == categoria_id))
        return result.scalar_one_or_none()

    async def create(self, categoria_data: dict) -> Categoria:
        categoria = Categoria(**categoria_data)
        self.db.add(categoria)
        await self.db.commit()
        await self.db.refresh(categoria)
        return categoria

    async def update(self, categoria_id: int, categoria_data: dict) -> Optional[Categoria]:
        categoria = await self.get_by_id(categoria_id)
        if not categoria:
            return None
        for key, value in categoria_data.items():
            setattr(categoria, key, value)
        await self.db.commit()
        await self.db.refresh(categoria)
        return categoria

    async def delete(self, categoria_id: int) -> bool:
        categoria = await self.get_by_id(categoria_id)
        if not categoria:
            return False
        await self.db.delete(categoria)
        await self.db.commit()
        return True
