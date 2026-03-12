from pydantic_settings import BaseSettings
from typing import Optional

class Settings(BaseSettings):
    # Database
    DB_SERVER: str
    DB_NAME: str
    DB_USER: Optional[str] = None
    DB_PASSWORD: Optional[str] = None
    DB_TRUSTED_CONNECTION: str = "yes"
    
    # JWT
    JWT_SECRET_KEY: str
    JWT_ALGORITHM: str = "HS256"
    JWT_ACCESS_TOKEN_EXPIRE_MINUTES: int = 120
    
    # App
    APP_NAME: str = "PlastiStock API"
    APP_VERSION: str = "1.0.0"
    DEBUG: bool = True
    
    @property
    def DATABASE_URL(self) -> str:
        if self.DB_TRUSTED_CONNECTION.lower() == "yes":
            return f"mssql+pymssql://@{self.DB_SERVER}/{self.DB_NAME}?trusted_connection=yes"
        return f"mssql+pymssql://{self.DB_USER}:{self.DB_PASSWORD}@{self.DB_SERVER}/{self.DB_NAME}"
    
    class Config:
        env_file = ".env"
        case_sensitive = True

settings = Settings()
