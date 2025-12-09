-- =====================================================
-- SCRIPTS SQL PARA SIMULAR DATOS DE REPORTES
-- Sistema TechStore - Base de Datos de Prueba
-- =====================================================

USE TechStoreDB;
GO

DELETE FROM CuentasCorrientes;
DELETE FROM DetallesVenta;
DELETE FROM Ventas;
DELETE FROM Inventario;
DELETE FROM Clientes;
DELETE FROM Productos;
DELETE FROM Categorias;
DELETE FROM Vendedores;
DELETE FROM Sucursales;
DELETE FROM MetodosPago;
DELETE FROM TiposCliente;

-- =====================================================
-- 1. SUCURSALES (3 sucursales)
-- =====================================================
SET IDENTITY_INSERT Sucursales ON;

INSERT INTO Sucursales (Id, Nombre, Direccion, Telefono, Email, Activo, FechaCreacion) VALUES
(1, 'Sucursal Centro', 'Av. Córdoba 1500, Rosario', '0341-4567890', 'centro@techstore.com', 1, '2023-01-15'),
(2, 'Sucursal Norte', 'Av. San Martín 3200, Rosario', '0341-4567891', 'norte@techstore.com', 1, '2023-03-20'),
(3, 'Sucursal Sur', 'Bv. Oroño 850, Rosario', '0341-4567892', 'sur@techstore.com', 1, '2023-06-10');

SET IDENTITY_INSERT Sucursales OFF;

-- =====================================================
-- 2. CATEGORÍAS (7 categorías)
-- =====================================================
SET IDENTITY_INSERT Categorias ON;

INSERT INTO Categorias (Id, Nombre, Descripcion, Activo) VALUES
(1, 'Laptops', 'Computadoras portátiles', 1),
(2, 'Periféricos', 'Mouse, teclados, auriculares', 1),
(3, 'Monitores', 'Pantallas y displays', 1),
(4, 'Almacenamiento', 'Discos duros, SSDs, memorias USB', 1),
(5, 'Componentes', 'Procesadores, RAM, placas madre', 1),
(6, 'Audio', 'Auriculares, parlantes, micrófonos', 1),
(7, 'Accesorios', 'Cables, adaptadores, fundas', 1);

SET IDENTITY_INSERT Categorias OFF;

-- =====================================================
-- 3. PRODUCTOS (30 productos variados)
-- =====================================================
SET IDENTITY_INSERT Productos ON;

INSERT INTO Productos (Id, Codigo, Nombre, Descripcion, CategoriaId, PrecioUnitario, StockMinimo, Activo, FechaCreacion) VALUES
-- Laptops (1-5)
(1, 'LAP001', 'Laptop Dell Inspiron 15', 'Intel i5, 8GB RAM, 256GB SSD', 1, 850000.00, 5, 1, '2024-01-10'),
(2, 'LAP002', 'Laptop HP Pavilion', 'Intel i7, 16GB RAM, 512GB SSD', 1, 1200000.00, 3, 1, '2024-01-10'),
(3, 'LAP003', 'Laptop Lenovo ThinkPad', 'Intel i5, 8GB RAM, 256GB SSD', 1, 950000.00, 4, 1, '2024-01-10'),
(4, 'LAP004', 'Laptop ASUS VivoBook', 'AMD Ryzen 5, 8GB RAM, 512GB SSD', 1, 780000.00, 5, 1, '2024-01-10'),
(5, 'LAP005', 'Laptop Acer Aspire', 'Intel i3, 4GB RAM, 128GB SSD', 1, 550000.00, 6, 1, '2024-01-10'),

-- Periféricos (6-12)
(6, 'MOU001', 'Mouse Logitech MX Master 3', 'Inalámbrico, ergonómico', 2, 45000.00, 15, 1, '2024-01-15'),
(7, 'MOU002', 'Mouse Razer DeathAdder', 'Gaming, RGB, 16000 DPI', 2, 38000.00, 20, 1, '2024-01-15'),
(8, 'TEC001', 'Teclado Logitech K380', 'Bluetooth, multidispositivo', 2, 32000.00, 12, 1, '2024-01-15'),
(9, 'TEC002', 'Teclado Mecánico Redragon', 'RGB, switches blue', 2, 55000.00, 10, 1, '2024-01-15'),
(10, 'AUR001', 'Auriculares Sony WH-1000XM4', 'Noise cancelling, Bluetooth', 2, 280000.00, 8, 1, '2024-01-15'),
(11, 'AUR002', 'Auriculares Logitech G733', 'Gaming, inalámbricos', 2, 120000.00, 10, 1, '2024-01-15'),
(12, 'WEB001', 'Webcam Logitech C920', 'Full HD 1080p', 2, 65000.00, 8, 1, '2024-01-15'),

