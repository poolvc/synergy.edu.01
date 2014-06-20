using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Synergy.Presentation.Web;
using Microsoft.Reporting.WebForms;
using System.Collections.Specialized;
using Synergy.Application.Edu;
using Synergy.Domain.Edu.Entities;
using Synergy.Infraestructure.CrossCutting;
using System.Web.Services;
using System.Web.Script.Services;

public partial class genPeriodoAcademicoListarAnterior : ObjectPage
{

    #region CONSTANTES

    protected const string REPORTE_RUTA = "/Reports/EDU/repIndicador";
    protected const string EDITAR_RUTA = "~/gen/genPeriodoAcademicoEditar.aspx";

    #endregion 

    #region PROPIEDADES

    protected int NumeroPagina = 0;

    #endregion PROPIEDADES

    #region EVENTOS

    protected void Page_Load(object sender, EventArgs e)
    {
        //Eventos del Master
        Master.MenuAccionEvento += new MasterPageMenuListarClickHandler(Master_MenuButton);
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
                ucpagLista.NumeroPagina = 1;
                Listar();
                break;
            case "Nuevo":
                Nuevo();
                break;
            case "Exportar":
                Exportar();
                break;
            case "Imprimir":
                Imprimir();
                break;
        }
    }

    protected void ucpagLista_CambioPagina(object sender, EventArgs e)
    {
        Listar();
    }

    protected void lvLista_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        string strId = e.CommandArgument.ToString();
        switch (e.CommandName)
        {
            case "Editar":
                Editar(strId);
                break;
            case "Eliminar":
                Eliminar(strId);
                break;
        }
    }

    protected void lvLista_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ImageButton ibt;
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            //ibt = (ImageButton)e.Item.FindControl("ibtEliminar");
            //Label lb = (Label)e.Item.FindControl("lblEstado");
            //if (((Label)e.Item.FindControl("lblEstado")).Text == Constantes.ESTADO_ACTIVO_DESC)
            //{
            //    ibt.ImageUrl = Constantes.ESTADO_ACTIVO_URL;
            //    ibt.ToolTip = Resources.resDiccionario.Inactivar;
            //}
            //else
            //{
            //    ibt.ImageUrl = Constantes.ESTADO_INACTIVO_URL;
            //    ibt.ToolTip = Resources.resDiccionario.Activar;
            //}
        }
    }

    #endregion 

    #region METODOS

    protected void Cargar()
    {
        //Master.Titulo.Text = "Nivel Alerta";
        ucpagLista.NumeroPagina = 1;
        ddlEstado.SelectedIndex = 0; //Estado Activo

        //Carga Estado
        Util.loadCombo(ddlEstado, ListarParametroDetalle(1, 1, Constantes.PARAMETRO_ESTADOREGISTRO), "Descripcion", "ParametroDetalle");
        //Configura Parametros
        ConfiguraParametros();
    }

    /// <summary>
    /// Método Lista
    /// </summary>
    /// <returns></returns>
    protected void Listar()
    {
        int intTotalFilas = 0, intFilasXPagina = 0;
        string strDescripcion, strEstado;

        strEstado = ddlEstado.SelectedValue;
        strDescripcion = "";
        int inNumeroPagina = NumeroPagina.Equals(0) ? ucpagLista.NumeroPagina : NumeroPagina;
        //Validar

        //Filtrar
        List<BEPeriodoAcademico> ls = ListarAcademico(strDescripcion, ddlEstado.SelectedValue, inNumeroPagina);
        lvLista.DataSource = ls;
        lvLista.DataBind();
        //if (dt.Rows.Count > 0)
        //{
        //    intFilasXPagina =  Convert.ToInt32(dt.Rows[0]["FilasXPagina"]);
        //    intTotalFilas = Convert.ToInt32(dt.Rows[0]["TotalFilas"]);
        //}
        ucpagLista.TotalRegistros = intTotalFilas;
        ucpagLista.RegistrosPorPagina = intFilasXPagina;
        ucpagLista.EnlazarPaginador();
        ucpagLista.NumeroPagina = inNumeroPagina;
    }

    protected void Nuevo()
    {
        string strCadena = "?a=n";
        Util.Redireciona(EDITAR_RUTA, strCadena, true);
    }

    protected void Editar(string strId)
    {
        string strCadena = string.Format("?a=e&i={0}&param1={1}", strId, ObtenerFiltroParametro());
        Util.Redireciona(EDITAR_RUTA, strCadena, true);
    }

    protected void Eliminar(string strId)
    {
        string strCadena = string.Format("?a=d&i={0}&param1={1}", strId, ObtenerFiltroParametro());
        Util.Redireciona(EDITAR_RUTA, strCadena, true);
    }

    protected void Exportar()
    {
        UtilExcel ut = new UtilExcel();
        List<BEPeriodoAcademico> ls = ListarAcademico(string.Empty, ddlEstado.SelectedValue, 0);
        //dt.Columns.Remove("FilasXPagina");
        //dt.Columns.Remove("TotalFilas");
        //ut.ExportaAExcelHoja(ls, Resources.resDiccionario.Archivo, Resources.resDiccionario.Datos);
    }

    protected void Imprimir()
    {
       
    }

    private string ObtenerFiltroParametro()
    {
        return string.Format("{0}|{1}", ucpagLista.NumeroPagina.ToString(), ddlEstado.SelectedValue);
    }

    void ConfiguraParametros()
    {
        string strParam = HttpUtility.UrlDecode(Request.QueryString["param1"]);
        if (!string.IsNullOrEmpty(strParam))
        {
            string[] strFiltros = strParam.Split('|');
            NumeroPagina = int.Parse(strFiltros[0]);
            Util.SelectCombo(ddlEstado, strFiltros[1]);
        }
    }

    #endregion

    #region Metodos de Aplicacion


    /// <summary>
    /// Método Lista Periodo Academico
    /// </summary>
    /// <returns>Devuelve un DataSet</returns>
    /// 
    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    protected List<BEFamiliaSel> ListarFamiliaPorCampo(string pstrColumna, string pstrValor, int pintPagina)
    {
        BEFamiliaSel be = new BEFamiliaSel()
        {
            Columna = pstrColumna,
            Valor = pstrValor,
            Pagina = pintPagina,
        };

        List<BEFamiliaSel> lst = new List<BEFamiliaSel>();
        BLFamiliaSel bl = new BLFamiliaSel();
        //using (BLPeriodoAcademico bl = new BLPeriodoAcademico())
        //{
        lst = bl.ListarPorCampo(be);
        //}
        return lst;
    }

    /// <summary>
    /// Método Lista Periodo Academico
    /// </summary>
    /// <returns>Devuelve un DataSet</returns>
    protected List<BEPeriodoAcademico> ListarAcademico(string pstrDescripcion, string pstrEstado, int pintPagina)
    {
        BEPeriodoAcademico be = new BEPeriodoAcademico()
        {
            Descripcion = pstrDescripcion,
            Estado = pstrEstado,
            Pagina = pintPagina,
        };

        List<BEPeriodoAcademico> lst = new List<BEPeriodoAcademico>();
        BLPeriodoAcademico bl = new BLPeriodoAcademico();
        //using (BLPeriodoAcademico bl = new BLPeriodoAcademico())
        //{
            lst = bl.Listar(be);
        //}
        return lst;
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

    #endregion Metodos de Aplicacion

}