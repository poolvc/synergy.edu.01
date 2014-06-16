using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Synergy.Presentation.Web;
using System.Web;
using System.Data;


// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WCAplicacion" in code, svc and config file together.
public class WCClientLocalizacion: IWCClientLocalizacion
{
    Utilitarios Util = new Utilitarios();
    /// <summary>
    /// Método Lista Localizaciones
    /// </summary>
    /// <returns>Devuelve un Array</returns>
    //public WCLocalizacion.BELocalizacion[] ListarPorVehiculo(int pinIdVehiculo, DateTime pdtFechaRegistroIni, DateTime pdtFechaRegistroFin, int pinPagina)
    //{
    //    WCLocalizacion.BELocalizacion[] arr = null;
    //    WCLocalizacion.BELocalizacion be = new WCLocalizacion.BELocalizacion()
    //    {
    //        IdVehiculo = pinIdVehiculo,
    //        FechaRegistroIni = pdtFechaRegistroIni,
    //        FechaRegistroFin = pdtFechaRegistroFin,
    //        Pagina = pinPagina
    //    };
    //    using (var client = new ServiceClient<WCLocalizacion.IWCLocalizacion>("BasicHttpBinding_IWCLocalizacion"))
    //    {
    //        var response = client.ServiceOperation<WCLocalizacion.BELocalizacion[]>(c => c.ListarPorVehiculo(be));
    //        arr = response;
    //    }

    //    return arr;
    //}

   
}
