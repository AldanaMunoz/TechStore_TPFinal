-- ========================================
-- SISTEMA DE GESTIÓN DE VENTAS - TECHSTORE S.A.
-- Script de Creación de Base de Datos
-- ========================================

-- Crear la base de datos
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'TechStoreDB')
BEGIN
    CREATE DATABASE TechStoreDB;
END
GO

USE TechStoreDB;
GO

-- ========================================
-- TABLAS DEL SISTEMA
-- ========================================

-- Tabla: Sucursales
CREATE TABLE Sucursales (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Direccion VARCHAR(200) NOT NULL,
    Telefono VARCHAR(20),
    Email VARCHAR(100),
    Activo BIT DEFAULT 1,
    FechaCreacion DATETIME DEFAULT GETDATE()
);

-- Tabla: Categorías de Productos
CREATE TABLE Categorias (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(500),
    Activo BIT DEFAULT 1
);

-- Tabla: Productos
CREATE TABLE Productos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Codigo VARCHAR(50) NOT NULL UNIQUE,
    Nombre VARCHAR(200) NOT NULL,
    Descripcion VARCHAR(500),
    CategoriaId INT NOT NULL,
    PrecioUnitario DECIMAL(18,2) NOT NULL,
    StockMinimo INT DEFAULT 10,
    Activo BIT DEFAULT 1,
    FechaCreacion DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CategoriaId) REFERENCES Categorias(Id)
);

-- Tabla: Inventario por Sucursal
CREATE TABLE Inventario (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ProductoId INT NOT NULL,
    SucursalId INT NOT NULL,
    StockActual INT NOT NULL DEFAULT 0,
    StockReservado INT DEFAULT 0,
    UltimaActualizacion DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ProductoId) REFERENCES Productos(Id),
    FOREIGN KEY (SucursalId) REFERENCES Sucursales(Id),
    CONSTRAINT UQ_Inventario_Producto_Sucursal UNIQUE (ProductoId, SucursalId)
);

-- Tabla: Tipos de Cliente
CREATE TABLE TiposCliente (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(50) NOT NULL, -- Minorista, Mayorista
    PorcentajeDescuento DECIMAL(5,2) DEFAULT 0,
    MontoMinimoCompra DECIMAL(18,2) DEFAULT 0,
    Descripcion VARCHAR(200)
);

-- Tabla: Clientes
CREATE TABLE Clientes (
    Id INT PRIMARY KEY IDENTITY(1,1),
    TipoClienteId INT NOT NULL,
    RazonSocial VARCHAR(200) NOT NULL,
    CUIT VARCHAR(13),
    Direccion VARCHAR(200),
    Telefono VARCHAR(20),
    Email VARCHAR(100),
    LimiteCredito DECIMAL(18,2) DEFAULT 0,
    SaldoActual DECIMAL(18,2) DEFAULT 0,
    Activo BIT DEFAULT 1,
    FechaRegistro DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (TipoClienteId) REFERENCES TiposCliente(Id)
);

-- Tabla: Métodos de Pago
CREATE TABLE MetodosPago (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(50) NOT NULL, -- Efectivo, Tarjeta, Transferencia
    RequiereValidacion BIT DEFAULT 0,
    Activo BIT DEFAULT 1
);

-- Tabla: Vendedores
CREATE TABLE Vendedores (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    Email VARCHAR(100),
    Telefono VARCHAR(20),
    SucursalId INT NOT NULL,
    Activo BIT DEFAULT 1,
    FechaIngreso DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (SucursalId) REFERENCES Sucursales(Id)
);

-- Tabla: Ventas (Cabecera)
CREATE TABLE Ventas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    NumeroFactura VARCHAR(20) NOT NULL UNIQUE,
    ClienteId INT NOT NULL,
    VendedorId INT NOT NULL,
    SucursalId INT NOT NULL,
    FechaVenta DATETIME DEFAULT GETDATE(),
    Subtotal DECIMAL(18,2) NOT NULL,
    PorcentajeDescuento DECIMAL(5,2) DEFAULT 0,
    MontoDescuento DECIMAL(18,2) DEFAULT 0,
    Total DECIMAL(18,2) NOT NULL,
    MetodoPagoId INT NOT NULL,
    Estado VARCHAR(20) DEFAULT 'Completada', -- Completada, Cancelada, Pendiente
    Observaciones VARCHAR(500),
    FOREIGN KEY (ClienteId) REFERENCES Clientes(Id),
    FOREIGN KEY (VendedorId) REFERENCES Vendedores(Id),
    FOREIGN KEY (SucursalId) REFERENCES Sucursales(Id),
    FOREIGN KEY (MetodoPagoId) REFERENCES MetodosPago(Id)
);

