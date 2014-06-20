﻿


var sColumnaAnterior;
function BuscarFamiliasSel() {
    var sColumna = $("#selCampo").val();
    sColumnaAnterior = sColumna;
    var sValor = $("#inValor").val();
    var iPagina = 1;
    ListarFamiliasSel(sColumna, sValor, iPagina, true);
}

function ListarFamiliasSel(psColumna, psValor, piPagina, pbPag) {
    
    $(document).ready(function () {

        var compilado = _.template($('#listaFamiliaSel').html());
        AbrirVentanaEspera({ title: "Cargando", message: "Espere un momento" });
        $.getJSON("../ws/WCServicio.svc/ListarFamiliaSelPorCampo",
            {
                pstrColumna : psColumna,
                pstrValor : psValor,
                pinPagina : piPagina
            },

            function (resultado) {
                // Datos simulados que vendrían de una llamada a AJAX
                var inPaginas = 0;
                console.log(resultado.Familias.length);
                if (resultado.Familias.length > 0)
                {
                    inPaginas = Math.ceil(resultado.Familias[0].TotalFilas / resultado.Familias[0].FilasXPagina);
                } else {
                    inPaginas = 1;
                }
                $('#selectorFamiliaSel').html(compilado(resultado));
                if (pbPag) {
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
                                ListarFamiliasSel(sColumnaNueva, sValorNueva, pageNumber, true);
                            }
                            else {
                                ListarFamiliasSel(sColumnaNueva, sValorNueva, pageNumber, false);
                            }
                            sColumnaAnterior = sColumnaNueva;
                        },
                    });
                }
                CerrarVentanaEspera();
            })
            .success(function () { })
            .error(function (result) { alert('Error listar familias - ' + '\n[ Code ' + result.status + ' 004 ]'); })
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



