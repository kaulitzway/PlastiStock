"""
Script para verificar que el proyecto está listo para GitHub
"""
import os
import sys

def check_file_exists(filepath, required=True):
    exists = os.path.exists(filepath)
    status = "✅" if exists else ("❌" if required else "⚠️")
    print(f"{status} {filepath}")
    return exists

def check_env_not_in_git():
    gitignore_path = ".gitignore"
    if os.path.exists(gitignore_path):
        with open(gitignore_path, 'r') as f:
            content = f.read()
            if '.env' in content and '.env' not in content.replace('.env.example', ''):
                print("✅ .env está en .gitignore")
                return True
    print("❌ .env NO está en .gitignore")
    return False

def main():
    print("=" * 50)
    print("🔍 VERIFICACIÓN DEL PROYECTO")
    print("=" * 50)
    
    print("\n📁 Archivos principales:")
    all_good = True
    
    # Archivos requeridos
    required_files = [
        "README.md",
        "requirements.txt",
        ".gitignore",
        ".env.example",
        "run.py",
        "app/__init__.py",
        "app/main.py",
        "app/config.py",
        "app/database.py",
    ]
    
    for file in required_files:
        if not check_file_exists(file, required=True):
            all_good = False
    
    print("\n📚 Documentación:")
    doc_files = [
        "INICIO_RAPIDO.md",
        "MIGRACION.md",
        "GUIA_GITHUB.md",
    ]
    
    for file in doc_files:
        check_file_exists(file, required=False)
    
    print("\n🔐 Seguridad:")
    if not check_env_not_in_git():
        all_good = False
    
    if os.path.exists(".env"):
        print("⚠️  .env existe (NO lo subas a GitHub)")
    else:
        print("✅ .env no existe en el proyecto")
    
    print("\n📦 Estructura de carpetas:")
    folders = [
        "app/models",
        "app/schemas",
        "app/repositories",
        "app/routers",
        "app/services",
    ]
    
    for folder in folders:
        if not check_file_exists(folder, required=True):
            all_good = False
    
    print("\n" + "=" * 50)
    if all_good:
        print("✅ TODO LISTO PARA SUBIR A GITHUB")
        print("\nSiguientes pasos:")
        print("1. git add .")
        print("2. git commit -m 'Migración completa a Python/FastAPI'")
        print("3. git push origin main")
    else:
        print("❌ HAY PROBLEMAS QUE RESOLVER")
        print("\nRevisa los archivos marcados con ❌")
    print("=" * 50)
    
    return 0 if all_good else 1

if __name__ == "__main__":
    sys.exit(main())
