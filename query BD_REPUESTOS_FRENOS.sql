USE master
GO

IF db_id('BDREPUESTOSFRENOS') is not null
begin
	ALTER DATABASE BDREPUESTOSFRENOS
	SET SINGLE_USER WITH ROLLBACK IMMEDIATE

	DROP DATABASE BDREPUESTOSFRENOS
end
go

CREATE DATABASE BDREPUESTOSFRENOS
COLLATE Modern_Spanish_CI_AI
GO

SET LANGUAGE SPANISH
SET NOCOUNT ON
GO

USE BDREPUESTOSFRENOS
GO

CREATE TABLE DISTRITOS (
	cod_dist char(5) NOT NULL primary key,
	nom_dist varchar(35) not null
)
GO


CREATE TABLE PROVEEDOR
(
	cod_prov varchar (5) NOT NULL Primary key,
	nom_prov varchar (30)NOT NULL ,
	pais_origen varchar (30) NULL,
	email varchar (100) null,
	telefono varchar(20) NULL,
	eli_prov char(2) null default ('NO')
)

CREATE TABLE CATEGORIA 
(
	cod_cate varchar (5) NOT NULL Primary key CHECK(cod_cate like 'CA[0-9][0-9][0-9]'),
	nom_cate varchar (30)NOT NULL ,
	descripcion VARCHAR(200)
)

CREATE TABLE PRODUCTOS
(
	cod_produc char (5) NOT NULL Primary key check(cod_produc like 'P[0-9][0-9][0-9][0-9]'),
	nom_pro varchar (50) NULL ,
	uni_med varchar (10) NULL ,
	pre_pro decimal(7,2) NULL ,
	stk_pro int NULL ,
	mat_pro varchar (20) NULL,
	fec_ingreso date null,
	cod_prov varchar (5) NOT NULL REFERENCES PROVEEDOR,
	cod_cate varchar (5) NOT NULL REFERENCES CATEGORIA,
	pro_eli char(2) default ('No')
)

CREATE TABLE ROLES
(
	cod_rol char(3) not null primary key,
	nom_rol char(15) not null 
)

CREATE TABLE USUARIO
(
	cod_usu char (5) NOT NULL Primary Key check(cod_usu like 'U[0-9][0-9][0-9][0-9]'),
	nom_usu varchar(50) NOT NULL ,
	ape_usu varchar(50) NOT NULL,
	tel_usu varchar(10) default('Sin telefono'),
	cor_usu varchar (50) NOT NULL Unique,
	pass_usu varchar(30) NOT NULL,
	fec_nac date default('10/03/2000'),
	cod_dist char(5) NOT NULL references DISTRITOS ,
	cod_rol char(3) not null references ROLES DEFAULT ('R02')
)
go


CREATE TABLE Ventas (
	num_vta char (5) NOT NULL Primary Key,
	fec_vta date NULL ,
	cod_usu char (5) NOT NULL REFERENCES USUARIO ,
	IGV DECIMAL(7,2) NULL,
	total_vent decimal(8,2) NULL
)
GO

CREATE TABLE DetalleVenta (
	num_vta char (5) NOT NULL REFERENCES Ventas,
	cod_produc char (5) NOT NULL REFERENCES PRODUCTOS ,
	precio decimal (7,2),
	cantidad  int NULL , 
        Primary Key(num_vta,cod_produc))
GO

-- Ingreso de Data a las Tablas
set dateformat dmy
set language spanish
go

INSERT INTO ROLES VALUES ('R01','Admisntrador'),
						 ('R02','Cliente')

INSERT INTO Distritos VALUES ('D0001', 'Lima'),
	('D0002', 'Los Olivos'),('D0003', 'Magdalena'),('D0004', 'Pueblo Libre'),
	('D0005', 'Rimac'),('D0006', 'San Martin de Porres'),('D0007', 'Jesus Maria'),
	('D0008', 'Lince'),('D0009', 'Miraflores'),('D0010', 'Surco'),
	('D0011', 'SJL'), ('D0012', 'Breña'), ('D0013','Independencia'), ('D0014','Callao')


