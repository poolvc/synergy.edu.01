
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class ucPaginador : System.Web.UI.UserControl
{
    #region CONSTANTES
    private const string INICIO = "I";
    private const string ANTERIOR = "A";
    private const string SIGUIENTE = "SI";
    private const string SELECCION = "SE";
    private const string FIN = "F";
    #endregion

    #region PROPERTIES
    public int NumeroPagina
    {
        get { return ddlPaginas.SelectedIndex + 1; }
        set 
        {
            if (ddlPaginas.Items.Count > 0)
            {
                if (value < 0 || value > ddlPaginas.Items.Count)
                    throw new Exception("El valor de la página actual es incorrecto.");
                ddlPaginas.SelectedIndex = value - 1;
                ValidaPaginacion();
            }
        }
    }

    private int _totalRegistros;

    public int TotalRegistros
    {
        set
        {
            lblTotalRegistros.Text = value.ToString();
            _totalRegistros = value; }
    }

    private int _registrosPorPagina;

    public int RegistrosPorPagina
    {
        get { return _registrosPorPagina; }
        set { _registrosPorPagina = value; }
    }

    private int _totalregistrosPorPagina;
    public int TotalRegistrosPorPagina
    {
        get { return _totalregistrosPorPagina; }
        set { _totalregistrosPorPagina = value; }
    }

    private int _alturaFila;
    public int AlturaFila
    {
        get { return _alturaFila; }
        set { _alturaFila = value; }
    }

    public Label TextoTotal
    {
        get { return lblTotal; }
        set { lblTotal = value; }
    }

    public Label TextoTotalRegistro
    {
        get { return lblTotalRegistros; }
        set { lblTotalRegistros = value; }
    }
    /// <summary>
    /// Evento que se lanza cuando se cambia de página en el paginador.
    /// </summary>
    public event EventHandler CambioPagina;

    #endregion

    #region EVENTOS

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hdFuncionJSPaginar.Value = string.Format("{0}_Paginar", this.ClientID);
            //
            ddlPaginas.Items.Clear();
            lblNumPagina.Text = string.Empty;
            for (int i = 0; i < 1; i++)
            {
                ddlPaginas.Items.Add(new ListItem((i + 1).ToString(), (i + 1).ToString()));
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ddlPaginas.Attributes.Add("onchange",FuncionJSPaginacion(SELECCION));
    }

    protected void btnPaginacion_Click(object sender, EventArgs e)
    {
        switch (hdTipoPaginacion.Value)
        {
            case INICIO:
                ddlPaginas.SelectedIndex = 0;
                break;
            case ANTERIOR:
                ddlPaginas.SelectedIndex--;
                break;
            case SIGUIENTE:
                ddlPaginas.SelectedIndex++;
                break;
            case FIN:
                ddlPaginas.SelectedIndex = int.Parse(lblNumPagina.Text) - 1;
                break;
        }
        upnPaginador.Update();
        if (CambioPagina != null)
            CambioPagina(this, EventArgs.Empty);
    }

    #endregion

    #region METODOS
    /// <summary>
    /// Obtiene la cadena que referencia a la funcion JS de paginacion segun el tipo ingresado.
    /// </summary>
    /// <param name="tipoPaginacion"></param>
    /// <returns></returns>
    string FuncionJSPaginacion(string tipoPaginacion)
    {
        return string.Format("{0}('{1}')",hdFuncionJSPaginar.Value,tipoPaginacion);
    }


    /// <summary>
    /// Llamarlo Dentro del método que carga los datos en la grilla luego de setear los valores de Total de registros y Registros por Página.
    /// </summary>
    /// <param name="numeroPaginas"></param>
    public void EnlazarPaginador()
    {
        int numeroPaginas = 0;
        if (_totalRegistros > 0 && _registrosPorPagina > 0)
        {
            numeroPaginas = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(_totalRegistros) / Convert.ToDouble(_registrosPorPagina)));
            if (numeroPaginas != ddlPaginas.Items.Count)
            {
                ddlPaginas.Items.Clear();
                lblNumPagina.Text = string.Empty;
                for (int i = 0; i < numeroPaginas; i++)
                {
                    ddlPaginas.Items.Add(new ListItem((i + 1).ToString(), (i + 1).ToString()));
                }
            }
            lblNumPagina.Text = numeroPaginas.ToString();
            ValidaPaginacion();
        }
        pnlPaginador.Visible = numeroPaginas > 0;

        //define al alto 
        int inRegAltoPag = _registrosPorPagina - _totalregistrosPorPagina;

        trPag.Style.Add("height", (inRegAltoPag * _alturaFila).ToString() + "px");
    }


    private void ValidaPaginacion()
    {
        imgInicio.Src = "~/img/pag/NavFirstPageDisabled.gif";
        imgInicio.Attributes.Remove("onclick");
        imgInicio.Style.Add(HtmlTextWriterStyle.Cursor, "default");

        imgAnterior.Src = "~/img/pag/NavPreviousPageDisabled.gif";
        imgAnterior.Attributes.Remove("onclick");
        imgAnterior.Style.Add(HtmlTextWriterStyle.Cursor, "default");

        imgSiguiente.Src = "~/img/pag/NavNextPageDisabled.gif";
        imgSiguiente.Attributes.Remove("onclick");
        imgSiguiente.Style.Add(HtmlTextWriterStyle.Cursor, "default");

        imgFinal.Src = "~/img/pag/NavLastPageDisabled.gif";
        imgFinal.Attributes.Remove("onclick");
        imgFinal.Style.Add(HtmlTextWriterStyle.Cursor, "default");
        if (ddlPaginas.Items.Count > 1)
        {
            if (ddlPaginas.SelectedIndex == 0)
            {
                imgSiguiente.Src = "~/img/pag/NavNextPage.gif";
                imgSiguiente.Attributes.Add("onclick", FuncionJSPaginacion(SIGUIENTE));
                imgSiguiente.Style.Add(HtmlTextWriterStyle.Cursor, "pointer");

                imgFinal.Src = "~/img/pag/NavLastPage.gif";
                imgFinal.Attributes.Add("onclick", FuncionJSPaginacion(FIN));
                imgFinal.Style.Add(HtmlTextWriterStyle.Cursor, "pointer");
                return;
            }

            if (ddlPaginas.SelectedIndex == ddlPaginas.Items.Count - 1)
            {
                imgInicio.Src = "~/img/pag/NavFirstPage.gif";
                imgInicio.Attributes.Add("onclick", FuncionJSPaginacion(INICIO));
                imgInicio.Style.Add(HtmlTextWriterStyle.Cursor, "pointer");

                imgAnterior.Src = "~/img/pag/NavPreviousPage.gif";
                imgAnterior.Attributes.Add("onclick", FuncionJSPaginacion(ANTERIOR));
                imgAnterior.Style.Add(HtmlTextWriterStyle.Cursor, "pointer");
                return;
            }

            imgInicio.Src = "~/img/pag/NavFirstPage.gif";
            imgInicio.Attributes.Add("onclick", FuncionJSPaginacion(INICIO));
            imgInicio.Style.Add(HtmlTextWriterStyle.Cursor, "pointer");

            imgAnterior.Src = "~/img/pag/NavPreviousPage.gif";
            imgAnterior.Attributes.Add("onclick", FuncionJSPaginacion(ANTERIOR));
            imgAnterior.Style.Add(HtmlTextWriterStyle.Cursor, "pointer");

            imgSiguiente.Src = "~/img/pag/NavNextPage.gif";
            imgSiguiente.Attributes.Add("onclick", FuncionJSPaginacion(SIGUIENTE));
            imgSiguiente.Style.Add(HtmlTextWriterStyle.Cursor, "pointer");

            imgFinal.Src = "~/img/pag/NavLastPage.gif";
            imgFinal.Attributes.Add("onclick", FuncionJSPaginacion(FIN));
            imgFinal.Style.Add(HtmlTextWriterStyle.Cursor, "pointer");
        }
    }

    #endregion
}
