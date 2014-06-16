using System;
using System.Collections.Generic;
using System.Text;  
using System.Data;


namespace Synergy.Presentation.Web
{
    public class BEUsuarioSistema : BEUsuario
    {
        public virtual int IdCompania { get; set; }
        public virtual int IdPersona { get; set; }
        public virtual string CodigoCompania { get; set; }
        public virtual string DescripcionCompania { get; set; }
        public virtual int IdAplicacion { get; set; }
        public virtual string CodigoAplicacion { get; set; }
        public virtual string DominioAplicacion { get; set; }
        public virtual int IdIdioma { get; set; }
        public virtual string CodigoIdioma { get; set; }
        public virtual List<BEControl> Control { get; set; }
        public virtual DataSet Menu { get; set; }
        public virtual string Llave { get; set; }
    }
}
