from sqlalchemy.orm import Session
from app.models.models import ProductoEnProceso
from typing import List, Optional

class ProductoEnProcesoRepository:
    def __init__(self, db: Session):
        self.db = db
    
    def get_all(self) -> List[ProductoEnProceso]:
        return self.db.query(ProductoEnProceso).all()
    
    def get_by_id(self, producto_id: int) -> Optional[ProductoEnProceso]:
        return self.db.query(ProductoEnProceso).filter(ProductoEnProceso.Id == producto_id).first()
    
    def create(self, producto_data: dict) -> ProductoEnProceso:
        producto = ProductoEnProceso(**producto_data)
        self.db.add(producto)
        self.db.commit()
        self.db.refresh(producto)
        return producto
    
    def update(self, producto_id: int, producto_data: dict) -> Optional[ProductoEnProceso]:
        producto = self.get_by_id(producto_id)
        if not producto:
            return None
        
        for key, value in producto_data.items():
            setattr(producto, key, value)
        
        self.db.commit()
        self.db.refresh(producto)
        return producto
    
    def delete(self, producto_id: int) -> bool:
        producto = self.get_by_id(producto_id)
        if not producto:
            return False
        
        self.db.delete(producto)
        self.db.commit()
        return True
