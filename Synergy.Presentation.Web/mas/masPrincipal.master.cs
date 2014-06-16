using System;
using System.Web.UI.WebControls;


public partial class masPrincipal : System.Web.UI.MasterPage
{

    //public Menu mnuAccion
    //{
    //    get { return _mnuAccion; }
    //}



    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!this.IsPostBack)
        {
            //string strUsuario = Session["Usuario"].ToString();
            //if (!string.IsNullOrEmpty(strUsuario))
            //{
            //    BEUsuario be = new BEUsuario();
            //    BLUsuario bl = new BLUsuario();
            //    be = bl.Obtener(strUsuario);
            //    if (!string.IsNullOrEmpty(be.Busqueda))
            //    {    
            //        LabelUsr.Text = be.Busqueda;
            //    }
            //    else
            //    {    
            //        LabelUsr.Text = string.Empty;
            //    }
            //}
            //else
            //{
            //    Response.Redirect("EndSession.aspx");
            //}
        }
        //if (string.IsNullOrEmpty(strUsuario))
        //{
        //   Response.Redirect("EndSession.aspx");
        //}
    }


    //protected void mnuPrincipal_MenuItemClick(object sender, MenuEventArgs e)
    //{

    //}
    ////protected void mnuAccion_MenuItemClick(object sender, MenuEventArgs e)
    ////{

    ////}
    //protected void mnuGeneral_MenuItemClick(object sender, MenuEventArgs e)
    //{

    //}

    //public void Prueba() 
    //{

    //}

}
