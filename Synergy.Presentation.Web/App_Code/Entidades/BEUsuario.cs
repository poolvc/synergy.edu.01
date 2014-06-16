using System;
using System.Collections.Generic;
using System.Text;

namespace Synergy.Presentation.Web
{
    public class BEUsuario : BEEntidad
    {
        public virtual string CodigoUsuario { get; set; }
        public virtual string Tipo { get; set; }
        public virtual string Nombre { get; set; }
        public virtual string Clave { get; set; }
        public virtual string ExpiraPasswordFlag { get; set; }
        public virtual string FlagAdministrador { get; set; }
    }
}
