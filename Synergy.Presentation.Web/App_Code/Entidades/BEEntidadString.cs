using System;
using System.Collections.Generic;
using System.Text;


public class BEEntidadString
{
    public virtual string Id { get; set; }
    public virtual string Descripcion { get; set; }
    public virtual string Valor { get; set; }

    public BEEntidadString()
    {
        Id = string.Empty;
        Descripcion = string.Empty;
    }

    public BEEntidadString(string pstrDescripcion, string pId)
    {       
        Descripcion = pstrDescripcion;
        Id = pId;
    }
    public BEEntidadString(string pstrDescripcion, string pstrId, string pstrValor)
    {
        Descripcion = pstrDescripcion;
        Id = pstrId;
        Valor = pstrValor;
    }
}
