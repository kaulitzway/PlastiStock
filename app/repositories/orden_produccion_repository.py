from sqlalchemy.orm import Session
from app.models.models import OrdenProduccion
from typing import List, Optional

class OrdenProduccionRepository:
    def __init__(self, db: Session):
        self.db = db
    
    def get_all(self) -> List[OrdenProduccion]:
        return self.db.query(OrdenProduccion).order_by(OrdenProduccion.FechaCreacion.desc()).all()
    
    def get_by_id(self, orden_id: int) -> Optional[OrdenProduccion]:
        return self.db.query(OrdenProduccion).filter(OrdenProduccion.Id == orden_id).first()
    
    def get_by_estado(self, estado: str) -> List[OrdenProduccion]:
        return self.db.query(OrdenProduccion).filter(OrdenProduccion.Estado == estado).all()
    
    def create(self, orden_data: dict) -> OrdenProduccion:
        orden = OrdenProduccion(**orden_data)
        self.db.add(orden)
        self.db.commit()
        self.db.refresh(orden)
        return orden
    
    def update(self, orden_id: int, orden_data: dict) -> Optional[OrdenProduccion]:
        orden = self.get_by_id(orden_id)
        if not orden:
            return None
        
        for key, value in orden_data.items():
            setattr(orden, key, value)
        
        self.db.commit()
        self.db.refresh(orden)
        return orden
    
    def delete(self, orden_id: int) -> bool:
        orden = self.get_by_id(orden_id)
        if not orden:
            return False
        
        self.db.delete(orden)
        self.db.commit()
        return True
