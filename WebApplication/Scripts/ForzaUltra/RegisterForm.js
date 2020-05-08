
//jQuery.validator.setDefaults({
//    debug: true,
//    success: "valid"
//});

$.validator.setDefaults({
    submitHandler: function () {
        registerUser();
    },
    success: "valid",
    debug: true
});

$(document).ready(function () {
        
    $('#frmStore').validate({
        rules: {
            txtFirstName: "required",
            firstName: "required",
            lastName: "required",
            email: {
                required: true,
                email: true
            }
        },
        messages: {
            firstName: "Por favor ingresa tu Nombre",
            lastName: "Por favor ingresa tu apellido",
            email: "Por favor ingresa un correo eléctronico valido"
        }
        //,
        //errorElement: "em",
        //errorPlacement: function (error, element) {
        //    // Add the `invalid-feedback` class to the error element
        //    error.addClass("invalid-feedback");

        //    if (element.prop("type") === "checkbox") {
        //        error.insertAfter(element.next("label"));
        //    } else {
        //        error.insertAfter(element);
        //    }
        //},
        //highlight: function (element, errorClass, validClass) {
        //    $(element).addClass("is-invalid").removeClass("is-valid");
        //},
        //unhighlight: function (element, errorClass, validClass) {
        //    $(element).addClass("is-valid").removeClass("is-invalid");
        //}
        ,submitHandler: function () {
            registerUser();
        }
    });

    //$('#btnRegisterUser').click(function (event) {
    //    event.preventDefault();
    //    if ($('#frmStore').valid()) {
    //        alert("ok 123");
    //    }
    //    else {
    //        alert("not ok");
    //    }
    //});
});


function registerUser() {
    alert("Success");
};