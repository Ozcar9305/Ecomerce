<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="WebApplication.ForzaUltra.ContactUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <br />
    <br />
    <br />
    <section id="contact">
        <div class="container">
            <h3 class="text-center text-uppercase">Cont&aacute;ctanos</h3>
            <%--<p class="text-center w-75 m-auto">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris interdum purus at sem ornare sodales. Morbi leo nulla, pharetra vel felis nec, ullamcorper condimentum quam.</p>--%>
            <div class="row">
                <div class="col-sm-12 col-md-6 col-lg-3 my-5">
                    <div class="card border-0">
                        <div class="card-body text-center">
                            <i class="fa fa-phone fa-5x mb-3" aria-hidden="true"></i>
                            <h4 class="text-uppercase mb-5">Oficinas centrales M&eacute;xico</h4>
                            <address>
                                T&eacute;lefono y Whatsapp
                            </address>
                            <p>55 1122 1514</p>
                            <p>55 1286 1728</p>
                        </div>
                    </div>
                </div>
                <div class="col-sm-12 col-md-6 col-lg-3 my-5">
                    <div class="card border-0">
                        <div class="card-body text-center">
                            <i class="fa fa-map-marker fa-5x mb-3" aria-hidden="true"></i>
                            <h4 class="text-uppercase mb-5">QUER&Eacute;TARO DE ARTEAGA</h4>
                            <address>
                                Quer&eacute;taro
                                <br />
                                C.P 76500
                            </address>
                            <p>Tel: 4411036705</p>
                        </div>
                    </div>
                </div>
                <div class="col-sm-12 col-md-6 col-lg-3 my-5">
                    <div class="card border-0">
                        <div class="card-body text-center">
                            <i class="fa fa-map-marker fa-5x mb-3" aria-hidden="true"></i>
                            <h4 class="text-uppercase mb-5">OFFICE US</h4>
                            <address>
                                2521 N SANTA FE AV<br />
                                Campton CA 90222<br />
                                US
                            </address>
                            <p>Tel: 🕂1 323 830 6980</p>
                        </div>
                    </div>
                </div>
                <div class="col-sm-12 col-md-6 col-lg-3 my-5">
                    <div class="card border-0">
                        <div class="card-body text-center">
                            <i class="fa fa-globe fa-5x mb-3" aria-hidden="true"></i>
                            <h4 class="text-uppercase mb-5">Correo electr&oacute;nico</h4>
                            <p>ventas@forzaltra.com</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
