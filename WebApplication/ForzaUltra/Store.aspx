<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Store.aspx.cs" Inherits="WebApplication.ForzaUltra.Store" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--Carousel Wrapper-->
    <div id="carousel-example-1z" class="carousel" data-ride="carousel">

        <!--Slides-->
        <div class="carousel-inner" role="listbox">

            <!--First slide-->
            <div class="carousel-item active">
                <div class="view" style="background-image: url('../Images/ForzaUltra/Site/fu_banner.jpg'); background-repeat: no-repeat; background-size: cover;">

                    <!-- Mask & flexbox options-->
                    <div class="mask d-flex justify-content-center align-items-center">

                        <!-- Content -->
                        <div class="text-center white-text mx-5 wow fadeIn">


                            <img src="../Images/ForzaUltra/Site/fu_logo.png" width="200" height="170" />
                        </div>
                        <!-- Content -->

                    </div>
                    <!-- Mask & flexbox options-->

                </div>
            </div>
            <!--/First slide-->

            <!--Second slide-->
            <div class="text-center">
            </div>
            <!--/Second slide-->

            <!--Third slide-->
            <!--/Third slide-->

        </div>
        <!--/.Slides-->

    </div>
    <!--/.Carousel Wrapper-->

    <main id="catalogMainPage">
    </main>


    <script type="text/x-handlebars-template" id="catalogMainPageTemplate">
        {{#each Result}}
        <div class="container">
            
            <!--Navbar-->
            <nav class="navbar navbar-expand-lg navbar-light lighten-3 mt-3 mb-5" style="box-shadow: none">
                <!-- Navbar brand -->
                <span class="navbar-brand dark-grey-text"><strong>{{Name}}</strong></span>
            </nav>
            <!--/.Navbar-->
           
            <!--Section: Products v.3-->
            <section class="text-center mb-4">

                <!--Grid row-->
                <div class="row wow fadeIn">
                     {{#each ProductList}}
                    <!--Grid column-->
                    <div class="col-lg-3 col-md-6 mb-4">

                        <!--Card-->
                        <div class="card">

                            <!--Card image-->
                            <div class="view overlay">
                                <img src="../Images/ForzaUltra/Upload/{{ImageName}}"  class="card-img-top"
                                    alt="">
                                <a>
                                    <div class="mask rgba-white-slight"></div>
                                </a>
                            </div>
                            <!--Card image-->

                            <!--Card content-->
                            <div class="card-body text-center">
                                <!--Category & Title-->
                                <a class="grey-text">
                                    <h5>{{ShortName}}</h5>
                                </a>
                                <h5>
                                    <strong>
                                        <a  class="dark-grey-text">{{Description}}
                      <%--<span class="badge badge-pill danger-color">NEW</span>--%>
                                        </a>
                                    </strong>
                                </h5>

                                <h4 class="font-weight-bold blue-text">
                                    <strong>${{numberFormat Price}}</strong>
                                </h4>
                                <input type="button" class="addToCart" data-category="{{ProductCategoryId}}" value="Agregar al carrito" />
                            </div>
                            <!--Card content-->

                        </div>
                        <!--Card-->

                    </div>
                    <!--Grid column-->
                    {{/each}}
                </div>
                <!--Grid row-->
            </section>
            <!--Section: Products v.3-->
            
        </div>
        {{/each}}
    </script>

    <script type="text/javascript">

        Handlebars.registerHelper('numberFormat', function (value, options) {
            // Helper parameters
            var dl = options.hash['decimalLength'] || 2;
            var ts = options.hash['thousandsSep'] || ',';
            var ds = options.hash['decimalSep'] || '.';

            // Parse to float
            var value = parseFloat(value);

            // The regex
            var re = '\\d(?=(\\d{3})+' + (dl > 0 ? '\\D' : '$') + ')';

            // Formats the number with the decimals
            var num = value.toFixed(Math.max(0, ~~dl));

            // Returns the formatted number
            return (ds ? num.replace('.', ds) : num).replace(new RegExp(re, 'g'), '$&' + ts);
        });

        (function () {

            var $catalogMainPageTemplate = $('#catalogMainPageTemplate').html(),
                $catalogMainPage = $('#catalogMainPage');


            $.ajax({
                type: "POST",
                url: "Store.aspx/GetStoreGetList",
                data: "",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                async: false,
                success: function (response) {
                    console.log(response.d);
                    var compile = Handlebars.compile($catalogMainPageTemplate);
                    $catalogMainPage.empty();
                    $catalogMainPage.append(compile(response.d));
                },
                failure: function () {
                    toastr.error("Error al consultar los datos");
                }
            });
        })();
    </script>
</asp:Content>
