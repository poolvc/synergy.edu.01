<%@ Page Title="" Language="C#" MasterPageFile="~/mas/masMantEditar.master" AutoEventWireup="true"
    CodeFile="genParametroEditar.aspx.cs" Inherits="genParametroEditar" %>

<%@ MasterType TypeName="masMantEditar" %>
<%@ Register Src="../ucx/ucPaginador.ascx" TagName="ucPaginador" TagPrefix="uc1" %>
<%@ Import Namespace="Synergy.Presentation.Web" %>
<%@ Import Namespace="Synergy.Infraestructure.CrossCutting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphArea" runat="Server">
    <script language="javascript" type="text/javascript">

        function fnValidar() {
            if (!fnValidarGeneral())
                return false;
            return true; 
        }

        function fnValidarGeneral() {

            var txtDescripcion = this.document.getElementById('<%= txtDescripcion.ClientID %>')
            if (txtDescripcion.value == "") {
                alert('<%=Resources.resMensaje.msgAlertaIngresarDescripcion %>');
                txtDescripcion.focus();
                return false;
            }
            return true;
        }


    </script>
    <script type="text/javascript">
        $(function () {
            var vFormatoFechaJQuery = '<%= AppSettings.FormatoFechaJQuery %>';
            $('#' + '<%= txtFecha.ClientID %>').datepicker({ dateFormat: vFormatoFechaJQuery });
            $('.FormatoFecha').datepicker({ dateFormat: vFormatoFechaJQuery });
            $("#tabFicha").tabs();
        });
    </script>
    <table width="100%">
        <tr>
            <td>
                <div id="edicion">
                    <div id="tabFicha" style="width: 100%">
                        <ul>
                            <li id="liGeneral" runat="server"><a id="lnkGeneral" runat="server" href="#tabGeneral">
                                <asp:Label ID="lblGeneral" runat="server" Text="<%$ Resources:resDiccionario, General %>"></asp:Label></a>
                            </li>
                            <li id="liDetalle" runat="server"><a id="lnkDetalle" runat="server" href="#tabDetalle">
                                <asp:Label ID="lblDetalle" runat="server" Text="<%$ Resources:resDiccionario, Detalle %>"></asp:Label></a>
                            </li>
                        </ul>
                        <div id="tabGeneral" runat="server">
                            <table class="forma">
                                <colgroup>
                                    <col width="15%" />
                                    <col width="13%" />
                                    <col width="15%" />
                                    <col width="20%" />
                                    <col />
                                </colgroup>
                                <tr class="forma_grupo">
                                    <td colspan="5">
                                        <%= Resources.resDiccionario.DatosGenerales %>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblId" runat="server" Text="<%$ Resources:resDiccionario,Id%>"></asp:Label>:
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtIdParametro" runat="server" MaxLength="10" Width="100px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right">
                                        &nbsp;<asp:Label ID="lblIdAplicacion0" runat="server" Text="<%$ Resources:resDiccionario,Compania %>"></asp:Label>:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlIdCompania" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblCodigo" runat="server" Text="<%$ Resources:resDiccionario,Codigo %>"></asp:Label>:
                                    </td>
                                    <td colspan="1">
                                        <asp:TextBox ID="txtCodigo" runat="server" MaxLength="100" Width="200px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right">
                                        &nbsp;
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblIdAplicacion" runat="server" Text="<%$ Resources:resDiccionario,Aplicacion %>"></asp:Label>:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlIdAplicacion" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblDescripcion" runat="server" Text="<%$ Resources:resDiccionario,Descripcion %>"></asp:Label>:
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtDescripcion" runat="server" MaxLength="100" Width="380px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblTipo" runat="server" Text="<%$ Resources:resDiccionario,Tipo%>"></asp:Label>:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTipo" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblTipoDato" runat="server" Text="<%$ Resources:resDiccionario,TipoDato%>"></asp:Label>:
                                    </td>
                                    <td colspan="1">
                                        <asp:DropDownList ID="ddlTipoDato" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoDato_SelectedIndexChanged">
                                            <asp:ListItem Text="<%$Resources:resDiccionario,Texto %>" Value="T"></asp:ListItem>
                                            <asp:ListItem Text="<%$Resources:resDiccionario,NumeroData %>" Value="N"></asp:ListItem>
                                            <asp:ListItem Text="<%$Resources:resDiccionario,Fecha %>" Value="F"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: left">
                                    <%= Resources.resDiccionario.Valor %>:
                                        <asp:TextBox ID="txtTexto" runat="server" MaxLength="100" Style="text-align: left"
                                            Width="100px"></asp:TextBox>
                                        <asp:TextBox ID="txtFecha" runat="server" MaxLength="100" Style="text-align: center"
                                            Width="100px"></asp:TextBox>
                                        <asp:TextBox ID="txtNumero" runat="server" MaxLength="100" Style="text-align: right"
                                            Width="100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <%= Resources.resDiccionario.Definicion %>:
                                    </td>
                                    <td colspan="4">
                                        <asp:TextBox ID="txtDefinicion" runat="server" MaxLength="255" Width="97%" TextMode="MultiLine"
                                            Height="50px" Rows="4"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="forma_grupo">
                                    <td colspan="4">
                                        <%= Resources.resDiccionario.DatosAuditoria %>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <asp:Label ID="lblEstado" runat="server" Text="<%$ Resources:resDiccionario, Estado %>"></asp:Label>:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlEstado" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <asp:Label ID="lblUsuarioCreacion" runat="server" Text="<%$ Resources:resDiccionario,UsuarioCreacion %>"></asp:Label>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtUsuarioCreacion" runat="server" BackColor="#EAF2F5" Enabled="False"
                                            Width="100px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="lblFechaCreacion" runat="server" Text="<%$ Resources:resDiccionario,FechaCreacion %>"></asp:Label>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFechaCreacion" runat="server" BackColor="#EAF2F5" Enabled="False"
                                            Width="125px"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="LblUsuarioModificacion" runat="server" Text="<%$ Resources:resDiccionario,UsuarioModificacion %>"></asp:Label>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtUsuarioModificacion" runat="server" BackColor="#EAF2F5" Enabled="False"
                                            Width="100px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="LblFechaModificacion" runat="server" Text="<%$ Resources:resDiccionario,FechaModificacion %>"></asp:Label>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFechaModificacion" runat="server" BackColor="#EAF2F5" Enabled="False"
                                            Width="125px"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="tabDetalle" runat="server">
                            <asp:UpdatePanel ID="upDetalle" runat="server">
                                <ContentTemplate>
                                    <div class="contents">
                                        <table class="forma" width="100%">
                                            <colgroup width="100%">
                                                <col width="100%" style="text-align: right" />
                                            </colgroup>
                                            <tr>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblNuevoDetalle" runat="server" Text="<%$ Resources:resDiccionario,Nuevo %>"></asp:Label>
                                                    <asp:ImageButton ID="ibtNuevoParametroDetalle" runat="server" BorderWidth="0" CommandName="Nuevo"
                                                        ImageUrl="~/img/ico_add2.png" OnClick="ibtNuevoParametroDetalle_Click" Style="height: 16px;"
                                                        ToolTip="<%$ Resources:resDiccionario,Nuevo %>" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div id="lista" style="width:100%">
                                                        <asp:ListView runat="server" ID="lvParametroDetalle" OnItemCommand="lvParametroDetalle_ItemCommand"
                                                            OnItemDataBound="lvParametroDetalle_ItemDataBound">
                                                            <LayoutTemplate>
                                                                <table runat="server" id="lista" class="lista">
                                                                    <thead>
                                                                        <tr>
                                                                            <th id="Th1" style="width: 1%">
                                                                                #
                                                                            </th>
                                                                            <th id="Th6" style="width: 7%">
                                                                                <%= Resources.resDiccionario.Acciones%>
                                                                            </th>
                                                                            <th id="Th2" style="width: 6%">
                                                                                <%= Resources.resDiccionario.Codigo%>
                                                                            </th>
                                                                            <th id="Th3" style="width: 10%">
                                                                                <%= Resources.resDiccionario.Descripcion%>
                                                                            </th>
                                                                            <th id="Th5" style="text-align: center; width: 5%">
                                                                                <%= Resources.resDiccionario.ValorTexto1%>
                                                                            </th>
                                                                            <th id="Th7" style="text-align: center; width: 5%">
                                                                                <%= Resources.resDiccionario.ValorTexto2%>
                                                                            </th>
                                                                            <th id="Th8" style="text-align: center; width: 5%">
                                                                                <%= Resources.resDiccionario.ValorTexto3%>
                                                                            </th>
                                                                            <th id="Th9" style="text-align: center; width: 5%">
                                                                                <%= Resources.resDiccionario.ValorTexto4%>
                                                                            </th>
                                                                            <th id="Th10" style="text-align: center; width: 5%">
                                                                                <%= Resources.resDiccionario.ValorTexto5%>
                                                                            </th>
                                                                            <th id="Th11" style="text-align: center; width: 5%">
                                                                                <%= Resources.resDiccionario.ValorTexto6%>
                                                                            </th>
                                                                            <th id="Th12" style="text-align: center; width: 5%">
                                                                                <%= Resources.resDiccionario.ValorNumero%>
                                                                            </th>
                                                                            <th id="Th13" style="text-align: center; width: 5%">
                                                                                <%= Resources.resDiccionario.ValorFecha%>
                                                                            </th>
                                                                            <th id="Th14" style="width: 10%">
                                                                                <%= Resources.resDiccionario.Explicacion%>
                                                                            </th>
                                                                            <th id="Th4" style="width: 5%">
                                                                                <%= Resources.resDiccionario.Estado%>
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
                                                                        <%# Container.DataItemIndex + 1 %>
                                                                        <asp:HiddenField ID="hfDetalleItemIdParametroDetalle" runat="server" Value='<%# Eval("IdParametro") %>' />
                                                                    </td>
                                                                    <td id="Td6" runat="server" style="text-align: center">
                                                                        <asp:ImageButton ID="ibtEditarItemParametroDetalle" runat="server" CommandName="EDITAR"
                                                                            CommandArgument='<%# Eval("ParametroDetalle").ToString() %>' ToolTip="<%$ Resources:resDiccionario, Editar %>"
                                                                            ImageUrl="~/img/ico_edit.png" BorderWidth="0" />
                                                                        <asp:ImageButton ID="ibtEliminarItemParametroDetalle" runat="server" CommandName="ELIMINAR"
                                                                            CommandArgument='<%# Eval("ParametroDetalle").ToString() %>' ToolTip="<%$ Resources:resDiccionario, Eliminar %>"
                                                                            ImageUrl="~/img/ico_trash.png" BorderWidth="0" />
                                                                    </td>
                                                                    <td id="Td2" runat="server">
                                                                        <asp:Label ID="lblDetalleItemParametroDetalle" runat="server" Text='<%# Eval("ParametroDetalle") %>'></asp:Label>
                                                                    </td>
                                                                    <td id="Td3" runat="server">
                                                                        <asp:Label ID="lblDetalleItemDescripcion" runat="server" Text='<%# Eval("Descripcion") %>'></asp:Label>
                                                                    </td>
                                                                    <td id="Td12" runat="server">
                                                                        <asp:Label ID="lblDetalleItemValorTexto1" runat="server" Text='<%# Eval("ValorTexto1") %>'></asp:Label>
                                                                    </td>
                                                                    <td id="Td13" runat="server">
                                                                        <asp:Label ID="lblDetalleItemValorTexto2" runat="server" Text='<%# Eval("ValorTexto2") %>'></asp:Label>
                                                                    </td>
                                                                    <td id="Td18" runat="server">
                                                                        <asp:Label ID="lblDetalleItemValorTexto3" runat="server" Text='<%# Eval("ValorTexto3") %>'></asp:Label>
                                                                    </td>
                                                                    <td id="Td19" runat="server">
                                                                        <asp:Label ID="lblDetalleItemValorTexto4" runat="server" Text='<%# Eval("ValorTexto4") %>'></asp:Label>
                                                                    </td>
                                                                    <td id="Td20" runat="server">
                                                                        <asp:Label ID="lblDetalleItemValorTexto5" runat="server" Text='<%# Eval("ValorTexto5") %>'></asp:Label>
                                                                    </td>
                                                                    <td id="Td21" runat="server">
                                                                        <asp:Label ID="lblDetalleItemValorTexto6" runat="server" Text='<%# Eval("ValorTexto6") %>'></asp:Label>
                                                                    </td>
                                                                    <td id="Td22" runat="server" style="text-align: right">
                                                                        <asp:Label ID="lblDetalleItemValorNumero" runat="server" Style="text-align: right"
                                                                            Text='<%# Eval("ValorNumero","{0:F2}") %>'></asp:Label>
                                                                    </td>
                                                                    <td id="Td23" runat="server">
                                                                        <asp:Label ID="lblDetalleItemValorFecha" runat="server" Text='<%# Eval("ValorFecha", "{0:dd/MM/yyy}").ToString() %>'></asp:Label>
                                                                    </td>
                                                                    <td id="Td24" runat="server">
                                                                        <asp:Label ID="lblDetalleItemExplicacion" runat="server" Text='<%# Eval("Explicacion") %>'></asp:Label>
                                                                    </td>
                                                                    <td id="Td4" runat="server">
                                                                        <asp:Label ID="lblDetalleItemEstado" runat="server" Text='<%# (Eval("Estado").ToString()=="A" ? Resources.resDiccionario.Activo : Resources.resDiccionario.Inactivo) %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <tr id="Tr1" runat="server">
                                                                    <td id="Td1" runat="server">
                                                                        <%# Container.DataItemIndex + 1 %>
                                                                        <asp:HiddenField ID="hfDetalleEditIdParametroDetalle" runat="server" Value='<%# Eval("IdParametro") %>' />
                                                                        <asp:HiddenField ID="hfDetalleEditGui" runat="server" Value='<%# Eval("RefGuid") %>' />
                                                                    </td>
                                                                    <td id="Td6" runat="server" style="text-align: center">
                                                                        <asp:ImageButton ID="ibtDetalleEditGrabar" runat="server" CommandName="GRABAR" CommandArgument='<%# Eval("Fila").ToString() %>'
                                                                            ToolTip="<%$ Resources: resDiccionario, Guardar %>" ImageUrl="~/img/ico_save2.png"
                                                                            BorderWidth="0" />
                                                                        <asp:ImageButton ID="ibtDetalleEditCancelar" runat="server" CommandName="CANCELAR"
                                                                            CommandArgument='<%# Eval("IdParametro").ToString() %>' ToolTip="<%$ Resources: resDiccionario, Cancelar %>"
                                                                            ImageUrl="~/img/ico_back2.gif" BorderWidth="0" />
                                                                    </td>
                                                                    <td id="Td2" runat="server">
                                                                        <asp:TextBox ID="txtDetalleEditParametroDetalle" runat="server" Text='<%# Eval("ParametroDetalle") %>'
                                                                            Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td3" runat="server">
                                                                        <asp:TextBox ID="txtDetalleEditDescripcion" runat="server" Text='<%# Eval("Descripcion") %>'
                                                                            Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td14" runat="server">
                                                                        <asp:TextBox ID="txtDetalleEditValorTexto1" runat="server" Text='<%# Eval("ValorTexto1") %>'
                                                                            Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td15" runat="server">
                                                                        <asp:TextBox ID="txtDetalleEditValorTexto2" runat="server" Text='<%# Eval("ValorTexto2") %>'
                                                                            Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td25" runat="server">
                                                                        <asp:TextBox ID="txtDetalleEditValorTexto3" runat="server" Text='<%# Eval("ValorTexto3") %>'
                                                                            Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td26" runat="server">
                                                                        <asp:TextBox ID="txtDetalleEditValorTexto4" runat="server" Text='<%# Eval("ValorTexto4") %>'
                                                                            Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td27" runat="server">
                                                                        <asp:TextBox ID="txtDetalleEditValorTexto5" runat="server" Text='<%# Eval("ValorTexto5") %>'
                                                                            Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td28" runat="server">
                                                                        <asp:TextBox ID="txtDetalleEditValorTexto6" runat="server" Text='<%# Eval("ValorTexto6") %>'
                                                                            Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td29" runat="server">
                                                                        <asp:TextBox ID="txtDetalleEditValorNumero" runat="server" Style="text-align: right"
                                                                            Text='<%# Eval("ValorNumero","{0:F2}") %>' Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td30" runat="server">
                                                                        <asp:TextBox ID="txtDetalleEditValorFecha" runat="server" Text='<%# Eval("ValorFecha", "{0:dd/MM/yyy}").ToString() %>'
                                                                            Width="100%" CssClass="FormatoFecha"></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td31" runat="server">
                                                                        <asp:TextBox ID="txtDetalleEditValorExplicacion" runat="server" Text='<%# Eval("Explicacion") %>'
                                                                            Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td5" runat="server">
                                                                        <asp:DropDownList ID="ddlDetalleEditEstado" runat="server" Width="100%">
                                                                            <asp:ListItem Value="A">Activo</asp:ListItem>
                                                                            <asp:ListItem Value="I">Inactivo</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </EditItemTemplate>
                                                            <InsertItemTemplate>
                                                                <tr id="Tr2" runat="server">
                                                                    <td id="Td8" runat="server">
                                                                        <asp:HiddenField ID="hfDetalleInsertSecuencia" runat="server" Value='<%# Eval("Secuencia") %>' />
                                                                        <asp:HiddenField ID="hfDetalleInsertGui" runat="server" Value='<%# Eval("RefGuid") %>' />
                                                                    </td>
                                                                    <td id="Td9" runat="server" style="text-align: center">
                                                                        <asp:ImageButton ID="ibtDetalleInsertGrabar" runat="server" CommandName="GRABARNUEVO"
                                                                            CommandArgument="0" ToolTip="<%$ Resources:resDiccionario, Grabar %>" ImageUrl="~/img/ico_save2.png"
                                                                            BorderWidth="0" />
                                                                        <asp:ImageButton ID="ibtDetalleInsertCancelar" runat="server" CommandName="CANCELAR"
                                                                            CommandArgument="0" ToolTip="<%$ Resources:resDiccionario, Cancelar %>" ImageUrl="~/img/ico_back2.gif"
                                                                            BorderWidth="0" />
                                                                    </td>
                                                                    <td id="Td10" runat="server">
                                                                        <asp:TextBox ID="txtDetalleInsertParametroDetalle" runat="server" Text="" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td11" runat="server">
                                                                        <asp:TextBox ID="txtDetalleInsertDescripcion" runat="server" Text="" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td16" runat="server">
                                                                        <asp:TextBox ID="txtDetalleInsertValorTexto1" runat="server" Text="" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td17" runat="server">
                                                                        <asp:TextBox ID="txtDetalleInsertValorTexto2" runat="server" Text="" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td32" runat="server">
                                                                        <asp:TextBox ID="txtDetalleInsertValorTexto3" runat="server" Text="" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td33" runat="server">
                                                                        <asp:TextBox ID="txtDetalleInsertValorTexto4" runat="server" Text="" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td34" runat="server">
                                                                        <asp:TextBox ID="txtDetalleInsertValorTexto5" runat="server" Text="" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td35" runat="server">
                                                                        <asp:TextBox ID="txtDetalleInsertValorTexto6" runat="server" Text="" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td36" runat="server">
                                                                        <asp:TextBox ID="txtDetalleInsertValorNumero" runat="server" Style="text-align: right"
                                                                            Text="" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td37" runat="server">
                                                                        <asp:TextBox ID="txtDetalleInsertValorFecha" runat="server" Text="" Width="100%"
                                                                            CssClass="FormatoFecha"></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td38" runat="server">
                                                                        <asp:TextBox ID="txtDetalleInsertExplicacion" runat="server" Text="" Width="100%"></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td7" runat="server">
                                                                        <asp:DropDownList ID="ddlDetalleInsertEstado" runat="server" Width="100%">
                                                                            <asp:ListItem Value="A" Text="<%$ Resources:resDiccionario, Activo %>"></asp:ListItem>
                                                                            <asp:ListItem Value="I" Text="<%$ Resources:resDiccionario, Inactivo %>"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </InsertItemTemplate>
                                                            <EmptyDataTemplate>
                                                                <table runat="server" id="lista" class="lista">
                                                                    <thead>
                                                                        <tr>
                                                                            <th style="width: 3%">
                                                                                #
                                                                            </th>
                                                                            <th style="width: 7%">
                                                                                <%= Resources.resDiccionario.Acciones%>
                                                                            </th>
                                                                            <th style="width: 6%">
                                                                                <%= Resources.resDiccionario.Codigo %>
                                                                            </th>
                                                                            <th style="width: 10%">
                                                                                <%= Resources.resDiccionario.Descripcion %>
                                                                            </th>
                                                                            <th style="width: 5%">
                                                                                <%= Resources.resDiccionario.ValorTexto1%>
                                                                            </th>
                                                                            <th style="width: 5%">
                                                                                <%= Resources.resDiccionario.ValorTexto2%>
                                                                            </th>
                                                                            <th style="width: 5%">
                                                                                <%= Resources.resDiccionario.ValorTexto3%>
                                                                            </th>
                                                                            <th style="width: 5%">
                                                                                <%= Resources.resDiccionario.ValorTexto4%>
                                                                            </th>
                                                                            <th style="width: 5%">
                                                                                <%= Resources.resDiccionario.ValorTexto5%>
                                                                            </th>
                                                                            <th style="width: 5%">
                                                                                <%= Resources.resDiccionario.ValorTexto6%>
                                                                            </th>
                                                                            <th style="width: 5%">
                                                                                <%= Resources.resDiccionario.ValorNumero%>
                                                                            </th>
                                                                            <th style="width: 5%">
                                                                                <%= Resources.resDiccionario.ValorFecha%>
                                                                            </th>
                                                                            <th style="width: 10%">
                                                                                <%= Resources.resDiccionario.Explicacion%>
                                                                            </th>
                                                                            <th style="width: 5%">
                                                                                <%= Resources.resDiccionario.Estado%>
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr>
                                                                            <td colspan="8">
                                                                                <%= Resources.resDiccionario.NoHayRegistros%>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </EmptyDataTemplate>
                                                        </asp:ListView>
                                                    </div>
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                        <div style="text-align: right">
                                            <div>
                                                <uc1:ucPaginador ID="ucpagListaParametroDetalle" runat="server" OnCambioPagina="ucpagListaParametroDetalle_CambioPagina" />
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hfIdParametro" runat="server" />
    <asp:HiddenField ID="hfIdAplicacion" runat="server" />
    <asp:HiddenField ID="hfAccion" runat="server" />
</asp:Content>