INSERT INTO PROVEEDOR (cod_prov, nom_prov, pais_origen, email,telefono,eli_prov) VALUES
('PV001','Knorr-Bremse AG', 'Alemania', 'contact@knorr-bremse.com', '+49 89 35470',DEFAULT),
('PV002','Quinelato S.A.', 'Brasil', 'atendimento@quinelato.com.br', '+55 16 2132 9800',DEFAULT),
('PV003','WABCO Holdings Inc.', 'Bélgica', 'info@wabco-auto.com', '+32 2 456 1100',DEFAULT),
('PV004','Frenosa S.A.', 'Perú', 'ventas@frenosa.com.pe', '+51 1 613 4141',DEFAULT),
('PV005','WABCO Holdings Inc.', 'Bélgica', 'info@wabco-auto.com', '+32 2 456 1100',DEFAULT),
('PV006','Fras-le S.A.', 'Brasil', 'export@fras-le.com', '+55 54 3239 1000',DEFAULT),
('PV007','TRP Parts', 'México', 'contacto@trpparts.mx', '+52 55 5555 1212',DEFAULT),
('PV008','Aumark Autopartes', 'Perú', 'info@aumark.pe', '+51 1 467 8901',DEFAULT);
go

INSERT INTO CATEGORIA (cod_cate, nom_cate,descripcion) VALUES
('CA001','Aceites y Lubricantes', 'Aceites de motor, transmisión, hidráulicos y aditivos de mantenimiento.'),
('CA002','Válvula de Control', 'Regula la presión y dirección del aire'),
('CA003','Filtros', 'Filtros de aceite, aire, combustible y cabina para distintos vehículos.'),
('CA004','Frenos', 'Discos, pastillas, tambores y líquidos para sistemas de frenos.'),
('CA005','Sistema de Escape', 'Silenciadores, catalizadores y tuberías de escape.');


INSERT INTO PRODUCTOS (cod_produc, nom_pro, uni_med, pre_pro, stk_pro, mat_pro, fec_ingreso, cod_prov, cod_cate) VALUES
('P0001', 'Aceite Sintético 5W30', 'Litros', 65.90, 120, 'Aceite', '2025-01-10', 'PV008', 'CA001'),
('P0002', 'Aceite Mineral 20W50', 'Litros', 38.50, 200, 'Aceite', '2025-01-12', 'PV004', 'CA001'),
('P0003', 'Válvula de Control EBS', 'Unidad', 325.00, 25, 'Aluminio', '2025-01-15', 'PV001', 'CA002'),
('P0004', 'Válvula de Modulación ABS', 'Unidad', 290.00, 18, 'Aluminio', '2025-01-16', 'PV003', 'CA002'),
('P0005', 'Filtro de Aire', 'Unidad', 45.00, 80, 'Fibra', '2025-01-20', 'PV007', 'CA003'),
('P0006', 'Filtro de Combustible', 'Unidad', 52.00, 60, 'Metal', '2025-01-22', 'PV008', 'CA003'),
('P0007', 'Pastillas de Freno Delanteras', 'Jgo', 120.00, 40, 'Cerámica', '2025-01-25', 'PV002', 'CA004'),
('P0008', 'Disco de Freno Ventilado', 'Unidad', 210.00, 25, 'Acero', '2025-01-28', 'PV006', 'CA004'),
('P0009', 'Tambor de Freno', 'Unidad', 185.50, 30, 'Hierro fundido', '2025-02-02', 'PV004', 'CA004'),
('P0010', 'Silenciador Universal', 'Unidad', 175.00, 15, 'Acero Inox.', '2025-02-05', 'PV006', 'CA005'),
('P0011', 'Tubo de Escape Curvo', 'Unidad', 89.90, 20, 'Acero', '2025-02-07', 'PV007', 'CA005'),
('P0012', 'Aceite Semi-Sintético 10W40', 'Litros', 58.90, 100, 'Aceite', '2025-02-10', 'PV004', 'CA001'),
('P0013', 'Filtro de Aceite Motor Diesel', 'Unidad', 48.00, 70, 'Metal', '2025-02-12', 'PV008', 'CA003'),
('P0014', 'Válvula de Descarga de Aire', 'Unidad', 150.00, 25, 'Aluminio', '2025-02-14', 'PV001', 'CA002'),
('P0015', 'Válvula de Freno de Mano', 'Unidad', 190.00, 12, 'Metal', '2025-02-16', 'PV003', 'CA002'),
('P0016', 'Juego de Pastillas TRP', 'Jgo', 135.00, 35, 'Cerámica', '2025-02-18', 'PV007', 'CA004'),
('P0017', 'Heve de Freno con Gancho', 'Unidad', 30.00, 50, 'Acero', '2025-02-24', 'PV006', 'CA004'),
('P0018', 'Disco de Freno Ventilado HD', 'Unidad', 250.00, 20, 'Acero', '2025-02-25', 'PV006', 'CA004'),
('P0019', 'Aceite Hidráulico ATF', 'Litros', 45.00, 90, 'Aceite', '2025-03-01', 'PV008', 'CA001'),
('P0020', 'Filtro Cabina Antialérgico', 'Unidad', 35.00, 55, 'Fibra', '2025-03-03', 'PV008', 'CA003'),
('P0021', 'Heve de Retorno para Válvula', 'Unidad', 18.50, 75, 'Metal', '2025-03-04', 'PV004', 'CA004'),
('P0022', 'Tambor de Freno 16”', 'Unidad', 210.00, 22, 'Hierro fundido', '2025-03-05', 'PV004', 'CA004'),
('P0023', 'Filtro de Aire Alto Flujo', 'Unidad', 72.00, 40, 'Sintético', '2025-03-07', 'PV007', 'CA003');
go