-- Monitores (13-17)
(13, 'MON001', 'Monitor Samsung 24" Full HD', '75Hz, IPS', 3, 180000.00, 6, 1, '2024-01-20'),
(14, 'MON002', 'Monitor LG 27" 4K', 'IPS, HDR', 3, 380000.00, 4, 1, '2024-01-20'),
(15, 'MON003', 'Monitor ASUS 24" Gaming', '144Hz, 1ms', 3, 250000.00, 5, 1, '2024-01-20'),
(16, 'MON004', 'Monitor Dell 27" QHD', 'IPS, USB-C', 3, 420000.00, 3, 1, '2024-01-20'),
(17, 'MON005', 'Monitor AOC 22" Full HD', '60Hz, HDMI', 3, 120000.00, 8, 1, '2024-01-20'),

-- Almacenamiento (18-22)
(18, 'SSD001', 'SSD Kingston 480GB', 'SATA III', 4, 45000.00, 15, 1, '2024-01-25'),
(19, 'SSD002', 'SSD Samsung 970 EVO 1TB', 'NVMe M.2', 4, 95000.00, 10, 1, '2024-01-25'),
(20, 'HDD001', 'Disco Duro Seagate 1TB', '7200 RPM, SATA', 4, 38000.00, 12, 1, '2024-01-25'),
(21, 'USB001', 'Pendrive SanDisk 64GB', 'USB 3.0', 4, 8500.00, 30, 1, '2024-01-25'),
(22, 'EXT001', 'Disco Externo WD 2TB', 'USB 3.0, portátil', 4, 85000.00, 8, 1, '2024-01-25'),

-- Componentes (23-27)
(23, 'RAM001', 'Memoria RAM Corsair 16GB', 'DDR4 3200MHz', 5, 55000.00, 12, 1, '2024-02-01'),
(24, 'RAM002', 'Memoria RAM Kingston 8GB', 'DDR4 2666MHz', 5, 28000.00, 15, 1, '2024-02-01'),
(25, 'GPU001', 'Placa de Video NVIDIA RTX 3060', '12GB GDDR6', 5, 450000.00, 4, 1, '2024-02-01'),
(26, 'PSU001', 'Fuente EVGA 650W', '80+ Bronze', 5, 68000.00, 8, 1, '2024-02-01'),
(27, 'CPU001', 'Procesador Intel i5-12400F', '6 cores, 12 threads', 5, 180000.00, 6, 1, '2024-02-01'),

-- Accesorios (28-30)
(28, 'CAB001', 'Cable HDMI 2m', 'Alta velocidad', 7, 3500.00, 40, 1, '2024-02-05'),
(29, 'ADP001', 'Adaptador USB-C a HDMI', 'Soporte 4K', 7, 12000.00, 20, 1, '2024-02-05'),
(30, 'PAD001', 'Mousepad Gamer XL', '80x35cm', 7, 15000.00, 25, 1, '2024-02-05');

SET IDENTITY_INSERT Productos OFF;

-- =====================================================
-- 4. TIPOS DE CLIENTE
-- =====================================================
SET IDENTITY_INSERT TiposCliente ON;

INSERT INTO TiposCliente (Id, Nombre, PorcentajeDescuento, MontoMinimoCompra, Descripcion) VALUES
(1, 'Minorista', 0.00, 0.00, 'Cliente minorista sin descuento'),
(2, 'Mayorista', 15.00, 500000.00, 'Cliente mayorista con 15% descuento'),
(3, 'Distribuidor', 25.00, 1000000.00, 'Cliente distribuidor con 25% descuento');

SET IDENTITY_INSERT TiposCliente OFF;

-- =====================================================
-- 5. CLIENTES (15 clientes variados)
-- =====================================================
SET IDENTITY_INSERT Clientes ON;

