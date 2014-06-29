


//var sColAntSelectorFamilia;
//function BuscarFamiliasSel() {
//    var sColumna = $("#selCampoSelectorFamilia").val();
//    sColAntSelectorFamilia = sColumna;
//    var sValor = $("#inValorSelectorFamilia").val();
//    ObtenerFichaAtributos(sColumna, sValor, iPagina, true);
//}
function ObtenerFichaAtributos(psPeriodoAcademico, psCodigo, psVinculo, psDocumentoIdentidad, psTipo) {
    
    var sTitulo = '';
    switch (psTipo) {
        case "A":
            sTitulo = 'Atributos de Alumno';
            break;
        case "F":
            sTitulo = 'Atributos de Familia';
            break;
        case "E":
            sTitulo = 'Atributos de Empleado';
            break;
    }
    
    $("#spTituloFichaAtributo").text(sTitulo);
    $(document).ready(function () {

        var compilado = _.template($('#listaFichaAtributos').html());
        //AbrirVentanaEspera({ title: "Cargando", message: "Espere un momento" });
        $.getJSON("../ws/WCServicio.svc/ObtenerFichaPorAtributo",
            {
                pstrPeriodoAcademico: psPeriodoAcademico,
                pstrCodigo: psCodigo,
                pstrVinculo: psVinculo,
                pstrDocumentoIdentidad : psDocumentoIdentidad,
                pstrTipo : psTipo
            },

            function (resultado) {
                // Datos simulados que vendrían de una llamada a AJAX
                $('#divFichaAtributos').html(compilado(resultado));
                
            })
            .success(function () { })
            .error(function (result) {
                alert('Error listar atributos - ' + '\n[ Code ' + result.status + ' 004 ]');
                //CerrarVentanaEspera();
            })
            .complete(function () {
                //CerrarVentanaEspera();
            });

    });
}