INSERT INTO USUARIO VALUES('U0001','Pedro','Garcia','960752451','pedro@gmail.com','pedro123','01/04/2000','D0001','R01')
GO
------------------------------------------------------------------
-----------------------------------------------------------------
--STORE PROCEDURE

CREATE OR ALTER PROC SP_LISTA_CATEGORIA
AS
	SELECT * FROM CATEGORIA
GO
-------------------------------------------
--PRODUCTO
CREATE OR ALTER PROC SP_LISTA_PRODUCTOS
@NAME VARCHAR(50)='%'
AS
	SELECT P.cod_produc,P.nom_pro,P.uni_med,P.pre_pro,P.stk_pro,
	P.mat_pro,P.fec_ingreso,P.cod_prov,PV.nom_prov,P.cod_cate,C.nom_cate
	FROM PRODUCTOS P JOIN PROVEEDOR PV ON P.cod_prov = PV.cod_prov 
	JOIN CATEGORIA C ON P.cod_cate = C.cod_cate
	WHERE P.pro_eli = 'NO' AND P.nom_pro LIKE @NAME+'%'
GO

CREATE OR ALTER PROC SP_LISTA_PRODUCTOS_CATE
@COD_CATE CHAR(5),@NAME VARCHAR(50)='%'
AS
	SELECT P.cod_produc,P.nom_pro,P.uni_med,P.pre_pro,P.stk_pro,
	P.mat_pro,P.fec_ingreso,P.cod_prov,PV.nom_prov,P.cod_cate,C.nom_cate
	FROM PRODUCTOS P JOIN PROVEEDOR PV ON P.cod_prov = PV.cod_prov 
	JOIN CATEGORIA C ON P.cod_cate = C.cod_cate
	WHERE P.pro_eli = 'NO' AND P.nom_pro LIKE @NAME+'%' AND P.cod_cate = @COD_CATE
GO
EXEC SP_LISTA_PRODUCTOS_CATE 'CA001','V'
GO
--EXEC SP_LISTA_PRODUCTOS ''