INSERT INTO Clientes (Id, TipoClienteId, RazonSocial, CUIT, Direccion, Telefono, Email, LimiteCredito, SaldoActual, Activo, FechaRegistro) VALUES
-- Minoristas (1-7)
(1, 1, 'Juan Pérez', '20-12345678-9', 'San Lorenzo 1234', '0341-4111111', 'juan.perez@email.com', 100000.00, 0.00, 1, '2024-01-05'),
(2, 1, 'María García', '27-87654321-3', 'Paraguay 5678', '0341-4222222', 'maria.garcia@email.com', 150000.00, 0.00, 1, '2024-01-10'),
(3, 1, 'Carlos Rodríguez', '20-11223344-5', 'Córdoba 910', '0341-4333333', 'carlos.rodriguez@email.com', 80000.00, 0.00, 1, '2024-01-15'),
(4, 1, 'Ana Martínez', '27-55667788-9', 'Santa Fe 1112', '0341-4444444', 'ana.martinez@email.com', 120000.00, 0.00, 1, '2024-02-01'),
(5, 1, 'Luis Fernández', '20-99887766-1', 'Entre Ríos 1314', '0341-4555555', 'luis.fernandez@email.com', 90000.00, 0.00, 1, '2024-02-15'),
(6, 1, 'Laura Gómez', '27-44332211-7', 'Mendoza 1516', '0341-4666666', 'laura.gomez@email.com', 110000.00, 0.00, 1, '2024-03-01'),
(7, 1, 'Diego López', '20-33445566-3', 'Salta 1718', '0341-4777777', 'diego.lopez@email.com', 95000.00, 0.00, 1, '2024-03-15'),

-- Mayoristas (8-12)
(8, 2, 'TecnoShop S.A.', '30-70123456-8', 'Av. Pellegrini 2020', '0341-4888888', 'ventas@tecnoshop.com', 2000000.00, 0.00, 1, '2024-01-08'),
(9, 2, 'CompuMundo S.R.L.', '30-70234567-9', 'Bv. Oroño 3030', '0341-4999999', 'compras@compumundo.com', 1500000.00, 0.00, 1, '2024-01-20'),
(10, 2, 'Electrónica Global', '30-70345678-0', 'San Martín 4040', '0341-4101010', 'info@elglobal.com', 1800000.00, 0.00, 1, '2024-02-10'),
(11, 2, 'InfoTech Mayorista', '30-70456789-1', 'Mitre 5050', '0341-4111011', 'pedidos@infotech.com', 2500000.00, 0.00, 1, '2024-02-25'),
(12, 2, 'Digital Center', '30-70567890-2', 'Rioja 6060', '0341-4121212', 'ventas@digitalcenter.com', 1200000.00, 0.00, 1, '2024-03-10'),

-- Distribuidores (13-15)
(13, 3, 'MegaTech Distribuidora', '30-80123456-7', 'Av. Circunvalación 7070', '0341-4131313', 'logistica@megatech.com', 5000000.00, 0.00, 1, '2024-01-12'),
(14, 3, 'TotalTech Argentina', '30-80234567-8', 'Ruta 9 Km 305', '0341-4141414', 'distribucin@totaltech.com', 6000000.00, 0.00, 1, '2024-02-05'),
(15, 3, 'ProveeTech Mayorista', '30-80345678-9', 'Autopista Rosario-Bs.As.', '0341-4151515', 'ventas@proveetech.com', 4500000.00, 0.00, 1, '2024-02-20');

SET IDENTITY_INSERT Clientes OFF;

-- =====================================================
-- 6. VENDEDORES (8 vendedores distribuidos por sucursal)
-- =====================================================
SET IDENTITY_INSERT Vendedores ON;

INSERT INTO Vendedores (Id, Nombre, Apellido, Email, Telefono, SucursalId, Activo, FechaIngreso) VALUES
-- Sucursal Centro (3 vendedores)
(1, 'Roberto', 'Sánchez', 'roberto.sanchez@techstore.com', '0341-15111111', 1, 1, '2023-02-01'),
(2, 'Patricia', 'Díaz', 'patricia.diaz@techstore.com', '0341-15222222', 1, 1, '2023-03-15'),
(3, 'Fernando', 'Castro', 'fernando.castro@techstore.com', '0341-15333333', 1, 1, '2023-05-20'),

