
let serviceUri = '';
$(document).ready(function () {

    serviceUri = window.location.href + '/';

    let $registerForm = $('#frmRegister');
    $('#txtFirstName').ForceLetterOnly();
    $('#txtLastName').ForceLetterOnly();

    $registerForm.validate({
        errorClass: "formValidateError",
        errorElement: "label",
        rules: {
            firstName: "required",
            lastName: "required",
            email: {
                required: true,
                email: true
            },
            password: "required",
            confirmPassword: {
                equalTo: "#password"
            }
        },
        messages: {
            firstName: "Por favor ingresa tu Nombre",
            lastName: "Por favor ingresa tu apellido",
            email: "Por favor ingresa un correo eléctronico valido",
            password: "Ingresa una contraseña",
            confirmPassword: "El valor de este campo debe ser igual a la contraseña"
        },
        highlight: function (element) {
            $(element).parent().find('input').addClass('error')
        },
        unhighlight: function (element) {
            $(element).parent().find('input').removeClass('error')
        }
    });

    $('#btnRegisterUser').click(function () {
        if ($registerForm.valid()) {
            registerUser();
        }
    });
});


function registerUser() {

    var customerObject = new Object();
    customerObject.FirstName = $('#txtFirstName').val().trim();
    customerObject.LastName = $('#txtLastName').val().trim();
    customerObject.Password = $('#password').val().trim();
    customerObject.ShippingAddress = $('#txtAddress').val().trim();
    customerObject.PhoneNumber = $('#txtPhone').val().trim();
    customerObject.Email = $('#txtEmail').val().trim();
    customerObject.Role = parseInt(1);
    
    $.ajax({
        type: "POST",
        url: serviceUri + 'RegisterUser',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ 'customer': customerObject }),
        success: function (response) {
            var data = response.d;
            if (data.Success) {
                alert('Se ha eliminado el registro correctamente');
                loadCategoryList();
            }
            else {
                alert("No fue posible eliminar el registro");
            }
        },
        failure: function (xhr, textStatus, errorThrown) {
            alert("Fail[LoadCategoryChangeStatus]" + xhr + " " + textStatus + " " + errorThrown);
        }
    });
};