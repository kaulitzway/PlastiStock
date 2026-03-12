from sqlalchemy.orm import Session
from app.models.models import MateriaPrima
from typing import List, Optional

class MateriaPrimaRepository:
    def __init__(self, db: Session):
        self.db = db
    
    def get_all(self) -> List[MateriaPrima]:
        return self.db.query(MateriaPrima).all()
    
    def get_by_id(self, materia_id: int) -> Optional[MateriaPrima]:
        return self.db.query(MateriaPrima).filter(MateriaPrima.Id == materia_id).first()
    
    def create(self, materia_data: dict) -> MateriaPrima:
        materia = MateriaPrima(**materia_data)
        self.db.add(materia)
        self.db.commit()
        self.db.refresh(materia)
        return materia
    
    def update(self, materia_id: int, materia_data: dict) -> Optional[MateriaPrima]:
        materia = self.get_by_id(materia_id)
        if not materia:
            return None
        
        for key, value in materia_data.items():
            setattr(materia, key, value)
        
        self.db.commit()
        self.db.refresh(materia)
        return materia
    
    def delete(self, materia_id: int) -> bool:
        materia = self.get_by_id(materia_id)
        if not materia:
            return False
        
        self.db.delete(materia)
        self.db.commit()
        return True