-- Sucursal Norte (3 vendedores)
(4, 'Gabriela', 'Morales', 'gabriela.morales@techstore.com', '0341-15444444', 2, 1, '2023-04-10'),
(5, 'Martín', 'Ruiz', 'martin.ruiz@techstore.com', '0341-15555555', 2, 1, '2023-06-05'),
(6, 'Valeria', 'Ortiz', 'valeria.ortiz@techstore.com', '0341-15666666', 2, 1, '2023-07-15'),

-- Sucursal Sur (2 vendedores)
(7, 'Adrián', 'Navarro', 'adrian.navarro@techstore.com', '0341-15777777', 3, 1, '2023-07-01'),
(8, 'Carolina', 'Silva', 'carolina.silva@techstore.com', '0341-15888888', 3, 1, '2023-08-20');

SET IDENTITY_INSERT Vendedores OFF;

-- =====================================================
-- 7. MÉTODOS DE PAGO
-- =====================================================
SET IDENTITY_INSERT MetodosPago ON;

INSERT INTO MetodosPago (Id, Nombre, RequiereValidacion, Activo) VALUES
(1, 'Efectivo', 0, 1),
(2, 'Tarjeta de Débito', 1, 1),
(3, 'Tarjeta de Crédito', 1, 1),
(4, 'Transferencia Bancaria', 1, 1),
(5, 'Mercado Pago', 1, 1);

SET IDENTITY_INSERT MetodosPago OFF;

-- =====================================================
-- 8. INVENTARIO (Stock inicial para todos los productos en todas las sucursales)
-- =====================================================
SET IDENTITY_INSERT Inventario ON;

DECLARE @ProductoId INT = 1;
DECLARE @SucursalId INT;
DECLARE @InventarioId INT = 1;

WHILE @ProductoId <= 30
BEGIN
    SET @SucursalId = 1;
    
    WHILE @SucursalId <= 3
    BEGIN
        -- Stock variable según producto y sucursal
        DECLARE @Stock INT = CASE 
            WHEN @ProductoId <= 5 THEN 8 + (@SucursalId * 2)  -- Laptops: 10-14
            WHEN @ProductoId <= 12 THEN 25 + (@SucursalId * 5) -- Periféricos: 30-40
            WHEN @ProductoId <= 17 THEN 10 + (@SucursalId * 3) -- Monitores: 13-19
            WHEN @ProductoId <= 22 THEN 20 + (@SucursalId * 4) -- Almacenamiento: 24-32
            WHEN @ProductoId <= 27 THEN 12 + (@SucursalId * 3) -- Componentes: 15-21
            ELSE 50 + (@SucursalId * 10) -- Accesorios: 60-80
        END;
        
        INSERT INTO Inventario (Id, ProductoId, SucursalId, StockActual, StockReservado, UltimaActualizacion)
        VALUES (@InventarioId, @ProductoId, @SucursalId, @Stock, 0, GETDATE());
        
        SET @InventarioId = @InventarioId + 1;
        SET @SucursalId = @SucursalId + 1;
    END
    
    SET @ProductoId = @ProductoId + 1;
END

SET IDENTITY_INSERT Inventario OFF;

-- =====================================================
-- 9. VENTAS (80 ventas distribuidas en los últimos 6 meses)
-- =====================================================
SET IDENTITY_INSERT Ventas ON;

-- Variables para generar ventas
DECLARE @VentaId INT = 1;
DECLARE @NumFactura VARCHAR(20);
DECLARE @FechaVenta DATETIME;
DECLARE @ClienteIdRandom INT;
DECLARE @VendedorIdRandom INT;
DECLARE @SucursalIdRandom INT;
DECLARE @MetodoPagoIdRandom INT;

