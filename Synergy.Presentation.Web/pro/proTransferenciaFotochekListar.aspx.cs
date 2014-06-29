using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Synergy.Presentation.Web;
using Microsoft.Reporting.WebForms;
using System.Collections.Specialized;
using Synergy.Infraestructure.CrossCutting;
using Synergy.Application.Edu;
using Synergy.Domain.Edu.Entities;
using System.Web.Services;

public partial class proTransferenciaFotochekListar : ObjectPage
{

    #region CONSTANTES

    private const string RUTA_LISTA = "~/pro/proPrincipal.aspx";

    #endregion 

    #region PROPIEDADES

    private StringBuilder _sbMsgScript = new StringBuilder();
    public StringBuilder MsgScript
    {
        get { return _sbMsgScript; }
        set { _sbMsgScript = value; }
    }


    #endregion PROPIEDADS

    #region EVENTOS

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "CargarTabs", "javascript:CargarTabs();", true);

        //Eventos del Master
        Master.MenuAccionEvento += new MasterPageMenuReporteVistaClickHandler(Master_MenuButton);
        if (!IsPostBack)
        {            
            Cargar();
            Listar();
        }
    }

    protected void Master_MenuButton(object sender, EventArgs e)
    {
        string strAccion = ((CommandEventArgs)e).CommandName;
        switch (strAccion)
        {
            case "Buscar":
                Session["mLista"] = null;
                Listar();
                break;
            case "Nuevo":
                
                break;
            case "Exportar":
                Guardar();
                break;
            case "Imprimir":
               // Imprimir();
                break;
            case "Salir":
                Salir();
                break;
        }
    }

    protected void ucpagLista_CambioPagina(object sender, EventArgs e)
    {

        llenarCodigoAlumno();
        ListarAlumno();
        VerificaSiExisteAlumno();
       
    }

	protected void ucpagListaFamilia_CambioPagina(object sender, EventArgs e)
    {
		llenarCodigoFamilia();
		ListarFamilia();
		VerificaSiExisteFamilia();
    }

    protected void ucpagListaEmpleado_CambioPagina(object sender, EventArgs e)
    {
        llenarCodigoEmpleado();
        ListarEmpleado();
        VerificaSiExisteEmpleado();
    }

    protected void lvLista_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        string strId = e.CommandArgument.ToString();
        switch (e.CommandName)
        {
            case "Editar":
                //Editar(strId, e.Item);
                break;
            case "Copiar":
               // Copiar(strId, e.Item);
                break;
        }
    }

    protected void lvLista_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        CheckBox chkExportado = (CheckBox)e.Item.FindControl("chkExportado");
		Label lblAlumnoCodigo = (Label)e.Item.FindControl("lblAlumnoCodigo");
        Image imgVer = (Image)e.Item.FindControl("imgVer");

		lblAlumnoCodigo.Attributes.Add("onclick", "fnCargarVentana();");
		lblAlumnoCodigo.Style["cursor"] = "pointer";

        if (chkSelTodo.Checked)
            chkExportado.Checked = true;

        imgVer.Attributes.Add("onclick", "javascript:return AbrirVisorFicha('visorConteinerFicha', '" + ddlPeriodoAcademico.SelectedValue + "', '" + lblAlumnoCodigo.Text + "', '', '', 'A');");
    }

	protected void lvListaFamilia_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        CheckBox chkExportado = (CheckBox)e.Item.FindControl("chkExportado");
        Label lblCodigo = (Label)e.Item.FindControl("lblCodigo");
        Label lblVinculo = (Label)e.Item.FindControl("lblVinculo");
        Label lblDocumentoIdentidad = (Label)e.Item.FindControl("lblDocumentoIdentidad");
        Image imgVer = (Image)e.Item.FindControl("imgVer");

        if (chkSelTodo.Checked)
            chkExportado.Checked = true;

        imgVer.Attributes.Add("onclick", "javascript:return AbrirVisorFicha('visorConteinerFicha', '" + ddlPeriodoAcademico.SelectedValue + "', '" + lblCodigo.Text + "', '" + lblVinculo.Text + "','" + lblDocumentoIdentidad.Text.Trim() + "','F');");

    }
	
	protected void lvListaEmpleado_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        CheckBox chkExportado = (CheckBox)e.Item.FindControl("chkExportado");
        Image imgVer = (Image)e.Item.FindControl("imgVer");
        Label lblCodigo = (Label)e.Item.FindControl("lblCodigo");

        imgVer.Attributes.Add("onclick", "javascript: return AbrirVisorFicha('visorConteinerFicha', '" + ddlPeriodoAcademico.SelectedValue + "', '" + lblCodigo.Text + "', '', '', 'E');");

        if (chkSelTodo.Checked)
            chkExportado.Checked = true;

    }
	 
     protected void btnAlumno_Click(object sender, ImageClickEventArgs e)
     {
         MsgScript.Clear();
         Session["mLista"] = null;
         chkSelTodo.Checked = false;
         ucpagLista.NumeroPagina = 1;
         ListarAlumno();
         MsgScript.Append("fnQuitarChekSelTodos();");
         MsgScript.Append("CerrarVentanaEspera();");
         Util.ejecutaScriptAJAX(this.Page, MsgScript.ToString());
     }

     protected void btnGuardarAlumno_Click(object sender, ImageClickEventArgs e)
     {
         MsgScript.Clear();
         this.GuardarAlumno();
         Session["mLista"] = null;
         chkSelTodo.Checked = false;
         //ucpagLista.NumeroPagina = 1;
         ListarAlumno();
         MsgScript.Append("fnQuitarChekSelTodos();");
         MsgScript.Append("CerrarVentanaEspera();");
         Util.ejecutaScriptAJAX(this.Page, MsgScript.ToString());
     }

     protected void btnFamilia_Click(object sender, ImageClickEventArgs e)
     {
         MsgScript.Clear();
         Session["mLista"] = null;
         chkSelTodo.Checked = false;
         ucpagListaFamilia.NumeroPagina = 1;
         this.ListarFamilia();
         MsgScript.Append("fnQuitarChekSelTodos();");
         MsgScript.Append("CerrarVentanaEspera();");
         Util.ejecutaScriptAJAX(this.Page, MsgScript.ToString());
     }

     protected void btnGuardarFamilia_Click(object sender, ImageClickEventArgs e)   
     {
         MsgScript.Clear();
         this.GuardarFamilia();
         Session["mLista"] = null;
         chkSelTodo.Checked = false;
         //ucpagListaFamilia.NumeroPagina = 1;
         ListarFamilia();
         MsgScript.Append("fnQuitarChekSelTodos();");
         MsgScript.Append("CerrarVentanaEspera();");
         Util.ejecutaScriptAJAX(this.Page, MsgScript.ToString());
         
     }

     protected void btnEmpleado_Click(object sender, ImageClickEventArgs e)
     {
         MsgScript.Clear();
         Session["mLista"] = null;
         chkSelTodo.Checked = false;
         ucpagListaEmpleado.NumeroPagina = 1;
         this.ListarEmpleado();
         MsgScript.Append("fnQuitarChekSelTodos();");
         MsgScript.Append("CerrarVentanaEspera();");
         Util.ejecutaScriptAJAX(this.Page, MsgScript.ToString());
     }

     protected void btnGuardarEmpleado_Click(object sender, ImageClickEventArgs e)
     {
         MsgScript.Clear();
         this.GuardarEmpleado();
         Session["mLista"] = null;
         chkSelTodo.Checked = false;
         //ucpagListaEmpleado.NumeroPagina = 1;
         ListarEmpleado();
         MsgScript.Append("fnQuitarChekSelTodos();");
         MsgScript.Append("CerrarVentanaEspera();");
         Util.ejecutaScriptAJAX(this.Page, MsgScript.ToString());
     }
	
    #endregion 

    #region METODOS


    protected void Salir()
    {
        string FiltroParam = Request.QueryString["f"];
        Util.Redireciona(RUTA_LISTA, "?a=l&f=" + FiltroParam, true);
    }

    protected void Cargar()
    {
        ucpagLista.NumeroPagina = 1;
        ddlPeriodoAcademico.SelectedIndex = 0;
        Master.MenuExportar.ImageUrl = "~/img/ico_ejecutar.png";
        Master.MenuExportar.ToolTip = Resources.resDiccionario.Exportar;
        Master.MenuExportar.Visible = true;
        Master.MenuImprimir.Visible = false;
        Master.MenuBuscar.Attributes.Add("onclick","javascript:return fnListar();");
        Master.MenuExportar.Attributes.Add("onclick", "javascript:return fnGuardar();");
        lnkAlumno.HRef = string.Format("#{0}", divAlumno.ClientID);
        lnkdivfamilias.HRef = string.Format("#{0}", divfamilias.ClientID);
        lnkdivEmpleados.HRef = string.Format("#{0}", divEmpleados.ClientID);

        Util.loadComboAndItem(ddlPeriodoAcademico, ListarAcademico().ToArray(), "DescripcionLocal", "PeriodoAcademico", string.Format("--{0}--", Resources.resDiccionario.Seleccione), "0");
        if(ddlPeriodoAcademico.Items.Count>1)
            ddlPeriodoAcademico.SelectedIndex = 1;
        Util.loadComboAndItem(ddlGrado, ListarGradoAcademico(), "Descripcion", "GradoCodigo", string.Format("--{0}--", Resources.resDiccionario.Todos), "");
       

    }

	protected void Guardar()
    {
		GuardarAlumno();
	}
    protected void GuardarAlumno()
    {
        string strUsuarioCreacion = UsuarioSistema.CodigoUsuario;
        try
        {
            BLAlumnos bl = new BLAlumnos();
            BEAlumnos obe = new BEAlumnos();

            if (Session["mLista"] != null)
            {
                List<string> mLista = (List<string>)Session["mLista"];
                foreach (string strval in devuelveSeleccionados())
                {
                    mLista.Add(strval);
                }
                if (chkSelTodo.Checked)
                {
                    Session["mLista"] = mLista;
                    obe = ObtenerFiltros();
                    obe.Pagina = 0;
                    obe.Alumno = DevuelveSelTodoCadena(mLista);
                    obe.UsuarioCreacion = strUsuarioCreacion;
                    obe = bl.InsetarMasivo(obe);
                }
                else
                {
                    obe = bl.Insetar(mLista, ddlPeriodoAcademico.SelectedValue, strUsuarioCreacion);
                }

                if (obe.Error == -1)
                {
                    MsgScript.Append(string.Format("alert('{0}');", Resources.resMensaje.msgErrorRegistrar));
                }
            }
            else
            {
                List<string> mLista = devuelveSeleccionados();
                 
                    if (chkSelTodo.Checked)
                    {
                        obe = ObtenerFiltros();
                        obe.Pagina = 0;
                        obe.Alumno = DevuelveSelTodoCadena(mLista);

                        obe = bl.InsetarMasivo(obe);
                    }
                    else
                    {
                        obe = bl.Insetar(mLista, ddlPeriodoAcademico.SelectedValue, strUsuarioCreacion);
                    }
                 
            }
        }
        catch (Exception ex)
        {
            MsgScript.Append(string.Format("alert('{0} - Error {1}');", Resources.resMensaje.msgErrorRegistrar , "E021"));
            Logger.Error(string.Format("'{0} - Error {1}'", Resources.resMensaje.msgErrorRegistrar, "E021"), ex);
        }
    }

	protected void GuardarFamilia()
    {
        string strUsuarioCreacion = UsuarioSistema.CodigoUsuario;
        try
        {
            BLFamilia bl = new BLFamilia();
            BEFamilia obe = new BEFamilia();

            if (Session["mLista"] != null)
            {
                List<BEFamilia> mLista = (List<BEFamilia>)Session["mLista"];

                foreach (BEFamilia be in devuelveSeleccionadosFamilia())
                {
                    mLista.Add(be);
                }

                if (chkSelTodo.Checked)
                {
                    Session["mLista"] = mLista;
                    obe = ObtenerFiltrosFamilia();
                    obe.Pagina = 0;
                    obe.CodigoFamilia = DevuelveSelTodoCadenaFamilia(mLista,1);
                    obe.Vinculo = DevuelveSelTodoCadenaFamilia(mLista, 2);
                    obe.DocumentoIdentidad = DevuelveSelTodoCadenaDocumentoIdentidad(mLista, 3);
                    obe.UsuarioCreacion = strUsuarioCreacion;
                    obe = bl.InsetarMasivo(obe);
                }
                else
                {
                    obe = bl.Insetar(mLista, ddlPeriodoAcademico.SelectedValue, strUsuarioCreacion);
                }

                if (obe.Error == -1)
                {
                    MsgScript.Append(string.Format("alert('{0}');", Resources.resMensaje.msgErrorRegistrar));
                }
            }
            else
            {
                List<BEFamilia> mLista = devuelveSeleccionadosFamilia();
                 
                    if (chkSelTodo.Checked)
                    {
                        obe = ObtenerFiltrosFamilia();
                        obe.Pagina = 0;
                        obe.CodigoFamilia = DevuelveSelTodoCadenaFamilia(mLista, 1);
                        obe.Vinculo = DevuelveSelTodoCadenaFamilia(mLista, 2);
                        obe.DocumentoIdentidad = DevuelveSelTodoCadenaDocumentoIdentidad(mLista, 3);
                        obe.UsuarioCreacion = strUsuarioCreacion;
                        obe = bl.InsetarMasivo(obe);
                    }
                    else
                    {
                        obe = bl.Insetar(mLista, ddlPeriodoAcademico.SelectedValue, strUsuarioCreacion);
                    }
                
            }
        }
        catch (Exception ex)
        {
            MsgScript.Append(string.Format("alert('{0} - Error {1}');", Resources.resMensaje.msgErrorRegistrar, "E031"));
            Logger.Error(ex);
        }
    }
	
	protected void GuardarEmpleado()
    {
        string strUsuarioCreacion = UsuarioSistema.CodigoUsuario;
        try
        {
            BLEmpleado bl = new BLEmpleado();
            BEEmpleado obe = new BEEmpleado();

            if (Session["mLista"] != null)
            {
                List<string> mLista = (List<string>)Session["mLista"];
                foreach (string strval in devuelveSeleccionadosEmpleado())
                {
                    mLista.Add(strval);
                }
                if (chkSelTodo.Checked)
                {
                    Session["mLista"] = mLista;
                    obe = ObtenerFiltrosEmpleado();
                    obe.Pagina = 0;
                    obe.Codigo = DevuelveSelTodoCadena(mLista);
                    obe.UsuarioCreacion = strUsuarioCreacion;
                    obe = bl.InsetarMasivo(obe);
                }
                else
                {
                    obe = bl.Insetar(mLista, ddlPeriodoAcademico.SelectedValue, strUsuarioCreacion);
                }

                if (obe.Error == -1)
                {
                    MsgScript.Append(string.Format("alert('{0}');", Resources.resMensaje.msgErrorRegistrar));
                }
            }
            else
            {
                List<string> mLista = devuelveSeleccionadosEmpleado();
                 
                    if (chkSelTodo.Checked)
                    {
                        obe = ObtenerFiltrosEmpleado();
                        obe.Pagina = 0;
                        obe.Codigo = DevuelveSelTodoCadena(mLista);
                        obe.UsuarioCreacion = strUsuarioCreacion;
                        obe = bl.InsetarMasivo(obe);
                    }
                    else
                    {
                        obe = bl.Insetar(mLista, ddlPeriodoAcademico.SelectedValue, strUsuarioCreacion);
                    }
                
            }
        }
        catch (Exception ex)
        {
            MsgScript.Append(string.Format("alert('{0} - Error {1}');", Resources.resMensaje.msgErrorRegistrar, "E041"));
            Logger.Error(ex);
        }
    }

    private string DevuelveSelTodoCadenaFamilia(List<BEFamilia> mLista,int iTipo)
    {
        string strRetorno = string.Empty;

        for (int i = 0; i < mLista.Count; i++)
        {
            strRetorno = iTipo == 1 ? strRetorno + "|" + mLista[i].CodigoFamilia : strRetorno + "|" + mLista[i].Vinculo;
        }
        strRetorno = strRetorno == "" ? "" : strRetorno.Substring(1);
        return strRetorno;
    }

    private string DevuelveSelTodoCadenaDocumentoIdentidad(List<BEFamilia> mLista, int iTipo)
    {
        string strRetorno = string.Empty;

        for (int i = 0; i < mLista.Count; i++)
        {
            strRetorno = iTipo == 1 ? strRetorno + "|" + mLista[i].DocumentoIdentidad : strRetorno + "|" + mLista[i].DocumentoIdentidad;
        }
        strRetorno = strRetorno == "" ? "" : strRetorno.Substring(1);
        return strRetorno;
    }

    private string DevuelveSelTodoCadena(List<string> mLista)
    {
        string strRetorno = string.Empty;

        for (int i = 0; i < mLista.Count; i++)
        {
            strRetorno = strRetorno + "|" + mLista[i].ToString();
        }
        strRetorno = strRetorno == "" ? "" : strRetorno.Substring(1);
        return strRetorno;
    }

    private BEAlumnos ObtenerFiltros()
    {
        BEAlumnos obe = new BEAlumnos();

        obe.PeriodoAcademico = ddlPeriodoAcademico.SelectedValue;
        obe.Grado = ddlGrado.SelectedValue;
        obe.Familia = txtCodigoFamilia.Text;
        obe.Foto = ddlFoto.SelectedValue;
        obe.Seccion = txtSeccion.Text;
        obe.Alumno = txtCodigoAlumno.Text;
        obe.Exportado = ddlExportado.SelectedValue;

        return obe;
    }
	
	private BEFamilia ObtenerFiltrosFamilia()
    {
        BEFamilia obe = new BEFamilia();
        obe.PeriodoAcademico = ddlPeriodoAcademico.SelectedValue;
        obe.Familia = txtCodigoFamilia.Text;
        obe.Foto = ddlFoto.SelectedValue;
        obe.Exportado = ddlExportado.SelectedValue;

        return obe;
    }
	
	private BEEmpleado ObtenerFiltrosEmpleado()
    {
        BEEmpleado obe = new BEEmpleado();
        obe.PeriodoAcademico = ddlPeriodoAcademico.SelectedValue;
        obe.Foto = ddlFoto.SelectedValue;
        obe.Exportado = ddlExportado.SelectedValue;

        return obe;
    }
	
	protected void Listar()
    {
		if(hfTabSel.Value == "1")
		{
			ucpagLista.NumeroPagina = 1;
			ListarAlumno();
		}
		else if(hfTabSel.Value == "2")
		{
			ucpagListaFamilia.NumeroPagina = 1;
            ListarFamilia();
		}
		else
		{
			ucpagListaEmpleado.NumeroPagina = 1;
			ListarEmpleado();
		}
	
	}

    protected void ListarAlumno()
    {
        int intTotalFilas = 0, intFilasXPagina = 0;

        BEAlumnos obe = new BEAlumnos();
        obe = ObtenerFiltros();
        obe.Pagina = ucpagLista.NumeroPagina;
        DataTable dt = ListarAlumnos(obe);

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
    
	protected void ListarFamilia()
    {
        int intTotalFilas = 0, intFilasXPagina = 0;

        BEFamilia obe = new BEFamilia();
        obe = ObtenerFiltrosFamilia();
        obe.Pagina = ucpagListaFamilia.NumeroPagina;
        DataTable dt = ListarFamilias(obe);

        lvListaFamilia.DataSource = dt;
        lvListaFamilia.DataBind();

        if (dt.Rows.Count > 0)
        {
            intFilasXPagina = Convert.ToInt32(dt.Rows[0]["FilasXPagina"]);
            intTotalFilas = Convert.ToInt32(dt.Rows[0]["TotalFilas"]);
        }
        ucpagListaFamilia.TotalRegistros = intTotalFilas;
        ucpagListaFamilia.RegistrosPorPagina = intFilasXPagina;
        ucpagListaFamilia.EnlazarPaginador();
    }
	
	protected void ListarEmpleado()
    {
        int intTotalFilas = 0, intFilasXPagina = 0;

        BEEmpleado obe = new BEEmpleado();
        obe = ObtenerFiltrosEmpleado();
        obe.Pagina = ucpagListaEmpleado.NumeroPagina;
        DataTable dt = ListarEmpleado(obe);

        lvListaEmpleado.DataSource = dt;
        lvListaEmpleado.DataBind();

        if (dt.Rows.Count > 0)
        {
            intFilasXPagina = Convert.ToInt32(dt.Rows[0]["FilasXPagina"]);
            intTotalFilas = Convert.ToInt32(dt.Rows[0]["TotalFilas"]);
        }
        ucpagListaEmpleado.TotalRegistros = intTotalFilas;
        ucpagListaEmpleado.RegistrosPorPagina = intFilasXPagina;
        ucpagListaEmpleado.EnlazarPaginador();

    }
	
    private List<string> devuelveSeleccionados()
    {
        List<string> mLista = new List<string>();
  
        foreach (ListViewDataItem Item in lvLista.Items)
        {
            CheckBox chkExportado = (CheckBox)Item.FindControl("chkExportado");
            Label lblAlumnoCodigo = (Label)Item.FindControl("lblAlumnoCodigo");

            if (chkSelTodo.Checked)
            {
                if (chkExportado.Checked==false)
                {
                    mLista.Add(lblAlumnoCodigo.Text.Trim());
                }
            }
            else
            {
                if (chkExportado.Checked)
                {
                    mLista.Add(lblAlumnoCodigo.Text.Trim());
                }
            }
            
        }

        return mLista;
        
    }

    private List<BEFamilia> devuelveSeleccionadosFamilia()
    {
        List<BEFamilia> mLista = new List<BEFamilia>();        
        foreach (ListViewDataItem Item in lvListaFamilia.Items)
        {
            CheckBox chkExportado = (CheckBox)Item.FindControl("chkExportado");
            Label lblCodigo = (Label)Item.FindControl("lblCodigo");
            Label lblVinculo = (Label)Item.FindControl("lblVinculo");
            Label lblDocumentoIdentidad = (Label)Item.FindControl("lblDocumentoIdentidad");

            BEFamilia obe = new BEFamilia();

            obe.CodigoFamilia = lblCodigo.Text.Trim();
            obe.Vinculo = lblVinculo.Text.Trim();
            obe.DocumentoIdentidad = lblDocumentoIdentidad.Text.Trim();
            if (chkSelTodo.Checked)
            {
                if (chkExportado.Checked==false)
                {
                    mLista.Add(obe);
                }
            }
            else
            {
                if (chkExportado.Checked)
                {
                    mLista.Add(obe);
                }
            }
            
        }

        return mLista;
        
    }

    private List<string> devuelveSeleccionadosEmpleado()
    {
        List<string> mLista = new List<string>();
  
        foreach (ListViewDataItem Item in lvListaEmpleado.Items)
        {
            CheckBox chkExportado = (CheckBox)Item.FindControl("chkExportado");
            Label lblCodigo = (Label)Item.FindControl("lblCodigo");

            if (chkSelTodo.Checked)
            {
                if (chkExportado.Checked==false)
                {
                    mLista.Add(lblCodigo.Text.Trim());
                }
            }
            else
            {
                if (chkExportado.Checked)
                {
                    mLista.Add(lblCodigo.Text.Trim());
                }
            }
            
        }

        return mLista;
        
    }
	
     private void llenarCodigoAlumno()
     {
         List<string> mLista = new List<string>();
         
         if (Session["mLista"] != null)
             mLista = (List<string>)Session["mLista"];


         foreach (ListViewDataItem Item in lvLista.Items)
         {
             CheckBox chkExportado = (CheckBox)Item.FindControl("chkExportado");
             Label lblAlumnoCodigo = (Label)Item.FindControl("lblAlumnoCodigo");

             if (Session["mLista"] != null)
             {
                 
                 for (int i = 0; i < mLista.Count; i++)
                     if (mLista[i].Contains(lblAlumnoCodigo.Text.Trim()))
                     {
                         mLista.RemoveAt(i);
                         i--;
                     }
             }

             if (chkSelTodo.Checked == false)
             {
                 if (chkExportado.Checked)
                     mLista.Add(lblAlumnoCodigo.Text.Trim());
             }
             else
             {
                 if (chkExportado.Checked== false)
                     mLista.Add(lblAlumnoCodigo.Text.Trim());
             }

         }

         Session["mLista"] = mLista;
     }
	 
	 private void llenarCodigoFamilia()
     {
         List<BEFamilia> mLista = new List<BEFamilia>();
         

         if (Session["mLista"] != null)
             mLista = (List<BEFamilia>)Session["mLista"];

         foreach (ListViewDataItem Item in lvListaFamilia.Items)
         {
             CheckBox chkExportado = (CheckBox)Item.FindControl("chkExportado");
             Label lblCodigo = (Label)Item.FindControl("lblCodigo");
             Label lblVinculo = (Label)Item.FindControl("lblVinculo");
             Label lblDocumentoIdentidad = (Label)Item.FindControl("lblDocumentoIdentidad");
             BEFamilia obe = new BEFamilia();

             obe.CodigoFamilia = lblCodigo.Text.Trim();
             obe.Vinculo = lblVinculo.Text.Trim();
             obe.DocumentoIdentidad = lblDocumentoIdentidad.Text.Trim();

             if (Session["mLista"] != null)
             {
                 
                 for (int i = 0; i < mLista.Count; i++)
                     if (mLista[i].CodigoFamilia ==lblCodigo.Text.Trim())
                     {
                         mLista.RemoveAt(i);
                         i--;
                     }
             }

             if (chkSelTodo.Checked == false)
             {
                 if (chkExportado.Checked)
                 {   
                     mLista.Add(obe);
                 }
                     
             }
             else
             {
                 if (chkExportado.Checked == false)
                 {
                     mLista.Add(obe);
                 }
             }

         }

         Session["mLista"] = mLista;
     }
	 
	 private void llenarCodigoEmpleado()
     {
         List<string> mLista = new List<string>();
         
         if (Session["mLista"] != null)
             mLista = (List<string>)Session["mLista"];


         foreach (ListViewDataItem Item in lvListaEmpleado.Items)
         {
             CheckBox chkExportado = (CheckBox)Item.FindControl("chkExportado");
             Label lblCodigo = (Label)Item.FindControl("lblCodigo");

             if (Session["mLista"] != null)
             {
                 
                 for (int i = 0; i < mLista.Count; i++)
                     if (mLista[i].Contains(lblCodigo.Text.Trim()))
                     {
                         mLista.RemoveAt(i);
                         i--;
                     }
             }

             if (chkSelTodo.Checked == false)
             {
                 if (chkExportado.Checked)
                     mLista.Add(lblCodigo.Text.Trim());
             }
             else
             {
                 if (chkExportado.Checked== false)
                     mLista.Add(lblCodigo.Text.Trim());
             }

         }

         Session["mLista"] = mLista;
     }
	 
     private void VerificaSiExisteAlumno()
     {
         foreach (ListViewDataItem Item in lvLista.Items)
         {
             CheckBox chkExportado = (CheckBox)Item.FindControl("chkExportado");
             Label lblAlumnoCodigo = (Label)Item.FindControl("lblAlumnoCodigo");

             if (Session["mLista"] != null)
             {
                 List<string> mLista = (List<string>)Session["mLista"];

                 for (int i = 0; i < mLista.Count; i++)
                 {
                     if (lblAlumnoCodigo.Text.Trim() == mLista[i])
                         if (chkSelTodo.Checked)
                             chkExportado.Checked = false;
                         else
                             chkExportado.Checked = true;
                 }
             }
         }
     }
    
	 private void VerificaSiExisteFamilia()
     {
         foreach (ListViewDataItem Item in lvListaFamilia.Items)
         {
             CheckBox chkExportado = (CheckBox)Item.FindControl("chkExportado");
             Label lblCodigo = (Label)Item.FindControl("lblCodigo");
             Label lblVinculo = (Label)Item.FindControl("lblVinculo");
             Label lblDocumentoIdentidad = (Label)Item.FindControl("lblDocumentoIdentidad");

             if (Session["mLista"] != null)
             {
                 List<BEFamilia> mLista = (List<BEFamilia>)Session["mLista"];

                 for (int i = 0; i < mLista.Count; i++)
                 {
                     if (lblCodigo.Text.Trim() == mLista[i].CodigoFamilia &&
                         lblVinculo.Text.Trim() == mLista[i].Vinculo &&
                         lblDocumentoIdentidad.Text.Trim() == mLista[i].DocumentoIdentidad)
                         if (chkSelTodo.Checked)
                             chkExportado.Checked = false;
                         else
                             chkExportado.Checked = true;
                 }
             }
         }
     }
	 
	 private void VerificaSiExisteEmpleado()
     {
         foreach (ListViewDataItem Item in lvListaEmpleado.Items)
         {
             CheckBox chkExportado = (CheckBox)Item.FindControl("chkExportado");
             Label lblCodigo = (Label)Item.FindControl("lblCodigo");

             if (Session["mLista"] != null)
             {
                 List<string> mLista = (List<string>)Session["mLista"];

                 for (int i = 0; i < mLista.Count; i++)
                 {
                     if (lblCodigo.Text.Trim() == mLista[i])
                         if (chkSelTodo.Checked)
                             chkExportado.Checked = false;
                         else
                             chkExportado.Checked = true;
                 }
             }
         }
     }
	 
	  public DataTable ObtenerTablaTranspuesta(DataTable dt)
    {
        DataTable newTable = new DataTable();
        newTable.Columns.Add(new DataColumn("0", typeof(string)));
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            DataRow newRow = newTable.NewRow();
            newRow[0] = dt.Columns[i].ColumnName;
            for (int j = 1; j <= dt.Rows.Count; j++)
            {
                if (newTable.Columns.Count < dt.Rows.Count + 1)
                    newTable.Columns.Add(new DataColumn(j.ToString(), typeof(string)));
                newRow[j] = dt.Rows[j - 1][i];
            }
            newTable.Rows.Add(newRow);
        }
        return newTable;
    }
    #endregion

	 [WebMethod]
    public static string SetValue(string val)
    {
        HttpContext.Current.Session["mLista"] = null;
        return "1";
    }
	
	 
    #region Llamada  a la capa negocio
 
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


    protected List<BEPeriodoAcademico> ListarAcademico()
    {
        BEPeriodoAcademico be = new BEPeriodoAcademico()
        {
            Descripcion = "",
            Estado = "A",
            Pagina = 0,
        };

        List<BEPeriodoAcademico> lst = new List<BEPeriodoAcademico>();
        BLPeriodoAcademico bl = new BLPeriodoAcademico();
         
            lst = bl.Listar(be);
        
        return lst;
    }

    protected DataTable ListarGradoAcademico()
    {
        BLGrado bl = new BLGrado();
        DataTable dt = new DataTable();
        dt = bl.Listar().Tables[0];

        return dt;
    }

    protected DataTable ListarAlumnos(BEAlumnos be)
    {
        BLAlumnos bl = new BLAlumnos();
        DataTable dt = new DataTable();
        dt = bl.Listar(be).Tables[0];

        return dt;
    }

	protected DataTable ListarFamilias(BEFamilia be)
    {
        BLFamilia bl = new BLFamilia();
        DataTable dt = new DataTable();
        dt = bl.Listar(be).Tables[0];

        return dt;
    }
	protected DataTable ListarEmpleado(BEEmpleado be)
    {
        BLEmpleado bl = new BLEmpleado();
        DataTable dt = new DataTable();
        dt = bl.Listar(be).Tables[0];

        return dt;
    }
    #endregion

   
    
}

 