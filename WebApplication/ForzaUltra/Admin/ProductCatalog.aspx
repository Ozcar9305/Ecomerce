<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductCatalog.aspx.cs" Inherits="WebApplication.ForzaUltra.ProductCatalog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <br />
    <br />
    <div class="row">
        <div class="container">
            <div class="col-lg-12">
                <div class="col-lg-12">
                    <h2>Administración de Productos</h2>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="container">
            <div class="col-lg-12">
                <div class="col-lg-3"></div>
                <div class="col-lg-3"></div>
                <div class="col-lg-3"></div>
                <div class="col-offset-3 col-lg-3 pull-right">
                    <a class="btn btn-sm btn-danger pull-right" id="btnNewProcut" data-toggle="modal" data-target="#basicExampleModal"><i class="fa fa-plus-circle"></i>&nbsp;Nuevo</a>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="container">
            <div class="col-lg-12">
                <div id="griProductCatalog">
                </div>
            </div>
        </div>
    </div>

    <!-- Button trigger modal -->


    <!-- Central Modal Small -->
    <!--Modal: Login / Register Form-->
    <div class="modal fade" id="modalLRForm" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog cascading-modal" role="document">
            <!--Content-->
            <div class="modal-content">

                <!--Modal cascading tabs-->
                <div class="modal-c-tabs">

                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs md-tabs tabs-2 special-color-dark text-white" role="tablist">
                        <li class="nav-item nav-item-forza">
                            <a class="nav-link text-white" data-toggle="tab" href="#panel7" role="tab"><i class="fa fa-pencil-square-o" aria-hidden="true"></i>
                                Nuevo Producto</a>
                        </li>
                    </ul>

                    <!-- Tab panels -->
                    <div class="tab-content">
                        <!--Panel 7-->
                        <div class="tab-pane fade in show active" id="panel7" role="tabpanel">

                            <!--Body-->
                            <div class="modal-body mb-1">

                                <div class="form-group">
                                    <label data-error="wrong" data-success="right" for="modalLRInput10">Nombre</label>
                                    <%--<i class="fas fa-envelope prefix"></i>--%>
                                    <input type="text" id="txtProductName" class="form-control">
                                    
                                </div>

                                <div class="form-group">
                                    <label data-error="wrong" data-success="right" for="modalLRInput10">Descripción</label>
                                    <%--<i class="fas fa-envelope prefix"></i>--%>
                                    <input type="text" id="txtProductDescription" class="form-control">
                                    
                                </div>

                                <div class="form-group">
                                    <%--<i class="fas fa-lock prefix"></i>--%>
                                    <label data-error="wrong" data-success="right" for="modalLRInput11">Precio</label>
                                    <input type="number" id="txtProductPrice" class="form-control form-control-sm validate">                                    
                                </div>
                                <%--<div class="text-center mt-2">
                                    <button class="btn btn-info">Log in <i class="fas fa-sign-in ml-1"></i></button>
                                </div>--%>
                            </div>
                            <!--Footer-->
                            <div class="modal-footer">
                                <%--<div class="options text-center text-md-right mt-1">
                                    <p>Not a member? <a href="#" class="blue-text">Sign Up</a></p>
                                    <p>Forgot <a href="#" class="blue-text">Password?</a></p>
                                </div>--%>
                                <button type="button" class="btn btn-outline-primary waves-effect" data-dismiss="modal">Cancelar</button>
                                <button type="button" class="btn btn-outline-danger waves-effect ml-auto" id="btnSaveObjectProduct">Guardar</button>
                            </div>

                        </div>
                        <!--/.Panel 7-->
                    </div>

                </div>
            </div>
            <!--/.Content-->
        </div>
    </div>
    <!--Modal: Login / Register Form-->

    <!-- Central Modal Small -->

    <script type="text/x-handlebars-template" id="gridProductCatalogTemplate">
        <div class="col-lg-12">
            <div class="row">
            </div>
            <br />
            <div class="row">
                <div class="col-lg-12">
                    <div class="table-responsive text-nowrap">

                        <table class="table table-bordered table-striped">
                            <thead class="thead thead-dark">
                                <tr>
                                    <th scope="col">No. Producto</th>
                                    <th scope="col">Nombre</th>
                                    <th scope="col">Descripción</th>
                                    <th scope="col">Precio</th>
                                    <th scope="col"></th>
                                </tr>
                            </thead>
                            <tbody>
                                {{#each Result}}                           
                                <tr>
                                    <th scope="row">{{Identifier}}</th>
                                    <td>{{ShortName}}</td>
                                    <td>{{Description}}</td>
                                    <td>{{numberFormat Price}}</td>
                                    <td></td>
                                </tr>
                                {{/each}}
                            </tbody>
                        </table>

                    </div>
                </div>
            </div>
        </div>

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


            var $gridProductCatalogTemplate = $('#gridProductCatalogTemplate').html(),
                $griProductCatalog = $('#griProductCatalog'),
                $modalProductCatalogMerge = $('#modalLRForm'),
                $btnNewProcut = $('#btnNewProcut'),
                $btnSaveObjectProduct = $('#btnSaveObjectProduct');

            function btnNewProduct_onClick() {
                $modalProductCatalogMerge.modal('show');
            }

            function btnSaveObjectProduct_onClick() {
                var product =
                {
                    Identifier: 1000,
                    ProductCategoryIdentifier: 1,
                    ShortName: $('#txtProductName').val(),
                    Description: $('#txtProductDescription').val(),
                    AditionalDescription: '',
                    Price: $('#txtProductPrice').val(),
                    ImageName: '',
                    Sizes: [],
                    ApplyDiscount: false,
                    DiscountAmount: 0,
                    Status: true
                };
                $.ajax({
                    type: "POST",
                    url: "ProductCatalog.aspx/Merge",
                    data: JSON.stringify({ 'product': product }),
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (response) {
                        if (response.d.Success) {
                            getData();
                        }
                    },
                    failure: function () {
                        alert("Sorry,there is a error!");
                    }
                });
            }

            function bindEvents() {
                $btnNewProcut.bind('click', btnNewProduct_onClick);
                $btnSaveObjectProduct.bind('click', btnSaveObjectProduct_onClick);
            }

            function getData() {
                $.ajax({
                    type: "POST",
                    url: "ProductCatalog.aspx/GetList",
                    data: "",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (response) {
                        if (response.d.Success) {
                            console.log(response.d);
                            var compile = Handlebars.compile($gridProductCatalogTemplate);
                            $griProductCatalog.empty();
                            $griProductCatalog.append(compile(response.d));
                        }
                    },
                    failure: function () {
                        alert("Sorry,there is a error!");
                    }
                });
            }

            bindEvents();
            getData();
        })();

    </script>
</asp:Content>
