from sqlalchemy.orm import Session
from app.models.models import Solicitud
from typing import List, Optional

class SolicitudRepository:
    def __init__(self, db: Session):
        self.db = db
    
    def get_all(self) -> List[Solicitud]:
        return self.db.query(Solicitud).all()
    
    def get_by_id(self, solicitud_id: int) -> Optional[Solicitud]:
        return self.db.query(Solicitud).filter(Solicitud.Id == solicitud_id).first()
    
    def get_by_usuario(self, usuario_id: int) -> List[Solicitud]:
        return self.db.query(Solicitud).filter(
            (Solicitud.UsuarioSolicitanteId == usuario_id) | 
            (Solicitud.UsuarioAfectadoId == usuario_id)
        ).all()
    
    def get_by_estado(self, estado: str) -> List[Solicitud]:
        return self.db.query(Solicitud).filter(Solicitud.Estado == estado).all()
    
    def create(self, solicitud_data: dict) -> Solicitud:
        solicitud = Solicitud(**solicitud_data)
        self.db.add(solicitud)
        self.db.commit()
        self.db.refresh(solicitud)
        return solicitud
    
    def update(self, solicitud_id: int, solicitud_data: dict) -> Optional[Solicitud]:
        solicitud = self.get_by_id(solicitud_id)
        if not solicitud:
            return None
        
        for key, value in solicitud_data.items():
            setattr(solicitud, key, value)
        
        self.db.commit()
        self.db.refresh(solicitud)
        return solicitud
    
    def delete(self, solicitud_id: int) -> bool:
        solicitud = self.get_by_id(solicitud_id)
        if not solicitud:
            return False
        
        self.db.delete(solicitud)
        self.db.commit()
        return True
