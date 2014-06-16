<%@ Page Language="C#" AutoEventWireup="false"  CodeFile="sesOut.aspx.cs" Inherits="sesOut" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>End Session</title>
<script language="javascript" type="text/javascript">

 function fnCerrar() 
 {
    //Cierra ventana sin preguntar
     document.forms[0].submit();  
     var wActual = window.self;  
     window.opener = wActual;  
     window.open("","_parent","");  
     top.close(); 
 }
</script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="lblMensaje" runat="server" Text="Session is Expired"></asp:Label>
    &nbsp;<asp:LinkButton ID="lbCerrar" runat="server"  OnClientClick="fnCerrar();">Close</asp:LinkButton>
    </form>
</body>
</html>
