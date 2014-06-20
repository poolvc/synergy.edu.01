<%@ Page Title="" Language="C#" MasterPageFile="~/mas/masMantListar.master" AutoEventWireup="true" CodeFile="genPeriodoAcademicoListarAnterior.aspx.cs"
    Inherits="genPeriodoAcademicoListarAnterior" %>

<%@ MasterType TypeName="masMantListar" %>
<%@ Register Src="~/ucx/ucPaginador.ascx" TagName="ucPaginador" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphArea" runat="Server">

    <link href="../Content/simplePagination.css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/underscore-min.js"></script>
    <script type="text/javascript" src="../Scripts/base/modTemplate.js"></script>
    <script src="../Scripts/jquery.simplePagination.js"></script>

    <script type="text/javascript">

        function AbrirSelectorFamilia(divname) {
            var inPaginas = 1;
            $("#hdFamilia").val("");
            $('#' + divname).dialog({
                    autoOpen: false,
                    modal: true,
                    bgiframe: true,
                    title: '<%= Resources.resDiccionario.Seleccion %>',
                    width: 800,
                    height: 450,
                    buttons:
                        {
                            "Aceptar": function () {
                                document.getElementById('<%= txtFamilia.ClientID %>').value = document.getElementById("hdFamilia").value;
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
                     <input  type="button" id="ibFamilias" onclick="javascript: return AbrirSelectorFamilia('selectorConteiner');"/>
                </td>
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

    <div id="selectorConteiner" style="display: none; font-size: 90%;">
        <div id="selectorFamiliaFiltro">
            <table class="filtro_bar">
                <tr>
                    <td style="width: 5%">
                        <asp:Label ID="lblCodigoCentroCosto" runat="server" Text="<%$ Resources:resDiccionario, Codigo %>"></asp:Label>:
                    </td>
                    <td style="width: 5%">
                         <select id="selCampo">
                            <option value="ApellidoPaterno">Apellido Paterno</option>
                            <option value="ApellidoMaterno">Apellido Materno</option>
                            <option value="AlumnoGrupo">Codigo Familia</option>
                        </select>
                    </td>
                   
                    <td style="width: 5%">
                        <asp:Label ID="lblDescripcion" runat="server" Text="<%$ Resources:resDiccionario, Descripcion %>"></asp:Label>:
                    </td>
                    <td style="width: 5%">
                        <input type="text" id="inValor"/>
                    </td>
                   
                    <td>
                       
                        <img alt="Buscar" src="../img/ico_buscar4.gif" onclick="BuscarFamiliasSel();"  />
                    </td>
                </tr>
            </table>
        </div>
        <div id="selectorFamiliaSel">
         </div>
        <div id="selectorFamiliaSelPag" style="position:absolute; bottom:0;"></div>
        <input type="hidden" id="hdFamilia" />
    </div>
    <script type="text/template" id="listaFamiliaSel">

      <table class="lista">
        <tr>
           <th style="width: 5%">Fila</th> 
           <th style="width: 10%">Codigo</th>
           <th style="width: 30%">Apellido Paterno</th>
           <th style="width: 30%">Apellido Materno</th>
           <th>Estado</th>
        </tr>
       <@ if(Familias.length>0) { @>
             <@ _.each(Familias, function(Familia) { @>
                  <tr onclick="fnSelecionarFilaFamilia(this,'<@ print(Familia.AlumnoGrupo); @>');"" >
                    <td><@ print(Familia.Fila); @></td> 
                    <td><@ print(Familia.AlumnoGrupo); @></td>
                    <td><@ print(Familia.ApellidoPaterno); @></td>
                    <td><@ print(Familia.ApellidoMaterno); @></td>
                     <@ if(Familias.Estado = "S") { @>
                        <td>Activo</td>
                      <@ }else { @>
                        <td>Inactivo</td>
                      <@ } @>
                  </tr>
            <@ }); @>
        <@ }else { @>
             <tr>
               <td colspan="3"> <%= Resources.resDiccionario.NoHayRegistros %></td>
            </tr>
        <@ } @>

      </table>
    </script>    
  

    
    <div id="loadingScreen"> </div>
</asp:Content>
