using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Synergy.Presentation.Web;
using Synergy.Infraestructure.CrossCutting;

public partial class segInterchangeSendHome : ObjectPage
{
    #region EVENTOS

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            EnviarUsuario();
        }
    }

    #endregion

    #region METODOS

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

    protected void EnviarUsuario()
    {
        string strCadena = string.Empty;
        string strAccion = Request.QueryString["a"];
        string strMensaje = string.Empty;
        string strIdioma = string.Empty;
        string strDominioURL = string.Empty, strDominio = string.Empty;
        StringBuilder strb = new StringBuilder();
        
        if (strAccion == "o") //Si no es Time Out (Fin de Session)
        {
            if (Request.Cookies["PerfilUsuario"] != null)
            {
                strIdioma = Request.Cookies["PerfilUsuario"]["CodigoIdioma"];
                strDominio = Request.Cookies["PerfilUsuario"]["DominioAplicacion"];
                //Obtener la ULR en base al Dominio de la Aplicacion
                strDominioURL = ObtenerUrlDominio(strDominio);
            }
            else
            {
                strIdioma = AppSettings.LenguajeDesarrollo;
            }
        }
        else //Para Otros casos
        {
            Encripta encry = new Encripta();
            BEUsuarioSistema beUsr = UsuarioSistema;

            //Obtener el Dominio de la Aplicacion
            strDominioURL = ObtenerUrlDominio(beUsr.DominioAplicacion);
            strMensaje = encry.EncriptarCadena(beUsr.Llave + "|" + beUsr.IdCompania.ToString() + "|" + beUsr.CodigoCompania + "|" + Constantes.IDAPLICACION_SYSTEM.ToString() + "|" + Constantes.CODIGO_APLICACION_SYSTEM + "|" + beUsr.CodigoUsuario + "|" + beUsr.DominioAplicacion);
            strMensaje = Server.UrlEncode(strMensaje);
            strIdioma = beUsr.CodigoIdioma;
            Salir();
        }
        if (string.IsNullOrEmpty(strDominioURL))
        {
            WCAplicacion.BEAplicacion be = ObtenerAplicacion(Constantes.IDAPLICACION_SYSTEM);
            strDominioURL = be.DominioURL;
        }
        Response.Redirect(strDominioURL + @"/seg/segInterchangeReceiveHome.aspx?u=" + strMensaje + "&i=" + strIdioma  + "&a=" + strAccion, false);
    }

    protected string ObtenerUrlDominio(string pstrDominio)
    {
        string strUrlDominio  = string.Empty;
        if (!string.IsNullOrEmpty(pstrDominio))
        {
            DataTable dtDominioRedir = ListarAplicacionDominioPorAplicacion(Constantes.IDAPLICACION_SYSTEM);
            DataRow[] drs = dtDominioRedir.Select(" Dominio='" + pstrDominio + "'");
            if (drs.Length > 0)
            {
                strUrlDominio = drs[0]["DominioURL"].ToString();
            }
        }
        return strUrlDominio;
    }


    #endregion

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
    /// Método Obtiene Aplicacion
    /// </summary>
    /// <returns>Devuelve una Aplicacion</returns>
    protected WCAplicacion.BEAplicacion ObtenerAplicacion(int pinIdAplicacion)
    {
        WCAplicacion.BEAplicacion be = new WCAplicacion.BEAplicacion();
        be.IdAplicacion = pinIdAplicacion;

        using (var client = new ServiceClient<WCAplicacion.IWCAplicacion>("BasicHttpBinding_IWCAplicacion"))
        {
            var response = client.ServiceOperation<WCAplicacion.BEAplicacion>(c => c.Obtener(be));
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
    /// Método recupera las Suits 
    /// </summary>
    /// <returns>Devuelve un DataSet</returns>
    public DataTable ListarAplicacionDominioPorAplicacion(int pinIdAplicacion)
    {
        DataTable dt = new DataTable();
        WCAplicacionDominio.BEAplicacionDominio beAplicacionDominio = new WCAplicacionDominio.BEAplicacionDominio();
        beAplicacionDominio.IdAplicacion = pinIdAplicacion;
        using (var client = new ServiceClient<WCAplicacionDominio.IWCAplicacionDominio>("BasicHttpBinding_IWCAplicacionDominio"))
        {
            var response = client.ServiceOperation<DataTable>(c => c.ListarPorAplicacion(beAplicacionDominio).Tables[0]);
            dt = response;
        }
        return dt;
    }

    #endregion
}