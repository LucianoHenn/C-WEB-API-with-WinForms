USE [master]
GO
/****** Object:  Database [proyecto_banco]    Script Date: 10/11/2021 08:44:55 ******/
CREATE DATABASE [proyecto_banco]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'proyecto_banco', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\proyecto_banco.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'proyecto_banco_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\proyecto_banco_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [proyecto_banco] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [proyecto_banco].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [proyecto_banco] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [proyecto_banco] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [proyecto_banco] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [proyecto_banco] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [proyecto_banco] SET ARITHABORT OFF 
GO
ALTER DATABASE [proyecto_banco] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [proyecto_banco] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [proyecto_banco] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [proyecto_banco] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [proyecto_banco] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [proyecto_banco] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [proyecto_banco] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [proyecto_banco] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [proyecto_banco] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [proyecto_banco] SET  ENABLE_BROKER 
GO
ALTER DATABASE [proyecto_banco] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [proyecto_banco] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [proyecto_banco] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [proyecto_banco] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [proyecto_banco] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [proyecto_banco] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [proyecto_banco] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [proyecto_banco] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [proyecto_banco] SET  MULTI_USER 
GO
ALTER DATABASE [proyecto_banco] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [proyecto_banco] SET DB_CHAINING OFF 
GO
ALTER DATABASE [proyecto_banco] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [proyecto_banco] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [proyecto_banco] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [proyecto_banco] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [proyecto_banco] SET QUERY_STORE = OFF
GO
USE [proyecto_banco]
GO
/****** Object:  Table [dbo].[clientes]    Script Date: 10/11/2021 08:44:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[clientes](
	[nro_cliente] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[apellido] [varchar](50) NULL,
	[dni] [varchar](8) NULL,
	[contrasenia] [varchar](20) NULL,
	[es_admin] [bit] NULL,
 CONSTRAINT [pk_clientes] PRIMARY KEY CLUSTERED 
(
	[nro_cliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cuentas]    Script Date: 10/11/2021 08:44:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cuentas](
	[nro_cliente] [int] NOT NULL,
	[id_tipo_cuenta] [int] NOT NULL,
	[cbu] [varchar](22) NULL,
	[saldo] [decimal](12, 2) NULL,
	[ultimo_movimiento] [datetime] NULL,
 CONSTRAINT [pk_cuentas] PRIMARY KEY CLUSTERED 
(
	[nro_cliente] ASC,
	[id_tipo_cuenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tipo_cuenta]    Script Date: 10/11/2021 08:44:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tipo_cuenta](
	[id_tipo_cuenta] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_tipo_cuenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[clientes] ON 

INSERT [dbo].[clientes] ([nro_cliente], [nombre], [apellido], [dni], [contrasenia], [es_admin]) VALUES (1011, N'Juan', N'Pont Verges', N'12456789', N'123', 1)
INSERT [dbo].[clientes] ([nro_cliente], [nombre], [apellido], [dni], [contrasenia], [es_admin]) VALUES (1013, N'Cliente', N'Test', N'1234', N'1234', 0)
INSERT [dbo].[clientes] ([nro_cliente], [nombre], [apellido], [dni], [contrasenia], [es_admin]) VALUES (1014, N'Juan', N'Pont', N'1245699', N'test123', 0)
INSERT [dbo].[clientes] ([nro_cliente], [nombre], [apellido], [dni], [contrasenia], [es_admin]) VALUES (1017, N'', N'', N'', N'', 0)
SET IDENTITY_INSERT [dbo].[clientes] OFF
GO
INSERT [dbo].[cuentas] ([nro_cliente], [id_tipo_cuenta], [cbu], [saldo], [ultimo_movimiento]) VALUES (1011, 3, N'alias.perro', CAST(65000.00 AS Decimal(12, 2)), CAST(N'2021-11-03T10:56:57.743' AS DateTime))
INSERT [dbo].[cuentas] ([nro_cliente], [id_tipo_cuenta], [cbu], [saldo], [ultimo_movimiento]) VALUES (1011, 4, N'96320', CAST(5000.00 AS Decimal(12, 2)), CAST(N'2021-11-03T10:56:57.743' AS DateTime))
INSERT [dbo].[cuentas] ([nro_cliente], [id_tipo_cuenta], [cbu], [saldo], [ultimo_movimiento]) VALUES (1013, 1, N'perro.loro', CAST(145000.00 AS Decimal(12, 2)), CAST(N'2021-11-10T06:23:57.677' AS DateTime))
INSERT [dbo].[cuentas] ([nro_cliente], [id_tipo_cuenta], [cbu], [saldo], [ultimo_movimiento]) VALUES (1013, 2, N'alias.gato', CAST(50000.00 AS Decimal(12, 2)), CAST(N'2021-11-10T08:37:25.983' AS DateTime))
INSERT [dbo].[cuentas] ([nro_cliente], [id_tipo_cuenta], [cbu], [saldo], [ultimo_movimiento]) VALUES (1014, 1, N'gato.perro', CAST(6335.00 AS Decimal(12, 2)), CAST(N'2021-11-10T05:04:28.060' AS DateTime))
INSERT [dbo].[cuentas] ([nro_cliente], [id_tipo_cuenta], [cbu], [saldo], [ultimo_movimiento]) VALUES (1014, 2, N'gato.perro.loro', CAST(6335.00 AS Decimal(12, 2)), CAST(N'2021-11-10T05:04:28.060' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[tipo_cuenta] ON 

INSERT [dbo].[tipo_cuenta] ([id_tipo_cuenta], [nombre]) VALUES (1, N'Cuenta Universal Gratuita')
INSERT [dbo].[tipo_cuenta] ([id_tipo_cuenta], [nombre]) VALUES (2, N'Caja de Ahorros')
INSERT [dbo].[tipo_cuenta] ([id_tipo_cuenta], [nombre]) VALUES (3, N'Cuenta Corriente')
INSERT [dbo].[tipo_cuenta] ([id_tipo_cuenta], [nombre]) VALUES (4, N'Cuenta Sueldo')
INSERT [dbo].[tipo_cuenta] ([id_tipo_cuenta], [nombre]) VALUES (1002, N'caja de ahorro en dolares')
INSERT [dbo].[tipo_cuenta] ([id_tipo_cuenta], [nombre]) VALUES (1003, N'CAJA EN URUGUAYOS')
INSERT [dbo].[tipo_cuenta] ([id_tipo_cuenta], [nombre]) VALUES (1021, N'TEST 2')
SET IDENTITY_INSERT [dbo].[tipo_cuenta] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__cuentas__D83669DD5D5DD08B]    Script Date: 10/11/2021 08:44:55 ******/
ALTER TABLE [dbo].[cuentas] ADD UNIQUE NONCLUSTERED 
(
	[cbu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cuentas]  WITH CHECK ADD  CONSTRAINT [fk_cuentas] FOREIGN KEY([nro_cliente])
REFERENCES [dbo].[clientes] ([nro_cliente])
GO
ALTER TABLE [dbo].[cuentas] CHECK CONSTRAINT [fk_cuentas]
GO
ALTER TABLE [dbo].[cuentas]  WITH CHECK ADD  CONSTRAINT [fk_cuentas_topo] FOREIGN KEY([id_tipo_cuenta])
REFERENCES [dbo].[tipo_cuenta] ([id_tipo_cuenta])
GO
ALTER TABLE [dbo].[cuentas] CHECK CONSTRAINT [fk_cuentas_topo]
GO
/****** Object:  StoredProcedure [dbo].[SP_BAJA_CLIENTE]    Script Date: 10/11/2021 08:44:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_BAJA_CLIENTE]
@nro_cliente int
as
begin
delete from cuentas
where nro_cliente = @nro_cliente
delete from clientes 
where nro_cliente = @nro_cliente
end
GO
/****** Object:  StoredProcedure [dbo].[SP_CREAR_TIPO_CUENTA]    Script Date: 10/11/2021 08:44:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_CREAR_TIPO_CUENTA]
@tipo_cuenta varchar(30)
as
insert into tipo_cuenta
values(@tipo_cuenta)
GO
/****** Object:  StoredProcedure [dbo].[SP_EDITAR_TIPO_CUENTA]    Script Date: 10/11/2021 08:44:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_EDITAR_TIPO_CUENTA]
@id int,
@tipo varchar(50)
as
update tipo_cuenta
set nombre = @tipo
where id_tipo_cuenta = @id
GO
/****** Object:  StoredProcedure [dbo].[SP_ELIMINAR_CUENTA]    Script Date: 10/11/2021 08:44:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_ELIMINAR_CUENTA]
@cbu varchar(22)
as
delete from cuentas
where cbu = @cbu
GO
/****** Object:  StoredProcedure [dbo].[SP_ELIMINAR_CUENTAS]    Script Date: 10/11/2021 08:44:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SP_ELIMINAR_CUENTAS]
@nro_cliente int
as
delete cuentas
where nro_cliente = @nro_cliente
GO
/****** Object:  StoredProcedure [dbo].[SP_ELIMINAR_TIPO_CUENTA]    Script Date: 10/11/2021 08:44:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_ELIMINAR_TIPO_CUENTA]
@id_tipo_cuenta int
as
delete from tipo_cuenta
where id_tipo_cuenta = @id_tipo_cuenta
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERTAR_CLIENTE]    Script Date: 10/11/2021 08:44:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SP_INSERTAR_CLIENTE]
@nro_cliente int output,
@nombre varchar(50),
@apellido varchar(50),
@dni varchar(8),
@contrasenia varchar(20),
@es_admin bit
as 
begin
insert into clientes
values(@nombre,@apellido,@dni,@contrasenia,@es_admin)
set @nro_cliente = SCOPE_IDENTITY()
end
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERTAR_CUENTA]    Script Date: 10/11/2021 08:44:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SP_INSERTAR_CUENTA]
@nro_cliente int,
@id_tipo_cuenta int,
@cbu varchar(22),
@saldo decimal(12,2)
as 
insert into cuentas
values(@nro_cliente,@id_tipo_cuenta,@cbu,@saldo,getdate())
GO
/****** Object:  StoredProcedure [dbo].[SP_LOGIN]    Script Date: 10/11/2021 08:44:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SP_LOGIN]
@contrasenia varchar(18),
@nro_cliente int
as
begin
if((Select contrasenia from clientes where nro_cliente = @nro_cliente)
= @contrasenia )
select c.nro_cliente, c.nombre, apellido, dni, contrasenia, es_admin,
cu.id_tipo_cuenta, cbu, saldo,ultimo_movimiento,t.nombre as 'nombre_tipo_cuenta'

from clientes c join cuentas cu on c.nro_cliente = cu.nro_cliente join
tipo_cuenta t on t.id_tipo_cuenta = cu.id_tipo_cuenta
where c.nro_cliente = @nro_cliente
end
GO
/****** Object:  StoredProcedure [dbo].[SP_MODIFICAR_CLIENTE]    Script Date: 10/11/2021 08:44:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_MODIFICAR_CLIENTE]
@nro_cliente int,
@nombre varchar(50),
@apellido varchar(50),
@dni varchar(8),
@contrasenia varchar(20),
@es_admin bit
as 
begin
update clientes
set nombre=@nombre, apellido = @apellido,dni = @dni, contrasenia =
@contrasenia, es_admin = @es_admin
where nro_cliente = @nro_cliente
end
GO
/****** Object:  StoredProcedure [dbo].[SP_MODIFICAR_CUENTA]    Script Date: 10/11/2021 08:44:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SP_MODIFICAR_CUENTA]
@nro_cliente int,
@id_tipo_cuenta int,
@cbu varchar(22),
@saldo decimal(12,2)
as
update cuentas
set id_tipo_cuenta= @id_tipo_cuenta,cbu=@cbu, saldo = @saldo,ultimo_movimiento = getdate()
where nro_cliente = @nro_cliente
GO
/****** Object:  StoredProcedure [dbo].[SP_REPORT]    Script Date: 10/11/2021 08:44:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SP_REPORT]
@desde varchar(14),
@hasta varchar(14)
as
select t.nombre 'tipoCuenta', count(*) 'cantidad'
from cuentas c join tipo_cuenta t on c.id_tipo_cuenta = t.id_tipo_cuenta
join clientes cl on cl.nro_cliente = c.nro_cliente
where ultimo_movimiento between @desde and @hasta
group by t.nombre
GO
/****** Object:  StoredProcedure [dbo].[SP_TRAER_CLIENTES]    Script Date: 10/11/2021 08:44:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SP_TRAER_CLIENTES]
as
select CONVERT(varchar,nro_cliente) + '- ' + nombre + ',' + apellido cliente from clientes
GO
/****** Object:  StoredProcedure [dbo].[SP_TRAER_TIPO_CUENTAS]    Script Date: 10/11/2021 08:44:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_TRAER_TIPO_CUENTAS]
as
select * from tipo_cuenta
GO
/****** Object:  StoredProcedure [dbo].[SP_TRAER_UN_CLIENTE]    Script Date: 10/11/2021 08:44:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SP_TRAER_UN_CLIENTE]
@nro_cliente int
as
begin
select c.nro_cliente, c.nombre, apellido, dni, contrasenia, es_admin,
cu.id_tipo_cuenta, cbu, saldo,ultimo_movimiento,t.nombre as 'nombre_tipo_cuenta'
from clientes c join cuentas cu on c.nro_cliente = cu.nro_cliente join
tipo_cuenta t on t.id_tipo_cuenta = cu.id_tipo_cuenta
where c.nro_cliente = @nro_cliente
end
GO
/****** Object:  StoredProcedure [dbo].[SP_TRANSFERIR]    Script Date: 10/11/2021 08:44:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_TRANSFERIR]
@cbu varchar(18),
@monto decimal(10,2)
as
begin
if(EXISTS(select * from cuentas where cbu = @cbu))
update cuentas
set saldo = (saldo + @monto)
where cbu = @cbu
end
GO
USE [master]
GO
ALTER DATABASE [proyecto_banco] SET  READ_WRITE 
GO
