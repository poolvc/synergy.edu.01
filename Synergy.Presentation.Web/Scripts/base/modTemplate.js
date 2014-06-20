


var sColumnaAnterior;
function BuscarFamiliasSel() {
    var sColumna = $("#selCampo").val();
    sColumnaAnterior = sColumna;
    var sValor = $("#inValor").val();
    var iPagina = 1;
    ListarFamiliasSel(sColumna, sValor, iPagina);
}

function ListarFamiliasSel(psColumna, psValor, piPagina) {
    
    $(document).ready(function () {

        var compilado = _.template($('#listaFamiliaSel').html());
        console.log("Prueba 1");
        $.getJSON("../ws/WCServicio.svc/ListarFamiliaSelPorCampo",
            {
                pstrColumna : psColumna,
                pstrValor : psValor,
                pinPagina : piPagina
            },

            function (resultado) {
                // Datos simulados que vendrían de una llamada a AJAX
                console.log(resultado);
                // Ejecutamos la funcion compilado y le pasamos la colección
                // que queremos que use y el HTML generado lo ponemos el DOM
                // con jQuery
                var inPaginas = 0;

                console.log(resultado.Familias.length);
                if (resultado.Familias.length > 0)
                {
                    inPaginas = Math.ceil(resultado.Familias[0].TotalFilas / resultado.Familias[0].FilasXPagina);
                } else {
                    inPaginas = 1;
                }
                $('#selectorFamiliaSel').html(compilado(resultado));

                $('#selectorFamiliaSelPag').pagination({
                    pages: inPaginas,
                    cssStyle: 'compact-theme',
                    displayedPages: 5,
                    onPageClick: function (pageNumber, event) {
                        // Callback triggered when a page is clicked
                        // Page number is given as an optional parameter
                        
                        var sColumnaNueva = $("#selCampo").val();
                        var sValorNueva = $("#inValor").val();
                        if (sColumnaAnterior != sColumnaNueva) {
                            pageNumber = 1;
                            ListarFamiliasSel(sColumnaNueva, sValorNueva, pageNumber)
                        }
                        else {
                            ListarFamiliasSel1(sColumnaNueva, sValorNueva, pageNumber)
                        }
                        sColumnaAnterior = sColumnaNueva;
                    },
                });

            })
            .success(function () { })
            .error(function (result) { alert('Error ' + '\n[ Code ' + result.status + ' 004 ]'); })
            .complete(function () { });

    });
}

function ListarFamiliasSel1(psColumna, psValor, piPagina) {

    $(document).ready(function () {
        AbrirVentanaEspera({ title: "Cargando", message: "Espere un momento" });
        var compilado = _.template($('#listaFamiliaSel').html());
        console.log("Prueba 1");
        $.getJSON("../ws/WCServicio.svc/ListarFamiliaSelPorCampo",
            {
                pstrColumna: psColumna,
                pstrValor: psValor,
                pinPagina: piPagina
            },

            function (resultado) {
                // Datos simulados que vendrían de una llamada a AJAX
                console.log(resultado);
                // Ejecutamos la funcion compilado y le pasamos la colección
                // que queremos que use y el HTML generado lo ponemos el DOM
                // con jQuery
                var inPaginas = 0;

                console.log(resultado.Familias.length);
                if (resultado.Familias.length > 0) {
                    inPaginas = parseInt(resultado.Familias[0].TotalFilas / 5) + 1;
                } else {
                    inPaginas = 1;
                }
                $('#selectorFamiliaSel').html(compilado(resultado));
                CerrarVentanaEspera();
            })
            .success(function () { })
            .error(function (result) { alert('Error ' + '\n[ Code ' + result.status + ' 004 ]'); })
            .complete(function () { });

    });
}

var vfilaAnterior = null;
function fnSelecionarFilaFamilia(pFila, pCodigoAlumnoa) {
    console.log("Prueba ");
    if (vfilaAnterior != null && pFila != vfilaAnterior) {
        vfilaAnterior.style.backgroundColor = 'white';
    }
    vfilaAnterior = pFila;
    pFila.style.backgroundColor = '#E5E5E5';
    this.document.getElementById('hdFamilia').value = pCodigoAlumnoa;
}



$(document).ready(function () {
    // create the loading window and set autoOpen to false
    $("#loadingScreen").dialog({
        autoOpen: false, // set this to false so we can manually open it
        dialogClass: "loadingScreenWindow",
        closeOnEscape: false,
        draggable: false,
        width: 300,
        minHeight: 100,
        modal: true,
        buttons: {},
        resizable: false,
        open: function () {
            // scrollbar fix for IE
            $('body').css('overflow', 'hidden');
        },
        close: function () {
            // reset overflow
            $('body').css('overflow', 'auto');
        }
    }); // end of dialog
});
function AbrirVentanaEspera(waiting) { // I choose to allow my loading screen dialog to be customizable, you don't have to
    $("#loadingScreen").html(waiting.message && '' != waiting.message ? waiting.message : 'Please wait...');
    $("#loadingScreen").dialog('option', 'title', waiting.title && '' != waiting.title ? waiting.title : 'Loading');
    $("#loadingScreen").dialog('open');
}
function CerrarVentanaEspera() {
    $("#loadingScreen").dialog('close');
}

