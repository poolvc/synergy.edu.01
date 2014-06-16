using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;


namespace Synergy.Domain.Edu.Entities
{
    [DataContract]
    public class BEEntidadParam  : BEEntidad
    {
        [DataMember]
        public virtual int ValorEntero { get; set; }
        [DataMember]
        public virtual string ValorCadena { get; set; }
        [DataMember]
        public virtual List<BEEntidadString> ListaCadenas { get; set; }
        [DataMember]
        public virtual List<BEEntidadInt> ListaNumeros { get; set; }
        
    }
}