-- Tabla: Detalle de Ventas (Líneas de productos)
CREATE TABLE DetallesVenta (
    Id INT PRIMARY KEY IDENTITY(1,1),
    VentaId INT NOT NULL,
    ProductoId INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(18,2) NOT NULL,
    Subtotal DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (VentaId) REFERENCES Ventas(Id),
    FOREIGN KEY (ProductoId) REFERENCES Productos(Id)
);

-- Tabla: Cuentas Corrientes (para clientes con crédito)
CREATE TABLE CuentasCorrientes (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ClienteId INT NOT NULL,
    VentaId INT NULL,
    TipoMovimiento VARCHAR(20) NOT NULL, -- Cargo, Pago
    Monto DECIMAL(18,2) NOT NULL,
    SaldoAnterior DECIMAL(18,2) NOT NULL,
    SaldoNuevo DECIMAL(18,2) NOT NULL,
    FechaMovimiento DATETIME DEFAULT GETDATE(),
    Descripcion VARCHAR(200),
    FOREIGN KEY (ClienteId) REFERENCES Clientes(Id),
    FOREIGN KEY (VentaId) REFERENCES Ventas(Id)
);

-- ========================================
-- ÍNDICES PARA OPTIMIZAR CONSULTAS
-- ========================================

CREATE INDEX IX_Productos_Codigo ON Productos(Codigo);
CREATE INDEX IX_Productos_Categoria ON Productos(CategoriaId);
CREATE INDEX IX_Ventas_Fecha ON Ventas(FechaVenta);
CREATE INDEX IX_Ventas_Cliente ON Ventas(ClienteId);
CREATE INDEX IX_Ventas_NumeroFactura ON Ventas(NumeroFactura);
CREATE INDEX IX_DetallesVenta_Producto ON DetallesVenta(ProductoId);
CREATE INDEX IX_Inventario_Sucursal ON Inventario(SucursalId);

-- ========================================
-- DATOS DE PRUEBA
-- ========================================

-- Insertar Sucursales
INSERT INTO Sucursales (Nombre, Direccion, Telefono, Email) VALUES
('Sucursal Centro', 'Av. Córdoba 1234, Rosario', '341-5551000', 'centro@techstore.com'),
('Sucursal Norte', 'Bv. Oroño 5678, Rosario', '341-5551001', 'norte@techstore.com'),
('Sucursal Sur', 'Av. Pellegrini 9012, Rosario', '341-5551002', 'sur@techstore.com');

-- Insertar Categorías
INSERT INTO Categorias (Nombre, Descripcion) VALUES
('Laptops', 'Computadoras portátiles y notebooks'),
('Smartphones', 'Teléfonos inteligentes y accesorios'),
('Tablets', 'Tabletas y iPad'),
('Componentes PC', 'Procesadores, RAM, discos, etc.'),
('Periféricos', 'Teclados, mouse, monitores'),
('Audio', 'Auriculares, parlantes, micrófonos'),
('Gaming', 'Consolas y accesorios gaming');

-- Insertar Productos
INSERT INTO Productos (Codigo, Nombre, Descripcion, CategoriaId, PrecioUnitario, StockMinimo) VALUES
('LAP001', 'Laptop HP Pavilion 15', 'Intel i5, 8GB RAM, 256GB SSD', 1, 450000, 5),
('LAP002', 'Laptop Lenovo IdeaPad 3', 'AMD Ryzen 5, 8GB RAM, 512GB SSD', 1, 380000, 5),
('LAP003', 'MacBook Air M2', 'Apple M2, 8GB RAM, 256GB SSD', 1, 1200000, 3),
('PHO001', 'Samsung Galaxy S23', '128GB, 8GB RAM', 2, 650000, 10),
('PHO002', 'iPhone 14', '128GB, Negro', 2, 900000, 8),
('PHO003', 'Motorola Edge 40', '256GB, 8GB RAM', 2, 400000, 15),
('TAB001', 'iPad 10ma Gen', '64GB WiFi', 3, 550000, 8),
('TAB002', 'Samsung Galaxy Tab S9', '128GB, Negro', 3, 480000, 6),
('CPU001', 'Procesador Intel Core i7-13700K', '16 núcleos, 3.4GHz', 4, 320000, 10),
('RAM001', 'Memoria Kingston 16GB DDR4', '3200MHz', 4, 45000, 20),
('SSD001', 'Disco SSD Samsung 1TB', 'M.2 NVMe', 4, 85000, 15),
('MOU001', 'Mouse Logitech G502', 'Gaming RGB', 5, 45000, 25),
('TEC001', 'Teclado Mecánico Redragon', 'RGB, Switch Blue', 5, 38000, 20),
('MON001', 'Monitor LG 27" 4K', 'IPS, 60Hz', 5, 280000, 8),
('AUR001', 'Auriculares Sony WH-1000XM5', 'Noise Cancelling', 6, 320000, 12),
('CON001', 'PlayStation 5', '825GB SSD', 7, 750000, 5),
('CON002', 'Xbox Series X', '1TB SSD', 7, 680000, 5);

