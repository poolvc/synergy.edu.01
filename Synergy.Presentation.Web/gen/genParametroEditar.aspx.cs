using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Synergy.Presentation.Web;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using Synergy.Infraestructure.CrossCutting;


public partial class genParametroEditar : ObjectPage
{
    #region CONSTANTES

    private const string RUTA_LISTA = "~/gen/genParametroListar.aspx";

    #endregion


    #region PROPIEDADES

    public WCParametro.BEParametro Parametro
    {
        get { return (WCParametro.BEParametro)ViewState["GrupoDato"]; }
        set { ViewState["GrupoDato"] = value; }
    }

    #endregion PROPIEDADES

    #region EVENTOS

    protected void Page_Load(object sender, EventArgs e)
    {
        //Eventos del Master
        Master.MenuAccionEvento += new MasterPageMenuEditarClickHandler(Master_MenuButton);
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
            case "Aceptar":
                Guardar("G");
                break;
            case "Aplicar":
                Guardar("A");
                break;
            case "Cancelar":
                Salir();
                break;
        }
    }

    #endregion

    #region METODOS

    protected void Cargar()
    {
        string strAccion = Request.QueryString["a"];   //a: N-Nuevo, e-Editar, v-Ver
        string strId = Request.QueryString["i"];  //Id
        string strIdAp = Request.QueryString["ap"];  //Id de Aplicacion
        //Eventos Java Script
        this.Master.MenuAceptar.Attributes.Add("OnClick", "javascript:return fnValidar();");
        this.txtNumero.Attributes.Add("onkeyup", "javascript:return fnDigitaTexto(this);");
        //Carga Aplicacion
        Util.loadComboAndItem(ddlIdAplicacion, ListarAplicaciones(), "Descripcion", "IdAplicacion", "Suit", string.Format("--{0}--", Resources.resDiccionario.Seleccione), "0");

        ddlIdAplicacion.SelectedValue = UsuarioSistema.IdAplicacion.ToString();
        ddlIdAplicacion.Enabled = false;
        //Carga Estado
        Util.loadCombo(ddlEstado, ListarParametroDetalle(1, 1, Constantes.PARAMETRO_ESTADOREGISTRO), "Descripcion", "ParametroDetalle");
        //Carga Compania
        Util.loadComboAndItem(ddlIdCompania, ListarCompania(), "Descripcion", "IdCompania", string.Format("--{0}--", Resources.resDiccionario.Seleccione), "0");
        //Carga Tipo
        Util.loadCombo(ddlTipo, ListarParametroDetalle(1, 1, Constantes.PARAMETRO_TIPOPARAMETRO), "Descripcion", "ParametroDetalle");

        lnkGeneral.HRef = string.Format("#{0}", tabGeneral.ClientID);
        lnkDetalle.HRef = string.Format("#{0}", tabDetalle.ClientID);

        switch (strAccion)
        {
            case "n":
                Master.SubTitulo.Text = Resources.resDiccionario.Nuevo;
                Nuevo();
                break;
            case "e":
                Master.SubTitulo.Text = Resources.resDiccionario.Editar;
                Editar(strAccion, strId);

                break;
            case "d":
                Master.SubTitulo.Text = Resources.resDiccionario.Inactivar;
                Editar(strAccion, strId);
                break;
            default:
                Master.SubTitulo.Text = Resources.resDiccionario.Unknow;
                break;
        }
    }

    protected void Nuevo()
    {
        hfIdParametro.Value = "0";
        hfAccion.Value = "n"; //Nuevo
        txtIdParametro.Text = string.Empty;
        txtIdParametro.Enabled = false;
        txtDescripcion.Text = string.Empty;
        ddlIdCompania.SelectedIndex = 0;
        ddlIdAplicacion.SelectedIndex = 0;
        ddlTipo.SelectedIndex = 0;
        ddlTipoDato.SelectedIndex = 0;
        txtDefinicion.Text = "";

        txtTexto.Text = string.Empty;
        txtNumero.Text = "0.00";
        txtFecha.Text = string.Empty;



        txtTexto.Visible = true;
        txtTexto.Enabled = true;
        txtFecha.Visible = false;
        txtFecha.Enabled = false;
        txtNumero.Visible = false;
        txtNumero.Enabled = false;


        //Datos de Auditoria
        ddlEstado.SelectedIndex = 0; //Representa el primero
        txtUsuarioCreacion.Text = string.Empty;
        txtFechaCreacion.Text = string.Empty;
        txtUsuarioModificacion.Text = string.Empty;
        txtFechaModificacion.Text = string.Empty;
    }

    protected void Editar(string pstrAccion, string pstrIdParametro)
    {

        WCParametro.BEParametro be = new WCParametro.BEParametro
        {
            IdParametro = Int32.Parse(pstrIdParametro)
        };
        be = ObtenerParametroMant(be);

        hfIdParametro.Value = pstrIdParametro;
        hfAccion.Value = pstrAccion; //Editar/Eliminar
        txtIdParametro.Text = hfIdParametro.Value;
        txtIdParametro.Enabled = false;
        Util.SelectCombo(ddlIdAplicacion, be.IdAplicacion.ToString());
        Util.SelectCombo(ddlIdCompania, be.IdCompania.ToString());
        txtCodigo.Text = be.Parametro;
        txtDescripcion.Text = be.Descripcion;
        Util.SelectCombo(ddlTipo, be.Tipo);
        Util.SelectCombo(ddlTipoDato, be.TipoDato);
        txtDefinicion.Text = be.Definicion;

        switch (be.TipoDato)
        {
            case "T":
                txtTexto.Text = be.Texto;
                txtTexto.Visible = true;
                txtTexto.Enabled = true;
                txtFecha.Visible = false;
                txtFecha.Enabled = false;
                txtNumero.Visible = false;
                txtNumero.Enabled = false;
                break;
            case "N":
                txtNumero.Text = be.Numero.ToString("0.00");
                txtNumero.Visible = true;
                txtNumero.Enabled = true;
                txtTexto.Visible = false;
                txtTexto.Enabled = false;
                txtFecha.Visible = false;
                txtFecha.Enabled = false;
                break;
            case "F":
                txtFecha.Text = be.Fecha == null ? string.Empty : ((DateTime)be.Fecha).ToString(AppSettings.FormatoFecha);
                txtFecha.Visible = true;
                txtFecha.Enabled = true;
                txtNumero.Visible = false;
                txtNumero.Enabled = false;
                txtTexto.Visible = false;
                txtTexto.Enabled = false;

                break;
        }


        //Datos de Auditoria
        Util.SelectCombo(ddlEstado, be.Estado);
        txtUsuarioCreacion.Text = be.UsuarioCreacion;
        txtFechaCreacion.Text = be.FechaCreacion.ToString(AppSettings.FormatoFechaHora);
        txtUsuarioModificacion.Text = be.UsuarioModificacion;
        txtFechaModificacion.Text = be.FechaModificacion.ToString(AppSettings.FormatoFechaHora);

        if (pstrAccion == "d")
        {
            Master.MenuAplicar.Visible = false; //Remueve el boton aplicar
            txtIdParametro.Enabled = false;
            txtCodigo.Enabled = false;
            txtDescripcion.Enabled = false;
            txtDefinicion.Enabled = false;
            ddlIdCompania.Enabled = false;
            ddlIdAplicacion.Enabled = false;
            ddlTipo.Enabled = false;
            ddlTipoDato.Enabled = false;
            txtTexto.Enabled = false;
            txtNumero.Enabled = false;
            txtFecha.Enabled = false;
            ddlEstado.Enabled = false;

            if (be.Estado == Constantes.ESTADO_ACTIVO)
            {
                Util.SelectCombo(ddlEstado, Constantes.ESTADO_INACTIVO);
            }
            else
            {
                Util.SelectCombo(ddlEstado, Constantes.ESTADO_ACTIVO);
            }
        }

        ListarParametroDetalle();
    }

    protected bool Validar(out WCParametro.BEParametro pbe)
    {
        pbe = new WCParametro.BEParametro();
        string strDescripcion = string.Empty, strIdMenu = string.Empty;
        string strUsuario = string.Empty, strEstado = string.Empty, strIdAplicacion = string.Empty, strCodigo = string.Empty;
        string strAccion = string.Empty, strTipo = string.Empty, strTipoDato = string.Empty, strIdCompania = string.Empty;
        string strTexto = string.Empty, strNumero = string.Empty, strFecha = string.Empty;
        string strDefinicion = "";

        strAccion = hfAccion.Value; //a: n-Nuevo, e-Editar, v-Ver

        strCodigo = txtCodigo.Text.Trim();
        strDescripcion = txtDescripcion.Text.Trim();
        strIdAplicacion = ddlIdAplicacion.SelectedValue;
        strIdCompania = ddlIdCompania.SelectedValue;
        strTipo = ddlTipo.SelectedValue;
        strTipoDato = ddlTipoDato.SelectedValue;
        strTexto = txtTexto.Text.Trim();
        strNumero = string.IsNullOrEmpty(txtNumero.Text.Trim()) ? "0" : txtNumero.Text.Trim();
        strFecha = txtFecha.Text.Trim();


        strEstado = ddlEstado.SelectedValue;
        strUsuario = UsuarioSistema.CodigoUsuario;
        strDefinicion = txtDefinicion.Text;

        //Inicializa
        WCParametro.BEParametro be = new WCParametro.BEParametro
        {
            IdParametro = Int32.Parse(hfIdParametro.Value),

            Parametro = strCodigo,
            Descripcion = strDescripcion,
            IdAplicacion = Int32.Parse(strIdAplicacion),
            IdCompania = Int32.Parse(strIdCompania),
            Tipo = strTipo,
            TipoDato = strTipoDato,
            Texto = strTexto,
            Numero = Decimal.Parse(strNumero),
            Fecha = string.IsNullOrEmpty(strFecha) ? (DateTime?)null : (DateTime?)Util.ObtenerFechaCadena(strFecha, AppSettings.FormatoFecha),
            Definicion = strDefinicion,

            Estado = strEstado,
            UsuarioCreacion = strUsuario,
            FechaCreacion = DateTime.Now,
            UsuarioModificacion = strUsuario,
            FechaModificacion = DateTime.Now
        };
        pbe = be;
        return true;
    }

    protected void Guardar(string pstrEvento) //G:Grabar, A:Aplicar
    {
        string strAccion = string.Empty;
        string strUsuario = string.Empty;

        strAccion = hfAccion.Value; //a: n-Nuevo, e-Editar, v-Ver
        strUsuario = UsuarioSistema.CodigoUsuario;

        WCParametro.BEParametro be;

        if (!Validar(out be)) //Valida e Inicializa
        {
            return;
        }

        //Grabar
        switch (strAccion)
        {
            case "n": //Nuevo
                be = InsertarParametro(be);
                if (be.Error != 0)
                {
                    if (be.Error == -2)
                    {
                        Util.ejecutaScriptAJAX(this.Page, string.Format("alert('{0}');", "El código ya existe."));
                    }
                    else
                    {
                        Util.ejecutaScriptAJAX(this.Page, string.Format("alert('{0}');", Resources.resMensaje.msgErrorRegistrar));
                    }
                }
                else
                {
                    hfAccion.Value = "e";
                    hfIdParametro.Value = be.IdParametro.ToString();
                    txtIdParametro.Text = be.IdParametro.ToString();
                    txtUsuarioCreacion.Text = strUsuario;
                    txtFechaCreacion.Text = be.FechaCreacion.ToString(AppSettings.FormatoFechaHora);
                    txtUsuarioModificacion.Text = strUsuario;
                    txtFechaModificacion.Text = be.FechaCreacion.ToString(AppSettings.FormatoFechaHora);
                }

                break;
            case "e": //Actualiza
                be = ActualizarParametro(be);
                if (be.Error != 0)
                {
                    if (be.Error == -2)
                    {
                        Util.ejecutaScriptAJAX(this.Page, string.Format("alert('{0}');", Resources.resMensaje.msgAlertaCodigoYaExiste));
                    }
                    else
                    {
                        Util.ejecutaScriptAJAX(this.Page, string.Format("alert('{0}');", Resources.resMensaje.msgErrorRegistrar));
                    }
                }
                else
                {
                    txtUsuarioModificacion.Text = strUsuario;
                    txtFechaModificacion.Text = be.FechaModificacion.ToString(AppSettings.FormatoFechaHora);
                }
                break;
            case "d": //Elimina
                be = EliminaParametro(be);
                if (be.Error != 0)
                {
                    Util.ejecutaScriptAJAX(this.Page, string.Format("alert('{0}');", Resources.resMensaje.msgErrorActualizar));
                }
                txtUsuarioModificacion.Text = strUsuario;
                txtFechaModificacion.Text = be.FechaModificacion.ToString(AppSettings.FormatoFechaHora);
                break;
        }
        if (pstrEvento == "G") { if (be.Error == 0) { Util.Redireciona(RUTA_LISTA, "?a=l", true); } }
    }

    protected void Salir()
    {
        string FiltroParam = Request.QueryString["f"];
        Util.Redireciona(RUTA_LISTA, "?a=l&f=" + FiltroParam, true);
    }

    #endregion

    #region TAB CONCEPTOS

    protected void ibtNuevoParametroDetalle_Click(object sender, ImageClickEventArgs e)
    {
        NuevoParametroDetalle();
    }

    protected void lvParametroDetalle_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        ParametroDetalleOperacion(e.CommandName, e.Item);
    }

    protected void lvParametroDetalle_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        BoundParametroDetalle(e.Item);
    }

    protected void ListarParametroDetalle()
    {
        int intTotalFilas = 0, intFilasXPagina = 0;
        //Obtiene lo ultimos no eliminados  
        WCParametroDetalle.BEParametroDetalle[] arrNuevo = null;
        arrNuevo = ListarParametroDetalle(int.Parse(hfIdParametro.Value), "", "A", ucpagListaParametroDetalle.NumeroPagina);

        lvParametroDetalle.DataSource = arrNuevo;
        lvParametroDetalle.DataBind();
        if (arrNuevo.Length > 0)
        {
            intFilasXPagina = arrNuevo[0].FilasXPagina;
            intTotalFilas = arrNuevo[0].TotalFilas;
        }
        ucpagListaParametroDetalle.TotalRegistros = intTotalFilas;
        ucpagListaParametroDetalle.RegistrosPorPagina = intFilasXPagina;
        ucpagListaParametroDetalle.EnlazarPaginador();
    }

    protected void NuevoParametroDetalle()
    {
        DataSet ds = new DataSet();
        lvParametroDetalle.EditIndex = -1;
        lvParametroDetalle.InsertItemPosition = InsertItemPosition.LastItem;
        ListarParametroDetalle();
        lblNuevoDetalle.Visible = false;
        ibtNuevoParametroDetalle.Visible = false;
    }


    protected void ParametroDetalleOperacion(string pstrComando, ListViewItem plviObj)
    {
        switch (pstrComando)
        {
            case Constantes.ACCION_GRABARNUEVO:
                GrabarParametroDetalleNuevo(plviObj);
                break;


            case Constantes.ACCION_EDITAR:
                EditarParametroDetalle(plviObj);
                break;
            case Constantes.ACCION_GRABAR:
                GrabarParametroDetalle(plviObj);

                break;
            case Constantes.ACCION_CANCELAR:
                CancelarParametroDetalle();
                break;
            case Constantes.ACCION_ELIMINAR:
                EliminarParametroDetalle(plviObj);
                break;
        }
    }

    protected void GrabarParametroDetalleNuevo(ListViewItem plceArg)
    {
        //Crear un item:
        TextBox txtDetalleInsertIdParametro = (TextBox)plceArg.FindControl("txtDetalleInsertIdParametro");
        TextBox txtDetalleInsertParametroDetalle = (TextBox)plceArg.FindControl("txtDetalleInsertParametroDetalle");
        TextBox txtDetalleInsertDescripcion = (TextBox)plceArg.FindControl("txtDetalleInsertDescripcion");
        TextBox txtDetalleInsertValorTexto1 = (TextBox)plceArg.FindControl("txtDetalleInsertValorTexto1");
        TextBox txtDetalleInsertValorTexto2 = (TextBox)plceArg.FindControl("txtDetalleInsertValorTexto2");
        TextBox txtDetalleInsertValorTexto3 = (TextBox)plceArg.FindControl("txtDetalleInsertValorTexto3");
        TextBox txtDetalleInsertValorTexto4 = (TextBox)plceArg.FindControl("txtDetalleInsertValorTexto4");
        TextBox txtDetalleInsertValorTexto5 = (TextBox)plceArg.FindControl("txtDetalleInsertValorTexto5");
        TextBox txtDetalleInsertValorTexto6 = (TextBox)plceArg.FindControl("txtDetalleInsertValorTexto6");
        TextBox txtDetalleInsertValorNumero = (TextBox)plceArg.FindControl("txtDetalleInsertValorNumero");
        TextBox txtDetalleInsertValorFecha = (TextBox)plceArg.FindControl("txtDetalleInsertValorFecha");
        TextBox txtDetalleInsertExplicacion = (TextBox)plceArg.FindControl("txtDetalleInsertExplicacion");
        DropDownList ddlDetalleInsertEstado = (DropDownList)plceArg.FindControl("ddlDetalleInsertEstado");

        string strFecha = txtDetalleInsertValorFecha.Text.Trim();
        string strValorNumero = txtDetalleInsertValorNumero.Text.Trim();

        WCParametroDetalle.BEParametroDetalle be = new WCParametroDetalle.BEParametroDetalle
        {
            RefGuid = Guid.NewGuid().ToString(),
            IdParametro = int.Parse(hfIdParametro.Value),
            ParametroDetalle = txtDetalleInsertParametroDetalle.Text.Trim(),
            Descripcion = txtDetalleInsertDescripcion.Text.Trim(),
            ValorTexto1 = txtDetalleInsertValorTexto1.Text.Trim(),
            ValorTexto2 = txtDetalleInsertValorTexto2.Text.Trim(),
            ValorTexto3 = txtDetalleInsertValorTexto3.Text.Trim(),
            ValorTexto4 = txtDetalleInsertValorTexto4.Text.Trim(),
            ValorTexto5 = txtDetalleInsertValorTexto5.Text.Trim(),
            ValorTexto6 = txtDetalleInsertValorTexto6.Text.Trim(),
            ValorNumero = string.IsNullOrEmpty(strValorNumero) ? 0 : Decimal.Parse(txtDetalleInsertValorNumero.Text.Trim()),
            ValorFecha = string.IsNullOrEmpty(strFecha) ? (DateTime?)null : (DateTime?)Util.ObtenerFechaCadena(strFecha, AppSettings.FormatoFecha),
            Explicacion = txtDetalleInsertExplicacion.Text.Trim(),
            Estado = ddlDetalleInsertEstado.SelectedValue,
            RefAccion = WCParametroDetalle.DbTipoAccion.Insertar,
            UsuarioCreacion = UsuarioSistema.CodigoUsuario,
        };

        //InsertarParametroDetalle(be);
        //// Cancela el modo de Insersion de ventana

        be = InsertarParametroDetalle(be);
        if (be.Error != 0)
        {
            if (be.Error == -2)
            {
                Util.ejecutaScriptAJAX(this.Page, string.Format("alert('{0}');", Resources.resMensaje.msgAlertaCodigoYaExiste));
            }
            else
            {
                Util.ejecutaScriptAJAX(this.Page, string.Format("alert('{0}');", Resources.resMensaje.msgErrorRegistrar));
            }
            return;
        }

        CancelarModoInsertar();
        //// Listar el Detalle 
        ListarParametroDetalle();
    }

    protected void GrabarParametroDetalle(ListViewItem plceArg)
    {

        //Obtiene los valres ingresados

        HiddenField hfDetalleEditIdParametro = (HiddenField)plceArg.FindControl("txtDetalleEditIdParametro");
        TextBox txtDetalleEditParametroDetalle = (TextBox)plceArg.FindControl("txtDetalleEditParametroDetalle");
        TextBox txtDetalleEditDescripcion = (TextBox)plceArg.FindControl("txtDetalleEditDescripcion");
        TextBox txtDetalleEditValorTexto1 = (TextBox)plceArg.FindControl("txtDetalleEditValorTexto1");
        TextBox txtDetalleEditValorTexto2 = (TextBox)plceArg.FindControl("txtDetalleEditValorTexto2");
        TextBox txtDetalleEditValorTexto3 = (TextBox)plceArg.FindControl("txtDetalleEditValorTexto3");
        TextBox txtDetalleEditValorTexto4 = (TextBox)plceArg.FindControl("txtDetalleEditValorTexto4");
        TextBox txtDetalleEditValorTexto5 = (TextBox)plceArg.FindControl("txtDetalleEditValorTexto5");
        TextBox txtDetalleEditValorTexto6 = (TextBox)plceArg.FindControl("txtDetalleEditValorTexto6");
        TextBox txtDetalleEditValorNumero = (TextBox)plceArg.FindControl("txtDetalleEditValorNumero");
        TextBox txtDetalleEditValorFecha = (TextBox)plceArg.FindControl("txtDetalleEditValorFecha");
        TextBox txtDetalleEditValorExplicacion = (TextBox)plceArg.FindControl("txtDetalleEditValorExplicacion");
        DropDownList ddlDetalleEditEstado = (DropDownList)plceArg.FindControl("ddlDetalleEditEstado");

        string strValorNumero = txtDetalleEditValorNumero.Text.Trim();
        string strFecha = txtDetalleEditValorFecha.Text.Trim();

        WCParametroDetalle.BEParametroDetalle be = new WCParametroDetalle.BEParametroDetalle
        {
            IdParametro = int.Parse(hfIdParametro.Value),
            ParametroDetalle = txtDetalleEditParametroDetalle.Text.Trim(),
            Descripcion = txtDetalleEditDescripcion.Text.Trim(),

            ValorTexto1 = txtDetalleEditValorTexto1.Text.Trim(),
            ValorTexto2 = txtDetalleEditValorTexto2.Text.Trim(),
            ValorTexto3 = txtDetalleEditValorTexto3.Text.Trim(),
            ValorTexto4 = txtDetalleEditValorTexto4.Text.Trim(),
            ValorTexto5 = txtDetalleEditValorTexto5.Text.Trim(),
            ValorTexto6 = txtDetalleEditValorTexto6.Text.Trim(),
            ValorNumero = string.IsNullOrEmpty(strValorNumero) ? 0 : Decimal.Parse(txtDetalleEditValorNumero.Text.Trim()),
            ValorFecha = string.IsNullOrEmpty(strFecha) ? (DateTime?)null : (DateTime?)Util.ObtenerFechaCadena(strFecha, AppSettings.FormatoFecha),
            Explicacion = txtDetalleEditValorExplicacion.Text.Trim(),
            Estado = ddlDetalleEditEstado.SelectedValue,
            UsuarioModificacion = UsuarioSistema.CodigoUsuario,

        };

        ActualizarParametroDetalle(be);

        lvParametroDetalle.EditIndex = -1;

        //// Cancela el modo de Insersion
        CancelarModoInsertar();

        //// Listar el Detalle de Salida
        ListarParametroDetalle();
    }

    protected void EliminarParametroDetalle(ListViewItem plceArg)
    {

        //TextBox hfDetalleItemIdParametro = (TextBox)plceArg.FindControl("txtDetalleEditIdParametro");
        Label lblDetalleItemParametroDetalle = (Label)plceArg.FindControl("lblDetalleItemParametroDetalle");

        WCParametroDetalle.BEParametroDetalle be = new WCParametroDetalle.BEParametroDetalle

        {
            IdParametro = int.Parse(hfIdParametro.Value),
            ParametroDetalle = lblDetalleItemParametroDetalle.Text.Trim(),
            Estado = Constantes.ESTADO_INACTIVO,
            //IdAplicacion = int.Parse(ddlIdAplicacion.SelectedValue),
            UsuarioModificacion = UsuarioSistema.CodigoUsuario,
        };

        EliminarParametroDetalle(be);

        lvParametroDetalle.EditIndex = -1;

        //// Listar el Detalle de Salida
        ListarParametroDetalle();
    }

    protected void EditarParametroDetalle(ListViewItem plceArg)
    {
        lvParametroDetalle.EditIndex = plceArg.DataItemIndex;
        HiddenField hfDetalleItemGui = (HiddenField)plceArg.FindControl("hfDetalleItemGui");
        // Listar Detalle
        ListarParametroDetalle();
    }

    protected void CancelarParametroDetalle()
    {
        lvParametroDetalle.EditIndex = -1;
        CancelarModoInsertar();
        // Listar Detalle
        ListarParametroDetalle();
    }

    protected void BoundParametroDetalle(ListViewItem plceArg)
    {
        if (plceArg.ItemType == ListViewItemType.DataItem)
        {
            if (plceArg.DataItemIndex == lvParametroDetalle.EditIndex)
            {
                TextBox txtDetalleEditValorFecha = (TextBox)plceArg.FindControl("txtDetalleEditValorFecha");
                //txtDetalleEditValorFecha.Text = be.FechaAdquisicion == null ? string.Empty : ((DateTime)be.FechaAdquisicion).ToString(AppSettings.FormatoFecha);
                TextBox txtDetalleEditValorNumero = (TextBox)plceArg.FindControl("txtDetalleEditValorNumero");
                txtDetalleEditValorNumero.Attributes.Add("onkeyup", "fnIngresoNumeroDecimales(this,6, true);");

            }
            else
            {

            }
        }
    }

    protected void CancelarModoInsertar()
    {
        lvParametroDetalle.InsertItemPosition = InsertItemPosition.None;
        lblNuevoDetalle.Visible = true;
        ibtNuevoParametroDetalle.Visible = true;
    }


    protected void ucpagListaParametroDetalle_CambioPagina(object sender, EventArgs e)
    {
        ListarParametroDetalle();
    }

    #endregion TAB CONCEPTOS


    #region LLAMDAS A WEBSERVICES

    /// <summary>
    /// Método Lista 
    /// </summary>
    /// <returns>Devuelve un DataSet</returns>
    protected WCParametro.BEParametro InsertarParametro(WCParametro.BEParametro pbe)
    {
        WCParametro.BEParametro be = new WCParametro.BEParametro();
        DataTable dt = new DataTable();
        using (var client = new ServiceClient<WCParametro.IWCParametro>("BasicHttpBinding_IWCParametro"))
        {
            var response = client.ServiceOperation<WCParametro.BEParametro>(c => c.Insertar(pbe));
            be = response;
        }
        return be;
    }

    /// <summary>
    /// Método Obtiene 
    /// </summary>
    /// <returns>Devuelve un Objeto</returns>
    protected WCParametro.BEParametro ObtenerParametroMant(WCParametro.BEParametro pbe)
    {
        WCParametro.BEParametro be = new WCParametro.BEParametro();
        DataTable dt = new DataTable();
        using (var client = new ServiceClient<WCParametro.IWCParametro>("BasicHttpBinding_IWCParametro"))
        {
            var response = client.ServiceOperation<WCParametro.BEParametro>(
               c => c.ObtenerMant(pbe)
               );
            ;
            be = response;
        }
        return be;
    }

    /// <summary>
    /// Método Actualiza
    /// </summary>
    /// <returns>Devuelve un DataSet</returns>
    protected WCParametro.BEParametro ActualizarParametro(WCParametro.BEParametro pbe)
    {
        WCParametro.BEParametro be = new WCParametro.BEParametro();
        DataTable dt = new DataTable();
        using (var client = new ServiceClient<WCParametro.IWCParametro>("BasicHttpBinding_IWCParametro"))
        {
            var response = client.ServiceOperation<WCParametro.BEParametro>(
               c => c.Actualizar(pbe)
               );
            ;
            be = response;
        }
        return be;
    }

    /// <summary>
    /// Método Elimina 
    /// </summary>
    /// <returns>Devuelve un DataSet</returns>
    protected WCParametro.BEParametro EliminaParametro(WCParametro.BEParametro pbe)
    {
        WCParametro.BEParametro be = new WCParametro.BEParametro();
        DataTable dt = new DataTable();
        using (var client = new ServiceClient<WCParametro.IWCParametro>("BasicHttpBinding_IWCParametro"))
        {
            var response = client.ServiceOperation<WCParametro.BEParametro>(
               c => c.Eliminar(pbe)
               );
            ;
            be = response;
        }
        return be;
    }

    /// <summary>
    /// Método Lista 
    /// </summary>
    /// <returns>Devuelve un dataTable</returns>
    protected DataTable ListarParametro()
    {
        WCParametro.BEParametro be = new WCParametro.BEParametro();
        be.IdAplicacion = 0;
        be.Descripcion = "";
        be.Tipo = "";
        be.TipoDato = "T";
        be.Estado = "A";
        be.Pagina = 0;
        DataTable dt = new DataTable();
        using (var client = new ServiceClient<WCParametro.IWCParametro>("BasicHttpBinding_IWCParametro"))
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
    /// Método Lista Companias
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

    protected DataTable ListarAplicacion()
    {
        DataTable dt = new DataTable();
        //WCAplicacion.BEAplicacion beAp = new WCAplicacion.BEAplicacion();
        //beAp.IdSuit = 0;
        //beAp.Descripcion = string.Empty;
        //beAp.Estado = "A";
        //beAp.Pagina = 0;
        using (var client = new ServiceClient<WCAplicacion.IWCAplicacion>("BasicHttpBinding_IWCAplicacion"))
        {
            var response = client.ServiceOperation<DataTable>(
                c => c.Listar().Tables[0]
                    );
            ;
            dt = response;
        }
        return dt;
    }

    /// <summary>
    /// Método Lista Niveles de Alerta
    /// </summary>
    /// <returns>Devuelve un DataSet</returns>
    protected WCParametroDetalle.BEParametroDetalle[] ListarParametroDetalle(int pinIdParametro, string pstrParametroDetalle, string pstrEstado, int pinPagina)
    {
        WCParametroDetalle.BEParametroDetalle be = new WCParametroDetalle.BEParametroDetalle();

        be.IdParametro = pinIdParametro;
        be.ParametroDetalle = pstrParametroDetalle;
        be.Estado = pstrEstado;
        be.Pagina = pinPagina;
        WCParametroDetalle.BEParametroDetalle[] arr = null;
        using (var client = new ServiceClient<WCParametroDetalle.IWCParametroDetalle>("BasicHttpBinding_IWCParametroDetalle"))
        {
            var response = client.ServiceOperation<WCParametroDetalle.BEParametroDetalle[]>(c => c.ListarMant(be));
            arr = response;
        }
        return arr;
    }

    /// <summary>
    /// Método Lista 
    /// </summary>
    /// <returns>Devuelve un DataSet</returns>
    protected WCParametroDetalle.BEParametroDetalle InsertarParametroDetalle(WCParametroDetalle.BEParametroDetalle pbe)
    {
        WCParametroDetalle.BEParametroDetalle be = new WCParametroDetalle.BEParametroDetalle();
        DataTable dt = new DataTable();
        using (var client = new ServiceClient<WCParametroDetalle.IWCParametroDetalle>("BasicHttpBinding_IWCParametroDetalle"))
        {
            var response = client.ServiceOperation<WCParametroDetalle.BEParametroDetalle>(c => c.Insertar(pbe));
            be = response;
        }
        return be;
    }

    /// <summary>
    /// Método Actualiza 
    /// </summary>
    /// <returns>Devuelve un DataSet</returns>
    protected WCParametroDetalle.BEParametroDetalle ActualizarParametroDetalle(WCParametroDetalle.BEParametroDetalle pbe)
    {
        WCParametroDetalle.BEParametroDetalle be = new WCParametroDetalle.BEParametroDetalle();
        DataTable dt = new DataTable();
        using (var client = new ServiceClient<WCParametroDetalle.IWCParametroDetalle>("BasicHttpBinding_IWCParametroDetalle"))
        {
            var response = client.ServiceOperation<WCParametroDetalle.BEParametroDetalle>(
               c => c.Actualizar(pbe)
               );
            ;
            be = response;
        }
        return be;
    }

    /// <summary>
    /// Método Elimina 
    /// </summary>
    /// <returns>Devuelve un DataSet</returns>
    protected WCParametroDetalle.BEParametroDetalle EliminarParametroDetalle(WCParametroDetalle.BEParametroDetalle pbe)
    {
        WCParametroDetalle.BEParametroDetalle be = new WCParametroDetalle.BEParametroDetalle();
        DataTable dt = new DataTable();
        using (var client = new ServiceClient<WCParametroDetalle.IWCParametroDetalle>("BasicHttpBinding_IWCParametroDetalle"))
        {
            var response = client.ServiceOperation<WCParametroDetalle.BEParametroDetalle>(
               c => c.Eliminar(pbe)
               );
            ;
            be = response;
        }
        return be;
    }

    protected DataTable ListarAplicaciones()
    {

        WCAplicacion.BEAplicacion be = new WCAplicacion.BEAplicacion();
        be.Estado = "A";

        DataTable dt = new DataTable();
        using (var client = new ServiceClient<WCAplicacion.IWCAplicacion>("BasicHttpBinding_IWCAplicacion"))
        {
            var response = client.ServiceOperation<DataTable>(c => c.Listar().Tables[0]);
            dt = response;
        }
        return dt;
    }


    protected DataTable ListarCompania()
    {

        WCCompania.BECompania be = new WCCompania.BECompania();
        be.Estado = "A";
        be.Descripcion = string.Empty;
        DataTable dt = new DataTable();
        using (var client = new ServiceClient<WCCompania.IWCCompania>("BasicHttpBinding_IWCCompania"))
        {
            var response = client.ServiceOperation<DataTable>(c => c.Listar_Sel(be).Tables[0]);
            dt = response;
        }
        return dt;
    }

    #endregion


    protected void ddlTipoDato_SelectedIndexChanged(object sender, EventArgs e)
    {

        //new WCParametro.BEParametro();
        string strdato = string.Empty;

        strdato = ddlTipoDato.SelectedValue;
        switch (strdato)
        {
            case "T":
                txtTexto.Visible = true;
                txtTexto.Enabled = true;
                txtFecha.Visible = false;
                txtFecha.Enabled = false;
                txtNumero.Visible = false;
                txtNumero.Enabled = false;
                break;
            case "N":
                txtNumero.Visible = true;
                txtNumero.Enabled = true;
                txtTexto.Visible = false;
                txtTexto.Enabled = false;
                txtFecha.Visible = false;
                txtFecha.Enabled = false;
                break;
            case "F":
                txtFecha.Visible = true;
                txtFecha.Enabled = true;
                txtNumero.Visible = false;
                txtNumero.Enabled = false;
                txtTexto.Visible = false;
                txtTexto.Enabled = false;

                break;
        }


    }

}