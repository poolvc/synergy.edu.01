using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Synergy.Presentation.Web;
using Synergy.Infraestructure.CrossCutting;

public partial class _default : ObjectPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(AppSettings.AplicacionKeyDesarrollo))
        {
            Response.Redirect(string.Format("{0}?u={1}&i={2}", "~/seg/segInterchange.aspx", AppSettings.AplicacionKeyDesarrollo, AppSettings.LenguajeDesarrollo));
        }
    }
}