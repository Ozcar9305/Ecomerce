<%@ Page Title="Registro" Language="C#" AutoEventWireup="true" CodeBehind="RegisterForm.aspx.cs" Inherits="WebApplication.ForzaUltra.RegisterForm" %>

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

    </style>
    <form id="frmRegister">
        <div class="body-content">
            <div class="container borderForm">
                <div class="row">
                    <div class="col-lg-8 col-lg-offset-2">
                        <div class="row" style="background-color:gray; border-bottom:5px solid crimson;">
                            <div class="col-md-12 text-center">
                                <img src="<%:ResolveUrl("~/Images/ForzaUltra/Site/fu_logo.png") %>" alt="Forza Ultra" width="200px" height="150px" />
                            </div>
                        </div>
                        <br />
                        <div class="controls">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <span class="formValidateError">*</span>
                                        <label for="form_name">Nombre(s)</label>
                                        <input id="txtFirstName" type="text" name="firstName" class="form-control" maxlength="30" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <span class="formValidateError">*</span>
                                        <label for="form_lastname">Apellido(s)</label>
                                        <input id="txtLastName" type="text" name="lastName" class="form-control" maxlength="30" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <span class="formValidateError">*</span>
                                        <label for="password">Contraseña</label>
                                        <input id="password" type="password" name="password" class="form-control" maxlength="10" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <span class="formValidateError">*</span>
                                        <label for="txtConfirmPassword">Confirmar contraseña</label>
                                        <input id="txtConfirmPassword" type="password" name="confirmPassword" class="form-control" maxlength="10" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <span class="formValidateError">*</span>
                                        <label for="txtEmail">Correo Electr&oacute;nico</label>
                                        <input id="txtEmail" type="email" name="email" class="form-control" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtPhone">Tel&eacute;fono</label>
                                        <input id="txtPhone" type="tel" name="phone" class="form-control" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label for="txtAddress">Direcci&oacute;n de entrega</label>
                                        <textarea id="txtAddress" name="message" class="form-control" rows="3"></textarea>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label for="txtRfc">RFC (En caso de requerir factura)</label>
                                        <input id="txtRfc" type="text" name="rfc" class="form-control" maxlength="13" />
                                    </div>
                                </div>
                                <div class="col-md-12 text-center">
                                    <input id="btnGoBack" type="button" class="btn btn-default" value="&laquo; Volver a la tienda" />
                                    <input id="btnRegisterUser" name="register" type="button" class="btn btn-danger btn-send" value="Registrarme">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <script type="text/javascript" src="../Scripts/ForzaUltra/RegisterForm.js"></script>
        </div>
    </form>
</body>
</html>