-- Insertar Inventario (stock inicial en cada sucursal)
DECLARE @ProductoId INT, @SucursalId INT;
DECLARE ProductoCursor CURSOR FOR SELECT Id FROM Productos;

OPEN ProductoCursor;
FETCH NEXT FROM ProductoCursor INTO @ProductoId;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Stock para cada sucursal
    INSERT INTO Inventario (ProductoId, SucursalId, StockActual) 
    VALUES 
        (@ProductoId, 1, FLOOR(RAND() * 50) + 10), -- Centro
        (@ProductoId, 2, FLOOR(RAND() * 50) + 10), -- Norte
        (@ProductoId, 3, FLOOR(RAND() * 50) + 10); -- Sur
    
    FETCH NEXT FROM ProductoCursor INTO @ProductoId;
END

CLOSE ProductoCursor;
DEALLOCATE ProductoCursor;

-- Insertar Tipos de Cliente
INSERT INTO TiposCliente (Nombre, PorcentajeDescuento, MontoMinimoCompra, Descripcion) VALUES
('Minorista', 0, 0, 'Cliente final, sin descuentos especiales'),
('Mayorista', 15, 500000, 'Descuento 15% en compras superiores a $500.000'),
('Distribuidor', 25, 2000000, 'Descuento 25% en compras superiores a $2.000.000');

-- Insertar Clientes
INSERT INTO Clientes (TipoClienteId, RazonSocial, CUIT, Direccion, Telefono, Email, LimiteCredito) VALUES
(1, 'Juan Pérez', '20-12345678-9', 'Sarmiento 123, Rosario', '341-4567890', 'juan.perez@email.com', 0),
(1, 'María González', '27-98765432-1', 'Mitre 456, Rosario', '341-7654321', 'maria.gonzalez@email.com', 0),
(2, 'TecnoHouse S.R.L.', '30-11223344-5', 'San Martín 789, Rosario', '341-1112233', 'ventas@tecnohouse.com', 5000000),
(2, 'ElectroMax S.A.', '30-55667788-9', 'Belgrano 1011, Rosario', '341-4445566', 'compras@electromax.com', 8000000),
(3, 'MegaDistribuidora S.A.', '30-99887766-3', 'Av. Francia 1500, Rosario', '341-8889999', 'info@megadist.com', 15000000);

-- Insertar Métodos de Pago
INSERT INTO MetodosPago (Nombre, RequiereValidacion) VALUES
('Efectivo', 0),
('Tarjeta de Débito', 1),
('Tarjeta de Crédito', 1),
('Transferencia Bancaria', 1),
('Cuenta Corriente', 1);

-- Insertar Vendedores
INSERT INTO Vendedores (Nombre, Apellido, Email, Telefono, SucursalId) VALUES
('Carlos', 'Rodríguez', 'carlos.rodriguez@techstore.com', '341-1001000', 1),
('Laura', 'Martínez', 'laura.martinez@techstore.com', '341-1001001', 1),
('Diego', 'Fernández', 'diego.fernandez@techstore.com', '341-1001002', 2),
('Ana', 'López', 'ana.lopez@techstore.com', '341-1001003', 2),
('Roberto', 'Sánchez', 'roberto.sanchez@techstore.com', '341-1001004', 3);

-- ========================================
-- VISTAS ÚTILES PARA REPORTES
-- ========================================

-- Vista: Productos con Stock Total
GO
CREATE VIEW vw_ProductosConStock AS
SELECT 
    p.Id,
    p.Codigo,
    p.Nombre,
    c.Nombre AS Categoria,
    p.PrecioUnitario,
    SUM(i.StockActual) AS StockTotal,
    p.Activo
FROM Productos p
INNER JOIN Categorias c ON p.CategoriaId = c.Id
LEFT JOIN Inventario i ON p.Id = i.ProductoId
GROUP BY p.Id, p.Codigo, p.Nombre, c.Nombre, p.PrecioUnitario, p.Activo;
GO

-- Vista: Ventas con Detalles
CREATE VIEW vw_VentasCompletas AS
SELECT 
    v.Id AS VentaId,
    v.NumeroFactura,
    v.FechaVenta,
    c.RazonSocial AS Cliente,
    tc.Nombre AS TipoCliente,
    vend.Nombre + ' ' + vend.Apellido AS Vendedor,
    s.Nombre AS Sucursal,
    mp.Nombre AS MetodoPago,
    v.Subtotal,
    v.MontoDescuento,
    v.Total,
    v.Estado
