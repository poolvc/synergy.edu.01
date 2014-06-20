<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucAlumnoSelector.ascx.cs"
    Inherits="ucAlumnoSelector" %>

<link href="../Content/simplePagination.css" rel="stylesheet" />
<script type="text/javascript" src="../Scripts/underscore-min.js"></script>
<script type="text/javascript" src="../Scripts/jquery.simplePagination.js"></script>
<script type="text/javascript" src="../Scripts/base/modAlumnoSelector.js"></script>

<div id="selectorAlumnoFiltro">
    <table class="filtro_bar">
        <tr>
            <td style="width: 10%">
                <%= Resources.resDiccionario.BuscarPor %>
            </td>
            <td style="width: 10%">
                    <select id="selCampoSelectorAlumno">
                    <option value="ApellidoPaterno"><%= Resources.resDiccionario.ApellidoPaterno %></option>
                    <option value="ApellidoMaterno"><%= Resources.resDiccionario.ApellidoMaterno %></option>
                    <option value="AlumnoGrupo"><%= Resources.resDiccionario.Codigo %></option>
                </select>
            </td>
                   
            <td style="width: 1%">
                 >=
            </td>
            <td style="width: 5%">
               <input type="text" id="inValorSelectorAlumno"/>
            </td>
            <td>
                <img alt="Buscar" src="../img/ico_buscar4.gif" onclick="BuscarAlumnosSel();"  />
            </td>
        </tr>
    </table>
</div>
<div id="selectorAlumnoSel">
    </div>
<div id="selectorAlumnoSelPag" style="position:absolute; bottom:0;"></div>
<input type="hidden" id="hdCodigoSelectorAlumno" />

<script type="text/template" id="listaAlumnoSel">

    <table class="lista">
    <tr>
        <th style="width: 5%; text-align:center">#</th> 
        <th style="width: 10%"><%= Resources.resDiccionario.Codigo %></th>
        <th style="width: 10%"><%= Resources.resDiccionario.Familia %></th>
        <th style="width: 20%"><%= Resources.resDiccionario.ApellidoPaterno %></th>
        <th style="width: 20%"><%= Resources.resDiccionario.ApellidoMaterno %></th>
        <th style="width: 20%"><%= Resources.resDiccionario.Nombre %></th>
        <th style="width: 8%"><%= Resources.resDiccionario.Sexo %></th>
        <th><%= Resources.resDiccionario.Estado %></th>
    </tr>
    <@ if(Alumnos.length>0) { @>
            <@ _.each(Alumnos, function(Alumno) { @>
                <tr onclick="fnSelecionarFilaSelectorAlumno(this,'<@ print(Alumno.AlumnoCodigo); @>');"" >
                <td><@ print(Alumno.Fila); @></td> 
                <td><@ print(Alumno.AlumnoCodigo); @></td>
                <td><@ print(Alumno.AlumnoGrupo); @></td>
                <td><@ print(Alumno.ApellidoPaterno); @></td>
                <td><@ print(Alumno.ApellidoMaterno); @></td>
                <td><@ print(Alumno.Nombre01); @></td>
                <td><@ print(Alumno.Sexo); @></td>
                <@ if(Alumnos.Estado = "S") { @>
                    <td><%= Resources.resDiccionario.Activo %></td>
                    <@ }else { @>
                    <td><%= Resources.resDiccionario.Inactivo %></td>
                    <@ } @>
                </tr>
        <@ }); @>
    <@ }else { @>
            <tr>
            <td colspan="7"> <%= Resources.resDiccionario.NoHayRegistros %></td>
        </tr>
    <@ } @>

    </table>
</script>    
