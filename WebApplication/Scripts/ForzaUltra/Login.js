
$(document).ready(function ()
{
    let $usrname = $('#usrname');
    let $password = $("#psw");
    let $loginButton = $('#btnValidateLogin');
    let $loginButtonAction = $('#hdnLoginButtonAction');

    $('#lnkLogin').click(function ()
    {
        console.log('login click');
        $loginButtonAction.val('Login');
        $usrname.val('');
        $password.val('');
        $('#divPasswordWrapper').show();
    });

    $('#btnValidateLogin').click(function (event)
    {
        let loginShouldContinue = true;
        let spanUser = $usrname.nextAll('span:first');
        if (spanUser.length > 0) {
            spanUser.remove();
        }

        let spanPassword = $password.nextAll('span:first');
        if (spanPassword.length > 0) {
            spanPassword.remove();
        }

        if ($usrname.val() === '')
        {
            $usrname.focus();
            $usrname.addClass("errorBorder");
            $usrname.after("<span class='spanUsuer error'>Ingresa tu correo electrónico</span>");
            loginShouldContinue = false;
        }

        if ($password.val() === '' && $loginButtonAction.val() === 'Login')
        {
            $password.focus();
            $password.addClass("errorBorder");
            $password.after("<span id='spanPassword' class='error'>Ingresa tu contraseña</span>");
            loginShouldContinue = false;
        }

        if(loginShouldContinue)
        {
            $usrname.removeClass("errorBorder");
            $password.removeClass("errorBorder");
        }

        if (!isEmail($usrname.val()) && $usrname.val() !== '')
        {
            $usrname.focus();
            $usrname.addClass("errorBorder");
            $usrname.after("<span class='spanUsuer error'>Ingresa un correo electrónico válido</span>");
            loginShouldContinue = false;
        }
        return loginShouldContinue;

    });

    $('#lnkForgotPassword').click(function (event)
    {
        event.preventDefault();
        $('#divPasswordWrapper').hide();
        $loginButton.val('Enviar contraseña');
        $loginButtonAction.val('ForgotPassword');
    });

    $usrname.keypress(function ()
    {
        let span = $(this).nextAll('span:first');
        if (span.length > 0) {
            span.remove();
        }
        $(this).removeClass("errorBorder");
    });

    $password.keypress(function ()
    {
        let span = $(this).nextAll('span:first');
        if (span.length > 0) {
            span.remove();
        }
        $(this).removeClass("errorBorder");
    });

    $usrname.blur(function ()
    {
        if ($(this).val() !== '')
        {
            let span = $(this).nextAll('span:first');
            if (span.length > 0) {
                span.remove();
            }
            $(this).removeClass("errorBorder");        
        }
    });

    $password.blur(function ()
    {
        if ($(this).val() !== '') {
            let span = $(this).nextAll('span:first');
            if (span.length > 0) {
                span.remove();
            }
            $(this).removeClass("errorBorder");
        }
    });

});