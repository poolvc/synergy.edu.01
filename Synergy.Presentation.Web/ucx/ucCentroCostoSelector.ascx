<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCentroCostoSelector.ascx.cs"
    Inherits="ucCentroCostoSelector" %>
<%@ Register Src="~/ucx/ucPaginador.ascx" TagName="ucPaginador" TagPrefix="uc1" %>
<script language="javascript" type="text/javascript">
    function fnAceptar() {

    }
    function fnCancelar() {

    }

    var vfilaAnterior = null;
    function fnSelecionarFila(pFila, pIdCentroCosto, pCodigoCentroCosto, pDescripcion) {
        if (vfilaAnterior != null && pFila != vfilaAnterior) {
            vfilaAnterior.style.backgroundColor = 'white';
        }
        vfilaAnterior = pFila;
        pFila.style.backgroundColor = '#E5E5E5';
        this.document.getElementById('<%= hfIdCentroCosto.ClientID %>').value = pIdCentroCosto;
        this.document.getElementById('<%= hfCodigoCentroCosto.ClientID %>').value = pCodigoCentroCosto;
        this.document.getElementById('<%= hfDescripcion.ClientID %>').value = pDescripcion;
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
                        <asp:Label ID="lblCodigoCentroCosto" runat="server" Text="<%$ Resources:resDiccionario, Codigo %>"></asp:Label>:
                    </td>
                    <td style="width: 5%">
                        <asp:TextBox ID="txtCodigoCentroCosto" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%">
                        <asp:Label ID="lblDescripcion" runat="server" Text="<%$ Resources:resDiccionario, Descripcion %>"></asp:Label>:
                    </td>
                    <td style="width: 5%">
                        <asp:TextBox ID="txtDescripcion" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%">
                        <asp:Label ID="lblEstado" runat="server" Text="<%$ Resources:resDiccionario, Estado %>"></asp:Label>
                    </td>
                    <td style="width: 5%">
                        <asp:DropDownList ID="ddlEstado" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 2%">
                        <asp:ImageButton ID="imgBuscarCentroCosto" runat="server" ImageUrl="~/img/ico_buscar4.gif" OnClick="imgBuscarCentroCosto_Click"   CausesValidation="false"   Style="height: 16px"  />
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
                                <th id="Th3" style="width: 10%">
                                    <%= Resources.resDiccionario.Codigo %>
                                </th>
                                <th id="Th9" style="width: 20%;">
                                    <%= Resources.resDiccionario.CentroCosto %>
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
                             <asp:HiddenField ID="hfItemIdCentroCosto" runat="server" Value='<%# Eval("IdCentroCosto")%>' />
                             <asp:HiddenField ID="hfItemCodigoCentroCosto" runat="server" Value='<%# Eval("CodigoCentroCosto")%>' />
                            <asp:HiddenField ID="hfItemDescripcion" runat="server" Value='<%# Eval("Descripcion")%>' />
                        </td>
                        <td id="Td9" runat="server" style="text-align: center">
                            <asp:Label ID="lblCentroCosto" runat="server" Text='<%# Eval("IdCentroCosto") %>' />
                        </td>
                        <td id="Td3" runat="server">
                            <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("Descripcion") %>' />
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
        <asp:HiddenField ID="hfIdCentroCosto" runat="server" />
        <asp:HiddenField ID="hfCodigoCentroCosto" runat="server" />
        <asp:HiddenField ID="hfDescripcion" runat="server" />

        &nbsp;
    </div>
</div>