FROM Ventas v
INNER JOIN Clientes c ON v.ClienteId = c.Id
INNER JOIN TiposCliente tc ON c.TipoClienteId = tc.Id
INNER JOIN Vendedores vend ON v.VendedorId = vend.Id
INNER JOIN Sucursales s ON v.SucursalId = s.Id
INNER JOIN MetodosPago mp ON v.MetodoPagoId = mp.Id;
GO

-- ========================================
-- STORED PROCEDURES ÚTILES
-- ========================================

-- SP: Registrar una venta nueva
CREATE PROCEDURE sp_RegistrarVenta
    @NumeroFactura VARCHAR(20),
    @ClienteId INT,
    @VendedorId INT,
    @SucursalId INT,
    @MetodoPagoId INT,
    @Observaciones VARCHAR(500) = NULL,
    @VentaId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    
    BEGIN TRY
        -- Insertar la venta (inicialmente con totales en 0)
        INSERT INTO Ventas (NumeroFactura, ClienteId, VendedorId, SucursalId, MetodoPagoId, 
                           Subtotal, MontoDescuento, Total, Observaciones)
        VALUES (@NumeroFactura, @ClienteId, @VendedorId, @SucursalId, @MetodoPagoId, 
                0, 0, 0, @Observaciones);
        
        SET @VentaId = SCOPE_IDENTITY();
        
        COMMIT TRANSACTION;
        RETURN 0;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

-- SP: Agregar detalle de venta y actualizar inventario
CREATE PROCEDURE sp_AgregarDetalleVenta
    @VentaId INT,
    @ProductoId INT,
    @SucursalId INT,
    @Cantidad INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    
    BEGIN TRY
        DECLARE @PrecioUnitario DECIMAL(18,2);
        DECLARE @Subtotal DECIMAL(18,2);
        DECLARE @StockDisponible INT;
        
        -- Obtener precio del producto
        SELECT @PrecioUnitario = PrecioUnitario FROM Productos WHERE Id = @ProductoId;
        
        -- Verificar stock disponible
        SELECT @StockDisponible = StockActual 
        FROM Inventario 
        WHERE ProductoId = @ProductoId AND SucursalId = @SucursalId;
        
        IF @StockDisponible < @Cantidad
        BEGIN
            RAISERROR('Stock insuficiente', 16, 1);
            RETURN -1;
        END
        
        -- Calcular subtotal
        SET @Subtotal = @PrecioUnitario * @Cantidad;
        
        -- Insertar detalle de venta
        INSERT INTO DetallesVenta (VentaId, ProductoId, Cantidad, PrecioUnitario, Subtotal)
        VALUES (@VentaId, @ProductoId, @Cantidad, @PrecioUnitario, @Subtotal);
        
        -- Actualizar inventario
        UPDATE Inventario 
        SET StockActual = StockActual - @Cantidad,
            UltimaActualizacion = GETDATE()
        WHERE ProductoId = @ProductoId AND SucursalId = @SucursalId;
        
        COMMIT TRANSACTION;
        RETURN 0;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

-- SP: Calcular y actualizar totales de la venta
CREATE PROCEDURE sp_CalcularTotalesVenta
    @VentaId INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    
    BEGIN TRY
        DECLARE @Subtotal DECIMAL(18,2);
        DECLARE @PorcentajeDescuento DECIMAL(5,2);
        DECLARE @MontoDescuento DECIMAL(18,2);
        DECLARE @Total DECIMAL(18,2);
        DECLARE @ClienteId INT;
        DECLARE @TipoClienteId INT;
        
        -- Calcular subtotal de los detalles
        SELECT @Subtotal = SUM(Subtotal) FROM DetallesVenta WHERE VentaId = @VentaId;
        
        -- Obtener tipo de cliente
        SELECT @ClienteId = ClienteId FROM Ventas WHERE Id = @VentaId;
        SELECT @TipoClienteId = TipoClienteId FROM Clientes WHERE Id = @ClienteId;
        
        -- Obtener porcentaje de descuento según tipo de cliente
        SELECT @PorcentajeDescuento = PorcentajeDescuento FROM TiposCliente WHERE Id = @TipoClienteId;
        
        -- Calcular descuento y total
        SET @MontoDescuento = @Subtotal * (@PorcentajeDescuento / 100);
        SET @Total = @Subtotal - @MontoDescuento;
        
        -- Actualizar la venta
        UPDATE Ventas
        SET Subtotal = @Subtotal,
            PorcentajeDescuento = @PorcentajeDescuento,
            MontoDescuento = @MontoDescuento,
            Total = @Total
        WHERE Id = @VentaId;
        
        COMMIT TRANSACTION;
        RETURN 0;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

PRINT '============================================';
PRINT 'Base de datos TechStoreDB creada exitosamente';
PRINT '============================================';
