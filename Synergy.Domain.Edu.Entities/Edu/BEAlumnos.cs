using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Synergy.Domain.Edu.Entities
{
    [DataContract]
    public class BEAlumnos : BEEntidad
    {
        
        [DataMember]
        public virtual string PeriodoAcademico { get; set; }
        [DataMember]
        public virtual string Grado { get; set; }
        [DataMember]
        public virtual string Familia { get; set; }
        [DataMember]
        public virtual string Foto { get; set; }
        [DataMember]
        public virtual string Seccion { get; set; }
        [DataMember]
        public virtual string Alumno { get; set; }
       
        [DataMember]
        public virtual string Exportado { get; set; }
    
        
        
         
    }
}

