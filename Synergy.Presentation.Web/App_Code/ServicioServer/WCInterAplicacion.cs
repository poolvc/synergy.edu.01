using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Synergy.Presentation.Web;
using System.Web;


// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WCAplicacion" in code, svc and config file together.
public class WCInterAplicacion : IWCInterAplicacion
{
    //BLMensaje _bl = new BLMensaje();

    public BEEntidadParam Asociar(BEUsuarioSistema pbeUsuarioSistema)
    {
        return ValidaUsuario(pbeUsuarioSistema);
    }

    protected BEEntidadParam ValidaUsuario(BEUsuarioSistema pbeUsuarioSistema)
    {
        BEEntidadParam be = new BEEntidadParam();
        string strUsuario = string.Empty, strClave = string.Empty;

        strUsuario = pbeUsuarioSistema.CodigoUsuario;
        strClave = pbeUsuarioSistema.Clave;

        if (string.IsNullOrEmpty(strUsuario))
        {
            be.Codigo = "001";
            be.Mensaje = "Ingrese el usuario.";
            return be;
        }

        //WCUsuarioRef.BEEntidadParam bepar = ValidarUsuario(pbeUsuarioSistema);
        ////Carga Informacion del Usuario

        //if (bepar.Id < 0)
        //{
        //    be.Codigo = "003";
        //    be.Mensaje = "Usuario no Valido";
        //    return be;
        //}
        be.Codigo = "000";
        be.Mensaje = "Ok";
        HttpContext.Current.Response.Redirect("http://localhost:1258/Galexito.Mof.Web/default.aspx",true);
        
        return be;
    }

    /// <summary>
    /// Método Valida el Usuario
    /// </summary>
    /// <returns>Devuelve un DataSet</returns>
    //protected WCUsuarioRef.BEEntidadParam ValidarUsuario(BEUsuarioSistema pbeUsuarioSistema)
    //{
    //    WCUsuarioRef.BEEntidadParam be = new WCUsuarioRef.BEEntidadParam();

    //    using (var client = new ServiceClient<WCUsuarioRef.IWCUsuarioRef>("BasicHttpBinding_IWCUsuarioRef"))
    //    {
    //        var response = client.ServiceOperation<WCUsuarioRef.BEEntidadParam>(
    //           c => c.ValidarUsuario(pbeUsuarioSistema.Codigo, pbeUsuarioSistema.Clave, 1)
    //           );
    //        ;
    //        be = response;
    //    }
    //    return be;
    //}

}
