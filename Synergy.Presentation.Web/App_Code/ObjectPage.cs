using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.Caching;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.IO;
using Synergy.Presentation.Web;
using log4net;



    /// <summary>
    /// Summary description 
    /// </summary>
    public class ObjectPage : System.Web.UI.Page
    {

        private ILog _logger = LogManager.GetLogger("gescon.logger");

        public ILog Logger
        {
            get { return _logger; }
        }

        private Utilitarios _util = new Utilitarios();
        public Utilitarios Util
        {
            get { return _util; }
        }

        public ObjectPage()
        {
        }


        public BEUsuarioSistema UsuarioSistema
        {
            get { return (BEUsuarioSistema)Session["UsuarioSistema"]; }
            set { Session["UsuarioSistema"] = value; }
        }


        protected override void OnError(EventArgs e)
        {
            Exception ex = Server.GetLastError();
            ex = ex.InnerException != null ? ex.InnerException : ex;
            Logger.Error("Error en la aplicación:" + ex);
            Response.Redirect("~/err/errLog.aspx");

        }

        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);


            //It appears from testing that the Request and Response both share the 
            // same cookie collection.  If I set a cookie myself in the Reponse, it is 
            // also immediately visible to the Request collection.  This just means that 
            // since the ASP.Net_SessionID is set in the Session HTTPModule (which 
            // has already run), that we can't use our own code to see if the cookie was 
            // actually sent by the agent with the request using the collection. Check if 
            // the given page supports session or not (this tested as reliable indicator 
            // if EnableSessionState is true), should not care about a page that does 
            // not need session
            if (Context.Session != null)
            {
                //Tested and the IsNewSession is more advanced then simply checking if 
                // a cookie is present, it does take into account a session timeout, because 
                // I tested a timeout and it did show as a new session
                if (Session.IsNewSession)
                {
                    // If it says it is a new session, but an existing cookie exists, then it must 
                    // have timed out (can't use the cookie collection because even on first 
                    // request it already contains the cookie (request and response
                    // seem to share the collection)
                    string szCookieHeader = Request.Headers["Cookie"];
                    if ((null != szCookieHeader) && (szCookieHeader.IndexOf("ASP.NET_SessionId") >= 0) &&
                        (string.Format("{0}", "~/seg/segInterchange.aspx").IndexOf(Request.AppRelativeCurrentExecutionFilePath) == -1))
                    {
                        Response.Redirect("~/seg/segInterchangeSendHome.aspx?a=o");

                    }
                }
            }
        }

        //public object ParametrosSistema
        //{
        //    get
        //    {
        //        if (Session["parametrosSistema"] == null)
        //            Session["parametrosSistema"] = new ParametrosSistema();
        //        return (ParametrosSistema)Session["parametrosSistema"];
        //    }
        //}

        //protected override object LoadPageStateFromPersistenceMedium()
        //{
        //    string vState = Request.Form["__VSTATE"];
        //    byte[] bytes = Convert.FromBase64String(vState);
        //    bytes = ViewStateZip.Decompress(bytes);
        //    LosFormatter format = new LosFormatter();
        //    return format.Deserialize(Convert.ToBase64String(bytes));
        //}

        //protected override void SavePageStateToPersistenceMedium(object state)
        //{
        //    LosFormatter format = new LosFormatter();
        //    StringWriter writer = new StringWriter();
        //    format.Serialize(writer, state);
        //    string viewStateStr = writer.ToString();
        //    byte[] bytes = Convert.FromBase64String(viewStateStr);
        //    bytes = ViewStateZip.Compress(bytes);
        //    string vStateStr = Convert.ToBase64String(bytes);
        //    ClientScript.RegisterHiddenField("__VSTATE", vStateStr);
        //}

        /// <summary>
        /// Overload del SavePageState;
        /// Registra en Cache el valor del viewstate que la pagina tendria que recibir;
        /// Se crea un valor univoco por cada host ip con date ticks y se crea como campo hidden; __VIEWSTATE_KEY
        /// </summary>
        /// <remarks></remarks>

        protected override void SavePageStateToPersistenceMedium(object state)
        {
            string str = string.Format("VIEWSTATE_{0}_{1}_{2}", Request.UserHostAddress, DateTime.Now.Ticks.ToString(), Guid.NewGuid());
            Cache.Add(str, state, null, DateTime.Now.AddMinutes(Session.Timeout), TimeSpan.Zero, CacheItemPriority.Default, null);
            ClientScript.RegisterHiddenField("__VIEWSTATE_KEY", str);
        }

        /// <summary>
        /// Recupera el Viewstate desde la cache utilizando el valor univoco __VIEWSTATE_KEY;
        /// </summary>
        protected override object LoadPageStateFromPersistenceMedium()
        {
            string str = Request.Form["__VIEWSTATE_KEY"];
            if (!str.StartsWith("VIEWSTATE_"))
                throw new Exception("Invalid viewstate key:" + str);

            return Cache[str];
        }

    }

