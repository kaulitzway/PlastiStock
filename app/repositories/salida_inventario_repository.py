from sqlalchemy.ext.asyncio import AsyncSession
from sqlalchemy import select
from app.models.models import SalidaInventario, Producto, Kardex
from typing import List, Optional

class SalidaInventarioRepository:
    def __init__(self, db: AsyncSession):
        self.db = db

    async def get_all(self) -> List[SalidaInventario]:
        result = await self.db.execute(select(SalidaInventario).order_by(SalidaInventario.Fecha.desc()))
        return result.scalars().all()

    async def get_by_id(self, salida_id: int) -> Optional[SalidaInventario]:
        result = await self.db.execute(select(SalidaInventario).where(SalidaInventario.Id == salida_id))
        return result.scalar_one_or_none()

    async def create(self, salida_data: dict) -> SalidaInventario:
        result = await self.db.execute(select(Producto).where(Producto.Id == salida_data["ProductoId"]))
        producto = result.scalar_one_or_none()
        if not producto or producto.Stock < salida_data["Cantidad"]:
            raise ValueError("Stock insuficiente")

        salida = SalidaInventario(**salida_data)
        self.db.add(salida)
        producto.Stock -= salida_data["Cantidad"]
        kardex = Kardex(
            ProductoId=salida_data["ProductoId"],
            TipoMovimiento="Salida",
            Cantidad=salida_data["Cantidad"]
        )
        self.db.add(kardex)

        await self.db.commit()
        await self.db.refresh(salida)
        return salida

    async def delete(self, salida_id: int) -> bool:
        salida = await self.get_by_id(salida_id)
        if not salida:
            return False
        await self.db.delete(salida)
        await self.db.commit()
        return True
