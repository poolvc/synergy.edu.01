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

public partial class genParametroListar : ObjectPage
{

    #region CONSTANTES

    protected const string REPORTE_RUTA = "/Reports/GALEXITO_GEN/repGrupo";
    protected const string EDITAR_RUTA = "~/gen/genParametroEditar.aspx";

    #endregion

    #region EVENTOS

    protected void Page_Load(object sender, EventArgs e)
    {
        //Eventos del Master
        Master.MenuAccionEvento += new MasterPageMenuListarClickHandler(Master_MenuButton);
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
        HiddenField hfIdAplicacion = (HiddenField)e.Item.FindControl("hfIdAplicacion");
        //string strIdAp = hfIdAplicacion.Value;
        switch (e.CommandName)
        {
            case "Editar":
                Editar(strId);
                break;
            case "Eliminar":
                Eliminar(strId);
                break;
        }
    }

    protected void lvLista_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ImageButton ibt;
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            ibt = (ImageButton)e.Item.FindControl("ibtEliminar");
            Label lb = (Label)e.Item.FindControl("lblEstado");
            if (((Label)e.Item.FindControl("lblEstado")).Text == Constantes.ESTADO_ACTIVO_DESC)
            {
                ibt.ImageUrl = Constantes.ESTADO_ACTIVO_URL;
                ibt.ToolTip = "Inactivar";
            }
            else
            {
                ibt.ImageUrl = Constantes.ESTADO_INACTIVO_URL;
                ibt.ToolTip = "Activar";
            }
        }
    }

    #endregion

    #region METODOS

    protected void Cargar()
    {
        //Master.Titulo.Text = "Nivel Alerta";
        ucpagLista.NumeroPagina = 1;
        ddlEstado.SelectedIndex = 0; //Estado Activo
        //Page.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
        //lvLista.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
        //Master.MenuAccion.Items.Remove(Master.MenuAccion.Items[1]);


        //Carga Estado
        Util.loadCombo(ddlEstado, ListarParametroDetalle(Constantes.IDCOMPANIA_DEFAULT, Constantes.IDAPLICACION_SYSTEM, Constantes.PARAMETRO_ESTADOREGISTRO), "Descripcion", "ParametroDetalle");
        //Carga Tipo
        Util.loadCombo(ddlTipo, ListarParametroDetalle(Constantes.IDCOMPANIA_DEFAULT, Constantes.IDAPLICACION_SYSTEM, Constantes.PARAMETRO_TIPOPARAMETRO), "Descripcion", "ParametroDetalle");

        ddlTipoDato.Items.Add(new ListItem(Resources.resDiccionario.TodosFiltro, string.Empty));
        ddlTipoDato.Items.Add(new ListItem(Resources.resDiccionario.Texto, "T"));
        ddlTipoDato.Items.Add(new ListItem(Resources.resDiccionario.NumeroData, "N"));
        ddlTipoDato.Items.Add(new ListItem(Resources.resDiccionario.Fecha, "F"));

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
        int intTotalFilas = 0, intFilasXPagina = 0, inIdAplicacion = 0;
        string strDescripcion, strEstado, strTipo, strTipoDato;

        strTipo = ddlTipo.SelectedValue;
        strTipoDato = ddlTipoDato.SelectedValue;
        strEstado = ddlEstado.SelectedValue;
        strDescripcion = txtDescripcion.Text.Trim();
        inIdAplicacion = UsuarioSistema.IdAplicacion;
        //Validar

        //Filtrar
        DataTable dt = ListarParametro(inIdAplicacion, strDescripcion, strTipoDato, strTipo, ddlEstado.SelectedValue, ucpagLista.NumeroPagina);
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
        ddlTipoDato.SelectedValue = SetearFiltro[1];
        ddlTipo.SelectedValue = SetearFiltro[2];
        ddlEstado.SelectedValue = SetearFiltro[3];
        ViewState["NumeroPagina"] = int.Parse(SetearFiltro[4]);

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
        Filtros = Filtros + "|" + ddlTipoDato.SelectedValue;// 1.-
        Filtros = Filtros + "|" + ddlTipo.SelectedValue;// 2.-
        Filtros = Filtros + "|" + ddlEstado.SelectedValue;// 3.-
        Filtros = Filtros + "|" + ucpagLista.NumeroPagina.ToString();// 4.-

        return Filtros;
    }


    protected void Nuevo()
    {
        string strCadena = "?a=n";
        Util.Redireciona(EDITAR_RUTA, strCadena, true);
    }

    protected void Editar(string strIdParametro)
    {
        string strCadena = "?a=e&i=" + strIdParametro + "&f=" + ObtenerFiltroParametro();
        Util.Redireciona(EDITAR_RUTA, strCadena, true);
    }

    protected void Eliminar(string strIdParametro)
    {
        string strCadena = "?a=d&i=" + strIdParametro + "&f=" + ObtenerFiltroParametro();
        Util.Redireciona(EDITAR_RUTA, strCadena, true);
    }

    protected void Exportar()
    {
        UtilExcel ut = new UtilExcel();
        DataTable dt = ListarParametro(UsuarioSistema.IdAplicacion, txtDescripcion.Text.Trim(), ddlTipoDato.SelectedValue, ddlTipo.SelectedValue, ddlEstado.SelectedValue, 0);
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

    protected DataTable ListarAplicacion()
    {
        DataTable dt = new DataTable();
        //WCAplicacion.BEAplicacion beAp = new WCAplicacion.BEAplicacion();
        //beAp.IdSuit = 0;
        //beAp.Descripcion = string.Empty;
        //beAp.Estado = "A";
        //beAp.Pagina = 0;
        using (var client = new ServiceClient<WCAplicacion.IWCAplicacion>("BasicHttpBinding_IWCAplicacion"))
        {
            var response = client.ServiceOperation<DataTable>(
                c => c.Listar().Tables[0]
                    );
            ;
            dt = response;
        }
        return dt;
    }

    /// <summary>
    /// Método Lista Niveles de Alerta
    /// </summary>
    /// <returns>Devuelve un DataSet</returns>
    protected DataTable ListarParametro(int pintIdAplicacion, string pstrDescripcion, string psrtTipoDato, string psrtTipo, string pstrEstado, int pintPagina)
    {

        WCParametro.BEParametro be = new WCParametro.BEParametro();
        be.IdAplicacion = pintIdAplicacion;
        be.Descripcion = pstrDescripcion;
        be.TipoDato = psrtTipoDato;
        be.Tipo = psrtTipo;
        be.Estado = pstrEstado;
        be.Pagina = pintPagina;
        DataTable dt = new DataTable();
        using (var client = new ServiceClient<WCParametro.IWCParametro>("BasicHttpBinding_IWCParametro"))
        {
            var response = client.ServiceOperation<DataTable>(
               c => c.Listar(be).Tables[0]
               );
            ;
            dt = response;
        }
        return dt;
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

    protected DataTable ListarAplicaciones()
    {

        WCAplicacion.BEAplicacion be = new WCAplicacion.BEAplicacion();
        be.Estado = "A";

        DataTable dt = new DataTable();
        using (var client = new ServiceClient<WCAplicacion.IWCAplicacion>("BasicHttpBinding_IWCAplicacion"))
        {
            var response = client.ServiceOperation<DataTable>(c => c.Listar().Tables[0]);
            dt = response;
        }
        return dt;
    }
    #endregion


}