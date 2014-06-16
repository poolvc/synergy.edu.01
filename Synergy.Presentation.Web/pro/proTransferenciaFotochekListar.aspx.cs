using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Synergy.Presentation.Web;
using Microsoft.Reporting.WebForms;
using System.Collections.Specialized;
using Synergy.Infraestructure.CrossCutting;

public partial class proTransferenciaFotochekListar : ObjectPage
{

    #region CONSTANTES

    private const string RUTA_LISTA = "~/pro/proPrincipal.aspx";
    protected const string REPORTE_RUTA = "/Reports/GALEXITO_ALE/repModeloNegocio";
    protected const string EDITAR_RUTA = "~/pro/rofSimulacionMensualEditar.aspx";

    #endregion 

    #region EVENTOS

    protected void Page_Load(object sender, EventArgs e)
    {
        //Eventos del Master
        Master.MenuAccionEvento += new MasterPageMenuReporteVistaClickHandler(Master_MenuButton);
        if (!IsPostBack)
        {            
            Cargar();
            Listar();
        }
    }

    protected void Master_MenuButton(object sender, EventArgs e)
    {
        string strAccion = ((CommandEventArgs)e).CommandName;
        switch (strAccion)
        {
            case "Buscar":
                ucpagLista.NumeroPagina = 1;
                ViewState["NumeroPagina"] = null;
                Listar();
                break;
            case "Nuevo":
                Nuevo();
                break;
            case "Exportar":
                Exportar();
                break;
            case "Imprimir":
                Imprimir();
                break;
            case "Salir":
                Salir();
                break;
        }
    }

    protected void ucpagLista_CambioPagina(object sender, EventArgs e)
    {
        ViewState["NumeroPagina"] = null;
        Listar();
    }

