"""
Script para verificar que la aplicación está correctamente configurada
"""
import sys
from pathlib import Path

def test_imports():
    """Verificar que todos los módulos se pueden importar"""
    print("🔍 Verificando imports...")
    
    try:
        from app.config import settings
        print("✅ Config importado correctamente")
        print(f"   - Base de datos: {settings.DB_NAME}")
        print(f"   - Servidor: {settings.DB_SERVER}")
    except Exception as e:
        print(f"❌ Error en config: {e}")
        return False
    
    try:
        from app.database import Base, engine, get_db
        print("✅ Database importado correctamente")
    except Exception as e:
        print(f"❌ Error en database: {e}")
        return False
    
    try:
        from app.models.models import Usuario, Producto, Categoria
        print("✅ Modelos importados correctamente")
    except Exception as e:
        print(f"❌ Error en modelos: {e}")
        return False
    
    try:
        from app.repositories.usuario_repository import UsuarioRepository
        from app.repositories.producto_repository import ProductoRepository
        print("✅ Repositorios importados correctamente")
    except Exception as e:
        print(f"❌ Error en repositorios: {e}")
        return False
    
    try:
        from app.routers.auth import router as auth_router
        from app.routers.usuarios import router as usuarios_router
        from app.routers.productos import router as productos_router
        print("✅ Routers importados correctamente")
    except Exception as e:
        print(f"❌ Error en routers: {e}")
        return False
    
    try:
        from app.services.jwt_service import create_access_token, hash_password
        print("✅ Servicios importados correctamente")
    except Exception as e:
        print(f"❌ Error en servicios: {e}")
        return False
    
    try:
        from app.main import app
        print("✅ Aplicación principal importada correctamente")
    except Exception as e:
        print(f"❌ Error en main: {e}")
        return False
    
    return True

def test_database_connection():
    """Verificar conexión a la base de datos"""
    print("\n🔍 Verificando conexión a base de datos...")
    
    try:
        from sqlalchemy import create_engine, text
        from app.config import settings
        
        # Intentar conectar
        engine = create_engine(settings.DATABASE_URL, echo=False)
        with engine.connect() as conn:
            result = conn.execute(text("SELECT 1"))
            print("✅ Conexión a base de datos exitosa")
            return True
    except Exception as e:
        print(f"⚠️  No se pudo conectar a la base de datos: {e}")
        print("   Esto es normal si SQL Server no está corriendo o la configuración es incorrecta")
        return False

def test_jwt():
    """Verificar que JWT funciona"""
    print("\n🔍 Verificando JWT...")
    
    try:
        from app.services.jwt_service import create_access_token, decode_token, hash_password, verify_password
        
        # Test hash de contraseña
        password = "test123"
        hashed = hash_password(password)
        if verify_password(password, hashed):
            print("✅ Hash de contraseñas funciona correctamente")
        else:
            print("❌ Error en hash de contraseñas")
            return False
        
        # Test JWT
        token_data = {"sub": "1", "email": "test@test.com", "role": "Admin"}
        token = create_access_token(token_data)
        decoded = decode_token(token)
        
        if decoded["sub"] == "1":
            print("✅ JWT funciona correctamente")
            return True
        else:
            print("❌ Error en JWT")
            return False
    except Exception as e:
        print(f"❌ Error en JWT: {e}")
        return False

def count_files():
    """Contar archivos del proyecto"""
    print("\n📊 Estadísticas del proyecto:")
    
    models = len(list(Path("app/models").glob("*.py"))) - 1  # -1 para __init__
    repos = len(list(Path("app/repositories").glob("*.py"))) - 1
    routers = len(list(Path("app/routers").glob("*.py"))) - 1
    
    print(f"   - Modelos: {models} archivo(s)")
    print(f"   - Repositorios: {repos} archivo(s)")
    print(f"   - Routers: {routers} archivo(s)")
    
    # Contar modelos en models.py
    try:
        with open("app/models/models.py", "r", encoding="utf-8") as f:
            content = f.read()
            model_count = content.count("class ") - content.count("class Config")
            print(f"   - Clases de modelo: {model_count}")
    except:
        pass

def main():
    print("=" * 60)
    print("🚀 VERIFICACIÓN DE MIGRACIÓN - PlastiStock API")
    print("=" * 60)
    
    all_ok = True
    
    # Test 1: Imports
    if not test_imports():
        all_ok = False
    
    # Test 2: Database
    test_database_connection()  # No afecta all_ok porque puede no estar configurado aún
    
    # Test 3: JWT
    if not test_jwt():
        all_ok = False
    
    # Estadísticas
    count_files()
    
    print("\n" + "=" * 60)
    if all_ok:
        print("✅ ¡TODO ESTÁ CORRECTO! La migración fue exitosa.")
        print("\nPróximos pasos:")
        print("1. Configura tu .env con los datos de tu base de datos")
        print("2. Ejecuta: python run.py")
        print("3. Abre: http://localhost:8000/docs")
    else:
        print("❌ Hay algunos problemas que resolver")
        print("\nRevisa los errores arriba y corrige los archivos indicados")
    print("=" * 60)
    
    return 0 if all_ok else 1

if __name__ == "__main__":
    sys.exit(main())
