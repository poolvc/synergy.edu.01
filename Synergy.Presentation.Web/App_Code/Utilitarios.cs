using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using log4net;
using System.Collections.Generic;
using System.Text;
using Synergy.Infraestructure.CrossCutting;

/// <summary>
/// Métodos Comunes Usados en el CodeBehind de las Páginas
/// </summary>
/// 
namespace Synergy.Presentation.Web
{
    public class Utilitarios
    {

        private ILog Logger;

        public void RemoveCache()
        {
            HttpContext.Current.Response.Cache.SetExpires(DateTime.Now.AddSeconds(1));
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.Public);
            HttpContext.Current.Response.Cache.SetSlidingExpiration(true);
            HttpContext.Current.Response.Cache.SetNoStore();
        }

        public Utilitarios()
        {
            Logger = LogManager.GetLogger(this.GetType().Name);
        }


        public void SetDDLIndexByValue(DropDownList pDDL, string StrValue)
        {
            pDDL.SelectedIndex = pDDL.Items.IndexOf(pDDL.Items.FindByValue(StrValue));
        }

        public bool EnviarMail(string pstrDestinatario, string pstrRemitente, string pstrCopia, string pstrAsunto, string pstrRuta, string pstrTexto, List<string> plstrRuta, string pstrSmtpServer)
        {
            string strTexto = string.Empty;
            System.Net.Mail.MailMessage objMessage = new System.Net.Mail.MailMessage();
            try
            {
                objMessage.From = new System.Net.Mail.MailAddress(pstrRemitente);
                objMessage.To.Add(pstrDestinatario);
                objMessage.CC.Add(pstrCopia);
                objMessage.Subject = pstrAsunto;
                objMessage.Body = pstrTexto;
                objMessage.IsBodyHtml = true;
                objMessage.Priority = System.Net.Mail.MailPriority.Normal;

                foreach (string str in plstrRuta)
                {
                    System.Net.Mail.Attachment attachFile = new System.Net.Mail.Attachment(pstrRuta + str);
                    objMessage.Attachments.Add(attachFile);
                }

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                if (!string.IsNullOrEmpty(pstrSmtpServer))
                {
                    smtp.Host = pstrSmtpServer; //Usa ppor defecto el servidor Web
                }

                smtp.Send(objMessage);
            }
            catch (Exception ex)
            {
                Logger.Error("Error al enviar SMTP.", ex);
                return false;
            }
            return true;
        }

        public void EnviarMailMasivo(String pDestinatario, String pAsunto, String pTexto)
        {
            String sTexto = "";
            String[] arDestinatarios;
            arDestinatarios = pDestinatario.Split(';');
            System.Net.Mail.MailMessage objMessage = new System.Net.Mail.MailMessage();
            objMessage.From = new System.Net.Mail.MailAddress(System.Configuration.ConfigurationManager.AppSettings["Remitente"].Trim());

            for (int i = 0; i < arDestinatarios.Length; i++)
            {
                if (arDestinatarios[i].Trim() == "")
                    return;

                objMessage.To.Add(arDestinatarios[i]);
            }

            sTexto += pTexto + "<br /><br /><b>Servicio Al Cliente</b><br />eTicket - Sistema de Reservación de Eventos de EMAPE<br />";
            objMessage.Body = sTexto;
            objMessage.IsBodyHtml = true;
            objMessage.Priority = System.Net.Mail.MailPriority.Normal;

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            smtp.Host = System.Configuration.ConfigurationManager.AppSettings["SmtpServer"].Trim();
            smtp.Send(objMessage);
        }

        public void controlError(Page pagPage, Exception ex)
        {
            WriteLog(pagPage, ex);
            String strAppPath = pagPage.Request.ApplicationPath;
            pagPage.Response.Redirect(strAppPath + "/Mensaje.aspx?mensaje=Ha ocurrido un error en la aplicacion");
        }

