﻿USE [Fcc]
GO

/****** Object:  Table [dbo].[Factura]    Script Date: 09/02/21 02:46:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Factura](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Pais] [varchar](100) NOT NULL,
	[Nif] [varchar](9) NOT NULL,
	[Importe] [decimal](18, 2) NOT NULL,
	[Base] [decimal](18, 2) NOT NULL,
	[Iva] [int] NOT NULL,
	[Fecha] [date] NOT NULL,
 CONSTRAINT [PK_Factura] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO