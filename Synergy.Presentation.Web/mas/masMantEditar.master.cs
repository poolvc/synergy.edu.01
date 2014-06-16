using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Synergy.Presentation.Web;

#region Public Delegates

// Expose delegates for Master Page Events
public delegate void MasterPageMenuEditarClickHandler(object sender, EventArgs e);

#endregion Public Delegates

public partial class masMantEditar : System.Web.UI.MasterPage
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


    public ImageButton MenuAceptar
    {
        set { this.ibtAceptar = value; }
        get { return this.ibtAceptar; }
    }

    public ImageButton MenuAplicar
    {
        set { this.ibtAplicar = value; }
        get { return this.ibtAplicar; }
    }

    public ImageButton MenuSalir
    {
        set { this.ibtSalir = value; }
        get { return this.ibtSalir; }
    }

    public ImageButton MenuEjecutar
    {
        set { this.ibtEjecutar = value; }
        get { return this.ibtEjecutar; }
    }

    public HtmlGenericControl BodyTagMantEdit
    {
        set { this.Master.BodyTagMant = value; }
        get { return this.Master.BodyTagMant; }
    }

    #endregion Public Properties

    #region Public Events

    public event MasterPageMenuEditarClickHandler MenuAccionEvento;

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
            MenuAccionEvento(this, e);
            //if (e.CommandName != "Cancelar")
            //{
            //    if (this.Page.IsValid)
            //    {
            //        MenuAccionEvento(this, e);
            //    }
            //}
            //else
            //{
            //    //Invokes the delegates.
            //    MenuAccionEvento(this, e);
            //}

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
