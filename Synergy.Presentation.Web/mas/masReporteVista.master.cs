using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Synergy.Presentation.Web;

#region Public Delegates

// Expose delegates for Master Page Events
public delegate void MasterPageMenuReporteVistaClickHandler(object sender, EventArgs e);

#endregion Public Delegates

public partial class masReporteVista : System.Web.UI.MasterPage
{

    #region Public Properties

    public Label Titulo
    {
        set { this.lblTitulo = value; }
        get { return this.lblTitulo; }
    }

    public Label SubTitulo
    {
        set { this.lblSubTitulo = value; }
        get { return this.lblSubTitulo; }
    }


    public ImageButton MenuBuscar
    {
        set { this.ibtBuscar = value; }
        get { return this.ibtBuscar; }
    }

    public ImageButton MenuExportar
    {
        set { this.ibtExportar = value; }
        get { return this.ibtExportar; }
    }

    public ImageButton MenuImprimir
    {
        set { this.ibtImprimir = value; }
        get { return this.ibtImprimir; }
    }

    public ImageButton MenuSalir
    {
        set { this.ibtSalir = value; }
        get { return this.ibtSalir; }
    }

    #endregion Public Properties

    #region Public Events

    public event MasterPageMenuReporteVistaClickHandler MenuAccionEvento;

    #endregion Public Events

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //CargarInit();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Cargar();
        }
    }

    protected void Cargar()
    {
        lblTitulo.Text = Master.MenuDato.Value + "  " + lblTitulo.Text ;
        string strAccion = Request.QueryString["a"];   //l-Carga manual
        string strIdApp = Request.QueryString["iA"];  //Id
        string strIdGrup = Request.QueryString["iG"];  //Id
        string strIdRep = Request.QueryString["iR"];  //Id

        hfAccion.Value = strAccion;
        hfIdAplicacion.Value = strIdApp;
        hfIdGrupo.Value = strIdGrup;
        hfIdReporte.Value = strIdRep;

        WCReporte.BEReporte be = ObtenerReporte(int.Parse(hfIdAplicacion.Value), int.Parse(hfIdGrupo.Value), int.Parse(hfIdReporte.Value));
        lblSubTitulo.Text = be.Descripcion;
    }


    /// <summary>
    /// Invokes subscribed delegates to menu click event.
    /// </summary>
    /// <param name="e">Click Event arguments</param>
    protected virtual void OnMenuButtonClick(CommandEventArgs e)
    {
        if (MenuAccionEvento != null)
        {
            //Invokes the delegates.
            MenuAccionEvento(this, e);
        }
    }

    /// <summary>
    /// Evento que lanza el Click((Command)
    /// </summary>
    /// <param name="e">Click Event arguments</param>
    protected void ibtMenu_Command(object sender, CommandEventArgs e)
    {
        OnMenuButtonClick(e);
    }

    

    #region LLAMDAS A WEBSERVICES

    /// <summary>
    /// Método Valida el Usuario
    /// </summary>
    /// <returns>Devuelve un DataSet</returns>
    protected WCReporte.BEReporte ObtenerReporte(int pinIdAplicacion, int pinIdGrupoReporte, int pintReporte)
    {
        WCReporte.BEReporte be = new WCReporte.BEReporte();
        be.IdAplicacion = pinIdAplicacion;
        be.IdGrupoReporte = pinIdGrupoReporte;
        be.IdReporte = pintReporte;

        using (var client = new ServiceClient<WCReporte.IWCReporte>("BasicHttpBinding_IWCReporte"))
        {
            var response = client.ServiceOperation<WCReporte.BEReporte>(
               c => c.Obtener(be)
               );
            ;
            be = response;
        }
        return be;
    }

    #endregion LLAMDAS A WEBSERVICES
}