CREATE OR ALTER PROC PA_GRABAR_PRODUCTO
@NOM_PRO VARCHAR(50),@UNI_MED VARCHAR(10),
@PRE_PRO DECIMAL(7,2),@STK INT,@MATERIAL VARCHAR(20),@fec DATE
,@COD_PROV CHAR(5),@COD_CATE CHAR(5)
AS
	-- DECLARANDO VARIABLE PARA EL NUEVO NUMERO DE LA VENTA
	DECLARE @NUMERO VARCHAR(5) 
	-- RECUPERANDO EL MAXIMO NUMERO DE VENTA
	SELECT @NUMERO=RIGHT(MAX(cod_produc),4)+1 FROM PRODUCTOS
	-- GENERANDO EL NUEVO NUMERO DE LA VENTA
	SELECT @NUMERO='P'+RIGHT('000'+@NUMERO,4)

	-----------------------------------------------
	-- INSERTANDO LOS DATOS DE LA NUEVA VENTA
	INSERT INTO PRODUCTOS VALUES(@NUMERO,
	@NOM_PRO, @UNI_MED, @PRE_PRO,@STK,@MATERIAL,@fec,@COD_PROV,@COD_CATE,DEFAULT)
	-- DEVOLVIENDO EL NUEVO NUMERO DE VENTA GENERADO
	SELECT @NUMERO AS NUMERO
GO
--EXEC PA_GRABAR_PRODUCTO 'Manguera de aire','Paquete',30.50,100,'Plastico','PV001','CA002'


CREATE OR ALTER PROC SP_UPDATE_PRODUCTO
@COD_PRO CHAR(5),@NOM_PRO VARCHAR(50),@UNI_MED VARCHAR(10),
@PRE_PRO DECIMAL(7,2),@STK INT,@MATERIAL VARCHAR(20),@COD_PROV CHAR(5),
@COD_CATE CHAR(5)
AS
UPDATE PRODUCTOS
SET nom_pro = @NOM_PRO,uni_med=@UNI_MED,pre_pro=@PRE_PRO,stk_pro=stk_pro+@STK,
	mat_pro=@MATERIAL,cod_prov=@COD_PROV,cod_cate=@COD_CATE
WHERE cod_produc = @COD_PRO
GO

--EXEC SP_UPDATE_PRODUCTO 'P0001','Cámara de Freno','Paquete',50.50,40,'Acero','PV001','CA001'

CREATE OR ALTER PROC SP_DELETE_PRODUCTO
@COD_PRO CHAR(5)
AS
	UPDATE PRODUCTOS
	SET pro_eli = 'SI'
	WHERE cod_produc = @COD_PRO
GO
--EXEC SP_DELETE_PRODUCTO 'P0012'

CREATE OR ALTER PROC SP_UPDATE_STOCK_PRODUCTO
@COD_PRO CHAR(5),@STOCK INT
AS
	UPDATE PRODUCTOS
	SET stk_pro = stk_pro + @STOCK
	WHERE cod_produc = @COD_PRO
	SELECT stk_pro FROM PRODUCTOS WHERE cod_produc = @COD_PRO 
GO

--EXEC SP_UPDATE_STOCK_PRODUCTO 'P0001',20
--GO

------------------------------------------------------------------
--USUARIO

CREATE OR ALTER PROC SP_LISTA_USUARIO
@APE VARCHAR(50)='%'
AS
	SELECT * FROM USUARIO
	WHERE cod_rol = 'R02' AND ape_usu LIKE @APE+'%'
GO
CREATE OR ALTER PROC SP_LISTA_ADMIN
@APE VARCHAR(50)='%'
AS
	SELECT * FROM USUARIO
	WHERE cod_rol = 'R01' AND ape_usu LIKE @APE+'%'
GO


CREATE OR ALTER PROC SP_VALIDAR_USUARIO
@EMAIL VARCHAR(50),@PASSWORD VARCHAR(30)
AS
	SELECT * FROM USUARIO
	WHERE cor_usu = @EMAIL AND pass_usu = @PASSWORD
GO


--EXEC SP_VALIDAR_USUARIO 'pedro@gmail.com','pedro123'
--go


