<%@ Page Title="" Language="C#" MasterPageFile="~/mas/masReporte.master" AutoEventWireup="true" CodeFile="proPrincipal.aspx.cs" Inherits="proPrincipal" %>
<%@ MasterType TypeName="masReporte" %>
<%@ Register Src="~/ucx/ucPaginador.ascx" TagName="ucPaginador" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphArea" runat="Server">
    <div id="filtros">
         <table class="filtro_bar">
            <tr>
                <td style="width:5%">
                    <asp:Label ID="lblDescripcion" runat="server" Text="<%$ Resources:resDiccionario, Descripcion %>"></asp:Label>:
                </td>
                <td style="width:5%">
                    <asp:TextBox ID="txtDescripcion" runat="server"></asp:TextBox>
                </td>
                 <td style="width:2%">                    
                </td>
                <td style="width:3%; text-align:right"">
                        <asp:Label ID="lblEstado" runat="server" Text="<%$ Resources:resDiccionario, Estado %>"></asp:Label>:
                </td>
                <td style="width:5%">
                    <asp:DropDownList ID="ddlEstado" runat="server">
                     </asp:DropDownList>
                </td>
                 <td></td>

                </tr>
             </table>
    </div>
    <div id="lista">
        <asp:ListView runat="server" ID="lvLista" onitemcommand="lvLista_ItemCommand" >
            <LayoutTemplate>
                <table runat="server" id="lista" class="lista">
                    <thead>
                        <tr>
                             <th id="Th1" style="width:3%">
                                #
                            </th>
                             <th id="Th6" style="width:7%; text-align:center">
                                 <%= Resources.resDiccionario.Acciones %>
                            </th>                          
                             <th id="Th4" style="width:10%">
                                 <%= Resources.resDiccionario.Codigo %>
                            </th>
                             <th id="Th5" style="width:10%">
                                 <%= Resources.resDiccionario.Grupo %>
                            </th>
                            <th id="Th3" style="width:30%">
                                 <%= Resources.resDiccionario.Descripcion %>
                            </th>
                            <th id="Th2" style="width:30%">
                                 <%= Resources.resDiccionario.Comentario %>
                            </th>                       
                            <th id="Th7" style="text-align:center">
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
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Fila") %>'/>
                        <asp:HiddenField ID="hfIdAplicacion" runat="server" Value='<%# Eval("IdAplicacion") %>' />
                        <asp:HiddenField ID="hfIdGrupo" runat="server" Value='<%# Eval("IdGrupoReporte") %>' />
                        <asp:HiddenField ID="hfIdReporte" runat="server" Value='<%# Eval("IdReporte") %>' />
                    </td>
                    <td id="Td6" runat="server" style="text-align:center">            
                         <asp:ImageButton ID="ibtEjecutar" runat="server" CommandName="Ejecutar" CommandArgument='<%# Eval("Formulario").ToString() %>' ToolTip="<%$ Resources:resDiccionario, Ejecutar %>" ImageUrl="~/img/ico_rep.gif" BorderWidth="0" />
                    </td>
                    <td id="Td4" runat="server">
                        <asp:Label ID="lblCodigoReporte" runat="server" Text='<%# Eval("CodigoReporte") %>'/>
                    </td>
                    <td id="Td5" runat="server">
                            <asp:Label ID="lblGrupo" runat="server" Text='<%# Eval("DescripcionGrupo") %>'/>
                    </td>
                    <td id="Td3" runat="server">   
                        <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("Descripcion") %>'/>         
                    </td>
                     <td id="Td2" runat="server">
                            <asp:Label ID="lblComentario" runat="server" Text='<%# Eval("Comentario") %>' />
                    </td>
                    <td id="Td7" runat="server" style="text-align:center">            
                         <asp:Label ID="lblEstado" runat="server" Text='<%# (Eval("Estado").ToString()=="A" ? Resources.resDiccionario.Activo : Resources.resDiccionario.Inactivo) %>'/>
                    </td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table runat="server" id="lista" class="lista">                    
                    <thead>
                        <tr>
                             <th style="width:3%">
                                #
                            </th>
                            <th style="width:7%">
                                <%= Resources.resDiccionario.Acciones %>
                            </th>
                            <th style="width:10%">
                                <%= Resources.resDiccionario.Codigo %>
                            </th>
                            <th style="width:10%">
                                <%= Resources.resDiccionario.Grupo %>
                            </th>
                            <th style="width:30%">
                                <%= Resources.resDiccionario.Descripcion %>
                            </th>
                             <th style="width:30%">
                                <%= Resources.resDiccionario.Comentario %>
                            </th>
                            <th>
                                <%= Resources.resDiccionario.Estado %>
                            </th>
                        </tr>
                    </thead> 
                    <tbody>
                        <tr>
                            <td colspan="7"> 
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
            <asp:HiddenField ID="hfMenuCodigo" runat="server" />
        </div>
    </div>
</asp:Content>
