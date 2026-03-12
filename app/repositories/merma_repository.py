from sqlalchemy.orm import Session
from app.models.models import Merma, Producto, Kardex
from typing import List, Optional

class MermaRepository:
    def __init__(self, db: Session):
        self.db = db
    
    def get_all(self) -> List[Merma]:
        return self.db.query(Merma).order_by(Merma.Fecha.desc()).all()
    
    def get_by_id(self, merma_id: int) -> Optional[Merma]:
        return self.db.query(Merma).filter(Merma.Id == merma_id).first()
    
    def get_by_producto(self, producto_id: int) -> List[Merma]:
        return self.db.query(Merma).filter(Merma.ProductoId == producto_id).all()
    
    def create(self, merma_data: dict) -> Merma:
        merma = Merma(**merma_data)
        self.db.add(merma)
        
        # Actualizar stock del producto (restar merma)
        producto = self.db.query(Producto).filter(Producto.Id == merma_data["ProductoId"]).first()
        if producto:
            producto.Stock -= merma_data["Cantidad"]
            
            # Registrar en Kardex
            kardex = Kardex(
                ProductoId=merma_data["ProductoId"],
                TipoMovimiento="Merma",
                Cantidad=merma_data["Cantidad"]
            )
            self.db.add(kardex)
        
        self.db.commit()
        self.db.refresh(merma)
        return merma
    
    def delete(self, merma_id: int) -> bool:
        merma = self.get_by_id(merma_id)
        if not merma:
            return False
        
        self.db.delete(merma)
        self.db.commit()
        return True
