using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.ServiceModel.Activation;
using System.Data;
using Synergy.Domain.Edu.Entities;
using Synergy.Application.Edu;

 //[AspNetCompatibilityRequirements(
 // RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WCAplicacion" in code, svc and config file together.

[DataContract]
[Serializable]
public class ContenedorFamilia
{
    [DataMember]
    public virtual List<BEFamiliaSel> Familias { get; set; }
}

public class ContenedorAlumno
{
    [DataMember]
    public virtual List<BEAlumnoSel> Alumnos { get; set; }
}
public class WCServicio : IWCServicio
{

    public ContenedorFamilia ListarFamiliaSelPorCampo(string pstrColumna, string pstrValor, int pinPagina)
    {
        ContenedorFamilia bes = new ContenedorFamilia();
        BEFamiliaSel be = new BEFamiliaSel()
        {
            Columna =  string.IsNullOrEmpty(pstrColumna) ? "ApellidoPaterno" : pstrColumna,
            Valor = pstrValor,
            Pagina = pinPagina,
        };
        bes.Familias = (new BLFamiliaSel()).ListarPorCampo(be);
        return bes;
    }

    public ContenedorAlumno ListarAlumnoSelPorCampo(string pstrColumna, string pstrValor, int pinPagina)
    {
        ContenedorAlumno bes = new ContenedorAlumno();
        BEAlumnoSel be = new BEAlumnoSel()
        {
            Columna = string.IsNullOrEmpty(pstrColumna) ? "ApellidoPaterno" : pstrColumna,
            Valor = pstrValor,
            Pagina = pinPagina,
        };
        bes.Alumnos = (new BLAlumnoSel()).ListarPorCampo(be);
        return bes;
    }
}
