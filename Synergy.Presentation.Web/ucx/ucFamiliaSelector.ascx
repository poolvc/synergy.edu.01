<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucFamiliaSelector.ascx.cs"
    Inherits="ucFamiliaSelector" %>

<link href="../Content/simplePagination.css" rel="stylesheet" />
<script type="text/javascript" src="../Scripts/underscore-min.js"></script>
<script type="text/javascript" src="../Scripts/jquery.simplePagination.js"></script>
<script type="text/javascript" src="../Scripts/base/modFamiliaSelector.js"></script>

<div id="selectorFamiliaFiltro">
    <table class="filtro_bar">
        <tr>
            <td style="width: 10%">
                <%= Resources.resDiccionario.BuscarPor %>
            </td>
            <td style="width: 10%">
                    <select id="selCampoSelectorFamilia">
                    <option value="ApellidoPaterno"><%= Resources.resDiccionario.ApellidoPaterno %></option>
                    <option value="ApellidoMaterno"><%= Resources.resDiccionario.ApellidoMaterno %></option>
                    <option value="AlumnoGrupo"><%= Resources.resDiccionario.Codigo %></option>
                </select>
            </td>
                   
            <td style="width: 1%">
                 >=
            </td>
            <td style="width: 5%">
               <input type="text" id="inValorSelectorFamilia"/>
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
<input type="hidden" id="hdCodigoSelectorFamilia" />

<script type="text/template" id="listaFamiliaSel">

    <table class="lista">
    <tr>
        <th style="width: 5%">#</th> 
        <th style="width: 10%"><%= Resources.resDiccionario.Codigo %></th>
        <th style="width: 30%"><%= Resources.resDiccionario.ApellidoPaterno %></th>
        <th style="width: 30%"><%= Resources.resDiccionario.ApellidoMaterno %></th>
        <th>Estado</th>
    </tr>
    <@ if(Familias.length>0) { @>
            <@ _.each(Familias, function(Familia) { @>
                <tr onclick="fnSelecionarFilaSelectorFamilia(this,'<@ print(Familia.AlumnoGrupo); @>');"" >
                <td><@ print(Familia.Fila); @></td> 
                <td><@ print(Familia.AlumnoGrupo); @></td>
                <td><@ print(Familia.ApellidoPaterno); @></td>
                <td><@ print(Familia.ApellidoMaterno); @></td>
                    <@ if(Familia.Estado == "A") { @>
                   <td><%= Resources.resDiccionario.Activo %></td>
                    <@ }else { @>
                    <td><%= Resources.resDiccionario.Inactivo %></td>
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
