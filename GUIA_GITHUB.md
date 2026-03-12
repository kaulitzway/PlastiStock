# 📋 Guía Paso a Paso para Subir a GitHub

## Paso 1: Inicializar Git (si no está inicializado)

```bash
git init
```

## Paso 2: Agregar todos los archivos

```bash
git add .
```

## Paso 3: Hacer el primer commit

```bash
git commit -m "Migración completa de PlastiStock de .NET a Python/FastAPI"
```

## Paso 4: Crear repositorio en GitHub

1. Ve a [GitHub](https://github.com)
2. Haz clic en el botón **"+"** (arriba a la derecha)
3. Selecciona **"New repository"**
4. Nombre sugerido: `plastistok-api-python`
5. Descripción: "API REST para PlastiStock - Sistema de gestión de inventario (Python/FastAPI)"
6. Selecciona **Público** o **Privado** según prefieras
7. **NO** marques "Initialize with README" (ya tienes uno)
8. Haz clic en **"Create repository"**

## Paso 5: Conectar tu repositorio local con GitHub

Copia los comandos que GitHub te muestra, algo como:

```bash
git remote add origin https://github.com/tu-usuario/plastistok-api-python.git
git branch -M main
git push -u origin main
```

**O si prefieres SSH:**

```bash
git remote add origin git@github.com:tu-usuario/plastistok-api-python.git
git branch -M main
git push -u origin main
```

## Paso 6: Verificar que se subió correctamente

Recarga la página de tu repositorio en GitHub y deberías ver todos los archivos.

## 🔐 IMPORTANTE: Seguridad

**NUNCA subas el archivo `.env` a GitHub** (ya está en `.gitignore`).

Verifica que `.env` NO aparezca en tu repositorio. Si aparece:

```bash
git rm --cached .env
git commit -m "Remover archivo .env del repositorio"
git push
```

## 📝 Comandos Git Útiles

### Ver estado de archivos
```bash
git status
```

### Ver historial de commits
```bash
git log --oneline
```

### Hacer cambios futuros
```bash
git add .
git commit -m "Descripción de los cambios"
git push
```

### Crear una nueva rama
```bash
git checkout -b nombre-de-la-rama
```

### Cambiar entre ramas
```bash
git checkout main
git checkout nombre-de-la-rama
```

## 🚀 Próximos Pasos

1. **Configurar GitHub Actions** (CI/CD)
2. **Agregar badges** al README (build status, coverage, etc.)
3. **Configurar protección de ramas** en GitHub
4. **Documentar más endpoints** según los agregues
5. **Agregar tests unitarios** con pytest

## 📦 Estructura Final en GitHub

```
plastistok-api-python/
├── .gitignore
├── .env.example
├── README.md
├── GUIA_GITHUB.md
├── requirements.txt
└── app/
    ├── __init__.py
    ├── main.py
    ├── config.py
    ├── database.py
    ├── models/
    ├── schemas/
    ├── repositories/
    ├── routers/
    └── services/
```

## ✅ Checklist Final

- [ ] Archivo `.env` NO está en el repositorio
- [ ] `.gitignore` está configurado correctamente
- [ ] README.md tiene instrucciones claras
- [ ] requirements.txt tiene todas las dependencias
- [ ] El código está comentado donde es necesario
- [ ] Has probado que la API funciona localmente

## 🆘 Solución de Problemas

### Error: "remote origin already exists"
```bash
git remote remove origin
git remote add origin <tu-url>
```

### Error: "failed to push some refs"
```bash
git pull origin main --rebase
git push origin main
```

### Olvidé agregar algo al .gitignore
```bash
git rm -r --cached nombre-archivo
git commit -m "Actualizar .gitignore"
git push
```
