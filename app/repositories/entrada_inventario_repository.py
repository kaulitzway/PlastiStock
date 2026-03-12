from sqlalchemy.orm import Session
from app.models.models import EntradaInventario, Producto, Kardex
from typing import List, Optional

class EntradaInventarioRepository:
    def __init__(self, db: Session):
        self.db = db
    
    def get_all(self) -> List[EntradaInventario]:
        return self.db.query(EntradaInventario).order_by(EntradaInventario.Fecha.desc()).all()
    
    def get_by_id(self, entrada_id: int) -> Optional[EntradaInventario]:
        return self.db.query(EntradaInventario).filter(EntradaInventario.Id == entrada_id).first()
    
    def create(self, entrada_data: dict) -> EntradaInventario:
        entrada = EntradaInventario(**entrada_data)
        self.db.add(entrada)
        
        # Actualizar stock del producto
        producto = self.db.query(Producto).filter(Producto.Id == entrada_data["ProductoId"]).first()
        if producto:
            producto.Stock += entrada_data["Cantidad"]
            
            # Registrar en Kardex
            kardex = Kardex(
                ProductoId=entrada_data["ProductoId"],
                TipoMovimiento="Entrada",
                Cantidad=entrada_data["Cantidad"]
            )
            self.db.add(kardex)
        
        self.db.commit()
        self.db.refresh(entrada)
        return entrada
    
    def delete(self, entrada_id: int) -> bool:
        entrada = self.get_by_id(entrada_id)
        if not entrada:
            return False
        
        self.db.delete(entrada)
        self.db.commit()
        return True
