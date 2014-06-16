using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Synergy.Presentation.Web;
using System.ServiceModel.Web;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWCAplicacion" in both code and config file together.
[ServiceContract]
public interface IWCClientVehiculo
{

  
    //[WebGet(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
    //[OperationContract]
    //WCVehiculo.BEVehiculo[] ListarVehiculosRastreo(string pstrDescripcion, string pstrEstado, string pstrIdCliente, string pstrPagina);
    
}
