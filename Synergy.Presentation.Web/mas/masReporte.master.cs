using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Public Delegates

// Expose delegates for Master Page Events
public delegate void MasterPageMenuReporteClickHandler(object sender, EventArgs e);

#endregion Public Delegates

public partial class masReporte : System.Web.UI.MasterPage
{

    #region Public Properties

    public Label Titulo
    {
        set { this.lblTitulo = value; }
        get { return this.lblTitulo; }
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

    #endregion Public Properties

    #region Public Events

    public event MasterPageMenuReporteClickHandler MenuAccionEvento;

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
}
