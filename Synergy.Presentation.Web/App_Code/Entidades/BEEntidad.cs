using System;
using System.Collections.Generic;
using System.Text;

namespace Synergy.Presentation.Web
{
    [Serializable]
    public class BEEntidad
    {
        public virtual int Id { get; set; }
        public virtual string Codigo { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual string Estado { get; set; }
        public virtual int Ref { get; set; }
        public virtual string Mensaje { get; set; }
        public virtual string UsuarioCreacion { get; set; }
        public virtual string UsuarioModificacion { get; set; }
        public virtual DateTime FechaCreacion { get; set; }
        public virtual DateTime FechaModificacion { get; set; }
        public virtual int Error { get; set; }
    }
}