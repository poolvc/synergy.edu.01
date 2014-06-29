<%@ Page Title="" Language="C#" MasterPageFile="~/mas/masReporteVista.master" AutoEventWireup="true"
    CodeFile="proTransferenciaFotochekListar.aspx.cs" Inherits="proTransferenciaFotochekListar" %>

<%@ MasterType TypeName="masReporteVista" %>
<%@ Register Src="~/ucx/ucPaginador.ascx" TagName="ucPaginador" TagPrefix="uc1" %>
<%@ Register Src="~/ucx/ucFamiliaSelector.ascx" TagPrefix="uc1" TagName="ucFamiliaSelector" %>
<%@ Register Src="~/ucx/ucAlumnoSelector.ascx" TagPrefix="uc1" TagName="ucAlumnoSelector" %>
<%@ Register Src="~/ucx/ucFichaAtributos.ascx" TagPrefix="uc1" TagName="ucFichaAtributos" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphArea" runat="Server">


    <script type="text/javascript">

        function AbrirSelectorFamilia(divname) {

            var inPaginas = 1;
            
            $('#imgBuscarSelectorFamilia').focus();
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
                                document.getElementById('<%= txtCodigoFamilia.ClientID %>').value = document.getElementById("hdCodigoSelectorFamilia").value.trim();
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
                                document.getElementById('<%= txtCodigoAlumno.ClientID %>').value = document.getElementById("hdCodigoSelectorAlumno").value.trim();
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

                function AbrirVisorFicha(divname, psPeriodoAcademico, psCodigo, psVinculo, psDocumentoIdentidad, psTipo) {
                    ObtenerFichaAtributos(psPeriodoAcademico, psCodigo, psVinculo, psDocumentoIdentidad, psTipo);


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

    <script type="text/javascript">

        function CargarTabs() {
            $(document).ready(function () {
                $('#tabFicha').tabs();

            });
        }

        function SeleccionarTodos() {

            var chk = document.getElementById('<%=  chkSelTodo.ClientID %>')
            var check = chk.checked;
            $(".chkCampo1 input[type='checkbox']").attr('checked', check);
        }

    function fnLimpiarSession(num) {
        document.getElementById('<%=  hfTabSel.ClientID %>').value = num;

	     var lblGrado = document.getElementById('<%=  lblGrado.ClientID %>');
	     var ddlGrado = document.getElementById('<%=  ddlGrado.ClientID %>');

	     var lblfamilia = document.getElementById('<%=  lblfamilia.ClientID %>');
	     var txtCodigoFamilia = document.getElementById('<%=  txtCodigoFamilia.ClientID %>');
	     var imgFamilia = document.getElementById('<%=  imgFamilia.ClientID %>');

	     var lblSeccion = document.getElementById('<%=  lblSeccion.ClientID %>');
	     var txtSeccion = document.getElementById('<%=  txtSeccion.ClientID %>');

	     var lblAlumno = document.getElementById('<%=  lblAlumno.ClientID %>');
	     var txtCodigoAlumno = document.getElementById('<%=  txtCodigoAlumno.ClientID %>');
	     var imgAlumno = document.getElementById('<%=  imgAlumno.ClientID %>');

	     if (num == '1') {
	         lblGrado.style.display = 'inline';
	         ddlGrado.style.display = 'inline';
	         lblfamilia.style.display = 'inline';
	         txtCodigoFamilia.style.display = 'inline';
	         imgFamilia.style.display = 'inline';
	         lblSeccion.style.display = 'inline';
	         txtSeccion.style.display = 'inline';
	         lblAlumno.style.display = 'inline';
	         txtCodigoAlumno.style.display = 'inline';
	         imgAlumno.style.display = 'inline';
	     }
	     else if (num == '2') {
	         lblGrado.style.display = 'none';
	         ddlGrado.style.display = 'none';
	         lblfamilia.style.display = 'inline';
	         txtCodigoFamilia.style.display = 'inline';
	         imgFamilia.style.display = 'inline';
	         lblSeccion.style.display = 'none';
	         txtSeccion.style.display = 'none';
	         lblAlumno.style.display = 'none';
	         txtCodigoAlumno.style.display = 'none';
	         imgAlumno.style.display = 'none';
	     }
	     else {
	         lblGrado.style.display = 'none';
	         ddlGrado.style.display = 'none';
	         lblfamilia.style.display = 'none';
	         txtCodigoFamilia.style.display = 'none';
	         imgFamilia.style.display = 'none';
	         lblSeccion.style.display = 'none';
	         txtSeccion.style.display = 'none';
	         lblAlumno.style.display = 'none';
	         txtCodigoAlumno.style.display = 'none';
	         imgAlumno.style.display = 'none';
	     }


	     jQuery.ajax({
	         type: 'POST',
	         url: 'TestMTC.aspx/SetValue',
	         data: '{ val:-9 }',
	         contentType: 'application/json; charset=utf-8',
	         dataType: 'json',
	         success: function (msg) {
	             alert("New Session value is " + msg.d);
	         }
	     });
	 }

	 function fnCargarVentana() {
	     jQuery('#divVer').dialog({
	         autoOpen: false,
	         modal: true,
	         bgiframe: true,
	         title: '',
	         width: 200,
	         height: 350,
	         buttons:
                             {
                                 'Aceptar': function () {
                                     $(this).dialog("close");


                                 }
                             }
	     });
	     $('#divVer').dialog('open');
	     $('#divVer').parent().appendTo($("form:first"));
	 }


	 function fnListar() {
	     AbrirVentanaEspera({ title: "Buscando...", message: "Espere un momento por favor" });
	     var chk = document.getElementById('<%=  chkSelTodo.ClientID %>')
        chk.checked = false;

        var tab = document.getElementById('<%=  hfTabSel.ClientID %>').value;
        if (tab == '1')
            document.getElementById('<%=  btnAlumno.ClientID %>').click();
        else if (tab == '2')
            document.getElementById('<%=  btnFamilia.ClientID %>').click();
        else
            document.getElementById('<%=  btnEmpleado.ClientID %>').click();
    return false;
}

function fnGuardar() {

    AbrirVentanaEspera({ title: "Procesando...", message: "Espere un momento por favor" });
    var tab = document.getElementById('<%=  hfTabSel.ClientID %>').value;
        if (tab == '1')
            document.getElementById('<%=  btnGuardarAlumno.ClientID %>').click();
        else if (tab == '2')
            document.getElementById('<%=  btnGuardarFamilia.ClientID %>').click();
        else
            document.getElementById('<%=  btnGuardarEmpleado.ClientID %>').click();
    return false;
}


function fnQuitarChekSelTodos() {
    var chk = document.getElementById('<%=  chkSelTodo.ClientID %>')
    chk.checked = false;
    $(".chkCampo1 input[type='checkbox']").attr('checked', false);
 }

    </script>


    <div id="filtros">
        <table class="filtro_bar">
            <colgroup>
                <col width="10%" />
                <col width="10%" />
                <col width="10%" />
                <col width="20%" />
                <col width="10%" />
                <col width="40%" />

            </colgroup>
            <tr>
                <td style="text-align: right">
                    <asp:Label ID="lblPeriodoAcademico" runat="server" Text="<%$ Resources:resDiccionario, PeriodoAcademico %>"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlPeriodoAcademico" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>

                <td style="text-align: right">
                    <asp:Label ID="lblGrado" runat="server" Text="<%$ Resources:resDiccionario, Grado %>"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlGrado" runat="server">
                    </asp:DropDownList>
                </td>
                <td style="text-align: right">
                    <asp:Label ID="lblfamilia" runat="server" Text="<%$ Resources:resDiccionario, Familia %>"></asp:Label>
                </td>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:TextBox runat="server" ID="txtCodigoFamilia"></asp:TextBox>
                                <asp:Image runat="server" ID="imgFamilia" ImageUrl="~/img/ico_buscar4.gif" onclick="javascript:return AbrirSelectorFamilia('selectorConteinerFamilia');" />

                            </td>
                        </tr>
                    </table>

                </td>

            </tr>
            <tr>
                <td style="text-align: right">
                    <asp:Label ID="lblFoto" runat="server" Text="<%$ Resources:resDiccionario, Foto %>"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlFoto" runat="server" Width="100px">
                        <asp:ListItem Text="--Todos--" Value=""></asp:ListItem>
                        <asp:ListItem Text="Con Foto" Value="S"></asp:ListItem>
                        <asp:ListItem Text="Sin Foto" Value="N"></asp:ListItem>
                    </asp:DropDownList>
                </td>

                <td style="text-align: right">
                    <asp:Label ID="lblSeccion" runat="server" Text="<%$ Resources:resDiccionario, Seccion %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtSeccion"></asp:TextBox>
                </td>
                <td style="text-align: right">
                    <asp:Label ID="lblAlumno" runat="server" Text="<%$ Resources:resDiccionario, Alumno %>"></asp:Label>
                </td>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:TextBox runat="server" ID="txtCodigoAlumno"></asp:TextBox>
                                <asp:Image runat="server" ID="imgAlumno" ImageUrl="~/img/ico_buscar4.gif" onclick="javascript:return AbrirSelectorAlumno('selectorConteinerAlumno');" />
                            </td>
                        </tr>
                    </table>

                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <asp:Label ID="lblExportado" runat="server" Text="<%$ Resources:resDiccionario, Exportado %>"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlExportado" runat="server" Width="100px">
                        <asp:ListItem Text="--Todos--" Value=""></asp:ListItem>
                        <asp:ListItem Text="Exportado" Value="S"></asp:ListItem>
                        <asp:ListItem Text="Sin Exportar" Value="N"></asp:ListItem>
                    </asp:DropDownList>
                </td>

                <td style="text-align: right">
                    <asp:Label ID="lblSelTodo" runat="server" Text="<%$ Resources:resDiccionario, SelTodo %>"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="chkSelTodo" onclick="javascript:return SeleccionarTodos();" />
                </td>
                <td style="text-align: right">&nbsp;</td>
                <td>
                    <asp:TextBox runat="server" ID="txtApellido" Style="visibility:hidden"></asp:TextBox>

                </td>
            </tr>
           
        </table>
    </div>
    <asp:HiddenField runat="server" ID="hfTabSel" Value="1" />
    <div id="tabFicha">
        <ul class="tabs1">
            <li id="liAlumno" runat="server"><a id="lnkAlumno" runat="server" href="#divAlumno" onclick="javascript:return fnLimpiarSession(1);">
                <asp:Label runat="server" ID="lblAlumnoLista" Text="<%$ Resources:resDiccionario, Alumno %>"></asp:Label>
            </a></li>

            <li id="liFamilias" runat="server"><a id="lnkdivfamilias" runat="server" href="#divfamilias" onclick="javascript:return fnLimpiarSession(2);">
                <asp:Label runat="server" ID="lbldivfamilias" Text="<%$ Resources:resDiccionario, familias %>"></asp:Label>
            </a></li>

            <li id="lidivEmpleados" runat="server"><a id="lnkdivEmpleados" runat="server" href="#divEmpleados" onclick="javascript:return fnLimpiarSession(3);">
                <asp:Label runat="server" ID="lbldivEmpleados" Text="<%$ Resources:resDiccionario, Empleados %>"></asp:Label>
            </a></li>
        </ul>

        <div id="divAlumno" runat="server">
            <asp:UpdatePanel runat="server" ID="updAlumno" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:ImageButton ID="btnAlumno" runat="server" OnClick="btnAlumno_Click" Style="display: none" />
                    <asp:ImageButton ID="btnGuardarAlumno" runat="server" OnClick="btnGuardarAlumno_Click" Style="display: none" />
                    <div id="listaAlumno">
                        <asp:ListView runat="server" ID="lvLista" OnItemCommand="lvLista_ItemCommand"
                            OnItemDataBound="lvLista_ItemDataBound">
                            <LayoutTemplate>
                                <table runat="server" id="lista" class="lista">
                                    <thead>
                                        <tr>
                                            <th id="Th1" style="width: 3%; text-align: center">#
                                            </th>
                                            <th id="Th20" style="width: 5%; text-align: center">
                                                <%= Resources.resDiccionario.Procesar%>
                                            </th>
                                            <th id="Th2" style="width: 7%; text-align: center">
                                                <%= Resources.resDiccionario.Exportado%>
                                            </th>
                                            <th id="Th3" style="width: 7%; text-align: center">
                                                <%= Resources.resDiccionario.Periodo%>
                                            </th>
                                            <th id="Th4" style="width: 4%; text-align: center">
                                                <%= Resources.resDiccionario.Foto%>
                                            </th>
                                            <th id="Th12" style="width: 4%; text-align: center">
                                                <%= Resources.resDiccionario.Ver%>
                                            </th>
                                            <th id="Th13" style="width: 10%; text-align: center">
                                                <%= Resources.resDiccionario.Grado%>
                                            </th>
                                            <th id="Th14" style="width: 5%">
                                                <%= Resources.resDiccionario.Seccion%>
                                            </th>
                                            <th id="Th15" style="width: 5%">
                                                <%= Resources.resDiccionario.Rol%>
                                            </th>
                                            <th id="Th5" style="width: 7%">
                                                <%= Resources.resDiccionario.Alumno%>
                                            </th>
                                            <th id="Th6" style="width: 20%">
                                                <%= Resources.resDiccionario.Apellidos%>
                                            </th>
                                            <th id="Th7" style="width: 15%">
                                                <%= Resources.resDiccionario.Nombres%>
                                            </th>
                                            <th id="Th7111" style="text-align: center">
                                                <%= Resources.resDiccionario.FechaProceso%>
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
                                    <td id="Td0" runat="server" style="text-align: right">

                                        <asp:Label ID="lblFila" runat="server" Text='<%# Eval("Fila") %>' />
                                    </td>
                                    <td id="Td13" runat="server" style="text-align: center">
                                        <asp:CheckBox runat="server" ID="chkExportado" CssClass="chkCampo1" />

                                    </td>
                                    <td id="Td1" runat="server" style="text-align: center">
                                        <asp:CheckBox runat="server" ID="chkProceado" Checked='<%# (Eval("ProcesadoFlag").ToString()=="S")%>' Enabled="false" />
                                    </td>
                                    <td id="Td2" runat="server" style="text-align: center">
                                        <asp:Label ID="lblPeriodo" runat="server" Text='<%# Eval("PeriodoAcademico") %>' />
                                    </td>
                                    <td id="Td3" runat="server" style="text-align: center">
                                        <asp:CheckBox runat="server" ID="chkFoto" Checked='<%# (Eval("FotoFlag").ToString()=="S")%>' Enabled="false" />
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Image runat="server" ID="imgVer" ImageUrl="~/img/ico_person4.gif" />
                                    </td>

                                    <td id="Td5" runat="server">
                                        <asp:Label ID="lblGrado" runat="server" Text='<%# Eval("Grado") %>' />
                                    </td>
                                    <td id="Td6" runat="server" style="text-align: center">
                                        <asp:Label ID="lblSeccion" runat="server" Text='<%# Eval("Seccion") %>' />
                                    </td>
                                    <td id="Td7" runat="server">
                                        <asp:Label ID="lblRol" runat="server" Text='<%# Eval("Rol") %>' />
                                    </td>
                                    <td id="Td8" runat="server">
                                        <asp:Label ID="lblAlumnoCodigo" runat="server" Text='<%# Eval("AlumnoCodigo") %>' />
                                    </td>
                                    <td id="Td9" runat="server">
                                        <asp:Label ID="lblApellidos" runat="server" Text='<%# Eval("Apellidos") %>' />
                                    </td>
                                    <td id="Td10" runat="server">
                                        <asp:Label ID="lblNombres" runat="server" Text='<%# Eval("Nombres") %>' />
                                    </td>
                                    <td id="Td15" runat="server" style="text-align: center">
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("FechaCreacion",  "{0:dd/MM/yyyy hh:mm}") %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table runat="server" id="lista" class="lista">

                                    <tr>
                                        <th id="Th1" style="width: 3%; text-align: center">#
                                        </th>
                                        <th id="Th20" style="width: 5%; text-align: center">
                                            <%= Resources.resDiccionario.Procesar%>
                                        </th>
                                        <th id="Th2" style="width: 7%; text-align: center">
                                            <%= Resources.resDiccionario.Exportado%>
                                        </th>
                                        <th id="Th3" style="width: 7%; text-align: center">
                                            <%= Resources.resDiccionario.Periodo%>
                                        </th>
                                        <th id="Th4" style="width: 4%; text-align: center">
                                            <%= Resources.resDiccionario.Foto%>
                                        </th>
                                        <th id="Th12" style="width: 4%; text-align: center">
                                            <%= Resources.resDiccionario.Ver%>
                                        </th>
                                        <th id="Th13" style="width: 10%; text-align: center">
                                            <%= Resources.resDiccionario.Grado%>
                                        </th>
                                        <th id="Th14" style="width: 5%">
                                            <%= Resources.resDiccionario.Seccion%>
                                        </th>
                                        <th id="Th15" style="width: 5%">
                                            <%= Resources.resDiccionario.Rol%>
                                        </th>
                                        <th id="Th5" style="width: 7%">
                                            <%= Resources.resDiccionario.Alumno%>
                                        </th>
                                        <th id="Th6" style="width: 20%">
                                            <%= Resources.resDiccionario.Apellidos%>
                                        </th>
                                        <th id="Th7" style="width: 15%">
                                            <%= Resources.resDiccionario.Nombres%>
                                        </th>
                                        <th id="Th7111" style="text-align: center">
                                            <%= Resources.resDiccionario.FechaProceso%>
                                        </th>
                                    </tr>


                                    <tr>
                                        <td colspan="13">
                                            <%= Resources.resDiccionario.NoHayRegistros %>
                                        </td>
                                    </tr>

                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </div>

                    <div>
                        <uc1:ucPaginador ID="ucpagLista" runat="server" OnCambioPagina="ucpagLista_CambioPagina" />
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
        <div id="divfamilias" runat="server">
            <asp:UpdatePanel runat="server" ID="updFamilia" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:ImageButton ID="btnFamilia" runat="server" OnClick="btnFamilia_Click" Style="display: none" />
                    <asp:ImageButton ID="btnGuardarFamilia" runat="server" OnClick="btnGuardarFamilia_Click" Style="display: none" />
                    <div id="listaFamilia">
                        <asp:ListView runat="server" ID="lvListaFamilia"
                            OnItemDataBound="lvListaFamilia_ItemDataBound">
                            <LayoutTemplate>
                                <table runat="server" id="lista" class="lista">
                                    <thead>
                                        <tr>
                                            <th id="Th1" style="width: 3%; text-align: center">#
                                            </th>
                                            <th id="Th19" style="width: 5%">
                                                <%= Resources.resDiccionario.Procesar%> 
                                            </th>
                                            <th id="Th2" style="width: 5%">

                                                <%= Resources.resDiccionario.Exportado%>
                                            </th>
                                            <th id="Th3" style="width: 5%; text-align: center">
                                                <%= Resources.resDiccionario.Codigo%>
                                            </th>
                                            <th id="Th4" style="width: 4%; text-align: center">
                                                <%= Resources.resDiccionario.Foto%>
                                            </th>
                                            <th id="Th12" style="width: 4%; text-align: center">
                                                <%= Resources.resDiccionario.Ver%>
                                            </th>
                                            <th id="Th5" style="width: 10%">
                                                <%= Resources.resDiccionario.Familia%>
                                            </th>
                                            <th id="Th6" style="width: 5%">
                                                <%= Resources.resDiccionario.Vinculo%>
                                            </th>
                                            <th id="Th7" style="width: 12%">
                                                <%= Resources.resDiccionario.Apellidos%>
                                            </th>
                                            <th id="Th8" style="width: 8%">
                                                <%= Resources.resDiccionario.Nombres%>
                                            </th>
                                            <th id="Th9" style="width: 15%">
                                                <%= Resources.resDiccionario.Direccion%>
                                            </th>
                                            <th id="Th10" style="width: 5%; text-align: center">
                                                <%= Resources.resDiccionario.TipoDocumento%>
                                            </th>
                                            <th id="Th11" style="width: 6%; text-align: center">
                                                <%= Resources.resDiccionario.DocumentoIdentidad%>
                                            </th>
                                            <th id="Th7111" style="text-align: center">
                                                <%= Resources.resDiccionario.FechaProceso%>
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
                                    <td id="Td0" runat="server" style="text-align: right">

                                        <asp:Label ID="lblFila" runat="server" Text='<%# Eval("Fila") %>' />
                                    </td>
                                    <td id="Td12" runat="server" style="text-align: center">

                                        <asp:CheckBox runat="server" ID="chkExportado" CssClass="chkCampo1" />
                                    </td>
                                    <td id="Td1" runat="server" style="text-align: center">
                                        <asp:CheckBox runat="server" ID="chkProcesado" Checked='<%# (Eval("ProcesadoFlag").ToString()=="S")%>' Enabled="false" />
                                    </td>
                                    <td id="Td2" runat="server" style="text-align: center">
                                        <asp:Label ID="lblCodigo" runat="server" Text='<%# Eval("Alumnogrupo") %>' />
                                    </td>
                                    <td id="Td3" runat="server" style="text-align: center">
                                        <asp:CheckBox runat="server" ID="chkFoto" Checked='<%# (Eval("FotoFlag").ToString()=="S")%>' Enabled="false" />
                                    </td>

                                    <td style="text-align: center">
                                        <asp:Image runat="server" ID="imgVer" ImageUrl="~/img/ico_person4.gif" />
                                    </td>

                                    <td id="Td5" runat="server">
                                        <asp:Label ID="lblFamilia" runat="server" Text='<%# Eval("Familia") %>' />
                                    </td>
                                    <td id="Td6" runat="server">
                                        <asp:Label ID="lblVinculo" runat="server" Text='<%# Eval("Vinculo") %>' />
                                    </td>
                                    <td id="Td7" runat="server">
                                        <asp:Label ID="lblApellidos" runat="server" Text='<%# Eval("Apellidos") %>' />
                                    </td>
                                    <td id="Td8" runat="server">
                                        <asp:Label ID="lblNombres" runat="server" Text='<%# Eval("Nombres") %>' />
                                    </td>
                                    <td id="Td9" runat="server">
                                        <asp:Label ID="lblDireccion" runat="server" Text='<%# Eval("Direccion") %>' />
                                    </td>
                                    <td id="Td10" runat="server">
                                        <asp:Label ID="lblTipodocumento" runat="server" Text='<%# Eval("Tipodocumento") %>' />
                                    </td>
                                    <td id="Td11" runat="server" style="text-align: center">
                                        <asp:Label ID="lblDocumentoIdentidad" runat="server" Text='<%# Eval("DocumentoIdentidad") %>' />
                                    </td>
                                    <td id="Tdaaa15" runat="server" style="text-align: center">
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("FechaCreacion",  "{0:dd/MM/yyyy hh:mm}") %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table runat="server" id="lista" class="lista">

                                    <tr>
                                        <th id="Th1" style="width: 3%; text-align: center">#
                                        </th>
                                        <th id="Th19" style="width: 5%">
                                            <%= Resources.resDiccionario.Procesar%> 
                                        </th>
                                        <th id="Th2" style="width: 5%">

                                            <%= Resources.resDiccionario.Exportado%>
                                        </th>
                                        <th id="Th3" style="width: 7%; text-align: center">
                                            <%= Resources.resDiccionario.Codigo%>
                                        </th>
                                        <th id="Th4" style="width: 4%; text-align: center">
                                            <%= Resources.resDiccionario.Foto%>
                                        </th>
                                        <th id="Th12" style="width: 4%; text-align: center">
                                            <%= Resources.resDiccionario.Ver%>
                                        </th>
                                        <th id="Th5" style="width: 10%">
                                            <%= Resources.resDiccionario.Familia%>
                                        </th>
                                        <th id="Th6" style="width: 5%">
                                            <%= Resources.resDiccionario.Vinculo%>
                                        </th>
                                        <th id="Th7" style="width: 12%">
                                            <%= Resources.resDiccionario.Apellidos%>
                                        </th>
                                        <th id="Th8" style="width: 10%">
                                            <%= Resources.resDiccionario.Nombres%>
                                        </th>
                                        <th id="Th9" style="width: 15%">
                                            <%= Resources.resDiccionario.Direccion%>
                                        </th>
                                        <th id="Th10" style="width: 5%; text-align: center">
                                            <%= Resources.resDiccionario.TipoDocumento%>
                                        </th>
                                        <th id="Th11" style="width: 6%; text-align: center">
                                            <%= Resources.resDiccionario.DocumentoIdentidad%>
                                        </th>
                                        <th id="Th7111" style="text-align: center">
                                            <%= Resources.resDiccionario.FechaProceso%>
                                        </th>

                                    </tr>
                                    <tr>
                                        <td colspan="14">
                                            <%= Resources.resDiccionario.NoHayRegistros %>
                                        </td>
                                    </tr>

                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </div>

                    <div>
                        <uc1:ucPaginador ID="ucpagListaFamilia" runat="server" OnCambioPagina="ucpagListaFamilia_CambioPagina" />
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
        <div id="divEmpleados" runat="server">
            <asp:UpdatePanel runat="server" ID="updEmpleado" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:ImageButton ID="btnEmpleado" runat="server" OnClick="btnEmpleado_Click" Style="display: none" />
                    <asp:ImageButton ID="btnGuardarEmpleado" runat="server" OnClick="btnGuardarEmpleado_Click" Style="display: none" />
                    <div id="listaEmpleado">
                        <asp:ListView runat="server" ID="lvListaEmpleado"
                            OnItemDataBound="lvListaEmpleado_ItemDataBound">
                            <LayoutTemplate>
                                <table runat="server" id="lista" class="lista">
                                    <thead>
                                        <tr>
                                            <th id="Th1" style="width: 3%; text-align: center">#
                                            </th>
                                            <th id="Th2" style="width: 5%">
                                                <%= Resources.resDiccionario.Procesar%>
                                            </th>
                                            <th id="Th21" style="width: 7%; text-align: center">
                                                <%= Resources.resDiccionario.Exportado%>
                                            </th>

                                            <th id="Th3" style="width: 4%; text-align: center">
                                                <%= Resources.resDiccionario.Foto%>
                                            </th>
                                            <th id="Th18" style="width: 4%; text-align: center">
                                                <%= Resources.resDiccionario.Ver%>
                                            </th>
                                            <th id="Th4" style="width: 5%">
                                                <%= Resources.resDiccionario.Empleado%>
                                            </th>
                                            <th id="Th5" style="width: 15%">
                                                <%= Resources.resDiccionario.Apellidos%>
                                            </th>
                                            <th id="Th6" style="width: 15%">
                                                <%= Resources.resDiccionario.Nombres%>
                                            </th>
                                            <th id="Th7" style="width: 5%">
                                                <%= Resources.resDiccionario.Sexo%>
                                            </th>
                                            <th id="Th8" style="width: 7%; text-align: center">
                                                <%= Resources.resDiccionario.PaisNacimiento%>
                                            </th>
                                            <th id="Th9" style="width: 10%; text-align: center">
                                                <%= Resources.resDiccionario.TipoDocumento%>
                                            </th>
                                            <th id="Th10" style="width: 7%; text-align: center">
                                                <%= Resources.resDiccionario.DocumentoIdentidad%>
                                            </th>
                                            <th id="Th711d1" style="text-align: center">
                                                <%= Resources.resDiccionario.FechaProceso%>
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
                                    <td id="Td0" runat="server" style="text-align: right">

                                        <asp:Label ID="lblFila" runat="server" Text='<%# Eval("Fila") %>' />
                                    </td>
                                    <td id="Td14" runat="server" style="text-align: center">
                                        <asp:CheckBox runat="server" ID="chkExportado" CssClass="chkCampo1" />
                                    </td>
                                    <td id="Td1" runat="server" style="text-align: center">

                                        <asp:CheckBox runat="server" ID="chkProcesado" Enabled="false" Checked='<%# (Eval("ProcesadoFlag").ToString()=="S")%>' />
                                    </td>

                                    <td id="Td2" runat="server" style="text-align: center">
                                        <asp:CheckBox runat="server" ID="chkFoto" Checked='<%# (Eval("FotoFlag").ToString()=="S")%>' Enabled="false" />
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Image runat="server" ID="imgVer" ImageUrl="~/img/ico_person4.gif" />
                                    </td>

                                    <td id="Td4" runat="server" style="text-align: right">
                                        <asp:Label ID="lblCodigo" runat="server" Text='<%# Eval("Empleado") %>' />
                                    </td>

                                    <td id="Td5" runat="server">
                                        <asp:Label ID="lblApellidos" runat="server" Text='<%# Eval("Apellidos") %>' />
                                    </td>
                                    <td id="Td6" runat="server">
                                        <asp:Label ID="lblNombres" runat="server" Text='<%# Eval("Nombres") %>' />
                                    </td>

                                    <td id="Td8" runat="server">
                                        <asp:Label ID="lblSexo" runat="server" Text='<%# Eval("Sexo") %>' />
                                    </td>
                                    <td id="Td9" runat="server">
                                        <asp:Label ID="lblpaisNaciimiento" runat="server" Text='<%# Eval("paisNaciimiento") %>' />
                                    </td>
                                    <td id="Td10" runat="server">
                                        <asp:Label ID="lblTipoDocumento" runat="server" Text='<%# Eval("TipoDocumento") %>' />
                                    </td>
                                    <td id="Td11" runat="server" style="text-align: center">
                                        <asp:Label ID="lbldocumento" runat="server" Text='<%# Eval("documento") %>' />
                                    </td>
                                    <td id="Tdaaa15" runat="server" style="text-align: center">
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("FechaCreacion",  "{0:dd/MM/yyyy hh:mm}") %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table runat="server" id="lista" class="lista">
                                    <tr>
                                        <th id="Th1" style="width: 3%; text-align: center">#
                                        </th>
                                        <th id="Th2" style="width: 5%">

                                            <%= Resources.resDiccionario.Procesar%>
                                        </th>
                                        <th id="Th21" style="width: 5%">
                                            <%= Resources.resDiccionario.Exportado%>
                                        </th>
                                        <th id="Th3" style="width: 4%; text-align: center">
                                            <%= Resources.resDiccionario.Foto%>
                                        </th>
                                        <th id="Th18" style="width: 4%; text-align: center">
                                            <%= Resources.resDiccionario.Ver%>
                                        </th>
                                        <th id="Th4" style="width: 5%">
                                            <%= Resources.resDiccionario.Empleado%>
                                        </th>
                                        <th id="Th5" style="width: 20%">
                                            <%= Resources.resDiccionario.Apellidos%>
                                        </th>
                                        <th id="Th6" style="width: 15%">
                                            <%= Resources.resDiccionario.Nombres%>
                                        </th>
                                        <th id="Th7" style="width: 5%">
                                            <%= Resources.resDiccionario.Sexo%>
                                        </th>
                                        <th id="Th8" style="width: 7%; text-align: center">
                                            <%= Resources.resDiccionario.PaisNacimiento%>
                                        </th>
                                        <th id="Th9" style="width: 5%; text-align: center">
                                            <%= Resources.resDiccionario.TipoDocumento%>
                                        </th>
                                        <th id="Th10" style="text-align: center">
                                            <%= Resources.resDiccionario.DocumentoIdentidad%>
                                        </th>

                                    </tr>

                                    <tr>
                                        <td colspan="12">
                                            <%= Resources.resDiccionario.NoHayRegistros %>
                                        </td>
                                    </tr>

                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </div>

                    <div>
                        <uc1:ucPaginador ID="ucpagListaEmpleado" runat="server" OnCambioPagina="ucpagListaEmpleado_CambioPagina" />
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>

        </div>


    </div>

    <div id="selectorConteinerFamilia" style="display: none; font-size: 90%;">
        <uc1:ucFamiliaSelector runat="server" ID="ucFamiliaSelector" />
    </div>

    <div id="selectorConteinerAlumno" style="display: none; font-size: 90%;">
        <uc1:ucAlumnoSelector runat="server" ID="ucAlumnoSelector" />
    </div>
    <div id="visorConteinerFicha" style="display: none; font-size: 90%;">
        <uc1:ucFichaAtributos runat="server" ID="ucFichaAtributos" />
    </div>
    <div id="loadingScreen"></div>

</asp:Content>
