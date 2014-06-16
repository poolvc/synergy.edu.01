using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Synergy.Domain.Edu.Entities
{
    [DataContract]
    public class BEEntidadInt
    {
        [DataMember]
        public virtual int Id { get; set; }
        [DataMember]
        public virtual string Descripcion { get; set; }

    }
}