using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Synergy.Domain.Edu.Entities
{
    [DataContract]
    public class BEPeriodoAcademico : BEEntidad
    {
        [DataMember]
        public virtual string PeriodoAcademico { get; set; }
        [DataMember]
        public virtual string DescripcionLocal { get; set; }
    }
}
