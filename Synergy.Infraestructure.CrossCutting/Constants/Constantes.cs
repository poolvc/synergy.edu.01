using System;
using System.Text;

namespace Synergy.Infraestructure.CrossCutting
{
    public class Constantes
    {
        public const string CADENA_NULA_DEFAULT     = "--";
        public const string FECHA_NULA              = "01/01/0001 12:00:00 a.m.";
        public const string FECHA_NULA_CORTA        = "01/01/0001";

        public const string ConnectionStringSQL     = "Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};";
        public const string ConnectionStringOracle  = "Data Source={0};User ID={1};Password={2};";

        public const string COMPANIA_DEFAULT        = "0000000000";
        public const string CODIGO_APLICACION_SYSTEM = "SYSTEM";

        public const string PARAMETRO_CAMBIACLAV    = "CAMBIACLAV";


        public const int IDCOMPANIA_DEFAULT = 1;
        public const int IDAPLICACION_SYSTEM = 1;

        public const string ESTADO_ACTIVO = "A";
        public const string ESTADO_INACTIVO = "I";

        public const string ESTADO_PREPARADO = "PR";
        public const string ESTADO_REVISADO = "RE";
        public const string ESTADO_APROBADO = "AP";
        public const string ESTADO_ANULADO = "AN";
        public const string ESTADO_CERRADO = "CE";

        public const string ESTADO_SI = "S";
        public const string ESTADO_NO = "N";

        public const string ESTADO_ACTIVO_DESC = "Activo";
        public const string ESTADO_INACTIVO_DESC = "Inactivo";

        public const string ESTADO_ACTIVO_URL = "~/img/ico_active.gif";
        public const string ESTADO_INACTIVO_URL = "~/img/ico_inactive.png";
        public const string MENSAJES_URL = @"~/App_Data/xml/xmlMensaje.xml";

        public const string ACCION_GRABARNUEVO = "GRABARNUEVO";
        public const string ACCION_GRABAR = "GRABAR";
        public const string ACCION_EDITAR = "EDITAR";
        public const string ACCION_ACTUALIZAR = "ACTUALIZAR";
        public const string ACCION_CANCELAR = "CANCELAR";
        public const string ACCION_ELIMINAR = "ELIMINAR";
        public const string ACCION_SUSTENTO = "SUSTENTO";
        public const string ACCION_PATRON = "PATRON";
        public const string ACCION_OBJETIVO = "OBJETIVO";

        public const string ACCION_DETALLE_GRABARNUEVO = "n";
        public const string ACCION_DETALLE_EDITAR = "e";
        public const string ACCION_DETALLE_CANCELAR = "c";
        public const string ACCION_DETALLE_ELIMINAR = "d";

        public const string PARAMETRO_IDIOMASGALEX = "IDIOMASGALEX";
        public const string PARAMETRO_TIPOFUNCION = "TIPOFUNCION";
        public const string PARAMETRO_TIPOCONTRATO = "TIPOCONTRATO";
        public const string PARAMETRO_TIPOPAGO = "TIPOPAGO";
        public const string PARAMETRO_TIPOTRABAJADOR = "TIPOTRABAJADOR";
        public const string PARAMETRO_TIPOPLANILLA = "TIPOPLANILLA";
        public const string PARAMETRO_TIPOPENSION = "TIPOPENSION";
        public const string PARAMETRO_TIPOCUENTACTS = "TIPOCUENTACTS";
        public const string PARAMETRO_TIPOSEGUROSALUD = "TIPOSEGUROSALUD";
        public const string PARAMETRO_CLASETRABAJADOR = "CLASETRABAJADOR";
        public const string PARAMETRO_SITUACIONTRABAJO = "SITUACIONTRABAJO";
        public const string PARAMETRO_ESTADOPROCESO = "ESTADOPROCESO";
        public const string PARAMETRO_PLANSEGUROSALUD = "PLANSEGUROSALUD";

        public const string PARAMETRO_LOCACIONPAGO = "LOCACIONPAGO";
        public const string PARAMETRO_TIPOCOMPETENCIA = "TIPOCOMPETENCIA";
        public const string PARAMETRO_TIPOFRR = "TIPOFRR";
        public const string PARAMETRO_GCOLAS = "GCOLAS";
        public const string PARAMETRO_ESTADOREGISTRO = "ESTADOREGISTRO";
        public const string PARAMETRO_ESTRATOSAL = "ESTRATOSAL";
        public const string PARAMETRO_TIPORESPONSABILIDAD = "TIPORESPONSABILIDAD";
        public const string PARAMETRO_TIPOPROCESOEC = "TIPOPROCESOEC";
        public const string PARAMETRO_CALIFICACIONCONDUCTA = "CALIFICACIONCONDUCTA";
        public const string PARAMETRO_EVALUACIONGENERAL = "EVALUACIONGENERAL";
        public const string PARAMETRO_TIPOSUGERENCIA = "TIPOSUGERENCIA";
        public const string PARAMETRO_ORIGEN = "ORIGEN";
        public const string PARAMETRO_TIPOEVALUACION = "TIPOEVALUACION";
        public const string PARAMETRO_TIPOPERIODO = "TIPOPERIODO";
        public const string PARAMETRO_TIPOPARAMETRO = "TIPOPARAMETRO";

        public const string PARAMETRO_PCSTABLA01 = "PCSTABLA01";
        public const string PARAMETRO_PCSTABLA02 = "PCSTABLA02";
        public const string PARAMETRO_PCSTABLA03 = "PCSTABLA03";
        public const string PARAMETRO_PCSTABLA04 = "PCSTABLA04";
        public const string PARAMETRO_PCSTABLA05 = "PCSTABLA05";
        public const string PARAMETRO_PCSTABLA06 = "PCSTABLA06";
        public const string PARAMETRO_PCSTABLA10 = "PCSTABLA10";// tipos de comprobantes de pago
        public const string PARAMETRO_PCSTABLA12 = "PCSTABLA12";// tipos de transaccion
        public const string PARAMETRO_PCSTABLA50 = "PCSTABLA50";
        public const string PARAMETRO_PCSTABLA55 = "PCSTABLA55";
        public const string PARAMETRO_VALORES = "VALORES";
        public const string PARAMETRO_ACCION = "ACCION";


    }
}
