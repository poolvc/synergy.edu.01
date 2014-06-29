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
public delegate void MasterPageMenuListarClickHandler(object sender, System.EventArgs e);

#endregion Public Delegates

public partial class masMantListar : System.Web.UI.MasterPage
{

    #region Public Properties

    
    public ImageButton MenuBuscar
    {
        set { this.ibtBuscar = value; }
        get { return this.ibtBuscar; }
    }

    public ImageButton MenuNuevo
    {
        set { this.ibtNuevo = value; }
        get { return this.ibtNuevo; }
    }


    public ImageButton MenuProcesar
    {
        set { this.ibtProcesar = value; }
        get { return this.ibtProcesar; }
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

    public Label Titulo
    {
        set { this.lblTitulo = value; }
        get { return this.lblTitulo; }
    }

    public string CodigoPagina
    {
        set { this.hfCodigoPagina.Value = value; }
        get { return this.hfCodigoPagina.Value; }
    }

    #endregion Public Properties

    #region Public Events

    public event MasterPageMenuListarClickHandler MenuAccionEvento;

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
        lblTitulo.Text = Master.MenuDato.Value;
        //CargarMenu();
        //CargarTexto();
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

    

    #endregion


}
