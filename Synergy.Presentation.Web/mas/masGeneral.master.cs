using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


public partial class masGeneral : System.Web.UI.MasterPage
{

    #region Public Properties

    public HtmlGenericControl BodyTag
    {
        set { this.bodyGeneral = value; }
        get { return this.bodyGeneral; }
    }

    #endregion Public Properties

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


}
