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
using Microsoft.Reporting.WebForms;
using Synergy.Presentation.Web;
/// <summary>
/// Métodos Comunes Usados en el CodeBehind de las Páginas
/// </summary>
public class UtilReporte
{

    private ILog Logger;

    public UtilReporte()
	{
        Logger = LogManager.GetLogger(this.GetType().Name);
	}

    public void Exportar(string pstrRuta, string pstrFormato, BEEntidadString[] pbeArr)
    {
        ReportViewer rp = new ReportViewer();
        ServerReport srepServidor = rp.ServerReport;
        ReportParameter[] parm = new ReportParameter[pbeArr.Length];
        int inCont = 0;

        foreach (BEEntidadString be in pbeArr)
        {
            parm[inCont] = new ReportParameter(be.Descripcion, be.Id);
            inCont = inCont + 1;
        }

        rp.ShowCredentialPrompts = false;

        RSCadenaConexion rs = new RSCadenaConexion();
        rp.ServerReport.ReportServerCredentials = new ReportCredentials(rs.UserId, rs.Password, "");
        rp.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        rp.ServerReport.ReportServerUrl = new System.Uri(rs.URLServer);
        rp.ServerReport.ReportPath = pstrRuta;
        rp.ShowParameterPrompts = false;
        rp.ServerReport.SetParameters(parm);
        rp.ServerReport.Refresh();

        string mimeType, encoding, extension, deviceInfo;
        string[] streamids;
        Microsoft.Reporting.WebForms.Warning[] warnings;

        deviceInfo =
        "<DeviceInfo>" +
        "<SimplePageHeaders>True</SimplePageHeaders>" +
        "</DeviceInfo>";

        byte[] bytes = rp.ServerReport.Render(pstrFormato, deviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);

        HttpContext.Current.Response.Clear();

        if (pstrFormato == "PDF")
        {
            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AddHeader("Content-disposition", "filename=output.pdf");
        }
        else if (pstrFormato == "Excel")
        {
            HttpContext.Current.Response.ContentType = "application/excel";
            HttpContext.Current.Response.AddHeader("Content-disposition", "filename=output.xls");
        }

        HttpContext.Current.Response.OutputStream.Write(bytes, 0, bytes.Length);
        HttpContext.Current.Response.OutputStream.Flush();
        HttpContext.Current.Response.OutputStream.Close();
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.Close();
    }

}
