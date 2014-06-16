<%@ Page Title="" Language="C#" MasterPageFile="~/mas/masMantListar.master" AutoEventWireup="true" CodeFile="genPeriodoAcademicoListar.aspx.cs" Inherits="genPeriodoAcademicoListar" %>
<%@ MasterType TypeName="masMantListar" %>
<%@ Register Src="~/ucx/ucPaginador.ascx" TagName="ucPaginador" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphArea" runat="Server">
    <div id="filtros">
         <table class="filtro_bar">
            <tr>
                <td style="width:3%; text-align:right">
                    <asp:Label ID="lblEstado" runat="server" Text="<%$ Resources:resDiccionario, Estado %>"></asp:Label>:
                </td>
                <td style="width:8%">
                    <asp:DropDownList ID="ddlEstado" runat="server">
                     </asp:DropDownList>
                </td>
                 <td>                    
                 </td>
                </tr>
             </table>
    </div>
    <div id="lista">
        <asp:ListView runat="server" ID="lvLista" onitemcommand="lvLista_ItemCommand" 
            onitemdatabound="lvLista_ItemDataBound">
            <LayoutTemplate>
                <table runat="server" id="lista" class="lista">
                   
                        <tr>
                             <th id="Th1" style="width:3%">
                                #
                            </th>
                             <th id="Th6" style="width:7%; text-align:center">
                                <%= Resources.resDiccionario.Acciones %>
                            </th>                          
                             <th id="Th8" style="width:20%">
                                <%= Resources.resDiccionario.PeriodoAcademico %>
                            </th>
                             <th id="Th7dd" style="width:60%">
                                <%= Resources.resDiccionario.Descripcion %>
                            </th>
                            <th id="Th7hf" style="width:10%">
                                <%= Resources.resDiccionario.Estado %>
                            </th>
                        </tr>
                   
                   
                        <tr runat="server" id="itemPlaceholder">
                        </tr>  
                                 
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr id="Tr1" runat="server">
                    <td id="Td1" runat="server">
                       
                    </td>
                    <td id="Td6" runat="server" style="text-align:center">            
                         <asp:ImageButton ID="ibtEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("PeriodoAcademico").ToString() %>' ToolTip="<%$ Resources:resDiccionario, Editar %>" ImageUrl="~/img/ico_edit.png" BorderWidth="0" />
                         <asp:ImageButton ID="ibtEliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("PeriodoAcademico").ToString() %>' ToolTip="<%$ Resources:resDiccionario, Estado %>" ImageUrl="~/img/ico_active.gif" BorderWidth="0" />
                    </td>
                    <td id="Td8" runat="server">
                        <asp:Label ID="lblPeriodoAnual" runat="server" Text='<%# Eval("PeriodoAcademico") %>'/>
                    </td>
                     <td id="Td2" runat="server">
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Descripcion") %>'/>
                    </td>
                    <td id="Td7" runat="server">            
                         <asp:Label ID="lblEstado" runat="server" Text='<%# (Eval("Estado").ToString()=="A" ? Resources.resDiccionario.Activo : Resources.resDiccionario.Inactivo) %>'/>
                    </td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table runat="server" id="lista" class="lista">                    
                   
                        <tr>
                            <th id="Th1" style="width:3%">
                                #
                            </th>
                             <th id="Thw6" style="width:7%; text-align:center">
                                <%= Resources.resDiccionario.Acciones %>
                            </th>                          
                             <th id="Thw8" style="width:20%">
                                <%= Resources.resDiccionario.PeriodoAcademico %>
                            </th>
                             <th id="Thw7" style="width:60%">
                                <%= Resources.resDiccionario.Descripcion %>
                            </th>
                            <th id="Th17" style="width:10%">
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
</asp:Content>
