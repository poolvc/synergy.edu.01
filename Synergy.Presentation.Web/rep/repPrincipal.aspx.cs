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

public partial class repPrincipal : ObjectPage
{

    #region CONSTANTES

     

    #endregion

    #region EVENTOS

    protected void Page_Load(object sender, EventArgs e)
    {
        //Eventos del Master
        Master.MenuAccionEvento += new MasterPageMenuReporteClickHandler(Master_MenuButton);
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
            case "Exportar":
                Exportar();
                break;
            case "Imprimir":
                Imprimir();
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
        string strFormulario = e.CommandArgument.ToString();
        switch (e.CommandName)
        {
            case "Ejecutar":
                Ejecutar(strFormulario, e.Item);
                break;
        }
    }



    #endregion

    #region METODOS

    protected void Cargar()
    {
        ucpagLista.NumeroPagina = 1;
        ddlEstado.SelectedIndex = 0; //Estado Activo
        hfMenuCodigo.Value = Request.QueryString["m"];   //l-Carga manual
        //Cargar Estado
        Util.loadCombo(ddlEstado, ListarParametroDetalle(1, 1, Constantes.PARAMETRO_ESTADOREGISTRO), "Descripcion", "ParametroDetalle");

        if (!string.IsNullOrEmpty(Request.QueryString["f"]))
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

        strEstado = ddlEstado.SelectedValue;
        strDescripcion = txtDescripcion.Text.Trim();
        //Validar
        int pintPagina = ObtenerNumeroPagina();
        //Filtrar
        DataTable dt = ListarReportePorUsuario(txtDescripcion.Text.Trim(), UsuarioSistema.IdAplicacion, UsuarioSistema.CodigoUsuario, ddlEstado.SelectedValue, pintPagina);
        lvLista.DataSource = dt;
        lvLista.DataBind();
        if (dt.Rows.Count > 0)
        {
            intFilasXPagina = Convert.ToInt32(dt.Rows[0]["FilasXPagina"]);
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
        string[] SetearFiltro = Request.QueryString["f"].ToString().Split('|');

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


    protected void Ejecutar(string pstrFormulario, ListViewItem plviObj)
    {
        HiddenField hfIdAplicacion = (HiddenField)plviObj.FindControl("hfIdAplicacion");
        HiddenField hfIdGrupoReporte = (HiddenField)plviObj.FindControl("hfIdGrupoReporte");
        HiddenField hfIdReporte = (HiddenField)plviObj.FindControl("hfIdReporte");

        string strCadena = string.Format("?a=r&iA={0}&iG={1}&iR={2}&f={3}", hfIdAplicacion.Value, hfIdGrupoReporte.Value, hfIdReporte.Value, ObtenerFiltroParametro());
        Util.Redireciona(pstrFormulario, strCadena, true);
    }



    protected void Exportar()
    {
        UtilExcel ut = new UtilExcel();
        DataTable dt = ListarReportePorUsuario(txtDescripcion.Text.Trim(), UsuarioSistema.IdAplicacion, UsuarioSistema.CodigoUsuario, ddlEstado.SelectedValue, 0);
        dt.Columns.Remove("FilasXPagina");
        dt.Columns.Remove("TotalFilas");
        ut.ExportaAExcelHoja(dt, Resources.resDiccionario.Archivo, Resources.resDiccionario.Datos);
    }

    protected void Imprimir()
    {
        Util.ejecutaScriptAJAX(this.Page, string.Format("window.open('../rep/repPreview.aspx');"));
    }
    #endregion

    #region WEBSERVICES

    /// <summary>
    /// Método Valida el Usuario
    /// </summary>
    /// <returns>Devuelve un DataSet</returns>
    protected DataTable ListarReportePorUsuario(string pstrDescripcion, int pinIdAplicacion, string pstrCodigoUsuario, string pstrEstado, int pintPagina)
    {
        WCReporte.BEReporte be = new WCReporte.BEReporte()
        {
            Descripcion = pstrDescripcion,
            IdAplicacion = pinIdAplicacion,
            CodigoUsuario = pstrCodigoUsuario,
            Pagina = pintPagina,
            Estado = pstrEstado,
            Tipo = "R",

        };

        DataTable dt = new DataTable();
        using (var client = new ServiceClient<WCReporte.IWCReporte>("BasicHttpBinding_IWCReporte"))
        {
            var response = client.ServiceOperation<DataTable>(c => c.ListarPorUsuario(be).Tables[0]);
            dt = response;
        }
        return dt;
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