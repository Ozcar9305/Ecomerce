<%@ Page Title="Actualizar Contraseña" Language="C#" AutoEventWireup="true" CodeBehind="NewPassword.aspx.cs" Inherits="WebApplication.ForzaUltra.NewPassword" %>

<!DOCTYPE html>
<html>
<head>
    <title>Registro</title>
    <%--Scripts--%>
    <script type="text/javascript" src="<%:ResolveUrl("~/Scripts/jquery-3.3.1.min.js") %>"></script>
    <script type="text/javascript" src="<%:ResolveUrl("~/Scripts/common.js") %>"></script>
    <script type="text/javascript" src="<%:ResolveUrl("~/Scripts/sweet-alert.min.js") %>"></script>
    <script type="text/javascript" src="<%:ResolveUrl("~/Scripts/jquery.validate.min.js") %>"></script>
    
    <%--Styles--%>
    <link rel="stylesheet" type="text/css" href="<%:ResolveUrl("~/Content/bootstrap.css") %>" />
    <%--<link rel="stylesheet" type="text/css" href="<%:ResolveUrl("~/Content/sweet-alert.css") %>" />--%>
</head>
<body>
    <style type="text/css">

        .formValidateError {
            color: crimson;
        }

        .borderForm{
            border-width: .2rem
        }

        .error{
            border:1px solid crimson;
        }

        a{
            color:gray !important;
            border-bottom:1px solid gray;
        }

    </style>
    <form id="frmRegister">
        <br />
        <div class="body-content">
            <div class="container borderForm">
                <div class="row">
                    <div class="col-lg-8 col-lg-offset-2">
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <img src="<%:ResolveUrl("~/Images/ForzaUltra/Site/fu_logo.png") %>" alt="Forza Ultra" width="250px" height="200px" />
                                <br />
                            </div>
                        </div>
                        <div class="controls">
                            <div class="row">
                                <div class="col-md-4">
                                    &nbsp;
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <span class="formValidateError">*</span>
                                        <label for="txtEmail">Correo Electr&oacute;nico</label>
                                        <input id="txtEmail" type="email" name="email" class="form-control" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    &nbsp;
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    &nbsp;
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <span class="formValidateError">*</span>
                                        <label for="password">Contraseña</label>
                                        <input id="password" type="password" name="password" class="form-control" maxlength="10" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    &nbsp;
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    &nbsp;
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <span class="formValidateError">*</span>
                                        <label for="txtConfirmPassword">Confirmar contraseña</label>
                                        <input id="txtConfirmPassword" type="password" name="confirmPassword" class="form-control" maxlength="10" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    &nbsp;
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 text-center">
                                    <br />
                                    <input id="btnRegisterUser" name="register" type="button" class="btn btn-danger btn-send" value="Actualizar contraseña">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <script type="text/javascript" src="../Scripts/common.js"></script>
            <script type="text/javascript" src="../Scripts/ForzaUltra/NewPasswordForm.js"></script>
        </div>
    </form>
</body>
</html>