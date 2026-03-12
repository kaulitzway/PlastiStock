from sqlalchemy.orm import Session
from app.models.models import Producto
from typing import List, Optional

class ProductoRepository:
    def __init__(self, db: Session):
        self.db = db
    
    def get_all(self) -> List[Producto]:
        return self.db.query(Producto).all()
    
    def get_by_id(self, producto_id: int) -> Optional[Producto]:
        return self.db.query(Producto).filter(Producto.Id == producto_id).first()
    
    def create(self, producto_data: dict) -> Producto:
        producto = Producto(**producto_data)
        self.db.add(producto)
        self.db.commit()
        self.db.refresh(producto)
        return producto
    
    def update(self, producto_id: int, producto_data: dict) -> Optional[Producto]:
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
