from fastapi import APIRouter, Depends, HTTPException, status
from sqlalchemy.ext.asyncio import AsyncSession
from app.database import get_db
from app.schemas.schemas import LoginRequest, TokenResponse
from app.repositories.usuario_repository import UsuarioRepository
from app.services.jwt_service import verify_password, create_access_token

router = APIRouter(prefix="/api/InicioSesion", tags=["Autenticación"])

@router.post("/login", response_model=TokenResponse)
async def login(login_data: LoginRequest, db: AsyncSession = Depends(get_db)):
    repo = UsuarioRepository(db)
    usuario = await repo.get_by_email(login_data.Correo)

    if not usuario or not verify_password(login_data.Contrasena, usuario.Contraseña):
        raise HTTPException(
            status_code=status.HTTP_401_UNAUTHORIZED,
            detail="Credenciales incorrectas"
        )

    token_data = {
        "sub": str(usuario.Id),
        "email": usuario.Correo,
        "role": usuario.Rol.Nombre if usuario.Rol else "Usuario"
    }

    token = create_access_token(token_data)
    return TokenResponse(success=True, token=token, message="Inicio de sesión exitoso")