        private void WriteLog(Page pagPage, Exception ex)
        {
            try
            {
                FileStream fs = new FileStream(ConfigurationManager.AppSettings["logPath"], FileMode.Append, FileAccess.Write, FileShare.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("Fecha y Hora: " + DateTime.Now.ToString());
                sw.WriteLine("Error: " + ex.ToString());
                sw.WriteLine("Página: " + pagPage.AppRelativeVirtualPath.Substring(2).ToUpper());
                //sw.WriteLine("Usuario: " + ((Usuario)(pagPage.Session["usuario"])).UserMaquina);
                sw.WriteLine("******************************************************");
                sw.WriteLine();
                sw.Close();
            }
            catch (Exception es)
            {
                Logger.Error(es.Message);

            }
        }

        public void loadCheckBoxList(CheckBoxList pobjCBL, DataTable pdtbSource, String pstrTextColumn, String pstrValueColumn)
        {
            pobjCBL.DataValueField = pstrValueColumn;
            pobjCBL.DataTextField = pstrTextColumn;
            pobjCBL.DataSource = pdtbSource;
            pobjCBL.DataBind();
        }

        public void loadCombo(DropDownList pobjDDL, DataTable pdtbSource, String pstrTextColumn, String pstrValueColumn)
        {

            pobjDDL.DataValueField = pstrValueColumn;
            pobjDDL.DataTextField = pstrTextColumn;
            pobjDDL.DataSource = pdtbSource;
            pobjDDL.DataBind();

        }

        public void loadCombo(DropDownList pobjDDL, DataTable pdtbSource, String pstrTextColumn, String pstrValueColumn, String pstrGroupColumn)
        {
            pobjDDL.Items.Clear();
            ListItem objItem = null;
            foreach (DataRow objRow in pdtbSource.Rows)
            {
                objItem = new ListItem() { Value = objRow[pstrValueColumn].ToString(), Text = objRow[pstrTextColumn].ToString() };
                objItem.Attributes["Group"] = objRow[pstrGroupColumn].ToString();
                pobjDDL.Items.Add(objItem);
            }
        }

        public void loadCombo(DropDownList pobjDDL, object[] obj, String pstrTextColumn, String pstrValueColumn)
        {

            pobjDDL.DataValueField = pstrValueColumn;
            pobjDDL.DataTextField = pstrTextColumn;
            pobjDDL.DataSource = obj;
            pobjDDL.DataBind();

        }

        public void loadComboSelectValue(DropDownList pobjDDL, DataTable pdtbSource, String pstrTextColumn, String pstrValueColumn, string pstrValueDefault)
        {

            pobjDDL.DataValueField = pstrValueColumn;
            pobjDDL.DataTextField = pstrTextColumn;
            pobjDDL.DataSource = pdtbSource;
            pobjDDL.DataBind();
            SelectCombo(pobjDDL, pstrValueDefault);
        }

        public void loadComboAndItem(DropDownList pobjDDL, DataTable pdtbSource, String pstrTextColumn, String pstrValueColumn, String pstrGroupColumn, String pstrItemText, String pstrItemValue)
        {
            if (pdtbSource.Rows.Count != 0)
            {
                loadCombo(pobjDDL, pdtbSource, pstrTextColumn, pstrValueColumn, pstrGroupColumn);
            }
            else
            {
                pobjDDL.Items.Clear();
            }
            pobjDDL.Items.Insert(0, new ListItem(pstrItemText, pstrItemValue));
            pobjDDL.SelectedIndex = 0;
        }

        public void loadComboAndItem(DropDownList pobjDDL, DataTable pdtbSource, String pstrTextColumn, String pstrValueColumn, String pstrItemText)
        {
            if (pdtbSource.Rows.Count != 0)
            {
                loadCombo(pobjDDL, pdtbSource, pstrTextColumn, pstrValueColumn);
            }
            else
            {
                pobjDDL.Items.Clear();


            }
            pobjDDL.Items.Insert(0, new ListItem(pstrItemText, "0"));
            pobjDDL.SelectedIndex = 0;

        }

        public void loadComboAndItem(DropDownList pobjDDL, DataTable pdtbSource, String pstrTextColumn, String pstrValueColumn, String pstrItemText, String pstrItemValue)
        {
            loadCombo(pobjDDL, pdtbSource, pstrTextColumn, pstrValueColumn);
            pobjDDL.Items.Insert(0, new ListItem(pstrItemText, pstrItemValue));
            pobjDDL.SelectedIndex = 0;
        }


        public void loadComboAndItem(DropDownList pobjDDL, object[] obj, String pstrTextColumn, String pstrValueColumn, String pstrItemText, String pstrItemValue)
        {
            loadCombo(pobjDDL, obj, pstrTextColumn, pstrValueColumn);
            if (!(string.IsNullOrEmpty(pstrItemText) &&
                  string.IsNullOrEmpty(pstrItemValue)))
            {
                pobjDDL.Items.Insert(0, new ListItem(pstrItemText, pstrItemValue));
            }
            pobjDDL.SelectedIndex = 0;
        }

        /// <summary>
        /// Seleciona un valor del DropDowList
        /// </summary>
        /// <returns>Devuelve un DataSet</returns> 
        /// 
        public void SelectCombo(DropDownList pddl, string pstrValor)
        {
            if (pddl.Items.IndexOf(pddl.Items.FindByValue(pstrValor)) >= 0)
            {
                pddl.SelectedItem.Selected = false;
                pddl.SelectedIndex = pddl.Items.IndexOf(pddl.Items.FindByValue(pstrValor));
            }
        }



        public void limpiaCombo(String pstrItemText, params object[] comboParams)
        {
            DropDownList combo;
            for (int i = 0; i <= comboParams.GetUpperBound(0); i++)
            {
                combo = new DropDownList();
                combo = (DropDownList)comboParams[i];
                combo.Items.Clear();
                combo.Items.Insert(0, new ListItem(pstrItemText, "0"));
                combo.SelectedIndex = 0;
            }
        }

        public void muestraMensaje(Page pPage, String mensaje)
        {
            String str_Popup = string.Empty;
            str_Popup = "<script language='javascript'>alert('" + mensaje + "')</script>";
            pPage.ClientScript.RegisterStartupScript(this.GetType(), "Mensaje", str_Popup);
        }

        public void mensajeAJAX(Page pPage, String mensaje)
        {
            ScriptManager.RegisterStartupScript(pPage, pPage.GetType(), "SCRIPTAJAX", "alert('" + mensaje + "');", true);
        }

        public void ejecutaScript(Page pPage, String strScript)
        {
            String str_Popup = string.Empty;
            str_Popup = "<script language='javascript'>" + strScript + "</script>";
            pPage.ClientScript.RegisterStartupScript(this.GetType(), "Script", str_Popup);
        }

        public void ejecutaScriptAJAX(Page pPage, String script)
        {
            ScriptManager.RegisterStartupScript(pPage, pPage.GetType(), "SCRIPTAJAX", script, true);
        }

        public void mostrarMensajeThickbox(Page pPage, bool bol_pCorrecto, String str_pMensaje)
        {
            Dictionary<bool, string> dctMensaje = new Dictionary<bool, string>();
            if (pPage.Session["Mensaje"] != null)
                dctMensaje = (Dictionary<bool, string>)pPage.Session["Mensaje"];

            dctMensaje.Clear();

            if (bol_pCorrecto)
                dctMensaje.Add(true, str_pMensaje);
            else
                dctMensaje.Add(false, str_pMensaje);

            pPage.Session["Mensaje"] = dctMensaje;

            String str_Popup = string.Empty;
            str_Popup = "document.getElementById('linkThick').click();";
            ScriptManager.RegisterStartupScript(pPage, pPage.GetType(), "SCRIPTAJAX", str_Popup, true);
        }

        public String FormatDecimal(Double objNumber)
        {
            return objNumber.ToString("#,##0.00");
        }

        public String FormatNumber(Double objNumber)
        {
            return objNumber.ToString("#,##0");
        }

        public String FormatShortDate(DateTime dtmDate)
        {
            return dtmDate.ToShortDateString();
        }

        public String FormatLongDate(DateTime objDate)
        {
            return objDate.ToLongDateString();
        }

        public String AddPeriod(string pstrPeriodo, int pintAdd)
        {
            DateTime date = new DateTime(int.Parse(pstrPeriodo.Substring(0, 4)), int.Parse(pstrPeriodo.Substring(4, 2)), 1);

            return date.AddMonths(pintAdd).ToString("yyyyMM");
        }


        public string LimpiaCadena(string pstrExpresion)
        {
            return "";
        }

        //Carga un dropdowlist con colores y sus respectivos nombres
        public void CargaDropColores(DropDownList dropDownlist)
        {
            if (dropDownlist.Items.Count < 1)
            {
                dropDownlist.CssClass = "";
                //dropDownlist.SkinID = "";
                int index = -1;
                if (dropDownlist.SelectedIndex > 0)
                    index = dropDownlist.SelectedIndex;
                //Inicializamos los colores y mostramos el mensaje de seleccionar color.
                dropDownlist.Items.Clear();
                dropDownlist.Items.Add(new ListItem("SELECCIONE"));
                foreach (int pos in Enum.GetValues(typeof(System.Drawing.KnownColor)))
                {
                    UserControl uc = new UserControl();

                    string strnColor = Enum.GetName(typeof(System.Drawing.KnownColor), pos);
                    System.Drawing.Color color = System.Drawing.Color.FromName(strnColor);


                    if (!color.IsSystemColor)
                    {
                        ListItem item = new ListItem(color.Name, pos.ToString());

                        item.Attributes.CssStyle.Add(HtmlTextWriterStyle.BackgroundColor, color.Name);
                        dropDownlist.Items.Add(item);

                    }

                }
            }

        }

        //eliminar un item de un dropdownlist
        public void EliminarItemCombo(DropDownList combo, string valor)
        {
            combo.Items.Remove(combo.Items.FindByValue(valor));
        }


        //eliminar un item de un dropdownlist
        public void SelecionaItemCombo(DropDownList combo, string valor)
        {
            ListItem li = null;
            li = combo.Items.FindByValue(valor);
            if (li != null)
            {
                combo.ClearSelection();
                li.Selected = true;
            }
        }

        //eliminar un item de un dropdownlist
        public void SelecionaItemComboContenido(DropDownList combo, string valor)
        {
            foreach (ListItem li in combo.Items)
            {
                if ((li.Value.Trim().IndexOf(valor.Trim())) >= 0)
                {
                    li.Selected = true;
                    break;
                }
            }
        }

        public DataTable XElementToDataTable(XElement x)
        {
            DataTable dt = new DataTable();

            //XElement setup = (from p in x.Descendants() select p).First();
            //foreach (XElement xe in setup.Descendants()) // build your DataTable
            //    dt.Columns.Add(new DataColumn(xe.Name.ToString(), typeof(string))); // add columns to your dt

            //var all = from p in x.Descendants(setup.Name.ToString()) select p;
            //foreach (XElement xe in all)
            //{
            //    DataRow dr = dt.NewRow();
            //    foreach (XElement xe2 in xe.Descendants())
            //        dr[xe2.Name.ToString()] = xe2.Value; //add in the values
            //    dt.Rows.Add(dr);
            //}
            return dt;
        }


        public void ReplaceEmptyStrings<T>(List<T> list, string replacement)
        {
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo p in properties)
            {
                // Only work with strings
                if (p.PropertyType != typeof(string)) { continue; }

                // If not writable then cannot null it; if not readable then cannot check it's value
                if (!p.CanWrite || !p.CanRead) { continue; }

                MethodInfo mget = p.GetGetMethod(false);
                MethodInfo mset = p.GetSetMethod(false);

                // Get and set methods have to be public
                if (mget == null) { continue; }
                if (mset == null) { continue; }

                foreach (T item in list)
                {
                    if (string.IsNullOrEmpty((string)p.GetValue(item, null)))
                    {
                        p.SetValue(item, replacement, null);
                    }
                }
            }
        }

