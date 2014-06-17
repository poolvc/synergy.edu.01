using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Synergy.Domain.Edu.Entities
{
    [DataContract]
    public class BEEntidad 
    {
        [DataMember]

        public virtual int Id { get; set; }
        [DataMember]
        public virtual string Codigo { get; set; }
        [DataMember]
        public virtual string Descripcion { get; set; }
        [DataMember]
        public virtual string Estado { get; set; }
        [DataMember]
        public virtual int Ref { get; set; }
        [DataMember]
        public virtual string Mensaje { get; set; }
        [DataMember]
        public virtual string UsuarioCreacion { get; set; }
        [DataMember]
        public virtual string UsuarioModificacion { get; set; }
        [DataMember]
        public virtual DateTime FechaCreacion { get; set; }
        [DataMember]
        public virtual DateTime FechaModificacion { get; set; }
        [DataMember]
        public virtual int Error { get; set; }
        [DataMember]
        public virtual int Pagina { get; set; }
        [DataMember]
        public virtual DbTipoAccion RefAccion { get; set; } //Indica el tipo de accion U/Update - I/Insertar - D/Eliminar
        [DataMember]
        public virtual int Fila { get; set; }
        [DataMember]
        public virtual string RefGuid { get; set; }
        [DataMember]
        public virtual int FilasXPagina { get; set; }
        [DataMember]
        public virtual int TotalFilas { get; set; }
    }
}
