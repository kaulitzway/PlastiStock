from sqlalchemy.ext.asyncio import AsyncSession
from sqlalchemy import select
from app.models.models import MateriaPrima
from typing import List, Optional

class MateriaPrimaRepository:
    def __init__(self, db: AsyncSession):
        self.db = db

    async def get_all(self) -> List[MateriaPrima]:
        result = await self.db.execute(select(MateriaPrima))
        return result.scalars().all()

    async def get_by_id(self, materia_id: int) -> Optional[MateriaPrima]:
        result = await self.db.execute(select(MateriaPrima).where(MateriaPrima.Id == materia_id))
        return result.scalar_one_or_none()

    async def create(self, materia_data: dict) -> MateriaPrima:
        materia = MateriaPrima(**materia_data)
        self.db.add(materia)
        await self.db.commit()
        await self.db.refresh(materia)
        return materia

    async def update(self, materia_id: int, materia_data: dict) -> Optional[MateriaPrima]:
        materia = await self.get_by_id(materia_id)
        if not materia:
            return None
        for key, value in materia_data.items():
            setattr(materia, key, value)
        await self.db.commit()
        await self.db.refresh(materia)
        return materia

    async def delete(self, materia_id: int) -> bool:
        materia = await self.get_by_id(materia_id)
        if not materia:
            return False
        await self.db.delete(materia)
        await self.db.commit()
        return True
