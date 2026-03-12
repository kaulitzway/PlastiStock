from pydantic import BaseModel, EmailStr, Field
from typing import Optional
from datetime import datetime
from decimal import Decimal

# Schemas de Usuario
class UsuarioBase(BaseModel):
    Nombre: str
    Apellido: str
    NumeroDocumento: str
    Correo: EmailStr
    TipoDocumentoId: int
    RolId: int

class UsuarioCreate(UsuarioBase):
    Contraseña: str

class UsuarioUpdate(UsuarioBase):
    Contraseña: Optional[str] = None

class UsuarioResponse(UsuarioBase):
    Id: int
    FechaRegistro: datetime
    
    class Config:
        from_attributes = True

# Schemas de Autenticación
class LoginRequest(BaseModel):
    Correo: EmailStr
    Contrasena: str

class TokenResponse(BaseModel):
    success: bool
    token: str
    message: str

# Schemas de Producto
class ProductoBase(BaseModel):
    Nombre: str
    Descripcion: Optional[str] = None
    CodigoInterno: Optional[str] = None
    PrecioUnitario: Decimal = Field(default=Decimal("0.00"))
    Stock: int = 0
    CategoriaId: Optional[int] = None

class ProductoCreate(ProductoBase):
    pass

class ProductoUpdate(ProductoBase):
    pass


class ProductoResponse(ProductoBase):
    Id: int
    
    class Config:
        from_attributes = True

# Schemas de Categoria
class CategoriaBase(BaseModel):
    Nombre: str

class CategoriaCreate(CategoriaBase):
    pass

class CategoriaResponse(CategoriaBase):
    Id: int
    
    class Config:
        from_attributes = True

# Schemas de MateriaPrima
class MateriaPrimaBase(BaseModel):
    Nombre: str
    CantidadDisponible: int
    UnidadMedida: str
    Descripcion: Optional[str] = None
    Cantidad: Optional[int] = None
    Precio: Optional[Decimal] = None

class MateriaPrimaCreate(MateriaPrimaBase):
    pass

class MateriaPrimaResponse(MateriaPrimaBase):
    Id: int
    
    class Config:
        from_attributes = True

# Schemas de Solicitud
class SolicitudBase(BaseModel):
    UsuarioSolicitanteId: int
    UsuarioAfectadoId: int
    RolSolicitadoId: int
    Estado: str = "Pendiente"

class SolicitudCreate(SolicitudBase):
    pass

class SolicitudUpdate(BaseModel):
    Estado: str

class SolicitudResponse(SolicitudBase):
    Id: int
    FechaSolicitud: datetime
    
    class Config:
        from_attributes = True

# Schemas de EntradaInventario
class EntradaInventarioBase(BaseModel):
    ProductoId: int
    Cantidad: int

class EntradaInventarioCreate(EntradaInventarioBase):
    pass

class EntradaInventarioResponse(EntradaInventarioBase):
    Id: int
    Fecha: datetime
    
    class Config:
        from_attributes = True

# Schemas de SalidaInventario
class SalidaInventarioBase(BaseModel):
    ProductoId: int
    Cantidad: int

class SalidaInventarioCreate(SalidaInventarioBase):
    pass

class SalidaInventarioResponse(SalidaInventarioBase):
    Id: int
    Fecha: datetime
    
    class Config:
        from_attributes = True

# Schemas de Kardex
class KardexBase(BaseModel):
    ProductoId: int
    TipoMovimiento: str
    Cantidad: int

class KardexCreate(KardexBase):
    pass

class KardexResponse(KardexBase):
    Id: int
    Fecha: datetime
    
    class Config:
        from_attributes = True

# Schemas de OrdenProduccion
class OrdenProduccionBase(BaseModel):
    NumeroOrden: str
    ProductoId: int
    CantidadSolicitada: int
    Estado: str = "Pendiente"

class OrdenProduccionCreate(OrdenProduccionBase):
    pass

class OrdenProduccionUpdate(BaseModel):
    Estado: str

class OrdenProduccionResponse(OrdenProduccionBase):
    Id: int
    FechaCreacion: datetime
    
    class Config:
        from_attributes = True

# Schemas de Merma
class MermaBase(BaseModel):
    ProductoId: int
    Cantidad: int
    Motivo: Optional[str] = None

class MermaCreate(MermaBase):
    pass

class MermaResponse(MermaBase):
    Id: int
    Fecha: datetime
    
    class Config:
        from_attributes = True

# Schemas de ProductoEnProceso
class ProductoEnProcesoBase(BaseModel):
    Nombre: str
    CantidadProducida: int = 0
    MateriaPrimaId: int

class ProductoEnProcesoCreate(ProductoEnProcesoBase):
    pass

class ProductoEnProcesoUpdate(ProductoEnProcesoBase):
    pass

class ProductoEnProcesoResponse(ProductoEnProcesoBase):
    Id: int
    
    class Config:
        from_attributes = True

# Schemas de ProductoTerminado
class ProductoTerminadoBase(BaseModel):
    Nombre: str
    CantidadDisponible: int = 0

class ProductoTerminadoCreate(ProductoTerminadoBase):
    pass

class ProductoTerminadoUpdate(ProductoTerminadoBase):
    pass

class ProductoTerminadoResponse(ProductoTerminadoBase):
    Id: int
    
    class Config:
        from_attributes = True

# Schemas de Proveedor
class ProveedorBase(BaseModel):
    Nombre: str
    Contacto: Optional[str] = None
    Telefono: Optional[str] = None

class ProveedorCreate(ProveedorBase):
    pass

class ProveedorUpdate(ProveedorBase):
    pass

class ProveedorResponse(ProveedorBase):
    Id: int
    
    class Config:
        from_attributes = True
