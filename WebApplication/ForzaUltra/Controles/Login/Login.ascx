<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="WebApplication.ForzaUltra.Controles.Login.Login" %>

<style>
    .modal-dialog.login {
        width: 375px !important;
    }

    .modal-header, h4, .close {
        color: #111 !important;
        text-align: center;
        font-size: 26px;
    }

    .modal-footer {
        background-color: #f9f9f9;
        text-align: center;
    }

    .error{
        color: crimson;
    }

    .errorBorder{
        border:1px solid crimson;
    }
    
</style>
<!-- Modal -->
<div class="modal fade" id="loginModal" role="dialog">
    <div class="login modal-dialog">
        <!-- Modal content-->
        <div class="modal-content">
            <div class="modal-header">
                <h4 class="modal-title text-light">
                    <span id="spnModalTitle">Tu cuenta Forza Ultra</span>
                </h4>
                <button type="button" class="close" style="color: #fff!important;" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
            </div>
            <div class="modal-body" style="padding: 40px 50px;">
                <form role="form">
                    <div class="form-group">
                        <asp:Label ID="lblEmail" runat="server" AssociatedControlID="usrname" Text="Correo Electrónico"></asp:Label>
                        <asp:TextBox runat="server" class="form-control" ID="usrname" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <div id="divPasswordWrapper">
                        <div class="form-group">
                            <asp:Label ID="lblPassword" runat="server" AssociatedControlID="psw" Text="Contraseña"></asp:Label>
                            <asp:TextBox runat="server" type="password" class="form-control" ID="psw" ClientIDMode="Static"></asp:TextBox>
                        </div>
                        <div class="checkbox">
                            <label>
                                <asp:CheckBox ID="chkRememberMe" runat="server" value="" Text="Recuérdame" /></label>
                        </div>
                    </div>
                    <asp:Button runat="server" 
                        ID="btnValidateLogin" 
                        class="btn btn-block" 
                        style="background-color: crimson; color: #ffffff;" 
                        Text="INICIAR SESION" 
                        ClientIDMode="Static"
                        OnClick="btnValidateLogin_Click"/>
                    <asp:HiddenField runat="server" ID="hdnLoginButtonAction" ClientIDMode="Static" value="Login" />
                </form>
            </div>
            <div class="modal-footer">
                <p>¿A&uacute;n no eres miembro? <a href='<%:ResolveUrl("~/ForzaUltra/RegisterForm.aspx") %>'>Registrate</a></p>
                <br />
                <p>¿Olvidaste tu <a id="lnkForgotPassword" href="#">Contraseña</a>?</p>
            </div>
        </div>
    </div>
</div>
<script type="text/javascript" src="<%:ResolveUrl(string.Format("~/Scripts/ForzaUltra/Login.js?{0}", DateTime.Now.Ticks)) %>"></script>