CREATE OR ALTER PROC PA_REGISTRAR_USUARIO
@NOM_USU VARCHAR(50),@APE VARCHAR(50),@TEL VARCHAR(10),@COR VARCHAR(50),@PASS VARCHAR(30),
@FEC DATE,@COD_DIST CHAR(5)
AS
	
	DECLARE @NUMERO VARCHAR(5) 
	SELECT @NUMERO=RIGHT(MAX(cod_usu),4)+1 FROM USUARIO
	SELECT @NUMERO='U'+RIGHT('000'+@NUMERO,4)
	
	INSERT INTO USUARIO VALUES(@NUMERO,
	@NOM_USU,@APE,@TEL,@COR ,@PASS,@FEC,@COD_DIST,'R02')

	SELECT @NUMERO AS CODIGO
GO

EXEC PA_REGISTRAR_USUARIO 'LUCio','Perez','964112471','luch@gmail.com','juan123','01/04/2000','D0002'
GO

CREATE OR ALTER PROC SP_UPDATE_USUARIO 
@COD_USU CHAR(5),@NOM_USU VARCHAR(50),@APE VARCHAR(50),@TEL VARCHAR(10),
@COR VARCHAR(50),@PASS VARCHAR(30),@FEC DATE,@COD_DIST CHAR(5)
AS 
	UPDATE USUARIO
	SET nom_usu=@NOM_USU,ape_usu=@APE,tel_usu=@TEL,cor_usu=@COR,pass_usu=@PASS,fec_nac=@FEC,cod_dist=@COD_DIST
	WHERE cod_usu = @COD_USU
GO



select * from PRODUCTOS
go
------------------------------------------------------
--VENTA

CREATE OR ALTER PROC PA_GRABAR_VENTA_IGV
@CLI_COD CHAR(5),@VTA_TOTAL DECIMAL(8,2)
AS
	DECLARE @NUMERO varchar(5)
    DECLARE @NUM INT
    -- Obtener el número más alto de venta, o 0 si no hay registros
    SELECT @NUM = ISNULL(CAST(RIGHT(MAX(num_vta), 4) AS INT), 0) + 1 FROM Ventas

    -- Formatear el nuevo número con ceros a la izquierda
    SELECT @NUMERO = 'V' + RIGHT('0000' + CAST(@NUM AS VARCHAR), 4)
	-------- CALCULAR EL IGV ----------------------------------
	DECLARE @IGV DECIMAL(7,2)=0
	SELECT @IGV = (@VTA_TOTAL/1.18)*0.18
	-----------------------------------------------
	-- INSERTANDO LOS DATOS DE LA NUEVA VENTA
	INSERT INTO Ventas VALUES(@NUMERO,GETDATE(),
		@CLI_COD, @IGV,@VTA_TOTAL)
	-- DEVOLVIENDO EL NUEVO NUMERO DE VENTA GENERADO
	SELECT @NUMERO AS NUMERO
GO

EXEC PA_GRABAR_VENTA_IGV 'U0001',524.52
GO

CREATE OR ALTER PROC PA_GRABAR_VENTAS_DETALLE
@VTA_NUM CHAR(5), @COD_PRO CHAR(5), 
 @PRECIO decimal (7,2),@CANTIDAD INT
AS
	-- INSERTANDO EL NUEVO DETALLE DE LA FACTURA
	INSERT INTO DetalleVenta 
	   VALUES(@VTA_NUM,@COD_PRO,@PRECIO,@CANTIDAD)
	-- ACTUALIZANDO EL STOCK DEL ARTICULO
	UPDATE PRODUCTOS 
		SET stk_pro=stk_pro - @CANTIDAD 
	WHERE cod_produc = @COD_PRO
	
GO
--EXEC PA_GRABAR_VENTAS_DETALLE  'V0002','P0001',20.2,5

CREATE OR ALTER PROC SP_LISTA_DISTRITOS
AS
	SELECT * FROM DISTRITOS
GO

-------------------------------------------------------------------
--PROVEEDORES
CREATE OR ALTER PROC SP_LISTA_PROVEEDORES
@NOM VARCHAR(30)='%'
AS
	SELECT * FROM PROVEEDOR
	WHERE eli_prov = 'NO' AND nom_prov LIKE @NOM+'%'