        public void Redireciona(string pstrRuta, string pStrParam, bool blResponse)
        {
            string strUrl = string.Empty;

            strUrl = HttpContext.Current.Request.QueryString["m"];
            if (string.IsNullOrEmpty(strUrl))
            {
                strUrl = string.Empty;
            }
            else
            {
                strUrl = "&m=" + strUrl;
            }

            HttpContext.Current.Response.Redirect(pstrRuta + pStrParam + strUrl, blResponse);
        }

        public DateTime ObtenerFechaCadena(string pstrFecha, string pstrFormato) // 2011/11/12  -   yyyy/MM/dd
        {
            DateTime dt = DateTime.ParseExact(pstrFecha, pstrFormato, null);
            return dt;
        }

        public DateTime ObtenerFechaCadena(string pstrFecha, string pstrFormato, int pinHora, int pinMinuto, int pinSegundo, int pinMilisegundos) // 2011/11/12  -   yyyy/MM/dd
        {
            DateTime dtParse = DateTime.ParseExact(pstrFecha, pstrFormato, null);
            DateTime dt = new DateTime(dtParse.Year, dtParse.Month, dtParse.Day, pinHora, pinMinuto, pinSegundo, pinMilisegundos);
            return dt;
        }

        public DateTime ObtenerPrimerDiaMes(DateTime pdtFechaRef) // 2011/11/12  00:00:00
        {
            return new DateTime(pdtFechaRef.Year, pdtFechaRef.Month, 1);
        }

