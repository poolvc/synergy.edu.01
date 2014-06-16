<%@ Page Title="" Language="C#" MasterPageFile="~/mas/masGeneral.master" AutoEventWireup="true" CodeFile="errLog.aspx.cs" Inherits="errLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" Runat="Server">

<div>
    <h2> Error en la aplicacion</h2>
    <p>
        Resultado: 
        <asp:Label ID="lblMensaje" runat="server" Text="..."></asp:Label>
    </p>
    

</div>


</asp:Content>

<asp:Content ID="content2" ContentPlaceHolderID="cphMaincol" runat="server">
            
</asp:Content>


<asp:Content ID="content3" ContentPlaceHolderID="cphFooter" runat="server">
            
</asp:Content>


