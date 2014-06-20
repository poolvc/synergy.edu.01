


var sColAntSelectorAlumno;
function BuscarAlumnosSel() {
    var sColumna = $("#selCampoSelectorAlumno").val();
    sColAntSelectorAlumno = sColumna;
    var sValor = $("#inValorSelectorAlumno").val();
    var iPagina = 1;
    ListarAlumnosSel(sColumna, sValor, iPagina, true);
}

function ListarAlumnosSel(psColumna, psValor, piPagina, pbPag) {
    
    $(document).ready(function () {

        var compilado = _.template($('#listaAlumnoSel').html());
        AbrirVentanaEspera({ title: "Cargando...", message: "Espere un momento por favor" });
        $.getJSON("../ws/WCServicio.svc/ListarAlumnoSelPorCampo",
            {
                pstrColumna : psColumna,
                pstrValor : psValor,
                pinPagina : piPagina
            },

            function (resultado) {
                // Datos simulados que vendrían de una llamada a AJAX
                var inPaginas = 0;
                if (resultado.Alumnos.length > 0)
                {
                    inPaginas = Math.ceil(resultado.Alumnos[0].TotalFilas / resultado.Alumnos[0].FilasXPagina);
                } else {
                    inPaginas = 1;
                }
                $('#selectorAlumnoSel').html(compilado(resultado));
                //pbPag solo hace a la primera vez o cuando cambia el criterio de busqueda
                if (pbPag) {
                    $('#selectorAlumnoSelPag').pagination({
                        pages: inPaginas,
                        cssStyle: 'compact-theme',
                        displayedPages: 5,
                        onPageClick: function (pageNumber, event) {
                            // Callback triggered when a page is clicked
                            // Page number is given as an optional parameter

                            var sColumnaNueva = $("#selCampoSelectorAlumno").val();
                            var sValorNueva = $("#inValorSelectorAlumno").val();
                            if (sColAntSelectorAlumno != sColumnaNueva) {
                                pageNumber = 1;
                                ListarAlumnosSel(sColumnaNueva, sValorNueva, pageNumber, true);
                            }
                            else {
                                ListarAlumnosSel(sColumnaNueva, sValorNueva, pageNumber, false);
                            }
                            sColAntSelectorAlumno = sColumnaNueva;
                        },
                    });
                }
                CerrarVentanaEspera();
            })
            .success(function () { })
            .error(function (result) { alert('Error listar Alumnos - ' + '\n[ Code ' + result.status + ' 004 ]'); })
            .complete(function () { });

    });
}


var vfilaAntSelectorAlumno = null;
function fnSelecionarFilaSelectorAlumno(pFila, pCodigoAlumnoa) {
    if (vfilaAntSelectorAlumno != null && pFila != vfilaAntSelectorAlumno) {
        vfilaAntSelectorAlumno.style.backgroundColor = 'white';
    }
    vfilaAntSelectorAlumno = pFila;
    pFila.style.backgroundColor = '#E5E5E5';
    $("#hdCodigoSelectorAlumno").val(pCodigoAlumnoa);
}



