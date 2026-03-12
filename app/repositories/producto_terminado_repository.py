from sqlalchemy.orm import Session
from app.models.models import ProductoTerminado
from typing import List, Optional

class ProductoTerminadoRepository:
    def __init__(self, db: Session):
        self.db = db
    
    def get_all(self) -> List[ProductoTerminado]:
        return self.db.query(ProductoTerminado).all()
    
    def get_by_id(self, producto_id: int) -> Optional[ProductoTerminado]:
        return self.db.query(ProductoTerminado).filter(ProductoTerminado.Id == producto_id).first()
    
    def create(self, producto_data: dict) -> ProductoTerminado:
        producto = ProductoTerminado(**producto_data)
        self.db.add(producto)
        self.db.commit()
        self.db.refresh(producto)
        return producto
    
    def update(self, producto_id: int, producto_data: dict) -> Optional[ProductoTerminado]:
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
