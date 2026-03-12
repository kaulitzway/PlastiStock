from sqlalchemy.orm import Session
from app.models.models import Usuario
from app.services.jwt_service import hash_password
from typing import List, Optional

class UsuarioRepository:
    def __init__(self, db: Session):
        self.db = db
    
    def get_all(self) -> List[Usuario]:
        return self.db.query(Usuario).all()
    
    def get_by_id(self, usuario_id: int) -> Optional[Usuario]:
        return self.db.query(Usuario).filter(Usuario.Id == usuario_id).first()
    
    def get_by_email(self, email: str) -> Optional[Usuario]:
        return self.db.query(Usuario).filter(Usuario.Correo == email).first()
    
    def create(self, usuario_data: dict) -> Usuario:
        usuario_data["Contraseña"] = hash_password(usuario_data["Contraseña"])
        usuario = Usuario(**usuario_data)
        self.db.add(usuario)
        self.db.commit()
        self.db.refresh(usuario)
        return usuario
    
    def update(self, usuario_id: int, usuario_data: dict) -> Optional[Usuario]:
        usuario = self.get_by_id(usuario_id)
        if not usuario:
            return None
        
        if "Contraseña" in usuario_data and usuario_data["Contraseña"]:
            usuario_data["Contraseña"] = hash_password(usuario_data["Contraseña"])
        
        for key, value in usuario_data.items():
            if value is not None:
                setattr(usuario, key, value)
        
        self.db.commit()
        self.db.refresh(usuario)
        return usuario
    
    def delete(self, usuario_id: int) -> bool:
        usuario = self.get_by_id(usuario_id)
        if not usuario:
            return False
        
        self.db.delete(usuario)
        self.db.commit()
        return True
