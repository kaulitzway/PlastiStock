from sqlalchemy import Column, Integer, String, DateTime, ForeignKey, Numeric, Text
from sqlalchemy.orm import relationship
from datetime import datetime
from app.database import Base

class TipoDocumento(Base):
    __tablename__ = "TiposDocumento"
    
    Id = Column(Integer, primary_key=True, index=True)
    Nombre = Column(String(100), nullable=False)
    
    Usuarios = relationship("Usuario", back_populates="TipoDocumento")

class Rol(Base):
    __tablename__ = "Roles"
    
    Id = Column(Integer, primary_key=True, index=True)
    Nombre = Column(String(100), nullable=False)
    
    Usuarios = relationship("Usuario", back_populates="Rol")
    RolesPermiso = relationship("RolPermiso", back_populates="Rol")

class Usuario(Base):
    __tablename__ = "Usuarios"
    
    Id = Column(Integer, primary_key=True, index=True)
    Nombre = Column(String(100), nullable=False)
    Apellido = Column(String(100), nullable=False)
    NumeroDocumento = Column(String(50), nullable=False)
    Correo = Column(String(100), nullable=False, unique=True)
    Contraseña = Column(String(255), nullable=False)
    FechaRegistro = Column(DateTime, default=datetime.utcnow)
    TipoDocumentoId = Column(Integer, ForeignKey("TiposDocumento.Id"))
    RolId = Column(Integer, ForeignKey("Roles.Id"))
    
    TipoDocumento = relationship("TipoDocumento", back_populates="Usuarios")
    Rol = relationship("Rol", back_populates="Usuarios")

class Categoria(Base):
    __tablename__ = "Categorias"
    
    Id = Column(Integer, primary_key=True, index=True)
    Nombre = Column(String(150), nullable=False)

class Producto(Base):
    __tablename__ = "Productos"
    
    Id = Column(Integer, primary_key=True, index=True)
    Nombre = Column(String(150), nullable=False)
    Descripcion = Column(Text, nullable=True)
    CodigoInterno = Column(String(50), nullable=True)
    PrecioUnitario = Column(Numeric(18, 2), default=0)
    Stock = Column(Integer, default=0)
    CategoriaId = Column(Integer, ForeignKey("Categorias.Id"), nullable=True)

class MateriaPrima(Base):
    __tablename__ = "MateriasPrimas"
    
    Id = Column(Integer, primary_key=True, index=True)
    Nombre = Column(String(150), nullable=False)
    CantidadDisponible = Column(Integer, nullable=False)
    UnidadMedida = Column(String(50), nullable=False)
    Descripcion = Column(String(250), nullable=True)
    Cantidad = Column(Integer, nullable=True)
    Precio = Column(Numeric(18, 2), nullable=True)
    
    ProductosEnProceso = relationship("ProductoEnProceso", back_populates="MateriaPrima")

class ProductoEnProceso(Base):
    __tablename__ = "ProductosEnProceso"
    
    Id = Column(Integer, primary_key=True, index=True)
    Nombre = Column(String(150), nullable=False)
    CantidadProducida = Column(Integer, default=0)
    MateriaPrimaId = Column(Integer, ForeignKey("MateriasPrimas.Id"))
    
    MateriaPrima = relationship("MateriaPrima", back_populates="ProductosEnProceso")

class ProductoTerminado(Base):
    __tablename__ = "ProductoTerminado"
    
    Id = Column(Integer, primary_key=True, index=True)
    Nombre = Column(String(150), nullable=False)
    CantidadDisponible = Column(Integer, default=0)

class Proveedor(Base):
    __tablename__ = "Proveedores"
    
    Id = Column(Integer, primary_key=True, index=True)
    Nombre = Column(String(150), nullable=False)
    Contacto = Column(String(100), nullable=True)
    Telefono = Column(String(20), nullable=True)

class Permiso(Base):
    __tablename__ = "Permisos"
    
    Id = Column(Integer, primary_key=True, index=True)
    Nombre = Column(String(100), nullable=False)
    
    RolesPermiso = relationship("RolPermiso", back_populates="Permiso")

class RolPermiso(Base):
    __tablename__ = "RolesPermisos"
    
    RolId = Column(Integer, ForeignKey("Roles.Id"), primary_key=True)
    PermisoId = Column(Integer, ForeignKey("Permisos.Id"), primary_key=True)
    
    Rol = relationship("Rol", back_populates="RolesPermiso")
    Permiso = relationship("Permiso", back_populates="RolesPermiso")

class Solicitud(Base):
    __tablename__ = "Solicitudes"
    
    Id = Column(Integer, primary_key=True, index=True)
    UsuarioSolicitanteId = Column(Integer, ForeignKey("Usuarios.Id"))
    UsuarioAfectadoId = Column(Integer, ForeignKey("Usuarios.Id"))
    RolSolicitadoId = Column(Integer, ForeignKey("Roles.Id"))
    Estado = Column(String(50), default="Pendiente")
    FechaSolicitud = Column(DateTime, default=datetime.utcnow)

class EntradaInventario(Base):
    __tablename__ = "EntradasInventario"
    
    Id = Column(Integer, primary_key=True, index=True)
    ProductoId = Column(Integer, ForeignKey("Productos.Id"))
    Cantidad = Column(Integer, nullable=False)
    Fecha = Column(DateTime, default=datetime.utcnow)

class SalidaInventario(Base):
    __tablename__ = "SalidasInventarios"
    
    Id = Column(Integer, primary_key=True, index=True)
    ProductoId = Column(Integer, ForeignKey("Productos.Id"))
    Cantidad = Column(Integer, nullable=False)
    Fecha = Column(DateTime, default=datetime.utcnow)

class Kardex(Base):
    __tablename__ = "Kardex"
    
    Id = Column(Integer, primary_key=True, index=True)
    ProductoId = Column(Integer, ForeignKey("Productos.Id"))
    TipoMovimiento = Column(String(50), nullable=False)
    Cantidad = Column(Integer, nullable=False)
    Fecha = Column(DateTime, default=datetime.utcnow)

class OrdenProduccion(Base):
    __tablename__ = "OrdenesProduccion"
    
    Id = Column(Integer, primary_key=True, index=True)
    NumeroOrden = Column(String(50), nullable=False)
    ProductoId = Column(Integer, ForeignKey("Productos.Id"))
    CantidadSolicitada = Column(Integer, nullable=False)
    Estado = Column(String(50), default="Pendiente")
    FechaCreacion = Column(DateTime, default=datetime.utcnow)

class Merma(Base):
    __tablename__ = "Mermas"
    
    Id = Column(Integer, primary_key=True, index=True)
    ProductoId = Column(Integer, ForeignKey("Productos.Id"))
    Cantidad = Column(Integer, nullable=False)
    Motivo = Column(String(250), nullable=True)
    Fecha = Column(DateTime, default=datetime.utcnow)
