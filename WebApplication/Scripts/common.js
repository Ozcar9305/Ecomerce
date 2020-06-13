console.log('common.js');

/**
 * Permite capturar unicamente texto en un input quitando caracteres especiales
 * */
jQuery.fn.ForceLetterOnly = function () {
    return this.each(function () {
        $(this).keydown(function (e) {
            var key = e.charCode || e.keyCode || 0;
            return (key == 8 || key === 32 || (key == 164 || key == 165 || key == 192) || (key >= 65 && key <= 90))
        });
    });
};

/**
 * Permite obtener valores del query string de la url
 * Param: Nombre del parametro en el query string
 **/
function GetUriValues(param) {
    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < url.length; i++) {
        var urlparam = url[i].split('=');
        if (urlparam[0] === param) {
            return urlparam[1];
        }
    }
}

/**
 * Permite validar que el valor ingresado sea un correo electronico
 */
function isEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}