    protected void lvLista_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        string strId = e.CommandArgument.ToString();
        switch (e.CommandName)
        {
            case "Editar":
                Editar(strId, e.Item);
                break;
            case "Copiar":
                Copiar(strId, e.Item);
                break;
        }
    }

    #endregion 

    #region METODOS


    protected void Salir()
    {
        string FiltroParam = Request.QueryString["f"];
        Util.Redireciona(RUTA_LISTA, "?a=l&f=" + FiltroParam, true);
    }


    protected void Cargar()
    {
        ucpagLista.NumeroPagina = 1;
        ddlEstado.SelectedIndex = 0; //Estado Activo
        //Master.MenuNuevo.Visible = false;
        Master.MenuExportar.Visible = false;
        Master.MenuImprimir.Visible = false;
       
        //Carga Estado
        Util.loadCombo(ddlEstado, ListarParametroDetalle(1, 1, Constantes.PARAMETRO_ESTADOREGISTRO), "Descripcion", "ParametroDetalle");


        if (!string.IsNullOrEmpty(Request.QueryString["f2"]))
        {
            SetarParametros();
        }

    }

    /// <summary>
    /// Método Lista
    /// </summary>
    /// <returns></returns>
    protected void Listar()
    {
        int intTotalFilas = 0, intFilasXPagina = 0;
        string strDescripcion, strEstado;

        strDescripcion = txtDescripcion.Text.Trim();
        strEstado = ddlEstado.SelectedValue;
        //Validar
        int pintPagina = ObtenerNumeroPagina();
        //Filtrar
        DataTable dt = ListarCargaManual(strDescripcion, ddlEstado.SelectedValue, pintPagina);
        lvLista.DataSource = dt;
        lvLista.DataBind();
        if (dt.Rows.Count > 0)
        {
            intFilasXPagina =  Convert.ToInt32(dt.Rows[0]["FilasXPagina"]);
            intTotalFilas = Convert.ToInt32(dt.Rows[0]["TotalFilas"]);
        }
        ucpagLista.TotalRegistros = intTotalFilas;
        ucpagLista.RegistrosPorPagina = intFilasXPagina;
        ucpagLista.EnlazarPaginador();

        if (ViewState["NumeroPagina"] != null)
        {
            int lnNumeroPagina = 0;
            int.TryParse(ViewState["NumeroPagina"].ToString(), out lnNumeroPagina);
            ucpagLista.NumeroPagina = lnNumeroPagina;
        }

    }


    void SetarParametros()
    {
        string[] SetearFiltro = Request.QueryString["f2"].ToString().Split('|');

        txtDescripcion.Text = SetearFiltro[0];

        ddlEstado.SelectedValue = SetearFiltro[1];
        ViewState["NumeroPagina"] = int.Parse(SetearFiltro[2]);

    }

    private int ObtenerNumeroPagina()
    {
        int lnNumeroPagina = 0;
        if (ViewState["NumeroPagina"] != null)
        {
            lnNumeroPagina = (Int32)ViewState["NumeroPagina"];
        }
        else
        {
            lnNumeroPagina = ucpagLista.NumeroPagina;
        }
        return lnNumeroPagina;
    }

    private string ObtenerFiltroParametro()
    {
        string Filtros = string.Empty;

        Filtros = txtDescripcion.Text; // 0.-

        Filtros = Filtros + "|" + ddlEstado.SelectedValue;// 1.-
        Filtros = Filtros + "|" + ucpagLista.NumeroPagina.ToString();// 2.-

        return Filtros;
    }

    protected void Nuevo()
    {
        string strCadena = "?a=n";
        Util.Redireciona(EDITAR_RUTA, strCadena, true);
    }

    protected void Editar(string strId, ListViewItem plvi)
    {
        Label lblPeriodoAnual = (Label)plvi.FindControl("lblPeriodoAnual");
        string strCadena = "?a=e&i=" + strId + "&p=" + lblPeriodoAnual.Text;

        strCadena = strCadena + string.Format("&iA={0}&iG={1}&iR={2}&f2={3}", Request.QueryString["iA"], Request.QueryString["iG"], Request.QueryString["iR"], ObtenerFiltroParametro());

        Util.Redireciona(EDITAR_RUTA, strCadena, true);
    }

    protected void Eliminar(string strId)
    {
        string strCadena = "?a=d&i=" + strId;
        Util.Redireciona(EDITAR_RUTA, strCadena, true);
    }

    protected void Copiar(string strId, ListViewItem plvi)
    {
        Label lblPeriodoAnual = (Label)plvi.FindControl("lblPeriodoAnual");
        if (string.IsNullOrEmpty(lblPeriodoAnual.Text))
        {
            Util.ejecutaScriptAJAX(this.Page, string.Format("alert('{0}');", "No hay datos para copiar."));
            return;
        }
        string strCadena = "?a=c&i=" + strId + "&p=" + lblPeriodoAnual.Text;
        Util.Redireciona(EDITAR_RUTA, strCadena, true);
    }

    protected void Exportar()
    {
        UtilReporte ut = new UtilReporte();
        BEEntidadString[] beArr = new  BEEntidadString[2];

        string strDescripcion = txtDescripcion.Text.Trim(), strEstado = ddlEstado.SelectedValue;
        //Segun el Reporting Service
        beArr[0] = new BEEntidadString("as_Descripcion", strDescripcion);
        beArr[1] = new BEEntidadString("as_Estado", strEstado);

        //ut.Exportar(REPORTE_RUTA, "Excel", beArr);
    }

    protected void Imprimir()
    {
        Util.ejecutaScriptAJAX(this.Page, string.Format("window.open('../rep/repPreview.aspx');"));
    }
    #endregion

    #region WEBSERVICES

    /// <summary>
    /// Método Listar las Cargas Manuales
    /// </summary>
    /// <returns>Devuelve un DataSet</returns>
    protected DataTable ListarCargaManual(string pstrDescripcion, string pstrEstado, int pinPagina)
    {
        
        return null;
    }

    /// <summary>
    /// Método Lista Companias
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