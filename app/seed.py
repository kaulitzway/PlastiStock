import asyncio
from sqlalchemy.ext.asyncio import AsyncSession
from app.database import AsyncSessionLocal, engine, Base
from app.models.models import Rol, TipoDocumento, Usuario
from passlib.context import CryptContext

pwd_context = CryptContext(schemes=["bcrypt"], deprecated="auto")

async def seed():
    async with AsyncSessionLocal() as db:
        # Crear roles
        admin = Rol(Nombre="Administrador")
        operario = Rol(Nombre="Operario")
        supervisor = Rol(Nombre="Supervisor")
        db.add_all([admin, operario, supervisor])
        await db.flush()  # Para obtener los IDs

        # Crear tipo de documento
        cc = TipoDocumento(Nombre="Cédula de Ciudadanía")
        db.add(cc)
        await db.flush()

        # Crear usuario administrador
        usuario_admin = Usuario(
            Nombre="Admin",
            Apellido="PlastiStock",
            NumeroDocumento="123456789",
            Correo="admin@plastistock.com",
            Contraseña=pwd_context.hash("Slive200608."),
            TipoDocumentoId=cc.Id,
            RolId=admin.Id
        )
        db.add(usuario_admin)
        await db.commit()
        print(" Datos iniciales creados correctamente")
        print(f"   Usuario: admin@plastistock.com")
        print(f"   Contraseña: Admin123.")

if __name__ == "__main__":
    asyncio.run(seed())