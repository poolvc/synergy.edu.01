using System;
using System.Collections.Generic;
using System.Text;

namespace Synergy.Presentation.Web
{
    public class BEControl : BEEntidad
    {
        public virtual string CodigoAplicacion { get; set; }
        public virtual string Ventana { get; set; }
        public virtual string Control { get; set; }
    }
}
