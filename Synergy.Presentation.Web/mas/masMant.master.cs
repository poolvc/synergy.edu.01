using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Synergy.Presentation.Web;

public partial class masMant : System.Web.UI.MasterPage
{

    #region CONSTANTES

    private const string CONTENIDO_SITIOCONFIG = "SITIOCONF";
    private const string CONTENIDO_SITIOCONFIG_SETDEVEL = "SETDEVEL";
    private const string CONTENIDO_SITIOCONFIG_Evercom = "Evercom";
    private const string CONTENIDO_SITIOCONFIG_MSNET = "MSNET";
    private const string CONTENIDO_SITIOCONFIG_MSSQL = "MSSQL";
    private const string CONTENIDO_SITIOCONFIG_REDSOCIAL1 = "REDSOCIAL1";
    private const string CONTENIDO_SITIOCONFIG_REDSOCIAL2 = "REDSOCIAL2";

    #endregion
    

    #region Public Properties

    private Utilitarios _util = new Utilitarios();
    public Utilitarios Util
    {
        get { return _util; }
    }

    public BEUsuarioSistema UsuarioSistema
    {
        get { return (BEUsuarioSistema)Session["UsuarioSistema"]; }
    }

    public HiddenField MenuDato
    {
        set { this.hfMenuDato = value; }
        get { return this.hfMenuDato; }
    }

    public HiddenField MenuCodigo
    {
        set { this.hfMenuCodigo = value; }
        get { return this.hfMenuCodigo; }
    }

    public String UsuarioCodigo
    {
        set { this.hfUsuarioCodigo.Value = value; }
        get { return this.hfUsuarioCodigo.Value; }
    }

    public HtmlGenericControl BodyTagMant
    {
        set { this.Master.BodyTag = value; }
        get { return this.Master.BodyTag; }
    }

