<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterForm.aspx.cs" Inherits="WebApplication.ForzaUltra.RegisterForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <br />
    <div class="container">
        <div class="row">
            <div class="col-lg-8 col-lg-offset-2">
                <div class="controls">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="form_name">* Nombre(s)</label>
                                <input id="txtFirstName" type="text" name="firstName" class="form-control">
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="form_lastname">* Apellido(s)</label>
                                <input id="txtLastName" type="text" name="lastName" class="form-control">
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="form_email">* Correo Electr&oacute;nico</label>
                                <input id="txtEmail" type="email" name="email" class="form-control">
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="form_phone">Tel&eacute;fono</label>
                                <input id="form_phone" type="tel" name="phone" class="form-control">
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label for="form_message">Direcci&oacute;n de entrega</label>
                                <textarea id="form_message" name="message" class="form-control" rows="4"></textarea>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                Aqui va el captcha
                               <%-- <div class="g-recaptcha" data-sitekey="6LfKURIUAAAAAO50vlwWZkyK_G2ywqE52NU7YO0S" data-callback="verifyRecaptchaCallback" data-expired-callback="expiredRecaptchaCallback"></div>
                                <input class="form-control hidden" data-recaptcha="true" required data-error="Please complete the Captcha">
                                <div class="help-block with-errors"></div>--%>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <input id="btnRegisterUser" type="button" class="btn btn-danger btn-send" value="Registrarme">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="../Scripts/ForzaUltra/RegisterForm.js"></script>
</asp:Content>
