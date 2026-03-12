from sqlalchemy.orm import Session
from app.models.models import Kardex
from typing import List, Optional

class KardexRepository:
    def __init__(self, db: Session):
        self.db = db
    
    def get_all(self) -> List[Kardex]:
        return self.db.query(Kardex).order_by(Kardex.Fecha.desc()).all()
    
    def get_by_id(self, kardex_id: int) -> Optional[Kardex]:
        return self.db.query(Kardex).filter(Kardex.Id == kardex_id).first()
    
    def get_by_producto(self, producto_id: int) -> List[Kardex]:
        return self.db.query(Kardex).filter(
            Kardex.ProductoId == producto_id
        ).order_by(Kardex.Fecha.desc()).all()
    
    def create(self, kardex_data: dict) -> Kardex:
        kardex = Kardex(**kardex_data)
        self.db.add(kardex)
        self.db.commit()
        self.db.refresh(kardex)
        return kardex
    
    def delete(self, kardex_id: int) -> bool:
        kardex = self.get_by_id(kardex_id)
        if not kardex:
            return False
        
        self.db.delete(kardex)
        self.db.commit()
        return True