GO

CREATE OR ALTER PROC SP_GRABAR_PROVEEDOR
@NOM_PROV VARCHAR(30),@PAIS VARCHAR(30),
@EMAIL VARCHAR(100),@TELEFONO VARCHAR(20)
AS
	DECLARE @NUMERO VARCHAR(5) 
	SELECT @NUMERO=RIGHT(MAX(cod_prov),3)+1 FROM PROVEEDOR
	SELECT @NUMERO='PV'+RIGHT('000'+@NUMERO,3)

	-----------------------------------------------
	INSERT INTO PROVEEDOR VALUES(@NUMERO,
	@NOM_PROV, @PAIS, @EMAIL,@TELEFONO,DEFAULT)

	SELECT @NUMERO AS NUMERO
GO

--EXEC PA_GRABAR_PROVEEDOR 'MERCEDES','E.E.U.U.','mercedes@email.com','+1 954287127'

CREATE OR ALTER PROC SP_UPDATE_PROVEEDOR
@COD_PROV CHAR(5),
@NOM_PROV VARCHAR(30),@PAIS VARCHAR(30),
@EMAIL VARCHAR(100),@TELEFONO VARCHAR(20)
AS
	UPDATE PROVEEDOR
	SET nom_prov = @NOM_PROV, pais_origen=@PAIS,email=@EMAIL,telefono=@TELEFONO
	WHERE cod_prov = @COD_PROV
GO

--EXEC SP_UPDATE_PROVEEDOR 'PV004', 'MERCEDES BENZ','E.E.U.U.','mercedes@email.com','+1 954287127'

CREATE OR ALTER PROC SP_DELETE_PROVEEDOR
@COD_PROV CHAR(5)
AS 
	UPDATE PROVEEDOR
	SET eli_prov = 'SI'
	WHERE cod_prov = @COD_PROV
GO
--exec SP_DELETE_PROVEEDOR 'PV004'


SELECT 'BD CREADO EXITOSAMENTE'
GO  
select * from PRODUCTOS
select * from Ventas
select * from DetalleVenta
SELECT * FROM USUARIO
INSERT INTO Ventas VALUES ('V0006','2024-01-01','U0002',0,20)
INSERT INTO DetalleVenta VALUES ('V0006','P0003',150,6)
INSERT INTO DetalleVenta VALUES ('V0006','P0005',121,4)
go

CREATE OR ALTER PROCEDURE SP_REPORTE_VENTA_PRODUCTO
@CANT INT
AS
	SELECT V.cod_usu,U.nom_usu,U.ape_usu, COUNT(*)as cantidad,MIN(V.fec_vta) AS firstVent,MAX(V.fec_vta) AS lastVent,SUM(DV.precio*DV.cantidad) AS importe
	FROM VENTAS V
	JOIN USUARIO U ON U.cod_usu = V.cod_usu JOIN DetalleVenta DV ON DV.num_vta = V.num_vta
	GROUP BY  V.cod_usu,U.nom_usu,U.ape_usu
	HAVING COUNT(*) >= @CANT
GO

EXEC SP_REPORTE_VENTA_PRODUCTO 1
GO

CREATE OR ALTER PROCEDURE SP_REPORTE_PRODUCTO
@IMPORTE DECIMAL(7,2)
AS
	SELECT P.cod_produc,P.nom_pro,C.nom_cate,SUM(DV.cantidad) as cantidad,
	SUM(DV.cantidad*DV.precio) as importe,COUNT(DV.num_vta) as venta
	FROM PRODUCTOS P
	JOIN DetalleVenta DV ON P.cod_produc = DV.cod_produc
	JOIN CATEGORIA C ON P.cod_cate = C.cod_cate
	GROUP BY P.cod_produc,P.nom_pro,C.nom_cate
	HAVING SUM(DV.cantidad*DV.precio) >= @IMPORTE
GO

EXEC SP_REPORTE_PRODUCTO 400
GO