using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using log4net;
using System.Collections.Generic;
using System.Text;
//using Microsoft.Reporting.WebForms;
using Synergy.Presentation.Web;
using System.IO;
using NPOI.HSSF.UserModel;
/// <summary>
/// Métodos Comunes Usados en el CodeBehind de las Páginas
/// </summary>
public class UtilExcel
{

    private ILog Logger;
    public UtilExcel()
	{
        Logger = LogManager.GetLogger(this.GetType().Name);
	}



    public virtual string Cabecera { get; set; }

    public void ExportaACSV(DataTable pdtData, string pstrNombre)
    {
        ExportaACSV(pdtData, pstrNombre, ";", ",");

    }

    public void ExportaAExcelHoja(DataTable pdtData, string pstrNombreArchivo, string pstrHoja)
    {
        // Creat the NpoiExport object
        HttpContext context = HttpContext.Current;
        using (var exporter = new NpoiExport())
        {
            exporter.ExportDataTableToWorkbook(pdtData, pstrHoja);
            context.Response.AddHeader("Content-Disposition", NombreArchivo(pstrNombreArchivo, ".xls"));
            context.Response.Clear();
            context.Response.BinaryWrite(exporter.GetBytes());
            context.Response.End();
        }
    }


    public void ExportaAExcel(DataTable pdtData, string pstrNombre)
    {
        HttpContext context = HttpContext.Current;
        context.Response.Clear();
        context.Response.AddHeader("content-disposition",  NombreArchivo(pstrNombre, ".xls"));
        context.Response.ContentType = "application/vnd.ms-excel";

        string sTab = "";
        foreach (DataColumn dc in pdtData.Columns)
        {
            HttpContext.Current.Response.Write(sTab + dc.ColumnName);
            sTab = "\t";
        }
        HttpContext.Current.Response.Write("\n");
        int i;
        foreach (DataRow dr in pdtData.Rows)
        {
            sTab = "";
            for (i = 0; i < pdtData.Columns.Count; i++)
            {
                HttpContext.Current.Response.Write(sTab + dr[i].ToString());
                sTab = "\t";
            }
            HttpContext.Current.Response.Write("\n");
        }
        HttpContext.Current.Response.End();
        context.Response.End();
    }
    

    public void ExportaACSV(DataTable pdtData, string pstrNombre, string pstrSimboloSep, string pstrSimbReemplazo)
    {
        HttpContext context = HttpContext.Current;
        context.Response.Clear();
        context.Response.ClearHeaders();
        context.Response.ClearContent();
        context.Response.ContentType = "text/csv";
        context.Response.AppendHeader("Content-Disposition", NombreArchivo(pstrNombre, ".csv"));
        context.Response.ContentEncoding = Encoding.Default;
        context.Response.Write(Environment.NewLine);
        EscribeFilas(pdtData, pstrSimboloSep, pstrSimbReemplazo);
    }

    public void ExportaATexto(DataTable pdtData, string pstrNombre, string pstrSimboloSep, string pstrSimbReemplazo)
    {
        HttpContext context = HttpContext.Current;
        context.Response.Clear();
        context.Response.ClearHeaders();
        context.Response.ClearContent();
        context.Response.ContentType = "text/text";
        context.Response.AppendHeader("Content-Disposition", NombreArchivo(pstrNombre, ".txt"));
        context.Response.ContentEncoding = Encoding.Default;
        context.Response.Write(Environment.NewLine);
        EscribeFilas(pdtData, pstrSimboloSep, pstrSimbReemplazo);
    }

    private string NombreArchivo(string pstrNombre, string pstrExtension)
    {
        return "attachment; filename=" + pstrNombre + "_" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + pstrExtension;
    }

    private void EscribeFilas(DataTable dtDatos, string pstrSimboloSep, string pstrSimbReemplazo)
    {
        EscribeColumnas();
        for (int i = 0; i < dtDatos.Rows.Count; i++)
        {
            EscribeInfoUsuario(dtDatos.Rows[i], pstrSimboloSep, pstrSimbReemplazo);
        }
        HttpContext.Current.Response.End();
    }

    private void EscribeColumnas()
    {
        string columnNames = Cabecera;
        HttpContext.Current.Response.Write(columnNames);
        HttpContext.Current.Response.Write(Environment.NewLine);
    }

    private void EscribeInfoUsuario(DataRow drFila, string pstrSimboloSep, string pstrSimbReemplazo)
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int j = 0; j < drFila.ItemArray.Length; j++)
        {
            AnadirSimbolo(drFila[j].ToString(), stringBuilder, pstrSimboloSep, pstrSimbReemplazo);   //Remplaza el ; por coma simple
        }
        if (drFila.ItemArray.Length > 0)
        {
            HttpContext.Current.Response.Write(stringBuilder.ToString());
            HttpContext.Current.Response.Write(Environment.NewLine);
        }
    }

    private void AnadirSimbolo(string value, StringBuilder pstrbNuevoValor, string pstrSimboloSep, string pstrSimbReemplazo)
    {
        pstrbNuevoValor.Append(value.Replace(pstrSimboloSep, pstrSimbReemplazo).Replace("\r\n", " ").Replace("\n", " ").Replace("\t", " "));
        pstrbNuevoValor.Append(pstrSimboloSep);
    }
}
