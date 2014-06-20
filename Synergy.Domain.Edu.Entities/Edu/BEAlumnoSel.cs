using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Synergy.Domain.Edu.Entities
{
    [DataContract]
    [Serializable]
    public class BEAlumnoSel : BEEntidad
    {
        [DataMember]
        public virtual string AlumnoCodigo { get; set; }
        [DataMember]
        public virtual string AlumnoGrupo { get; set; }
        [DataMember]
        public virtual string ApellidoPaterno { get; set; }
        [DataMember]
        public virtual string ApellidoMaterno { get; set; }
        [DataMember]
        public virtual string Nombre01 { get; set; }
        [DataMember]
        public virtual string Sexo { get; set; }
        [DataMember]
        public virtual string Columna { get; set; }
        [DataMember]
        public virtual string Valor { get; set; }

        
    }
}

