using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Synergy.Domain.Edu.Entities
{
    [DataContract]
    public partial class BEEntidadString
        {
            [DataMember]
            public virtual string Id { get; set; }
            [DataMember]
            public virtual string Descripcion { get; set; }
        }
}