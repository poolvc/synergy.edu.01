<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPersonaSelector.ascx.cs"
    Inherits="ucPersonaSelector" %>
<%@ Register Src="~/ucx/ucPaginador.ascx" TagName="ucPaginador" TagPrefix="uc1" %>
<script language="javascript" type="text/javascript">
    function fnAceptar() {

    }
    function fnCancelar() {

    }

    var vfilaAnterior = null;
    function fnSelecionarFila<%= this.ClientID %>(pFila, pIdPersona, pNombreBusqueda) {
        if (vfilaAnterior != null && pFila != vfilaAnterior) {
            vfilaAnterior.style.backgroundColor = 'white';
        }
        vfilaAnterior = pFila;
        pFila.style.backgroundColor = '#E5E5E5';
        this.document.getElementById('<%= hfIdPersona.ClientID %>').value = pIdPersona;
        this.document.getElementById('<%= hfNombreBusqueda.ClientID %>').value = pNombreBusqueda;
    }

</script>
<script language="javascript" type="text/javascript">
    $(document).ready(function () {
        $('input[name$="gnSeleccion"]').attr('name', 'gnSeleccion');

    });
</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="filtros">
            <table class="filtro_bar">
                <tr>
                    <td style="width: 5%">
                        <asp:Label ID="lblNombre" runat="server" Text="<%$ Resources:resDiccionario, Nombres %>"></asp:Label>:
                    </td>
                    <td style="width: 5%">
                        <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 2%">
                    </td>
                    <td style="width: 5%">
                        <asp:Label ID="lblEstado" runat="server" Text="<%$ Resources:resDiccionario, Estado %>"></asp:Label>
                    </td>
                    <td style="width: 5%">
                        <asp:DropDownList ID="ddlEstado" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 2%">
                        <asp:ImageButton ID="imgBuscar" runat="server" ImageUrl="~/img/ico_buscar4.gif" OnClick="imgBuscar_Click"
                            Style="height: 16px" CausesValidation="false" />
                    </td>
                    <td style="width: 3%; text-align: left">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div id="lista1">
            <asp:ListView runat="server" ID="lvLista" OnItemCommand="lvLista_ItemCommand" OnItemDataBound="lvLista_ItemDataBound">
                <LayoutTemplate>
                    <table runat="server" id="lista" class="lista">
                        <thead>
                            <tr>
                                <th id="Th1" style="width: 3%">
                                    #
                                </th>
                                 <th id="Th6" style="width: 5%">
                                    <%= Resources.resDiccionario.Id %>
                                </th>
                                <th id="Th3" style="width: 50%">
                                    <%= Resources.resDiccionario.Nombres %>
                                </th>
                                <th id="Th5" style="width: 3%; text-align: center">
                                    E
                                </th>
                                <th id="Th2" style="width: 3%; text-align: center">
                                    C
                                </th>
                                <th id="Th4" style="width: 3%; text-align: center">
                                    P
                                </th>
                                <th id="Th8" style="width: 3%; text-align: center">
                                    O
                                </th>
                                <th id="Th9" style="width: 5%;">
                                    <%= Resources.resDiccionario.Documento %>
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
                    <tr id="TrFila" runat="server">
                        <td id="Td1" runat="server">
                            <asp:Label ID="lblFila" runat="server" Text='<%# Eval("Fila") %>' />
                             <asp:HiddenField ID="hfItemIdPersona" runat="server" Value='<%# Eval("IdPersona")%>' />
                            <asp:HiddenField ID="hfItemNombreBusqueda" runat="server" Value='<%# Eval("NombreBusqueda")%>' />
                        </td>
                        <td id="Td6" runat="server">
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("IdPersona") %>' />
                        </td>
                        <td id="Td3" runat="server">
                            <asp:Label ID="lblNombreBusqueda" runat="server" Text='<%# Eval("NombreBusqueda") %>' />
                        </td>
                        <td id="Td5" runat="server" style="text-align: center">
                            <asp:CheckBox ID="cbxFlagEmpleado" runat="server" Enabled="false" Checked='<%# (Eval("EsEmpleado").ToString()=="S" ? true : false)  %>' />
                        </td>
                        <td id="Td2" runat="server" style="text-align: center">
                            <asp:CheckBox ID="cbxFlagCliente" runat="server" Enabled="false" Checked='<%# (Eval("EsCliente").ToString()=="S" ? true : false) %>' />
                        </td>
                        <td id="Td4" runat="server" style="text-align: center">
                            <asp:CheckBox ID="cbxFlagProveedor" runat="server" Enabled="false" Checked='<%# (Eval("EsProveedor").ToString()=="S" ? true : false) %>' />
                        </td>
                        <td id="Td8" runat="server" style="text-align: center">
                            <asp:CheckBox ID="cbxFlagOtro" runat="server" Enabled="false" Checked='<%# (Eval("EsOtro").ToString()=="S" ? true : false) %>' />
                        </td>
                        <td id="Td9" runat="server" style="text-align: center">
                            <asp:Label ID="lblDocumento" runat="server" Text='<%# Eval("Documento") %>' />
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
                                 <th style="width: 5%">
                                     <%= Resources.resDiccionario.Id %>
                                </th>
                                <th style="width: 50%">
                                     <%= Resources.resDiccionario.Nombres %>
                                </th>
                                <th style="width: 3%; text-align: center">
                                    E
                                </th>
                                <th style="width: 3%; text-align: center">
                                    C
                                </th>
                                <th style="width: 3%; text-align: center">
                                    P
                                </th>
                                <th style="width: 3%; text-align: center">
                                    O
                                </th>
                                <th style="width: 5%">
                                     <%= Resources.resDiccionario.Documento %>
                                </th>
                                <th>
                                     <%= Resources.resDiccionario.Estado %>
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td colspan="8">
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
    </ContentTemplate>
</asp:UpdatePanel>

<div style="text-align: right; width: 95%;">
    <div>
        <asp:HiddenField ID="hfIdPersona" runat="server" />
        <asp:HiddenField ID="hfNombreBusqueda" runat="server" />
        <asp:HiddenField ID="hfEsEmpleado" runat="server" Value="" />
        <asp:HiddenField ID="hfEsCliente" runat="server" Value=""/>
        <asp:HiddenField ID="hfEsProveedor" runat="server" Value=""/>
        <asp:HiddenField ID="hfEsOtros" runat="server" Value=""/>
        &nbsp;
    </div>
</div>

