<%@ Page Title="" Language="C#" MasterPageFile="~/mas/masReporteVista.master" AutoEventWireup="true"
    CodeFile="repRrhReporte1.aspx.cs" Inherits="repRrhReporte1" %>

<%@ MasterType TypeName="masReporteVista" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphArea" runat="Server">
    <script language="javascript" type="text/javascript">

        function fnValidar() {
            if (!fnValidarGeneral())
                return false;
            return true;
        }

        function fnValidarGeneral() {   
           
           
            return true;
        }

    </script>
            <div id="Div1">
                <table class="filtro_bar">
                    <tr>
                        <td style="width: 5%">
                           <asp:Label ID="lblPeriodo" runat="server" Text="Centro de Costo"></asp:Label>
                        </td>
                        <td style="width: 5%">
                            <asp:TextBox ID="txtCentroCosto" runat="server" MaxLength="6" Width="45px"></asp:TextBox>
                        </td>
                        <td style="width: 2%">
                        </td>
                        <td style="width: 10%">
                             <asp:Label ID="lblModeloNegocio" runat="server" Text="Persona"></asp:Label>
                        </td>
                        <td style="width: 12%">
                            <asp:TextBox ID="txtPersona" runat="server" MaxLength="6" Width="45px"></asp:TextBox>
                        </td>
                        <td style="width: 3%; text-align:right">
                             &nbsp;</td>
                        <td>
                   
                        </td>
                        <td style=" text-align: left">
                            &nbsp;</td>
                    </tr>
                </table>
            </div>
            
            <div id="lista">
                <asp:Panel ID="pnlReporte" runat="server">
                    <rsweb:ReportViewer ID="rvReporte" runat="server"  ProcessingMode="Remote" Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
                        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" Height="100%">
                    <ServerReport ReportPath="" ReportServerUrl="" />
                    </rsweb:ReportViewer>
                </asp:Panel>
            </div>
            <asp:HiddenField ID="hfAccion" runat="server" />
            <asp:HiddenField ID="hfIdModelo" runat="server" />
</asp:Content>
