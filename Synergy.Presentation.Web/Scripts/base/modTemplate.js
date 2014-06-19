

function Validar(valor) {
    if (valor < 15) return "Stock Bajooooo!!!!!";
    else return "Todo Normal";
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
                console.log("Prueba 2");
                // Ejecutamos la funcion compilado y le pasamos la colección
                // que queremos que use y el HTML generado lo ponemos el DOM
                // con jQuery
                $('#selectorFamiliaSel').html(compilado(resultado));


                $('#selectorFamiliaSelPag').pagination({
                    pages: 5,
                    cssStyle: 'compact-theme',
                    displayedPages: 3
                });

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
}