        public DateTime ObtenerUltimoDiaMes(DateTime pdtFechaRef) // 2011/10/31  23:59:59 9999
        {
            DateTime firstDayOfTheMonth = new DateTime(pdtFechaRef.Year, pdtFechaRef.Month, 1, 23, 59, 59, 999);
            return firstDayOfTheMonth.AddMonths(1).AddDays(-1);
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


        public void GeneraGridColumns(GridView pgv, DataTable dt)
        {
            pgv.AutoGenerateColumns = false;

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                BoundField field = new BoundField();
                field.DataField = dt.Columns[i].ColumnName;
                field.HtmlEncode = false;
                pgv.Columns.Add(field);
            }
        }

        #region XML

        public DataSet ReadXML(string purlXML)
        {
            string strMapServer = HttpContext.Current.Server.MapPath(purlXML);
            DataSet ds = new DataSet();
            ds.ReadXml(strMapServer);
            return ds;

        }

        public string ObtenerMensaje(string pstrName, string pstrLanguage)
        {
            string strValue = string.Empty;
            DataSet ds = ReadXML(Constantes.MENSAJES_URL);
            if (ds != null)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow[] drs = ds.Tables["message"].Select("name = '" + pstrName + "' AND language = '" + pstrLanguage + "' ");
                    if (drs.Length > 0)
                    {
                        strValue = drs[0]["value"].ToString();
                    }
                }
            }
            return strValue;
        }



        #endregion XML
    }
}