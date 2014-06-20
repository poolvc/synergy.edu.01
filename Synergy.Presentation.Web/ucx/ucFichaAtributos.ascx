<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucFichaAtributos.ascx.cs"
    Inherits="ucFichaAtributos" %>

<link href="../Content/simplePagination.css" rel="stylesheet" />
<script type="text/javascript" src="../Scripts/underscore-min.js"></script>
<script type="text/javascript" src="../Scripts/jquery.simplePagination.js"></script>
<script type="text/javascript" src="../Scripts/base/modFichaAtributo.js"></script>
<div id="selectorFamiliaFiltro">
    <table class="filtro_bar">
        <tr>
            <td style="width: 50%">
                <h4> <span id="spTituloFichaAtributo"></span></h4>
               
            </td>
            
            <td>
            </td>
        </tr>
    </table>
</div>
<div id="divFichaAtributos">
</div>

<script type="text/template" id="listaFichaAtributos">

    <table class="lista">
    <tr>
        <th style="width: 5%">#</th> 
        <th style="width: 40%"><%= Resources.resDiccionario.Propiedad %></th>
        <th><%= Resources.resDiccionario.Valor %></th>
    </tr>
    <@ if(Atributos.length>0) { @>
            <@ _.each(Atributos, function(Atributo) { @>
                <tr>
                    <td><@ print(Atributo.Fila); @></td> 
                    <td><@ print(Atributo.Descripcion); @></td>
                    <td><@ print(Atributo.Valor); @></td>
                </tr>
        <@ }); @>
    <@ }else { @>
            <tr>
            <td colspan="3"> <%= Resources.resDiccionario.NoHayRegistros %></td>
        </tr>
    <@ } @>

    </table>
</script>    
