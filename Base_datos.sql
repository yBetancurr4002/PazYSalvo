/*

# Modelo de base de datos - Sistema de gestión de clientes y deudas 
## Nombre de la Web App: Paz y Salvo

*/

-- CREATE DATABASE PazSalvo

CREATE TABLE Personas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombres NVARCHAR(100) NOT NULL,
    Telefono NVARCHAR(20) NOT NULL,
    CorreoElectronico NVARCHAR(100) NOT NULL,
    DocumentoIdentificacion NVARCHAR(20) NOT NULL
);

CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    PersonaId INT FOREIGN KEY REFERENCES Personas(Id),
    NombreUsuario NVARCHAR(50) NOT NULL,
    Contrasena NVARCHAR(100) NOT NULL
);

CREATE TABLE Servicios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255),
    Precio DECIMAL(10, 2) NOT NULL
);

CREATE TABLE MediosDePago (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255)
);

CREATE TABLE Roles (
    Id INT PRIMARY KEY IDENTITY (1,1),
    Nombre VARCHAR(25),
    Descripcion VARCHAR(100),
    Activo BIT DEFAULT 0

);

CREATE TABLE Clientes (
    Id INT PRIMARY KEY IDENTITY(1,1),
    PersonaId INT FOREIGN KEY REFERENCES Personas(Id),
    RolId INT FOREIGN KEY REFERENCES Roles(Id),
    FechaDeCreación DATETIME DEFAULT GETDATE()
);

CREATE TABLE Facturas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ClienteId INT FOREIGN KEY REFERENCES Clientes(Id),
    ServicioAdquiridoId INT FOREIGN KEY REFERENCES Servicios(Id),
    MedioDePagoId INT FOREIGN KEY REFERENCES MediosDePago(Id),
	   PagoId INT FOREIGN KEY REFERENCES Pagos(Id),
    FechaDeCreación DATETIME DEFAULT GETDATE()
);

CREATE TABLE Pagos (
	Id INT PRIMARY KEY IDENTITY(1,1),
	Monto DECIMAL(10, 2) DEFAULT 0,
	Estado BIT DEFAULT 0
);