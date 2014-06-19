<%@ Page Title="" Language="C#" MasterPageFile="~/mas/masMantListar.master" AutoEventWireup="true" CodeFile="genPeriodoAcademicoListar.aspx.cs"
    Inherits="genPeriodoAcademicoListar" %>

<%@ MasterType TypeName="masMantListar" %>
<%@ Register Src="~/ucx/ucPaginador.ascx" TagName="ucPaginador" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphArea" runat="Server">

    <!--link href="../Content/bootstrap.min.css" rel="stylesheet" /-->
    
    <script type="text/javascript" src="../Scripts/underscore-min.js"></script>
    <script type="text/javascript" src="../Scripts/base/modTemplate.js"></script>
    <script src="../Scripts/jquery.simplePagination.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {

            var compilado = _.template($('#listaFamiliaSel').html());
            console.log("Prueba 1");
            $.getJSON("../ws/WCServicio.svc/ListarFamiliaSelPorCampo",
                {
                    pstrColumna: "ApellidoPaterno",
                    pstrValor: "AB",
                    pinPagina:1
                },

                function (resultado) {
                    // Datos simulados que vendrían de una llamada a AJAX
                    console.log("Prueba 2");
                    // Ejecutamos la funcion compilado y le pasamos la colección
                    // que queremos que use y el HTML generado lo ponemos el DOM
                    // con jQuery
                    $('#selectorFamiliaSel').html(compilado(resultado));


                    $('#selectorFamiliaSelPag').pagination({
                        pages: 5,
                        cssStyle: 'compact-theme',
                        displayedPages: 3
                    });

                })
                .success(function () { })
                .error(function (result) { alert('Error ' + '\n[ Code ' + result.status + ' 004 ]'); })
                .complete(function () { });

        });




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
                    <img alt="Add" src="../img/ico_add.gif" style="width: 30px; height: 30px" /></td>
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

    <div id="selectorFamiliaSel">
     </div>
    <div id="selectorFamiliaSelPag"></div>

    <script type="text/template" id="listaFamiliaSel">
      <table class="lista">
        <tr>
           <th>Nombre</th>
           <th>Nombre</th>
           <th>Edad</th>
        </tr>
        <@ _.each(Familias, function(Familia) { @>
          <tr onclick="fnSelecionarFilaFamilia(this,'<@ print(Familia.AlumnoGrupo); @>');"" >
            <td><@ print(Familia.AlumnoGrupo); @></td>
            <td><@ print(Familia.ApellidoPaterno); @></td>
            <td><@ print(Familia.ApellidoMaterno); @></td>
          </tr>
        <@ }); @>
      </table>
    </script>    
  

    <link href="../Content/simplePagination.css" rel="stylesheet" />

</asp:Content>
