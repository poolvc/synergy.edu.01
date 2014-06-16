<%@ Page Title="" Language="C#" MasterPageFile="~/mas/masMantListar.master" AutoEventWireup="true"
    CodeFile="genParametroListar.aspx.cs" Inherits="genParametroListar" %>

<%@ MasterType TypeName="masMantListar" %>
<%@ Register Src="~/ucx/ucPaginador.ascx" TagName="ucPaginador" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphArea" runat="Server">
    <div id="filtros">
        <table class="filtro_bar">
            <tr>
               
                <td style="width: 10%; text-align: right">
                    <asp:Label ID="lblDescripcion" runat="server" Text="<%$Resources:resDiccionario,Descripcion %>"></asp:Label>:
                    &nbsp;
                </td>
                <td style="width: 5%">
                    <asp:TextBox ID="txtDescripcion" runat="server"></asp:TextBox>
                </td>
                <td style="width: 7%; text-align: right">
                    <asp:Label ID="lblTipoDato" runat="server" Text="<%$Resources:resDiccionario,TipoDato %>"></asp:Label>:
                </td>
                <td>
                    <asp:DropDownList ID="ddlTipoDato" runat="server">
                      
                    </asp:DropDownList>
                </td>
                <td style="width: 7%; text-align: right">
                    <asp:Label ID="lblTipo" runat="server" Text="<%$Resources:resDiccionario,Tipo %>"></asp:Label>:
                </td>
                <td>
                    <asp:DropDownList ID="ddlTipo" runat="server">
                    </asp:DropDownList>
                </td>
                
                <td style="width: 2%">
                </td>
                <td style="width: 3%; text-align: right">
                    <asp:Label ID="lblEstado" runat="server" Text="<%$Resources:resDiccionario,Estado %>"></asp:Label>:
                </td>
                <td style="width: 5%">
                    <asp:DropDownList ID="ddlEstado" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <div id="lista">
        <asp:ListView runat="server" ID="lvLista" OnItemCommand="lvLista_ItemCommand" OnItemDataBound="lvLista_ItemDataBound">
            <LayoutTemplate>
                <table runat="server" id="lista" class="lista">
                    <thead>
                        <tr>
                            <th id="Th1" style="width: 3%">
                                #
                            </th>
                            <th id="Th6" style="width: 7%; text-align: center">
                                <%= Resources.resDiccionario.Acciones%>
                            </th>
                            <th id="Th4" style="width: 15%">
                                <%= Resources.resDiccionario.Parametro %>
                            </th>
                            <th id="Th8" style="width: 15%">
                                <%= Resources.resDiccionario.Compania %>
                            </th>
                            <th id="Th3" style="width: 35%">
                                <%= Resources.resDiccionario.Descripcion %>
                            </th>
                             <th id="Th5" style="width: 6%">
                                <%= Resources.resDiccionario.TipoDato %>
                            </th>
                            <th id="Th2" style="width: 10%">
                                <%= Resources.resDiccionario.Tipo %>
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
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Fila") %>' />
                    </td>
                    <td id="Td6" runat="server" style="text-align: center">
                        <asp:HiddenField ID="hfIdParametro" runat="server" Value='<%# Eval("IdParametro") %>' />
                        <asp:ImageButton ID="ibtEditar" runat="server" CommandName="<%$ Resources:resDiccionario, Editar %>" CommandArgument='<%# Eval("IdParametro").ToString()%>' ToolTip="<%$ Resources:resDiccionario, Editar %>" ImageUrl="~/img/ico_edit.png" BorderWidth="0" />
                        <asp:ImageButton ID="ibtEliminar" runat="server" CommandName="<%$ Resources:resDiccionario, Eliminar %>" CommandArgument='<%# Eval("IdParametro").ToString()%>' ToolTip="<%$ Resources:resDiccionario, Estado %>" ImageUrl="~/img/ico_active.gif" BorderWidth="0" />
                    </td>
                    <td id="Td4" runat="server">
                        <asp:Label ID="lblParametro" runat="server" Text='<%# Eval("Parametro")%>' />
                    </td>
                    <td id="Td5" runat="server">
                        <asp:Label ID="lblIdCompania" runat="server" Text='<%# Eval("DescripcionCompania") %>' />
                    </td>
                    <td id="Td3" runat="server">
                        <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("Descripcion") %>' />
                    </td>
                    <td id="Td2" runat="server">
                        <%--<asp:Label ID="lblTipoDato" runat="server" Text='<%# Eval("TipoDato") %>' />--%>
                        <asp:Label ID="lblTipoDato" runat="server" Text='<%# (Eval("TipoDato").ToString()=="T"? Resources.resDiccionario.Texto : Eval("TipoDato").ToString()=="N"? Resources.resDiccionario.NumeroData : Resources.resDiccionario.Fecha) %>' />
                    </td>
                    <td id="Td8" runat="server">
                        <asp:Label ID="lblTipo" runat="server" Text='<%# Eval("DescripcionTipo") %>' />
                    </td>
                    <td id="Td7" runat="server" style="text-align: center">
                        <asp:Label ID="lblEstado" runat="server" Text='<%# (Eval("Estado").ToString()=="A"? Resources.resDiccionario.Activo : Resources.resDiccionario.Inactivo) %>' />
                        
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
                            <th style="width: 7%">
                                <%= Resources.resDiccionario.Acciones %>
                            </th>
                            <th style="width: 15%">
                                <%= Resources.resDiccionario.Parametro %>
                            </th>
                            <th style="width: 5%">
                                <%= Resources.resDiccionario.Compania %>
                            </th>
                            <th style="width: 35%">
                                <%= Resources.resDiccionario.Descripcion %>
                            </th>
                            <th style="width: 6%">
                                <%= Resources.resDiccionario.TipoDato %>
                            </th>
                            <th style="width: 10%">
                                <%= Resources.resDiccionario.Tipo %>
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
