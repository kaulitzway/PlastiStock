from sqlalchemy.orm import Session
from app.models.models import SalidaInventario, Producto, Kardex
from typing import List, Optional

class SalidaInventarioRepository:
    def __init__(self, db: Session):
        self.db = db
    
    def get_all(self) -> List[SalidaInventario]:
        return self.db.query(SalidaInventario).order_by(SalidaInventario.Fecha.desc()).all()
    
    def get_by_id(self, salida_id: int) -> Optional[SalidaInventario]:
        return self.db.query(SalidaInventario).filter(SalidaInventario.Id == salida_id).first()
    
    def create(self, salida_data: dict) -> SalidaInventario:
        # Verificar stock disponible
        producto = self.db.query(Producto).filter(Producto.Id == salida_data["ProductoId"]).first()
        if not producto or producto.Stock < salida_data["Cantidad"]:
            raise ValueError("Stock insuficiente")
        
        salida = SalidaInventario(**salida_data)
        self.db.add(salida)
        
        # Actualizar stock del producto
        producto.Stock -= salida_data["Cantidad"]
        
        # Registrar en Kardex
        kardex = Kardex(
            ProductoId=salida_data["ProductoId"],
            TipoMovimiento="Salida",
            Cantidad=salida_data["Cantidad"]
        )
        self.db.add(kardex)
        
        self.db.commit()
        self.db.refresh(salida)
        return salida
    
    def delete(self, salida_id: int) -> bool:
        salida = self.get_by_id(salida_id)
        if not salida:
            return False
        
        self.db.delete(salida)
        self.db.commit()
        return True