-- Generar 80 ventas
WHILE @VentaId <= 80
BEGIN
    -- Fecha aleatoria en los últimos 6 meses
    SET @FechaVenta = DATEADD(DAY, -ABS(CHECKSUM(NEWID()) % 180), GETDATE());
    
    -- Cliente aleatorio (ponderado: 60% minoristas, 30% mayoristas, 10% distribuidores)
    SET @ClienteIdRandom = CASE 
        WHEN ABS(CHECKSUM(NEWID()) % 100) < 60 THEN (ABS(CHECKSUM(NEWID()) % 7) + 1)  -- Minoristas 1-7
        WHEN ABS(CHECKSUM(NEWID()) % 100) < 90 THEN (ABS(CHECKSUM(NEWID()) % 5) + 8)  -- Mayoristas 8-12
        ELSE (ABS(CHECKSUM(NEWID()) % 3) + 13)  -- Distribuidores 13-15
    END;
    
    -- Vendedor y sucursal aleatorios
    SET @VendedorIdRandom = (ABS(CHECKSUM(NEWID()) % 8) + 1);  -- 1-8
    SET @SucursalIdRandom = (ABS(CHECKSUM(NEWID()) % 3) + 1);  -- 1-3
    SET @MetodoPagoIdRandom = (ABS(CHECKSUM(NEWID()) % 5) + 1);  -- 1-5
    
    -- Número de factura
    SET @NumFactura = 'FAC-' + RIGHT('00000' + CAST(@VentaId AS VARCHAR), 5);
    
    -- Insertar venta (totales se calcularán después con los detalles)
    INSERT INTO Ventas (Id, NumeroFactura, ClienteId, VendedorId, SucursalId, FechaVenta, Subtotal, PorcentajeDescuento, MontoDescuento, Total, MetodoPagoId, Estado, Observaciones)
    VALUES (@VentaId, @NumFactura, @ClienteIdRandom, @VendedorIdRandom, @SucursalIdRandom, @FechaVenta, 0, 0, 0, 0, @MetodoPagoIdRandom, 'Completada', NULL);
    
    SET @VentaId = @VentaId + 1;
END

SET IDENTITY_INSERT Ventas OFF;

-- =====================================================
-- 10. DETALLES DE VENTA (2-6 productos por venta)
-- =====================================================
SET IDENTITY_INSERT DetallesVenta ON;

DECLARE @DetalleId INT = 1;
DECLARE @VentaIdLoop INT = 1;
DECLARE @CantidadProductos INT;
DECLARE @ProductoLoop INT;
DECLARE @ProductoIdRandom INT;
DECLARE @Cantidad INT;
DECLARE @PrecioUnitario DECIMAL(18,2);
DECLARE @SubtotalDetalle DECIMAL(18,2);

WHILE @VentaIdLoop <= 80
BEGIN
    -- Cantidad de productos por venta (2-6)
    SET @CantidadProductos = (ABS(CHECKSUM(NEWID()) % 5) + 2);
    SET @ProductoLoop = 0;
    
    WHILE @ProductoLoop < @CantidadProductos
    BEGIN
        -- Producto aleatorio (con ponderación: más ventas de productos baratos)
        SET @ProductoIdRandom = CASE 
            WHEN ABS(CHECKSUM(NEWID()) % 100) < 40 THEN (ABS(CHECKSUM(NEWID()) % 13) + 6)  -- Periféricos (más vendidos)
            WHEN ABS(CHECKSUM(NEWID()) % 100) < 70 THEN (ABS(CHECKSUM(NEWID()) % 10) + 18) -- Almacenamiento
            WHEN ABS(CHECKSUM(NEWID()) % 100) < 85 THEN (ABS(CHECKSUM(NEWID()) % 5) + 13)  -- Monitores
            ELSE (ABS(CHECKSUM(NEWID()) % 5) + 1)  -- Laptops (menos vendidas pero más caras)
        END;
        
        -- Cantidad (1-5)
        SET @Cantidad = (ABS(CHECKSUM(NEWID()) % 5) + 1);
        
        -- Obtener precio del producto
        SELECT @PrecioUnitario = PrecioUnitario FROM Productos WHERE Id = @ProductoIdRandom;
        SET @SubtotalDetalle = @Cantidad * @PrecioUnitario;
        
        -- Insertar detalle
        INSERT INTO DetallesVenta (Id, VentaId, ProductoId, Cantidad, PrecioUnitario, Subtotal)
        VALUES (@DetalleId, @VentaIdLoop, @ProductoIdRandom, @Cantidad, @PrecioUnitario, @SubtotalDetalle);
        
        SET @DetalleId = @DetalleId + 1;
        SET @ProductoLoop = @ProductoLoop + 1;
    END
    
    SET @VentaIdLoop = @VentaIdLoop + 1;
END

SET IDENTITY_INSERT DetallesVenta OFF;

