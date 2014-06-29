﻿CREATE TABLE [dbo].[Staff] (
    [Periodoacademico]       CHAR (10)     NOT NULL,
    [empleado]               INT           NOT NULL,
    [Apellidos]              VARCHAR (50)  NULL,
    [Nombres]                VARCHAR (50)  NULL,
    [Sexo]                   VARCHAR (10)  NOT NULL,
    [PaisNaciimiento]        CHAR (50)     NULL,
    [DepartamentoNacimiento] CHAR (50)     NULL,
    [provincianacimiento]    CHAR (50)     NULL,
    [DistritoNacimiento]     CHAR (50)     NULL,
    [fechanacimiento]        DATETIME      NULL,
    [Direccion]              VARCHAR (100) NULL,
    [Departamento]           CHAR (50)     NULL,
    [Provincia]              CHAR (50)     NULL,
    [CodigoPostal]           CHAR (50)     NULL,
    [Telefono]               VARCHAR (20)  NULL,
    [Fax]                    VARCHAR (20)  NULL,
    [Ceular]                 CHAR (20)     NULL,
    [TipoDocumento]          CHAR (50)     NOT NULL,
    [documento]              CHAR (20)     NOT NULL,
    [nacionalidad]           CHAR (50)     NULL,
    [CorreoElectronico]      VARCHAR (50)  NULL,
    [GrupoSanguineo]         VARCHAR (100) NULL,
    [NombreEmergencia]       VARCHAR (50)  NULL,
    [DireccionEmergencia]    VARCHAR (100) NULL,
    [TelefonoEmergencia]     VARCHAR (20)  NULL,
    [celularemergencia]      CHAR (20)     NULL,
    [NombreEmergencia2]      CHAR (50)     NULL,
    [DireccionEmergencia2]   CHAR (100)    NULL,
    [TelefonoEmergencia2]    CHAR (20)     NULL,
    [CelularEmergencia2]     CHAR (20)     NULL,
    [CentroCostos]           CHAR (50)     NULL,
    [Proyecto]               CHAR (50)     NULL,
    [correointerno]          CHAR (50)     NULL,
    [anexo]                  CHAR (10)     NULL,
    [CodigoUsuario]          CHAR (20)     NULL,
    [TipoPlanilla]           CHAR (50)     NULL,
    [TipoTrabajador]         CHAR (50)     NULL,
    [FechaIngreso]           DATETIME      NULL,
    [fechacese]              DATETIME      NULL,
    [fechareingreso]         DATETIME      NULL,
    [TipoContrato]           CHAR (50)     NULL,
    [FechaInicioContrato]    DATETIME      NULL,
    [FechaFinContrato]       DATETIME      NULL,
    [numerocontrato]         CHAR (20)     NULL,
    [NombreCarnet]           VARCHAR (50)  NULL,
    [ApellidoCarnet]         VARCHAR (50)  NULL,
    [PuestoCarnet]           VARCHAR (50)  NULL,
    [tipohorario]            CHAR (100)    NULL,
    [Cargo]                  CHAR (1000)   NULL,
    [CategoriaFuncional]     CHAR (10)     NULL,
    [flagcentroformacion]    CHAR (10)     NULL,
    [Niveleducativo]         VARCHAR (100) NULL,
    [CategoriaOcupacional]   VARCHAR (100) NULL,
    [RutaFoto]               CHAR (150)    NULL,
    [imgstaff]               IMAGE         NULL,
    [CheckReference]         INT           NOT NULL,
    [FechaCreacion]          DATETIME      NULL,
    [UsuarioCreacion]        VARCHAR (20)  NULL,
    CONSTRAINT [PK_Staff_1] PRIMARY KEY CLUSTERED ([empleado] ASC, [CheckReference] ASC)
);

