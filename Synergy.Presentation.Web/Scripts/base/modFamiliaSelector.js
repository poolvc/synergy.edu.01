


var sColAntSelectorFamilia;
function BuscarFamiliasSel() {
    var sColumna = $("#selCampoSelectorFamilia").val();
    sColAntSelectorFamilia = sColumna;
    var sValor = $("#inValorSelectorFamilia").val();
    var iPagina = 1;
    ListarFamiliasSel(sColumna, sValor, iPagina, true);
}

function ListarFamiliasSel(psColumna, psValor, piPagina, pbPag) {
    
    $(document).ready(function () {

        var compilado = _.template($('#listaFamiliaSel').html());
        AbrirVentanaEspera({ title: "Cargando...", message: "Espere un momento por favor" });
        $.getJSON("../ws/WCServicio.svc/ListarFamiliaSelPorCampo",
            {
                pstrColumna : psColumna,
                pstrValor : psValor,
                pinPagina : piPagina
            },

            function (resultado) {
                // Datos simulados que vendrían de una llamada a AJAX
                var inPaginas = 0;
                if (resultado.Familias.length > 0)
                {
                    inPaginas = Math.ceil(resultado.Familias[0].TotalFilas / resultado.Familias[0].FilasXPagina);
                } else {
                    inPaginas = 1;
                }
                $('#selectorFamiliaSel').html(compilado(resultado));
                //pbPag solo hace a la primera vez o cuando cambia el criterio de busqueda
                if (pbPag) {
                    $('#selectorFamiliaSelPag').pagination({
                        pages: inPaginas,
                        cssStyle: 'compact-theme',
                        displayedPages: 5,
                        onPageClick: function (pageNumber, event) {
                            // Callback triggered when a page is clicked
                            // Page number is given as an optional parameter

                            var sColumnaNueva = $("#selCampoSelectorFamilia").val();
                            var sValorNueva = $("#inValorSelectorFamilia").val();
                            if (sColAntSelectorFamilia != sColumnaNueva) {
                                pageNumber = 1;
                                ListarFamiliasSel(sColumnaNueva, sValorNueva, pageNumber, true);
                            }
                            else {
                                ListarFamiliasSel(sColumnaNueva, sValorNueva, pageNumber, false);
                            }
                            sColAntSelectorFamilia = sColumnaNueva;
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


var vfilaAntSelectorFamilia = null;
function fnSelecionarFilaSelectorFamilia(pFila, pCodigoAlumnoa) {
    if (vfilaAntSelectorFamilia != null && pFila != vfilaAntSelectorFamilia) {
        vfilaAntSelectorFamilia.style.backgroundColor = 'white';
    }
    vfilaAntSelectorFamilia = pFila;
    pFila.style.backgroundColor = '#E5E5E5';
    $("#hdCodigoSelectorFamilia").val(pCodigoAlumnoa);
}



