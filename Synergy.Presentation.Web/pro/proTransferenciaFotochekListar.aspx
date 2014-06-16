<%@ Page Title="" Language="C#" MasterPageFile="~/mas/masReporteVista.master" AutoEventWireup="true"
    CodeFile="proTransferenciaFotochekListar.aspx.cs" Inherits="proTransferenciaFotochekListar" %>

<%@ MasterType TypeName="masReporteVista" %>
<%@ Register Src="~/ucx/ucPaginador.ascx" TagName="ucPaginador" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphArea" runat="Server">
    <div id="filtros">
        <table class="filtro_bar">
            <tr>
                <td style="width: 5%; text-align:right"> 
                   <asp:Label ID="lblDescripcion" runat="server" Text="<%$ Resources:resDiccionario, Descripcion %>"></asp:Label>:
                </td>
                <td style="width: 5%">
                    <asp:TextBox ID="txtDescripcion" runat="server"></asp:TextBox>
                </td>
                <td style="width: 2%">
                </td>
                <td style="width: 5%; text-align:right">
                    <asp:Label ID="Label1" runat="server" Text="<%$ Resources:resDiccionario, Estado %>"></asp:Label>:
                </td>
                <td style="width: 8%">
                    <asp:DropDownList ID="ddlEstado" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <div id="lista">
        <asp:ListView runat="server" ID="lvLista" OnItemCommand="lvLista_ItemCommand">
            <LayoutTemplate>
                <table runat="server" id="lista" class="lista">
                    <thead>
                        <tr>
                            <th id="Th1" style="width: 3%">
                                #
                            </th>
                            <th id="Th6" style="width: 10%; text-align: center">
                                <%= Resources.resDiccionario.Acciones %>
                            </th>
                            <th id="Th2" style="width: 10%">
                                <%= Resources.resDiccionario.Codigo %>
                            </th>
                            <th id="Th3" style="width: 30%">
                                <%= Resources.resDiccionario.Descripcion %>
                            </th>
                            <th id="Th4" style="width: 10%">
                               <%= Resources.resDiccionario.Descripcion %>
                            </th>
                            <th id="Th5" style="width: 20%">
                               <%= Resources.resDiccionario.Descripcion %>
                            </th>
                            <th id="Th7" style="text-align: center">
                                 <%= Resources.resDiccionario.Estado %>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr runat="server" id="itemPlaceholder">
                        </tr>
                    </tbody>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr id="Tr1" runat="server">
                    <td id="Td1" runat="server">
                        <asp:Label ID="lblFila" runat="server" Text='<%# Eval("Fila") %>' />
                    </td>
                    <td id="Td6" runat="server" style="text-align: center">
                        <asp:ImageButton ID="ibtEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("IdModelo").ToString() %>'
                            ToolTip="Cargar" ImageUrl="~/img/ico_exp.png" BorderWidth="0" />
                    </td>
                    <td id="Td2" runat="server">
                        <asp:Label ID="lblCodigo" runat="server" Text='<%# Eval("CodigoModelo") %>' />
                    </td>
                    <td id="Td3" runat="server">
                        <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("Descripcion") %>' />
                    </td>
                    <td id="Td4" runat="server">
                        <asp:Label ID="lblPeriodoAnual" runat="server" Text='<%# Eval("Anio") %>' />
                    </td>
                    <td id="Td5" runat="server">
                        <asp:Label ID="lblVigente" runat="server" Text='<%# (Eval("Vigente").ToString()=="S" ? Resources.resDiccionario.Si : Resources.resDiccionario.No) %>' />
                    </td>
                    <td id="Td7" runat="server" style="text-align: center">
                        <asp:Label ID="lblEstado" runat="server" Text='<%# (Eval("Estado").ToString()=="A" ? Resources.resDiccionario.Activo : Resources.resDiccionario.Inactivo) %>' />
                    </td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table runat="server" id="lista" class="lista">
                    <thead>
                        <tr>
                            <th style="width: 3%">
                                #
                            </th>
                            <th style="width: 10%">
                                 <%= Resources.resDiccionario.Codigo %>
                            </th>
                            <th style="width: 10%">
                                 <%= Resources.resDiccionario.Codigo %>
                            </th>
                            <th style="width: 30%">
                                 <%= Resources.resDiccionario.Descripcion %>
                            </th>
                            <th style="width: 10%">
                                 <%= Resources.resDiccionario.Descripcion %>
                            </th>
                            <th style="width: 20%">
                                <%= Resources.resDiccionario.Descripcion %>
                            </th>
                            <th>
                                 <%= Resources.resDiccionario.Estado %>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td colspan="6">
                                 <%= Resources.resDiccionario.NoHayRegistros %>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
    </div>
    <div style="text-align: right">
        <div>
            <uc1:ucPaginador ID="ucpagLista" runat="server" OnCambioPagina="ucpagLista_CambioPagina" />
        </div>
    </div>
</asp:Content>
