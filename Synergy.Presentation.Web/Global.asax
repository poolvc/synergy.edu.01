<%@ Application Language="C#" %>
<%@ Import Namespace="log4net" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
        log4net.Config.XmlConfigurator.Configure();
        ILog Logger = LogManager.GetLogger("gescon.logger");
        Logger.Info("Aplicación Galexito.Mof iniciada");
    } 
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown
        ILog Logger = LogManager.GetLogger("gescon.logger");
        Logger.Info("Aplicación Galexito.Mof terminada");
    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started
    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

        ILog Logger = LogManager.GetLogger("gescon.logger");
        Logger.Info("Sesión terminada por el sistema");
        GC.Collect();
    }
    
    
       
</script>
