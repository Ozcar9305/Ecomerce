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

    <a>
        <img class="img-fluid z-depth-1" src="https://mdbootstrap.com/img/screens/yt/screen-video-1.jpg" alt="video"
            data-toggle="modal" data-target="#modal1"></a>
    <!--Modal: Name-->
    <div class="modal fade" id="modal1" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">

            <!--Content-->
            <div class="modal-content">

                <!--Body-->
                <div class="modal-body mb-0 p-0">

                    <div class="embed-responsive embed-responsive-16by9 z-depth-1-half">
                        <iframe class="embed-responsive-item" src="https://www.youtube.com/embed/XxXZGDWUhQQ"></iframe>
                    </div>

                </div>

                <!--Footer-->
                <div class="modal-footer justify-content-center">                    
                    <button type="button" class="btn btn-outline-primary btn-rounded btn-md ml-4" data-dismiss="modal">Close</button>
                </div>

            </div>
            <!--/.Content-->

        </div>
    </div>
    <!--Modal: Name-->

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
                                <img src="../Images/ForzaUltra/Upload/{{ImageName}}" class="card-img-top"
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
                                        <a class="dark-grey-text">{{Description}}</a>
                                    </strong>
                                </h5>

                                <h4 class="font-weight-bold blue-text">
                                    <strong>${{numberFormat Price}}</strong>
                                </h4>
                                <a class="btn btn-sm btn-danger product-identifier" data-identifier="{{Identifier}}" data-category="{{ProductCategoryIdentifier}}" data-price="{{Price}}">
                                    <i class="fa fa-cart-plus"></i>
                                    <span class="clearfix d-none d-sm-inline-block">&nbsp;Agregar</span>
                                </a>
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

            function product_onClick(e) {
                var session = "True"; <%--'<%= HttpContext.Current.Session["SessionInit"] %>';--%>
                var categoryIentifier = $(this).attr('data-category');
                var productIdentifier = $(this).attr('data-identifier');
                var productPrice = $(this).attr('data-price');

                if (session === 'False') {
                    $("#loginModal").modal();
                } else {
                    var item = {
                        Identifier: '',
                        ProductCategory: {
                            Identifier: categoryIentifier
                        },
                        ProductCatalog: {
                            Identifier: productIdentifier,
                            Price: productPrice,
                            Sizes: [
                                {
                                    Identifier: 5
                                }
                            ]
                        },
                        Quantity: 1
                    };

                    $.ajax({
                        type: "POST",
                        url: "Store.aspx/CartItemExecute",
                        data: "",
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify({ "item": item }),
                        async: false,
                        success: function (ressult) {
                            var response = ressult.d;
                            if (response.Success) {
                                $.publish('cart-elements-count:onChange');
                                toastr.success("Se agrego un elemento a tu carrito de compras.");
                            } else {
                                toastr.error("Error al agregar el producto")
                            }
                        },
                        failure: function () {
                            toastr.error("Error al agregar el producto.");
                        }
                    });
                }
            }

            function bindEvents() {
                $('.product-identifier').bind('click', product_onClick);
            }


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
                    bindEvents();
                },
                failure: function () {
                    toastr.error("Error al consultar los datos");
                }
            });
        })();
    </script>
</asp:Content>
