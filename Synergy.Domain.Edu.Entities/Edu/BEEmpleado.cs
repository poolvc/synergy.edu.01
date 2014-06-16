using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Synergy.Domain.Edu.Entities
{
    [DataContract]
    public class BEEmpleado : BEEntidad
    {
        [DataMember]
        public virtual int IdEmpleado { get; set; }
        [DataMember]
        public virtual string EmpleadoNombre { get; set; }
        [DataMember]
        public virtual int IdCompania { get; set; }
        [DataMember]
        public virtual string CompaniaDescripcion { get; set; }
        [DataMember]
        public virtual string CiudadesResidencia { get; set; }
        [DataMember]
        public virtual string TipoTrabajador { get; set; }
        [DataMember]
        public virtual string SituacionTrabajo { get; set; }
        [DataMember]
        public virtual DateTime? FechaSituacionTrabajo { get; set; }
        [DataMember]
        public virtual int? IdMonedaPago { get; set; }
        [DataMember]
        public virtual string TipoPago { get; set; }
        [DataMember]
        public virtual string TipoPension { get; set; }
        [DataMember]
        public virtual decimal Remuneracion { get; set; }
        [DataMember]
        public virtual DateTime? FechaInicioPension { get; set; }
        [DataMember]
        public virtual int? IdAFP { get; set; }
        [DataMember]
        public virtual string NumeroAFP { get; set; }
        [DataMember]
        public virtual int? IdBancoCTS { get; set; }
        [DataMember]
        public virtual string TipoCuentaCTS { get; set; }
        [DataMember]
        public virtual int? IdMonedaCTS { get; set; }
        [DataMember]
        public virtual string NumeroCuentaCTS { get; set; }
        [DataMember]
        public virtual string TipoSeguroSalud { get; set; }
        [DataMember]
        public virtual string PlanSeguroSalud { get; set; }
        [DataMember]
        public virtual string NumeroCarnetSeguroSalud { get; set; }
        [DataMember]
        public virtual int? IdPersonaSeguroSalud { get; set; }
        [DataMember]
        public virtual int? IdTipoContrato { get; set; }
        [DataMember]
        public virtual int? IdTipoPuesto { get; set; }
        [DataMember]
        public virtual DateTime? FechaInicioContrato { get; set; }
        [DataMember]
        public virtual DateTime? FechaTerminoContrato { get; set; }
        [DataMember]
        public virtual string LocacionPago { get; set; }
        [DataMember]
        public virtual int? IdCentroCosto { get; set; }        
        [DataMember]
        public virtual string CentroCosto { get; set; }
        [DataMember]
        public virtual string Tipo { get; set; }
        
        [DataMember]
        public virtual int? IdProyecto { get; set; }
        [DataMember]
        public virtual int? IdDepartamentoTrabajo { get; set; }
        [DataMember]
        public virtual int? IdSucursal { get; set; }
        [DataMember]
        public virtual string TipoPlanilla { get; set; }
        [DataMember]
        public virtual int? IdGradoSalario { get; set; }
        [DataMember]
        public virtual int? IdPuesto { get; set; }
        [DataMember]
        public virtual int? IdReclutamiento { get; set; }
        [DataMember]
        public virtual int? IdAreaFuncional { get; set; }
        [DataMember]
        public virtual string SeguroEssaludVida { get; set; }
        [DataMember]
        public virtual string SeguroAccidenteTrabajo { get; set; }
        [DataMember]
        public virtual string SCTRSalud { get; set; }
        [DataMember]
        public virtual string SCTRPension { get; set; }
        [DataMember]
        public virtual string Pensionista { get; set; }
        [DataMember]
        public virtual string Discapacidad { get; set; }
        [DataMember]
        public virtual string Sindicalizado { get; set; }
        [DataMember]
        public virtual string CodigoEstrato { get; set; }
        [DataMember]
        public virtual string ClaseTrabajador { get; set; }
        [DataMember]
        public virtual int? IdLineaCarrera { get; set; }
        [DataMember]
        public virtual Byte[] Foto { get; set; }
    }
}

