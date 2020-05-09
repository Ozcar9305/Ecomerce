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