from sqlalchemy.orm import Session
from app.models.models import Categoria
from typing import List, Optional

class CategoriaRepository:
    def __init__(self, db: Session):
        self.db = db
    
    def get_all(self) -> List[Categoria]:
        return self.db.query(Categoria).all()
    
    def get_by_id(self, categoria_id: int) -> Optional[Categoria]:
        return self.db.query(Categoria).filter(Categoria.Id == categoria_id).first()
    
    def create(self, categoria_data: dict) -> Categoria:
        categoria = Categoria(**categoria_data)
        self.db.add(categoria)
        self.db.commit()
        self.db.refresh(categoria)
        return categoria
    
    def update(self, categoria_id: int, categoria_data: dict) -> Optional[Categoria]:
        categoria = self.get_by_id(categoria_id)
        if not categoria:
            return None
        
        for key, value in categoria_data.items():
            setattr(categoria, key, value)
        
        self.db.commit()
        self.db.refresh(categoria)
        return categoria
    
    def delete(self, categoria_id: int) -> bool:
        categoria = self.get_by_id(categoria_id)
        if not categoria:
            return False
        
        self.db.delete(categoria)
        self.db.commit()
        return True
