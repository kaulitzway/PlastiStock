from sqlalchemy.orm import Session
from app.models.models import Proveedor
from typing import List, Optional

class ProveedorRepository:
    def __init__(self, db: Session):
        self.db = db
    
    def get_all(self) -> List[Proveedor]:
        return self.db.query(Proveedor).all()
    
    def get_by_id(self, proveedor_id: int) -> Optional[Proveedor]:
        return self.db.query(Proveedor).filter(Proveedor.Id == proveedor_id).first()
    
    def create(self, proveedor_data: dict) -> Proveedor:
        proveedor = Proveedor(**proveedor_data)
        self.db.add(proveedor)
        self.db.commit()
        self.db.refresh(proveedor)
        return proveedor
    
    def update(self, proveedor_id: int, proveedor_data: dict) -> Optional[Proveedor]:
        proveedor = self.get_by_id(proveedor_id)
        if not proveedor:
            return None
        
        for key, value in proveedor_data.items():
            setattr(proveedor, key, value)
        
        self.db.commit()
        self.db.refresh(proveedor)
        return proveedor
    
    def delete(self, proveedor_id: int) -> bool:
        proveedor = self.get_by_id(proveedor_id)
        if not proveedor:
            return False
        
        self.db.delete(proveedor)
        self.db.commit()
        return True
