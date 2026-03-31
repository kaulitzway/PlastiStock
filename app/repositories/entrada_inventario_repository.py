from sqlalchemy.ext.asyncio import AsyncSession
from sqlalchemy import select
from app.models.models import EntradaInventario, Producto, Kardex
from typing import List, Optional

class EntradaInventarioRepository:
    def __init__(self, db: AsyncSession):
        self.db = db

    async def get_all(self) -> List[EntradaInventario]:
        result = await self.db.execute(select(EntradaInventario).order_by(EntradaInventario.Fecha.desc()))
        return result.scalars().all()

    async def get_by_id(self, entrada_id: int) -> Optional[EntradaInventario]:
        result = await self.db.execute(select(EntradaInventario).where(EntradaInventario.Id == entrada_id))
        return result.scalar_one_or_none()

    async def create(self, entrada_data: dict) -> EntradaInventario:
        entrada = EntradaInventario(**entrada_data)
        self.db.add(entrada)

        result = await self.db.execute(select(Producto).where(Producto.Id == entrada_data["ProductoId"]))
        producto = result.scalar_one_or_none()
        if producto:
            producto.Stock += entrada_data["Cantidad"]
            kardex = Kardex(
                ProductoId=entrada_data["ProductoId"],
                TipoMovimiento="Entrada",
                Cantidad=entrada_data["Cantidad"]
            )
            self.db.add(kardex)

        await self.db.commit()
        await self.db.refresh(entrada)
        return entrada

    async def delete(self, entrada_id: int) -> bool:
        entrada = await self.get_by_id(entrada_id)
        if not entrada:
            return False
        await self.db.delete(entrada)
        await self.db.commit()
        return True
