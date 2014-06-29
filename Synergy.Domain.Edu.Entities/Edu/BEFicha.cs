using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Synergy.Domain.Edu.Entities
{
    [DataContract]
    [Serializable]
    public class BEFicha : BEEntidad
    {
        [DataMember]
        public virtual string TipoFicha { get; set; }
        [DataMember]
        public virtual string Columna { get; set; }
        [DataMember]
        public virtual string Valor { get; set; }
        [DataMember]
        public virtual string PeriodoAcademico { get; set; }
        [DataMember]
        public virtual string CodigoAlumno { get; set; }
        [DataMember]
        public virtual string AlumnoGrupo { get; set; }
        [DataMember]
        public virtual string Vinculo { get; set; }
        [DataMember]
        public virtual string CodigoEmpleado { get; set; }
        [DataMember]
        public virtual string DocumentoIdentidad { get; set; }
    }
}

