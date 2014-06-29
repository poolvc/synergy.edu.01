<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPaginador.ascx.cs"
    Inherits="ucPaginador" %>

<script type="text/javascript">
    function <%= this.ClientID %>_Paginar(tipo)
    {
        AbrirVentanaEspera({ title: "Cargando...", message: "Espere un momento por favor" });
        document.getElementById('<%= hdTipoPaginacion.ClientID %>').value=tipo;
        document.getElementById('<%= btnPaginacion.ClientID %>').click();
    }
</script>

<asp:Panel ID="pnlPaginador" runat="server" Visible="false">
    <table class="cl_paginacion_tabla" border="0" >
        <tbody>
            <tr id = "trPag" runat="server">
                <td></td>
            </tr> 
            <tr>
                <td class="detalleAlt" style="text-align: center; height: 22px;">
                    <asp:UpdatePanel ID="upnPaginador" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table>
                                <tr>
                                    <td style="width: 10px">
                                        <img style="vertical-align: bottom; cursor: hand" id="imgInicio" src="~/img/pag/NavFirstPageDisabled.gif"
                                            runat="server" alt="Ir a la primera página." /></td>
                                    <td style="width: 10px">
                                        <img style="vertical-align: bottom; cursor: hand" id="imgAnterior" src="~/img/pag/NavPreviousPageDisabled.gif"
                                            runat="server" alt="Ir a la página anterior." /></td>
                                    <td style="width: 20px">
                                        Página</td>
                                    <td style="width: 30px">
                                        <asp:DropDownList ID="ddlPaginas" runat="server" CssClass="small">
                                        </asp:DropDownList></td>
                                    <td style="width: 10px">
                                        de</td>
                                    <td style="width: 10px">
                                        <asp:Label ID="lblNumPagina" runat="server"></asp:Label></td>
                                    <td style="width: 10px">
                                        <img runat="server" style="vertical-align: bottom; cursor: hand" id="imgSiguiente" src="~/img/pag/NavNextPageDisabled.gif" alt="Ir a la siguiente página." /></td>
                                    <td style="width: 10px">
                                        <img runat="server" style="vertical-align: bottom; cursor: hand" id="imgFinal" src="~/img/pag/NavLastPageDisabled.gif"
                                            alt="Ir a la última página." /></td>
                                     <td style="width: 120px"> <asp:Label ID="lblTotal" runat="server" Text="Total Registros"></asp:Label>
                                         <asp:Label ID="lblTotalRegistros" runat="server"></asp:Label>
                                        </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </tbody>
    </table>
    <asp:Button ID="btnPaginacion" runat="server" OnClick="btnPaginacion_Click" Style="display: none"
        Text="Paginacion" />
    <input id="hdTipoPaginacion" runat="server" type="hidden" />
    <input id="hdFuncionJSPaginar" runat="server" type="hidden" />
</asp:Panel>
