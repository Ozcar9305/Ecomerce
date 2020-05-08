
$(document).ready(function () {

    loadCategoryAndProductList();

});

function loadCategoryAndProductList() {

    $.ajax({
        type: "POST",
        url: "LoadCategoryAndProductList",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            bloquearPantalla();
        },
        success: function (response) {

        },
        failure: function (response) {
            window.location = ;
        },
        complete: function () {
            desbloquearPantalla();
        }
    });


}