-- =====================================================
-- 11. ACTUALIZAR TOTALES DE VENTAS CON DESCUENTOS
-- =====================================================
UPDATE V
SET 
    V.Subtotal = D.TotalDetalles,
    V.PorcentajeDescuento = ISNULL(TC.PorcentajeDescuento, 0),
    V.MontoDescuento = (D.TotalDetalles * ISNULL(TC.PorcentajeDescuento, 0) / 100),
    V.Total = D.TotalDetalles - (D.TotalDetalles * ISNULL(TC.PorcentajeDescuento, 0) / 100)
FROM Ventas V
INNER JOIN (
    SELECT VentaId, SUM(Subtotal) AS TotalDetalles
    FROM DetallesVenta
    GROUP BY VentaId
) D ON V.Id = D.VentaId
INNER JOIN Clientes C ON V.ClienteId = C.Id
LEFT JOIN TiposCliente TC ON C.TipoClienteId = TC.Id;

-- =====================================================
-- 12. ACTUALIZAR INVENTARIO (descontar stock vendido)
-- =====================================================
UPDATE I
SET I.StockActual = I.StockActual - ISNULL(V.CantidadVendida, 0)
FROM Inventario I
INNER JOIN (
    SELECT 
        DV.ProductoId,
        V.SucursalId,
        SUM(DV.Cantidad) AS CantidadVendida
    FROM DetallesVenta DV
    INNER JOIN Ventas V ON DV.VentaId = V.Id
    GROUP BY DV.ProductoId, V.SucursalId
) V ON I.ProductoId = V.ProductoId AND I.SucursalId = V.SucursalId;

GO

-- =====================================================
-- CONSULTAS DE VERIFICACIÓN
-- =====================================================

-- Ver resumen de ventas por mes
SELECT 
    FORMAT(FechaVenta, 'yyyy-MM') AS Mes,
    COUNT(*) AS CantidadVentas,
    SUM(Total) AS TotalVentas
FROM Ventas
GROUP BY FORMAT(FechaVenta, 'yyyy-MM')
ORDER BY Mes DESC;

-- Ver productos más vendidos
SELECT TOP 10
    P.Nombre,
    SUM(DV.Cantidad) AS CantidadVendida,
    SUM(DV.Subtotal) AS TotalVentas
FROM DetallesVenta DV
INNER JOIN Productos P ON DV.ProductoId = P.Id
GROUP BY P.Id, P.Nombre
ORDER BY CantidadVendida DESC;

-- Ver ventas por vendedor
SELECT 
    V.Nombre + ' ' + V.Apellido AS Vendedor,
    COUNT(VT.Id) AS CantidadVentas,
    SUM(VT.Total) AS TotalVentas
FROM Ventas VT
INNER JOIN Vendedores V ON VT.VendedorId = V.Id
GROUP BY V.Id, V.Nombre, V.Apellido
ORDER BY TotalVentas DESC;

-- Ver ventas por sucursal
SELECT 
    S.Nombre AS Sucursal,
    COUNT(V.Id) AS CantidadVentas,
    SUM(V.Total) AS TotalVentas
FROM Ventas V
INNER JOIN Sucursales S ON V.SucursalId = S.Id
GROUP BY S.Id, S.Nombre
ORDER BY TotalVentas DESC;

-- Ver estado del inventario
SELECT 
    P.Nombre,
    S.Nombre AS Sucursal,
    I.StockActual,
    P.StockMinimo,
    CASE 
        WHEN I.StockActual <= P.StockMinimo THEN 'BAJO'
        WHEN I.StockActual <= P.StockMinimo * 2 THEN 'MEDIO'
        ELSE 'OK'
    END AS Estado
FROM Inventario I
INNER JOIN Productos P ON I.ProductoId = P.Id
INNER JOIN Sucursales S ON I.SucursalId = S.Id
ORDER BY Estado, P.Nombre;

PRINT '✓ Scripts ejecutados exitosamente';
PRINT '✓ Base de datos poblada con datos de prueba';
PRINT '✓ 80 ventas generadas en los últimos 6 meses';
PRINT '✓ 30 productos en inventario';
PRINT '✓ 15 clientes (7 minoristas, 5 mayoristas, 3 distribuidores)';
PRINT '✓ 8 vendedores en 3 sucursales';
