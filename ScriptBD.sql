Create database AforoMex
USE [AforoMex]
GO
/****** Object:  Table [dbo].[Direcciones]    Script Date: 18/01/2021 09:18:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Direcciones](
	[IdDireccion] [int] IDENTITY(1,1) NOT NULL,
	[Calle] [nvarchar](100) NOT NULL,
	[Colonia] [nvarchar](100) NOT NULL,
	[Ciudad] [nvarchar](100) NOT NULL,
	[Estado] [nvarchar](100) NOT NULL,
	[Pais] [nvarchar](100) NOT NULL,
	[CodigoPostal] [int] NOT NULL,
	[Numero] [int] NOT NULL,
	[NumeroInterior] [int] NULL,
	[IdNegocio] [int] NOT NULL,
 CONSTRAINT [PK_Direcciones] PRIMARY KEY CLUSTERED 
(
	[IdDireccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Horarios]    Script Date: 18/01/2021 09:18:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Horarios](
	[IdHorario] [int] IDENTITY(1,1) NOT NULL,
	[Dia] [nvarchar](30) NOT NULL,
	[HoraInicio] [time](1) NOT NULL,
	[HoraFin] [time](1) NOT NULL,
	[IdNegocio] [int] NOT NULL,
 CONSTRAINT [PK_Horarios] PRIMARY KEY CLUSTERED 
(
	[IdHorario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Negocios]    Script Date: 18/01/2021 09:18:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Negocios](
	[IdNegocio] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Categoria] [nvarchar](50) NOT NULL,
	[Correo] [nvarchar](80) NOT NULL,
	[SitioWeb] [nvarchar](50) NULL,
	[Telefono] [nvarchar](15) NULL,
	[Celular] [nvarchar](15) NULL,
	[Descripcion] [nvarchar](1000) NOT NULL,
	[Facebook] [nvarchar](50) NULL,
	[Logo] [nvarchar](max) NULL,
	[Abierto] [bit] NOT NULL,
	[Aforo] [int] NOT NULL,
	[CupoOcupado] [int] NOT NULL,
	[PermitirReservaciones] [bit] NOT NULL,
	[AforoReservaciones] [decimal](3, 2) NOT NULL,
	[LimiteReservaciones] [int] NOT NULL,
	[IdUsuario] [int] NULL,
 CONSTRAINT [PK_Negocios] PRIMARY KEY CLUSTERED 
(
	[IdNegocio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reservaciones]    Script Date: 18/01/2021 09:18:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservaciones](
	[IdReservacion] [int] IDENTITY(1,1) NOT NULL,
	[FechaReserva] [datetime] NOT NULL,
	[NumLugares] [int] NOT NULL,
	[Estado] [nvarchar](30) NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[IdNegocio] [int] NOT NULL,
 CONSTRAINT [PK_Reservaciones] PRIMARY KEY CLUSTERED 
(
	[IdReservacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 18/01/2021 09:18:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Apellidos] [nvarchar](50) NOT NULL,
	[Telefono] [nvarchar](15) NOT NULL,
	[Correo] [nvarchar](80) NOT NULL,
	[FechaNacimiento] [date] NOT NULL,
	[Contrasena] [nvarchar](100) NOT NULL,
	[Rol] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Direcciones]  WITH CHECK ADD  CONSTRAINT [FK_Direcciones_Negocios] FOREIGN KEY([IdNegocio])
REFERENCES [dbo].[Negocios] ([IdNegocio])
GO
ALTER TABLE [dbo].[Direcciones] CHECK CONSTRAINT [FK_Direcciones_Negocios]
GO
ALTER TABLE [dbo].[Horarios]  WITH CHECK ADD  CONSTRAINT [FK_Horarios_Negocios] FOREIGN KEY([IdNegocio])
REFERENCES [dbo].[Negocios] ([IdNegocio])
GO
ALTER TABLE [dbo].[Horarios] CHECK CONSTRAINT [FK_Horarios_Negocios]
GO
ALTER TABLE [dbo].[Negocios]  WITH CHECK ADD  CONSTRAINT [FK_Negocios_Usuarios] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuarios] ([IdUsuario])
GO
ALTER TABLE [dbo].[Negocios] CHECK CONSTRAINT [FK_Negocios_Usuarios]
GO
ALTER TABLE [dbo].[Reservaciones]  WITH CHECK ADD  CONSTRAINT [FK_Reservaciones_Negocios] FOREIGN KEY([IdNegocio])
REFERENCES [dbo].[Negocios] ([IdNegocio])
GO
ALTER TABLE [dbo].[Reservaciones] CHECK CONSTRAINT [FK_Reservaciones_Negocios]
GO
ALTER TABLE [dbo].[Reservaciones]  WITH CHECK ADD  CONSTRAINT [FK_Reservaciones_Usuarios] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuarios] ([IdUsuario])
GO
ALTER TABLE [dbo].[Reservaciones] CHECK CONSTRAINT [FK_Reservaciones_Usuarios]
GO
