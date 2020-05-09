﻿<%@ Page Title="Registro" Language="C#" AutoEventWireup="true" CodeBehind="RegisterForm.aspx.cs" Inherits="WebApplication.ForzaUltra.RegisterForm" %>

<html>
<head>
    <title>Registro</title>
    <script type="text/javascript" src="<%:ResolveUrl("~/Scripts/jquery-3.3.1.min.js") %>"></script>
    <script type="text/javascript" src="<%:ResolveUrl("~/Scripts/jquery.validate.min.js") %>"></script>
    <script type="text/javascript" src="<%:ResolveUrl("~/Scripts/bootstrap.min.js") %>"></script>
    <script type="text/javascript" src="<%:ResolveUrl("~/Scripts/common.js") %>"></script>
    <link rel="stylesheet" type="text/css" href="<%:ResolveUrl("~/Content/bootstrap.css") %>">
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
                                        <%-- <div class="g-recaptcha" data-sitekey="6LfKURIUAAAAAO50vlwWZkyK_G2ywqE52NU7YO0S" data-callback="verifyRecaptchaCallback" data-expired-callback="expiredRecaptchaCallback"></div>
                                <input class="form-control hidden" data-recaptcha="true" required data-error="Please complete the Captcha">
                                <div class="help-block with-errors"></div>--%>
                                    </div>
                                </div>
                                <div class="col-md-12 text-center">
                                    <input id="btnRegisterUser" name="register" type="button" class="btn btn-danger btn-send" value="Registrarme">
                                </div>
                                <div class="col-md-12 text-center">
                                    <a href="Store.aspx">Regresar</a>
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
