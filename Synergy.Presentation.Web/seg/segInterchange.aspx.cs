using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Synergy.Presentation.Web;

public partial class segInterchange : ObjectPage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            ValidaUsuario();
        }
    }

    protected void Salir()
    {
        Session.Clear();
        Session.Abandon();
        EliminaCookies();

    }

    protected void EliminaCookies()
    {
        string[] cookies = Request.Cookies.AllKeys;
        foreach (string cookie in cookies)
        {
            Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
        }
    }


    protected bool ValidaUsuario()
    {
        BEEntidadParam be = new BEEntidadParam();
        string strUsuario = string.Empty, strClave = string.Empty;
        string strMensaje = Request.QueryString["u"];
        string strIdioma = Request.QueryString["i"];
        //strMensaje = Server.UrlDecode(strMensaje);
        if (string.IsNullOrEmpty(strMensaje))
        {
            be.Codigo = "001";
            be.Mensaje = "Sin mensaje";
            return false;
        }
        if (strMensaje.Length <= 1)
        {
            be.Codigo = "002";
            be.Mensaje = "Cadena Incorrecta";
            return false;
        }
        Encripta encry = new Encripta();
        if (!encry.EsValidoDesencriptarCadena(strMensaje))
        {
            be.Codigo = "003";
            be.Mensaje = "Cadena no encriptada";
            return false;
        }
        
        WCUsuario.BEEntidadParam beParam = ValidarUsuario(strMensaje);
        ////Carga Informacion del Usuario

        if (beParam.ListaCadenas.Length < 5)
        {
            be.Codigo = "004";
            be.Mensaje = "Valores Incorrectos";
            return false;
        }

        UsuarioSistema = InicializaUsuario(beParam, strIdioma);

        Response.Redirect("~/gen/genPrincipal.aspx", true);
        return true;
    }

    protected BEUsuarioSistema InicializaUsuario(WCUsuario.BEEntidadParam pbe, string pstrIdioma)
    {
        BEUsuarioSistema be = new BEUsuarioSistema();

        be.Llave = pbe.ListaCadenas[0].Id;
        be.IdCompania = int.Parse(pbe.ListaCadenas[1].Id);
        WCCompania.BECompania beCom = ObtenerCompania(be.IdCompania);
        be.CodigoCompania = beCom.CodigoCompania;
        be.DescripcionCompania = beCom.Descripcion;
        be.IdAplicacion = int.Parse(pbe.ListaCadenas[3].Id);
        be.CodigoAplicacion = pbe.ListaCadenas[4].Id;
        be.CodigoUsuario = pbe.ListaCadenas[5].Id;
        be.CodigoIdioma = pstrIdioma;
        be.IdPersona = int.Parse(pbe.ListaCadenas[6].Id);
        be.DominioAplicacion = pbe.ListaCadenas[7].Id;
        be.Menu = ListarMenuPorUsuario(be.IdAplicacion, be.CodigoUsuario);

        //Anade Cookie para datos adicionales
        HttpCookie hcUsuario = new HttpCookie("PerfilUsuario");
        hcUsuario["CodigoIdioma"] = be.CodigoIdioma;
        hcUsuario["DominioAplicacion"] = be.DominioAplicacion;
        hcUsuario.Expires = DateTime.Now.AddHours(12);
        Response.Cookies.Add(hcUsuario);

        return be;
    }


    #region LLAMDAS A WEBSERVICES
    /// <summary>
    /// Método Valida el Usuario
    /// </summary>
    /// <returns>Devuelve un DataSet</returns>
    protected WCUsuario.BEEntidadParam ValidarUsuario(string pstrMensaje)
    {
        WCUsuario.BEEntidadParam be = new WCUsuario.BEEntidadParam();

        using (var client = new ServiceClient<WCUsuario.IWCUsuario>("BasicHttpBinding_IWCUsuario"))
        {
            var response = client.ServiceOperation<WCUsuario.BEEntidadParam>(
               c => c.ValidarUsuarioExiste(pstrMensaje)
               );
            ;
            be = response;
        }
        return be;
    }


    /// <summary>
    /// Método Valida el Usuario
    /// </summary>
    /// <returns>Devuelve un DataSet</returns>
    protected DataSet ListarMenuPorUsuario(int pinIdAplicacion, string pstrUsuario)
    {
        DataSet ds = new DataSet();
        using (var client = new ServiceClient<WCMenu.IWCMenu>("BasicHttpBinding_IWCMenu"))
        {
            var response = client.ServiceOperation<DataSet>(
               c => c.ListarPorUsuario(pinIdAplicacion, pstrUsuario)
               );
            ;
            ds = response;
        }
        return ds;
    }


    /// <summary>
    /// Método Valida el Usuario
    /// </summary>
    /// <returns>Devuelve un DataSet</returns>
    protected WCCompania.BECompania ObtenerCompania(int pintIdCompania)
    {
        WCCompania.BECompania be = new WCCompania.BECompania();
        be.IdCompania = pintIdCompania;
        using (var client = new ServiceClient<WCCompania.IWCCompania>("BasicHttpBinding_IWCCompania"))
        {
            var response = client.ServiceOperation<WCCompania.BECompania>(c => c.Obtener(be));
            be = response;
        }
        return be;
    }

    /// <summary>
    /// Método Obtiene Usuario
    /// </summary>
    /// <returns>BEUsuario</returns>
    protected WCUsuario.BEUsuario ObtenerUsuario(string psUsuario)
    {
        WCUsuario.BEUsuario be = new WCUsuario.BEUsuario();
        be.CodigoUsuario = psUsuario;
        using (var client = new ServiceClient<WCUsuario.IWCUsuario>("BasicHttpBinding_IWCUsuario"))
        {
            var response = client.ServiceOperation<WCUsuario.BEUsuario>(c => c.Obtener(be));
            be = response;
        }
        return be;
    }
    #endregion
}