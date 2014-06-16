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
public partial class ucCentroCostoSelector : System.Web.UI.UserControl
{
    #region CONSTANTES


    #endregion

    #region PROPERTIES




    public string IdCentroCosto
    {
        get { return hfIdCentroCosto.Value; }
        set { hfIdCentroCosto.Value = value; }
    }

    public string CodigoCentroCosto
    {
        get { return hfCodigoCentroCosto.Value; }
        set { hfCodigoCentroCosto.Value = value; }
    }

    public string Descripcion
    {
        get { return hfDescripcion.Value; }
        set { hfDescripcion.Value = value; }
    }

    public HiddenField hfIdCentroCostoSel
    {
        get { return hfIdCentroCosto; }
        set { hfIdCentroCosto = value; }
    }

    public HiddenField hfCodigoCentroCostoSel
    {
        get { return hfCodigoCentroCosto; }
        set { hfCodigoCentroCosto = value; }
    }

    public HiddenField hfDescripcionSel
    {
        get { return hfDescripcion; }
        set { hfDescripcion = value; }
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

    protected void imgBuscarCentroCosto_Click(object sender, ImageClickEventArgs e)
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
            HiddenField hfItemIdCentroCosto = (HiddenField)e.Item.FindControl("hfItemIdCentroCosto");
            HiddenField hfItemCodigoCentroCosto = (HiddenField)e.Item.FindControl("hfItemCodigoCentroCosto");
            HiddenField hfItemDescripcion = (HiddenField)e.Item.FindControl("hfItemDescripcion");
            HtmlTableRow htr = (HtmlTableRow)e.Item.FindControl("TrFila");
            htr.Attributes.Add("onclick", "fnSelecionarFila(this, '" + hfItemIdCentroCosto.Value + "','" + hfItemCodigoCentroCosto.Value + "','" + hfItemDescripcion.Value + "')");
        }
    }

    protected void ucpagLista_CambioPagina(object sender, EventArgs e)
    {
        //Listar();
    }

    #endregion

    #region METODOS



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
        string strCodigoCentroCosto = string.Empty;
        string strDescripcion = string.Empty;
        string strEstado = string.Empty;

        strCodigoCentroCosto = txtCodigoCentroCosto.Text.Trim();
        strDescripcion = txtDescripcion.Text.Trim();
        strEstado = ddlEstado.SelectedValue;

        DataTable dt = ListarCentroCosto(strCodigoCentroCosto, strDescripcion, strEstado, ucpagLista.NumeroPagina);
        //DataTable dt = ListarCentroCosto(strDescripcion, "", "S", "", "", strEstado, ucpagLista.NumeroPagina);

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
    protected DataTable ListarCentroCosto(string pstrCodigoCentroCosto, string pstrDescripcion, string pstrEstado, int pinPagina)
    {
        WCCentroCosto.BECentroCosto be = new WCCentroCosto.BECentroCosto();
        be.CodigoCentroCosto = pstrCodigoCentroCosto;
        be.Descripcion = pstrDescripcion;
        be.Estado = pstrEstado;
        be.Pagina = pinPagina;

        DataTable dt = new DataTable();
        using (var client = new ServiceClient<WCCentroCosto.IWCCentroCosto>("BasicHttpBinding_IWCCentroCosto"))
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