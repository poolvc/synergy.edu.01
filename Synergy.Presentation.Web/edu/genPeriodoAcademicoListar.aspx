<%@ Page Title="" Language="C#" MasterPageFile="~/mas/masMantListar.master" AutoEventWireup="true" CodeFile="genPeriodoAcademicoListar.aspx.cs"
    Inherits="genPeriodoAcademicoListar" %>

<%@ MasterType TypeName="masMantListar" %>
<%@ Register Src="~/ucx/ucPaginador.ascx" TagName="ucPaginador" TagPrefix="uc1" %>
<%@ Register Src="~/ucx/ucFamiliaSelector.ascx" TagPrefix="uc1" TagName="ucFamiliaSelector" %>
<%@ Register Src="~/ucx/ucAlumnoSelector.ascx" TagPrefix="uc1" TagName="ucAlumnoSelector" %>
<%@ Register Src="~/ucx/ucFichaAtributos.ascx" TagPrefix="uc1" TagName="ucFichaAtributos" %>



<asp:Content ID="Content1" ContentPlaceHolderID="cphArea" runat="Server">

   
    <script type="text/javascript">

        function AbrirSelectorFamilia(divname) {
            var inPaginas = 1;
            $('#' + divname).dialog({
                    autoOpen: false,
                    modal: true,
                    bgiframe: true,
                    title: '<%= Resources.resDiccionario.Familias %>',
                    width: 800,
                    height: 450,
                    buttons:
                        {
                            "Aceptar": function () {
                                document.getElementById('<%= txtFamilia.ClientID %>').value = document.getElementById("hdCodigoSelectorFamilia").value;
                                $(this).dialog("close");
                            },
                            "Cancelar": function () {
                                $(this).dialog("close");
                                //cancel
                            }
                        }
                     });
                    $('#' + divname).dialog('open');
                    $('#' + divname).parent().appendTo($("form:first"));
        }

        function AbrirSelectorAlumno(divname) {
            var inPaginas = 1;
            $('#' + divname).dialog({
                autoOpen: false,
                modal: true,
                bgiframe: true,
                title: '<%= Resources.resDiccionario.Alumnos %>',
                    width: 800,
                    height: 450,
                    buttons:
                        {
                            "Aceptar": function () {
                                document.getElementById('<%= txtFamilia.ClientID %>').value = document.getElementById("hdCodigoSelectorAlumno").value;
                                $(this).dialog("close");
                            },
                            "Cancelar": function () {
                                $(this).dialog("close");
                                //cancel
                            }
                        }
            });
                    $('#' + divname).dialog('open');
                    $('#' + divname).parent().appendTo($("form:first"));
        }

        function AbrirVisorFicha(divname, psPeriodoAcademico, psCodigo, psVinculo, psTipo) {
            ObtenerFichaAtributos(psPeriodoAcademico, psCodigo, psVinculo, psTipo)
           
           
            $('#' + divname).dialog({
                autoOpen: false,
                modal: true,
                bgiframe: true,
                title: '<%= Resources.resDiccionario.Ficha %>',
                width: 500,
                height: 450,
                buttons:
                    {
                        "Aceptar":
                            {
                                text: 'Aceptar',
                                class: "ui-button",
                                click: function () {
                                    $(this).dialog("close");
                                }
                            }
                    }
            });
                    $('#' + divname).dialog('open');
                    $('#' + divname).parent().appendTo($("form:first"));
                }

    </script>




    <div id="filtros">

        <table class="filtro_bar">
            <tr>
                <td style="width: 3%; text-align: right">
                    <asp:Label ID="lblEstado" runat="server" Text="<%$ Resources:resDiccionario, Estado %>"></asp:Label>:
                </td>
                <td style="width: 8%">
                    <asp:DropDownList ID="ddlEstado" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="txtFamilia" runat="server"></asp:TextBox>
                     <input  type="button" id="ibFamilias" onclick="javascript: return AbrirSelectorFamilia('selectorConteinerFamilia');"/>
                     <input  type="button" id="ibAlumnos" onclick="javascript: return AbrirSelectorAlumno('selectorConteinerAlumno');"/>
                </td>
            </tr>
            <tr>
                <td style="width: 3%; text-align: right">
                    &nbsp;</td>
                <td style="width: 8%">
                     <input  type="button" id="ibFichaAlumno" onclick="javascript: return AbrirVisorFicha('visorConteinerFicha', '2012-2013', '0411601', '', 'A');"/>
                     <input  type="button" id="ibFichaFamilia" onclick="javascript: return AbrirVisorFicha('visorConteinerFicha', '2012-2013', '04430', 'Mother','F');"/>
                     <input  type="button" id="ibFichaEmpleado" onclick="javascript: return AbrirVisorFicha('visorConteinerFicha', '2012-2013', '480', '', 'E');"/>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </div>
    <div id="lista">
        <asp:ListView runat="server" ID="lvLista" OnItemCommand="lvLista_ItemCommand"
            OnItemDataBound="lvLista_ItemDataBound">
            <LayoutTemplate>
                <table runat="server" id="lista" class="lista">

                    <tr>
                        <th id="Th1" style="width: 3%">#
                        </th>
                        <th id="Th6" style="width: 7%; text-align: center">
                            <%= Resources.resDiccionario.Acciones %>
                        </th>
                        <th id="Th8" style="width: 20%">
                            <%= Resources.resDiccionario.PeriodoAcademico %>
                        </th>
                        <th id="Th7dd" style="width: 60%">
                            <%= Resources.resDiccionario.Descripcion %>
                        </th>
                        <th id="Th7hf" style="width: 10%">
                            <%= Resources.resDiccionario.Estado %>
                        </th>
                    </tr>


                    <tr runat="server" id="itemPlaceholder">
                    </tr>

                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr id="Tr1" runat="server">
                    <td id="Td1" runat="server"></td>
                    <td id="Td6" runat="server" style="text-align: center">
                        <asp:ImageButton ID="ibtEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("PeriodoAcademico").ToString() %>' ToolTip="<%$ Resources:resDiccionario, Editar %>" ImageUrl="~/img/ico_edit.png" BorderWidth="0" />
                        <asp:ImageButton ID="ibtEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("PeriodoAcademico").ToString() %>' ToolTip="<%$ Resources:resDiccionario, Estado %>" ImageUrl="~/img/ico_active.gif" BorderWidth="0" />
                    </td>
                    <td id="Td8" runat="server">
                        <asp:Label ID="lblPeriodoAnual" runat="server" Text='<%# Eval("PeriodoAcademico") %>' />
                    </td>
                    <td id="Td2" runat="server">
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Descripcion") %>' />
                    </td>
                    <td id="Td7" runat="server"></td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table runat="server" id="lista" class="lista">

                    <tr>
                        <th id="Th1" style="width: 3%">#
                        </th>
                        <th id="Thw6" style="width: 7%; text-align: center">
                            <%= Resources.resDiccionario.Acciones %>
                        </th>
                        <th id="Thw8" style="width: 20%">
                            <%= Resources.resDiccionario.PeriodoAcademico %>
                        </th>
                        <th id="Thw7" style="width: 60%">
                            <%= Resources.resDiccionario.Descripcion %>
                        </th>
                        <th id="Th17" style="width: 10%">
                            <%= Resources.resDiccionario.Estado %>
                        </th>
                    </tr>

                    <tr>
                        <td colspan="7">
                            <%= Resources.resDiccionario.NoHayRegistros %>
                        </td>
                    </tr>

                </table>
            </EmptyDataTemplate>
        </asp:ListView>
    </div>
    <div style="text-align: right">
        <div>
            <uc1:ucPaginador ID="ucpagLista" runat="server" OnCambioPagina="ucpagLista_CambioPagina" />
        </div>
    </div>

    <div id="selectorConteinerFamilia" style="display: none; font-size: 90%;">
        <uc1:ucFamiliaSelector runat="server" ID="ucFamiliaSelector" />
    </div>
    
     <div id="selectorConteinerAlumno" style="display: none; font-size: 90%;">
            <uc1:ucAlumnoSelector runat="server" ID="ucAlumnoSelector" />
     </div>
     <div id="visorConteinerFicha" style="display: none; font-size: 90%;">
           <uc1:ucFichaAtributos runat="server" id="ucFichaAtributos" />
     </div>
    <div id="loadingScreen"> </div>
</asp:Content>
