﻿//-- ---------------------------------------------------------------------------------------------
//-- Constantes
//-- ---------------------------------------------------------------------------------------------

  var PARAM_PI = 3.14159;  
   

//-- ---------------------------------------------------------------------------------------------
//-- Para Numeros, Decimales, Enteros
//-- ---------------------------------------------------------------------------------------------

function fnIngresoNumeroDecimales(obj, decimalPlaces, allowNegative) {
    var temp = obj.value;

    // avoid changing things if already formatted correctly
    var reg0Str = '[0-9]*';
    if (decimalPlaces > 0) {
        reg0Str += '\\.?[0-9]{0,' + decimalPlaces + '}';
    } else if (decimalPlaces < 0) {
        reg0Str += '\\.?[0-9]*';
    }
    reg0Str = allowNegative ? '^-?' + reg0Str : '^' + reg0Str;
    reg0Str = reg0Str + '$';
    var reg0 = new RegExp(reg0Str);
    if (reg0.test(temp)) return true;

    // first replace all non numbers
    var reg1Str = '[^0-9' + (decimalPlaces != 0 ? '.' : '') + (allowNegative ? '-' : '') + ']';
    var reg1 = new RegExp(reg1Str, 'g');
    temp = temp.replace(reg1, '');

    if (allowNegative) {
        // replace extra negative
        var hasNegative = temp.length > 0 && temp.charAt(0) == '-';
        var reg2 = /-/g;
        temp = temp.replace(reg2, '');
        if (hasNegative) temp = '-' + temp;
    }

    if (decimalPlaces != 0) {
        var reg3 = /\./g;
        var reg3Array = reg3.exec(temp);
        if (reg3Array != null) {
            // keep only first occurrence of .
            //  and the number of places specified by decimalPlaces or the entire string if decimalPlaces < 0
            var reg3Right = temp.substring(reg3Array.index + reg3Array[0].length);
            reg3Right = reg3Right.replace(reg3, '');
            reg3Right = decimalPlaces > 0 ? reg3Right.substring(0, decimalPlaces) : reg3Right;
            temp = temp.substring(0, reg3Array.index) + '.' + reg3Right;
        }
    }

    obj.value = temp;
}

function fnValidaDecimal(numero) {
    if (!/^([0-9])*[.]?[0-9]*$/.test(numero)) {
        return false;
    }
    return true;
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
