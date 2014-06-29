using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Synergy.Domain.Edu.Entities
{
    [DataContract]
    public class BEFamilia : BEEntidad
    {
        
        [DataMember]
        public virtual string PeriodoAcademico { get; set; }
        [DataMember]
        public virtual string Familia { get; set; }
        [DataMember]
        public virtual string Foto { get; set; }       
        [DataMember]
        public virtual string Exportado { get; set; }
        [DataMember]
        public virtual string CodigoFamilia { get; set; }
        [DataMember]
        public virtual string Vinculo { get; set; }
        [DataMember]
        public virtual string DocumentoIdentidad { get; set; }

    }
}

