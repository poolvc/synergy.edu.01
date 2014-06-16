using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using Synergy.Presentation.Web;
using Synergy.Infraestructure.CrossCutting;

public partial class ucPersonaSelector : System.Web.UI.UserControl
{
    #region CONSTANTES


    #endregion

    #region PROPERTIES


    public string EsEmpleado
    {
        get { return hfEsEmpleado.Value; }
        set { hfEsEmpleado.Value = value; }
    }

    public string EsCliente
    {
        get { return hfEsCliente.Value; }
        set { hfEsCliente.Value = value; }
    }

    public string EsProveedor
    {
        get { return hfEsProveedor.Value; }
        set { hfEsProveedor.Value = value; }
    }

    public string EsOtros
    {
        get { return hfEsOtros.Value; }
        set { hfEsOtros.Value = value; }
    }

    public string IdPersona
    {
        get { return hfIdPersona.Value; }
        set { hfIdPersona.Value = value; }
    }

    public string NombreBusqueda
    {
        get { return hfNombreBusqueda.Value; }
        set { hfNombreBusqueda.Value = value; }
    }

    public HiddenField hfPersonaSel
    {
        get { return hfIdPersona; }
        set { hfIdPersona = value; }
    }
    public HiddenField hfNombreBusquedaSel
    {
        get { return hfNombreBusqueda; }
        set { hfNombreBusqueda = value; }
    }

    #endregion

    #region EVENTOS

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Cargar();
            //Listar();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void imgBuscar_Click(object sender, ImageClickEventArgs e)
    {
        Listar();
    }

    protected void butAceptar_Click(object sender, EventArgs e)
    {

    }
    protected void butCancelar_Click(object sender, EventArgs e)
    {

    }

    protected void lvLista_ItemCommand(object sender, ListViewCommandEventArgs e)
    {

    }

    protected void lvLista_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            HiddenField hfItemIdPersona = (HiddenField)e.Item.FindControl("hfItemIdPersona");
            HiddenField hfItemNombreBusqueda = (HiddenField)e.Item.FindControl("hfItemNombreBusqueda");
            HtmlTableRow htr = (HtmlTableRow)e.Item.FindControl("TrFila");
            htr.Attributes.Add("onclick", string.Format("fnSelecionarFila{0}(this,'{1}','{2}');", this.ClientID, hfItemIdPersona.Value, hfItemNombreBusqueda.Value));
        }
    }

    protected void ucpagLista_CambioPagina(object sender, EventArgs e)
    {
        Listar();
    }

    #endregion

    #region METODOS

    public void Inicializar(string pstrEsEmpleado, string pstrEsCliente, string pstrEsProveedor, string pstrEsOtros)
    {
        this.EsEmpleado = pstrEsEmpleado;
        this.EsCliente = pstrEsCliente;
        this.EsProveedor = pstrEsProveedor;
        this.EsOtros = pstrEsOtros;
    }

    protected void Cargar()
    {
        //Cargar Estado
        Utilitarios Util = new Utilitarios();
        Util.loadCombo(ddlEstado, ListarParametroDetalle(1, 1, Constantes.PARAMETRO_ESTADOREGISTRO), "Descripcion", "ParametroDetalle");
    }

    /// <summary>
    /// Método Lista
    /// </summary>
    /// <returns></returns> 
    public void Listar()
    {
        int intTotalFilas = 0, intFilasXPagina = 0;
        string strNombre, srtFlagEmpleado = string.Empty, strFlagCliente = string.Empty, strFlagProveedor = string.Empty, strFlagOtro = string.Empty;
        string strEstado = string.Empty;

        srtFlagEmpleado = this.EsEmpleado;
        strFlagCliente = this.EsCliente;
        strFlagProveedor = this.EsProveedor;
        strFlagOtro = this.EsOtros;

        strNombre = txtNombre.Text.Trim();
        strEstado = ddlEstado.SelectedValue;

        DataTable dt = ListarPersona(strNombre, srtFlagEmpleado, strFlagCliente, strFlagProveedor, strFlagOtro, strEstado, ucpagLista.NumeroPagina);
        //DataTable dt = ListarPersona(strNombre, "", "S", "", "", strEstado, ucpagLista.NumeroPagina);

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
    }



    #endregion

    #region WEBSERVICES

    /// <summary>
    /// Método Lista estructura
    /// </summary>
    /// <returns>Devuelve un DataSet</returns>
    protected DataTable ListarPersona(string pstrNombre, string pstrFlagEmpleado, string pstrFlagCliente, string pstrFlagProveedor, string pstrFlagOtro, string pstrEstado, int pinPagina)
    {
        WCPersona.BEPersona be = new WCPersona.BEPersona();
        be.NombreBusqueda = pstrNombre;
        be.EsEmpleado = pstrFlagEmpleado;
        be.EsCliente = pstrFlagCliente;
        be.EsProveedor = pstrFlagProveedor;
        be.EsOtro = pstrFlagOtro;
        be.Estado = pstrEstado;
        be.Pagina = pinPagina;

        DataTable dt = new DataTable();
        using (var client = new ServiceClient<WCPersona.IWCPersona>("BasicHttpBinding_IWCPersona"))
        {
            var response = client.ServiceOperation<DataTable>(
               c => c.ListarPorTipo(be).Tables[0]
               );
            ;
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
    #endregion WEBSERVICES
}