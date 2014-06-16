using System;
using System.Collections.Generic;
using System.Text;

namespace Synergy.Presentation.Web
{
    [Serializable]
    public class BEEntidadParam  : BEEntidad
    {
        public virtual int ValorEntero { get; set; }
        public virtual string ValorCadena { get; set; }

        public virtual List<BEEntidadString> ListaCadenas { get; set; }
        public virtual List<BEEntidadInt> ListaNumeros { get; set; }
        
    }
}