    #endregion Public Properties

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarIni();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Cargar();
        }
    }

    protected void CargarIni()
    {
        BEUsuarioSistema beUsu = this.UsuarioSistema;
        Cargar(beUsu);
        CargarMenu(beUsu);
    }

    protected void CargarMenu(BEUsuarioSistema pbeUsu)
    {       
        StringBuilder strbMenu = new StringBuilder();
        string strFiltro = string.Empty;

        DataSet dt = pbeUsu.Menu;
        this.UsuarioCodigo = hfUsuarioCodigo.Value;
        DataRow[] drs = null;
        string strAct = Request.QueryString["m"];
        MenuCodigo.Value = strAct;
        
        if (string.IsNullOrEmpty(strAct))
        {
            drs = dt.Tables[0].Select("Nivel > 0 and Nivel = 1 "); //Solo extrae del nivel 0
        }
        else
        {
            int inCont = 1;
            strFiltro = " Nivel = 1 ";
            while (inCont <= strAct.Length / 3)
            {
                strFiltro = strFiltro + "OR (Orden Like SUBSTRING('" + strAct + "',1," + (inCont * 3).ToString() + ") + '%' AND Nivel = " + (inCont + 1).ToString() + ")";
                inCont = inCont + 1;
            }

            drs = dt.Tables[0].Select(strFiltro, " Orden Asc "); //Solo extrae del nivel 0
        }
        
        string strActivo = string.Empty;

        WCAplicacion.BEAplicacion be = ObtenerAplicacion(pbeUsu.IdAplicacion);
        strbMenu.AppendFormat(" <div class='{0}'>", "menutitulo");
        strbMenu.AppendFormat(" <h2>{0}</h2>",  be.Descripcion);
        strbMenu.AppendFormat("</div>");

        int inNivel = 0, intNivelAnt = 0;
        foreach (DataRow dr in drs)
        {
            intNivelAnt = inNivel;
            inNivel = int.Parse(dr["Nivel"].ToString());

            string strObjeto = dr["Objeto"].ToString();
            string strQuery = string.Empty;
            int inPosQuery = strObjeto.IndexOf('?');
            if (inPosQuery >= 0)
            {
                strObjeto = dr["Objeto"].ToString().Substring(0, inPosQuery);
                strQuery = dr["Objeto"].ToString().Substring(inPosQuery).Replace('?', '&');
            }
            if (strAct == dr["Orden"].ToString())
            {
                strActivo = "active";
                MenuDato.Value = dr["Descripcion"].ToString();
            }
            else
            {
                strActivo = string.Empty;
            }

            if (inNivel == intNivelAnt)
            {
                strbMenu.AppendFormat(" <li><a class='{0}' href='{1}?m={2}{3}'>{4}</a></li>", strActivo, strObjeto, dr["Orden"].ToString(), strQuery, dr["Descripcion"].ToString());
            }
            else if (inNivel > intNivelAnt)
            {
                strbMenu.AppendFormat(" <ul class='{0}'>", "menu-side" + inNivel.ToString());
                strbMenu.AppendFormat(" <li><a class='{0}' href='{1}?m={2}{3}'>{4}</a></li>", strActivo, strObjeto, dr["Orden"].ToString(), strQuery, dr["Descripcion"].ToString());
            }
            else if (inNivel < intNivelAnt)
            {                
                strbMenu.AppendFormat(" </ul>");
                strbMenu.AppendFormat(" <li><a class='{0}' href='{1}?m={2}{3}'>{4}</a></li>", strActivo, strObjeto, dr["Orden"].ToString(), strQuery, dr["Descripcion"].ToString());
            }

        }
        strbMenu.Append("       </ul>");
        lblMenu.Text = strbMenu.ToString();
    }



    protected void Cargar(BEUsuarioSistema pbeUsu)
    {
        ConfiguraHeader(pbeUsu);
        ConfiguraFooter();
    }

    protected void ConfiguraHeader(BEUsuarioSistema pbeUsu)
    {
        //Mostrar Perfil
        lblCompania.Text = pbeUsu.DescripcionCompania + "(" + pbeUsu.CodigoCompania + ")";
        lblUsuario.Text = pbeUsu.CodigoUsuario.ToUpper();

        //CargarIdioma
        //Util.loadCombo(ddlIdioma, ListarParametroDetalle(1, 1, Constantes.PARAMETRO_IDIOMASGALEX), "Descripcion", "ParametroDetalle");
        //Util.SelecionaItemCombo(ddlIdioma, pbeUsu.CodigoIdioma);

    }

    protected void ConfiguraFooter()
    {
        DataTable dt = ListarContenidoPorSeccionCodigo(CONTENIDO_SITIOCONFIG);
        DataRow[] arrdr = dt.Select(string.Format("CodigoContenido='{0}'", CONTENIDO_SITIOCONFIG_SETDEVEL));
        if (arrdr.Length > 0)
        {
            lblEmpresaDesarrolla.Text = arrdr[0]["Titulo"].ToString();
            aHtmUrlDevel.HRef = arrdr[0]["Url"].ToString();
            aHtmUrlDevel.InnerText = arrdr[0]["Titulo"].ToString();
        }
        arrdr = dt.Select(string.Format("CodigoContenido='{0}'", CONTENIDO_SITIOCONFIG_Evercom));
        if (arrdr.Length > 0)
        {
            aHtmUrlEmpresa.HRef = arrdr[0]["Url"].ToString();
            aHtmUrlEmpresa.InnerHtml = arrdr[0]["Titulo"].ToString();
            aHtmUrlEmpresa.InnerText = arrdr[0]["Titulo"].ToString();

            aHtmlCont.HRef = "mailto:" + arrdr[0]["TextoIntro"].ToString();
            aHtmlCont.InnerHtml = arrdr[0]["TextoIntro"].ToString();
            aHtmlCont.InnerText = "e-mail: " + arrdr[0]["TextoIntro"].ToString();
        }

        arrdr = dt.Select(string.Format("CodigoContenido='{0}'", CONTENIDO_SITIOCONFIG_MSSQL));
        if (arrdr.Length > 0)
        {
            aHtmUrlSql.HRef = arrdr[0]["Url"].ToString();
            aHtmUrlSql.Title = arrdr[0]["Titulo"].ToString();
            imgHtmSql.Src = arrdr[0]["Imagen"].ToString();
            imgHtmSql.Alt = arrdr[0]["Titulo"].ToString();
        }
        arrdr = dt.Select(string.Format("CodigoContenido='{0}'", CONTENIDO_SITIOCONFIG_MSNET));
        if (arrdr.Length > 0)
        {
            aHtmUrlNet.HRef = arrdr[0]["Url"].ToString();
            aHtmUrlNet.Title = arrdr[0]["Titulo"].ToString();
            imgHtmNet.Src = arrdr[0]["Imagen"].ToString();
            imgHtmNet.Alt = arrdr[0]["Titulo"].ToString();
        }

        arrdr = dt.Select(string.Format("CodigoContenido='{0}'", CONTENIDO_SITIOCONFIG_REDSOCIAL1));
        if (arrdr.Length > 0)
        {
            aHtmUrlRed1.HRef = arrdr[0]["Url"].ToString();
            aHtmUrlRed1.Title = arrdr[0]["Titulo"].ToString();
            imgHtmRed1.Src = arrdr[0]["Imagen"].ToString();
            imgHtmRed1.Alt = arrdr[0]["Titulo"].ToString();
        }

        arrdr = dt.Select(string.Format("CodigoContenido='{0}'", CONTENIDO_SITIOCONFIG_REDSOCIAL2));
        if (arrdr.Length > 0)
        {
            aHtmUrlRed2.HRef = arrdr[0]["Url"].ToString();
            aHtmUrlRed2.Title = arrdr[0]["Titulo"].ToString();
            imgHtmRed2.Src = arrdr[0]["Imagen"].ToString();
            imgHtmRed2.Alt = arrdr[0]["Titulo"].ToString();
        }
    }


    #region LLAMDAS A WEBSERVICES

    //<summary>
    //Método Obtiene la Aplicacion
    //</summary>
    //<returns>Devuelve un Objeto</returns>

    protected DataTable ListarContenidoPorSeccionCodigo(string pstrCodigoSeccion)
    {
        WCContenido.BEContenido be = new WCContenido.BEContenido() { CodigoSeccion = pstrCodigoSeccion };
        DataTable dt = null;
        using (var client = new ServiceClient<WCContenido.IWCContenido>("BasicHttpBinding_IWCContenido"))
        {
            dt = client.ServiceOperation<DataTable>(c => c.ListarPorSeccionCodigo(be).Tables[0]);
        }
        return dt;
    }

    /// <summary>
    /// Método Obtiene la Aplicacion
    /// </summary>
    /// <returns>Devuelve un Objeto</returns>
    protected WCAplicacion.BEAplicacion ObtenerAplicacion(int pintIdAplicacion)
    {
        WCAplicacion.BEAplicacion be = new WCAplicacion.BEAplicacion();
        be.IdAplicacion = pintIdAplicacion;
        using (var client = new ServiceClient<WCAplicacion.IWCAplicacion>("BasicHttpBinding_IWCAplicacion"))
        {
            var response = client.ServiceOperation<WCAplicacion.BEAplicacion>(c => c.Obtener(be));
            be = response;
        }
        return be;
    }

    /// <summary>
    /// Método Lista Datos de ParametroDetalle
    /// </summary>
    /// <returns>Devuelve un dataTable</returns>
    protected DataTable ListarParametroDetalle(int pintidCompania, int pintIdAplicacion, string pstrParametro)
    {
        WCParametroDetalle.BEParametroDetalle be = new WCParametroDetalle.BEParametroDetalle();
        be.IdCompania = pintidCompania;
        be.IdAplicacion = pintIdAplicacion;
        be.Parametro = pstrParametro;
        DataTable dt = new DataTable();
        using (var client = new ServiceClient<WCParametroDetalle.IWCParametroDetalle>("BasicHttpBinding_IWCParametroDetalle"))
        {
            var response = client.ServiceOperation<DataTable>(c => c.Listar(be).Tables[0]);
            dt = response;
        }
        return dt;
    }

    #endregion
}
