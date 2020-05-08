<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs" Inherits="WebApplication.ForzaUltra.Controles.Login.Login" %>

<style>
    .modal-dialog .login{
        width:375px !important;
    }

    .modal-header, h4, .close {
        color:#111 !important;
        text-align: center;
        font-size: 26px;
    }
    .modal-footer {
        background-color: #f9f9f9;
        text-align:center;
    }
</style>
  <!-- Modal -->
  <div class="modal fade" id="loginModal" role="dialog">
    <div class="login modal-dialog">
      <!-- Modal content-->
      <div class="modal-content">
        <div class="modal-header" style="padding:0px 10px;">
          <button type="button" class="close black" data-dismiss="modal">&times;</button>
          <h4>Tu cuenta <br /> Forza Ultra</h4>
        </div>
        <div class="modal-body" style="padding:40px 50px;">
          <form role="form">
            <div class="form-group">
              <label for="usrname">Direcci&oacute;n de Correo Electr&oacute;nico</label>
              <input type="text" class="form-control" id="usrname">
            </div>
            <div class="form-group">
              <label for="psw">Contraseña</label>
              <input type="password" class="form-control" id="psw" autocomplete="email" autocorrect="off" autocapitalize="off" spellcheck="false">
            </div>
            <div class="checkbox">
              <label><input type="checkbox" value="" checked>Recuerdame</label>
            </div>
            <input id="btnValidateLogin" type="button" class="btn btn-block" style="background-color:crimson; color:#ffffff;" value="INICIAR SESION" />
          </form>
        </div>
        <div class="modal-footer">
          <p>¿A&uacute;n no eres miembro? <a href='<%:ResolveUrl("~/ForzaUltra/RegisterForm.aspx") %>'>Registrate</a></p>
          <p>¿Olvidaste tu <a href="#">Contraseña</a>?</p>
        </div>
      </div>
    </div>
  </div>
