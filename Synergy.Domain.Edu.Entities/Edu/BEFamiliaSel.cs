using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Synergy.Domain.Edu.Entities
{
    [DataContract]
    [Serializable]
    public class BEFamiliaSel : BEEntidad
    {
        [DataMember]
        public virtual string AlumnoGrupo { get; set; }
        [DataMember]
        public virtual int IdCompania { get; set; }
        [DataMember]
        public virtual string ApellidoPaterno { get; set; }
        [DataMember]
        public virtual string ApellidoMaterno { get; set; }
        [DataMember]
        public virtual string Columna { get; set; }
        [DataMember]
        public virtual string Valor { get; set; }

        
    }
}

