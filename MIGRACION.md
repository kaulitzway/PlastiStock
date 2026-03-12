# 📝 Documentación de Migración: .NET → Python

## Resumen de la Migración

Este documento explica cómo se migró el proyecto PlastiStock de **ASP.NET Core (C#)** a **Python con FastAPI**.

## Equivalencias de Tecnologías

| .NET | Python | Propósito |
|------|--------|-----------|
| ASP.NET Core | FastAPI | Framework web |
| Entity Framework Core | SQLAlchemy | ORM (mapeo objeto-relacional) |
| Data Annotations | Pydantic | Validación de modelos |
| System.IdentityModel.Tokens.Jwt | python-jose | Manejo de JWT |
| BCrypt.Net | passlib[bcrypt] | Encriptación de contraseñas |
| Microsoft.EntityFrameworkCore.SqlServer | pymssql | Conector SQL Server |
| Swashbuckle (Swagger) | FastAPI (integrado) | Documentación API |

## Estructura de Carpetas

### Antes (.NET)
```
PlastiStock/
├── Controllers/
├── Models/
├── Repositories/
├── Services/
├── Data/
├── Program.cs
└── appsettings.json
```

### Después (Python)
```
app/
├── routers/          # Controllers → Routers
├── models/           # Models (SQLAlchemy)
├── schemas/          # DTOs con Pydantic
├── repositories/     # Repositories
├── services/         # Services
├── database.py       # DbContext
├── config.py         # appsettings
└── main.py           # Program.cs
```

## Cambios Principales

### 1. Modelos de Base de Datos

**Antes (C# - Entity Framework):**
```csharp
public class Usuario
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [EmailAddress]
    public required string Correo { get; set; }
    
    [ForeignKey("Rol")]
    public int RolId { get; set; }
    public Rol? Rol { get; set; }
}
```

**Después (Python - SQLAlchemy):**
```python
class Usuario(Base):
    __tablename__ = "Usuarios"
    
    Id = Column(Integer, primary_key=True, index=True)
    Correo = Column(String(100), nullable=False, unique=True)
    RolId = Column(Integer, ForeignKey("Roles.Id"))
    
    Rol = relationship("Rol", back_populates="Usuarios")
```

### 2. Validación de Datos (DTOs)

**Antes (C# - Data Annotations):**
```csharp
public class UsuarioCreate
{
    [Required]
    public required string Nombre { get; set; }
    
    [EmailAddress]
    public required string Correo { get; set; }
}
```

**Después (Python - Pydantic):**
```python
class UsuarioCreate(BaseModel):
    Nombre: str
    Correo: EmailStr
```

### 3. Controladores/Routers

**Antes (C# - Controller):**
```csharp
[Route("api/[controller]")]
[ApiController]
public class UsuariosController : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var usuarios = await _repo.GetAllAsync();
        return Ok(usuarios);
    }
}
```

**Después (Python - Router):**
```python
router = APIRouter(prefix="/api/Usuarios", tags=["Usuarios"])

@router.get("", response_model=List[UsuarioResponse])
async def get_all_usuarios(
    db: Session = Depends(get_db),
    current_user: dict = Depends(get_current_user)
):
    repo = UsuarioRepository(db)
    return repo.get_all()
```

### 4. Inyección de Dependencias

**Antes (C# - Program.cs):**
```csharp
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
```

**Después (Python - FastAPI Depends):**
```python
def get_db():
    db = SessionLocal()
    try:
        yield db
    finally:
        db.close()

# Uso en endpoints
async def endpoint(db: Session = Depends(get_db)):
    ...
```

### 5. Autenticación JWT

**Antes (C# - JwtService):**
```csharp
var token = new JwtSecurityToken(
    issuer: _config["Jwt:Issuer"],
    audience: _config["Jwt:Audience"],
    claims: claims,
    expires: DateTime.Now.AddHours(2),
    signingCredentials: creds
);
```

**Después (Python - jose):**
```python
def create_access_token(data: dict):
    to_encode = data.copy()
    expire = datetime.utcnow() + timedelta(hours=2)
    to_encode.update({"exp": expire})
    return jwt.encode(to_encode, SECRET_KEY, algorithm=ALGORITHM)
```

### 6. Configuración

**Antes (appsettings.json):**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=...;Database=..."
  },
  "Jwt": {
    "Key": "...",
    "Issuer": "..."
  }
}
```

**Después (.env):**
```env
DB_SERVER=...
DB_NAME=...
JWT_SECRET_KEY=...
```

## Ventajas de la Migración

✅ **Simplicidad**: Menos código boilerplate
✅ **Velocidad**: FastAPI es extremadamente rápido
✅ **Documentación automática**: Swagger integrado
✅ **Type hints**: Validación automática con Pydantic
✅ **Async nativo**: Mejor manejo de concurrencia
✅ **Comunidad**: Ecosistema Python muy activo

## Funcionalidades Mantenidas

- ✅ Autenticación JWT
- ✅ Autorización por roles
- ✅ CRUD completo de usuarios y productos
- ✅ Encriptación de contraseñas con BCrypt
- ✅ Validación de datos
- ✅ Documentación Swagger
- ✅ Manejo de errores
- ✅ Conexión a SQL Server

## Próximos Pasos Recomendados

1. **Agregar tests**: Usar pytest para testing
2. **Migraciones**: Implementar Alembic para migraciones de BD
3. **Logging**: Configurar logging estructurado
4. **Cache**: Agregar Redis para caché
5. **Monitoreo**: Implementar métricas con Prometheus
6. **Docker**: Containerizar la aplicación

## Comandos Útiles

### Ejecutar la aplicación
```bash
python run.py
# o
uvicorn app.main:app --reload
```

### Instalar dependencias
```bash
pip install -r requirements.txt
```

### Ver documentación
```
http://localhost:8000/docs
```
