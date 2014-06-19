using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;
using Synergy.Domain.Edu.Entities;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWCAplicacion" in both code and config file together.
[ServiceContract]

public interface IWCServicio
{
    //[WebGet(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
    //[OperationContract]
    //[WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]

    //[OperationContract]
    //[WebInvoke(Method = "GET",
    //    ResponseFormat = WebMessageFormat.Json,
    //    BodyStyle = WebMessageBodyStyle.Wrapped,
    //    UriTemplate = "GetUrl/{iType}")]
    //[WebGet(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
    //[OperationContract]
    //List<BEFamiliaSel> ListarFamiliaSelPorCampo(string pstrColumna, string pstrValor, int pinPagina );


    [WebGet(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
    [OperationContract]
    //List<BEFamiliaSel> ListarFamiliaSelPorCampo();
    ContenedorFamilia ListarFamiliaSelPorCampo(string pstrColumna, string pstrValor, int pinPagina);
 }
