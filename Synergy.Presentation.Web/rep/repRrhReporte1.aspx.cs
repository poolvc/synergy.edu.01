using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using Synergy.Presentation.Web;
using Microsoft.Reporting.WebForms;


public partial class repRrhReporte1 : ObjectPage
{
    #region CONSTANTES

    private const string RUTA_LISTA = "~/rep/repPrincipal.aspx";
    protected const string REPORTE_RUTA = "/Galexito_RRH/Report1";

    #endregion

    #region PROPIEDADES

    #endregion PROPIEDADES

    #region EVENTOS

    protected void Page_Load(object sender, EventArgs e)
    {
        //Eventos del Master
        Master.MenuAccionEvento += new MasterPageMenuReporteVistaClickHandler(Master_MenuButton);
        if (!IsPostBack)
        {            
            Cargar();
        }
    }

    protected void Master_MenuButton(object sender, EventArgs e)
    {
        string strAccion = ((CommandEventArgs)e).CommandName;
        switch (strAccion)
        {
            case "Buscar":
                Buscar();
                break;
            case "Salir":
                Salir();
                break;
        }
    }
  
    #endregion EVENTOS

    #region METODOS GENERAL

    protected void Cargar()
    {
        //Eventos Java Script
        this.Master.MenuBuscar.Attributes.Add("OnClick", "javascript:return fnValidar();");
        this.Master.MenuImprimir.Visible = false;
        this.Master.MenuExportar.Visible = false;
        //this.txtCentroCosto.Attributes.Add("onkeyup", "javascript:return fnIngresoNumeroDecimales(this,0,false);");
        //this.txtPersona.Attributes.Add("onkeyup", "javascript:return fnIngresoNumeroDecimales(this,0,false);");

        string strAccion =  Request.QueryString["a"];   //l-Carga manual
        string strId = Request.QueryString["i"];  //Id

        //carga los modelos activos
        //Util.loadComboAndItem(ddlModeloNegocio, ListarModeloNegocio("", "A", 0), "Descripcion", "IdModelo", "-- Seleccione --", "0");

        switch (strAccion)
        {
            case "r":
                //Master.SubTitulo.Text = "Reporte Financiero";
                break;
        }
        rvReporte.ShowCredentialPrompts = false;
    }
    
    protected void Buscar()
    {

        if (!Validar()) //Valida e Inicializa
        {
            return;
        }

        string strPersona = string.IsNullOrEmpty(txtPersona.Text.Trim()) ? "0" : txtPersona.Text.Trim(); 
        ServerReport srepServidor = rvReporte.ServerReport;

        ReportParameter[] parm = new ReportParameter[2];
        parm[0] = new ReportParameter("as_centrocosto", txtCentroCosto.Text);
        parm[1] = new ReportParameter("an_persona", strPersona);
        rvReporte.ShowCredentialPrompts = false;

        RSCadenaConexion rs = new RSCadenaConexion();
        rvReporte.ServerReport.ReportServerCredentials = new ReportCredentials(rs.UserId, rs.Password, "");
        rvReporte.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        rvReporte.ServerReport.ReportServerUrl = new System.Uri(rs.URLServer);
        rvReporte.ServerReport.Timeout = rs.Timeout;
        rvReporte.ServerReport.ReportPath = rs.Path + REPORTE_RUTA;
        rvReporte.ShowParameterPrompts = false;
        rvReporte.ServerReport.SetParameters(parm);
        rvReporte.ServerReport.Refresh();

    }

    protected bool Validar()
    {

        //string strCodigo = string.Empty, strDescripcion = string.Empty;
        //if (string.IsNullOrEmpty(hfIdModelo.Value) || hfIdModelo.Value == "0")
        //{
        //    Util.ejecutaScriptAJAX(this.Page, string.Format("alert('{0}');", "Seleccione un modelo de negocio"));
        //    return false;
        //}
       
        return true;
    }

    protected void Salir()
    {
        Util.Redireciona(RUTA_LISTA, "?a=l", true);
    }

    #endregion METODO GENERAL


    #region METODO DE LOS CARGA MANUAL



    #endregion METODO DE LOS BudgetS

    #region LLAMDAS A WEBSERVICES

    /// <summary>
    /// Método Obtiene Nivel de Alerta
    /// </summary>
    /// <returns>Devuelve un Objeto</returns>
    //protected WCModeloNegocio.BEModeloNegocio ObtenerModeloNegocio(WCModeloNegocio.BEModeloNegocio pbe)
    //{
    //    WCModeloNegocio.BEModeloNegocio be = new WCModeloNegocio.BEModeloNegocio();
    //    DataTable dt = new DataTable();
    //    using (var client = new ServiceClient<WCModeloNegocio.IWCModeloNegocio>("BasicHttpBinding_IWCModeloNegocio"))
    //    {
    //        var response = client.ServiceOperation<WCModeloNegocio.BEModeloNegocio>(
    //           c => c.Obtener(pbe)
    //           );
    //        ;
    //        be = response;
    //    }
    //    return be;
    //}


    

    #endregion WEBSERVICES




}