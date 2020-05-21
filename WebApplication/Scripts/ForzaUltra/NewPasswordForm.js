
let serviceUri = '';
let $token = '';

$(document).ready(function () {

    serviceUri = window.location.href + '/';

    let $registerForm = $('#frmRegister');
    $token = GetUriValues("tk");
    
    $registerForm.validate({
        errorClass: "formValidateError",
        errorElement: "label",
        rules: {
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

    $('#btnRegisterUser').click(function ()
    {
        console.log('token: ' + $token);
        if ($token != undefined) {
            if ($registerForm.valid()) {
                changePassword();
            }
        }
        else {
            swal('Error', 'No es posible actualizar tu contraseña en este momento, por favor intente nuevamente desde tu correo electronico', 'danger');
        }

    });
});


function changePassword()
{
    console.log('changePassword');
    console.log('UpdateCustomerPassword: ' + window.location.href.split("?")[0] + '/' + 'UpdateCustomerPassword');

    var customerObject = new Object();
    customerObject.Email = $('#txtEmail').val().trim();
    customerObject.Password = $('#password').val().trim();
    customerObject.EncryptedPassword = $token + '=';

    $.ajax({
        type: "POST",
        url: window.location.href.split("?")[0] + '/' + 'UpdateCustomerPassword',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ 'customer': customerObject }),
        success: function (response) {
            var data = response.d;
            if (data.Success) {
                swal({
                    title: "Forza Ultra",
                    text: "Tu contraseña ha sido actualizada",
                    type: "success"
                });
            }
            else {
                swal({
                    title: "Ha ocurrido un error",
                    text: "No pudimos actualizar tu contraseña, por favor intenta de nuevo más tarde desde tu correo eléctronico",
                    type: "danger"
                });
            }
        },
        failure: function (xhr, textStatus, errorThrown) {
            console.log("Fail[UpdateCustomerPassword]" + xhr + " " + textStatus + " " + errorThrown);
        }
    });
}; 