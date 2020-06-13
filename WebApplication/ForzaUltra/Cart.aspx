<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="WebApplication.ForzaUltra.Cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="../Content/ForzaUltra/cart.css" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <br />
    <main>
        <div class="container">
            <div class="card shopping-cart">
                <div class="card-header bg-dark text-light">
                    <i class="fa fa-shopping-cart" aria-hidden="true"></i>
                    Carrito de compras
                    <a href="<%:ResolveUrl("~/ForzaUltra/Store.aspx") %>" class="btn btn-light btn-sm pull-right">Continuar comprando</a>
                    <div class="clearfix"></div>
                </div>
                <div class="card-body" id="shopping-cart-body">
                </div>
                <div class="card-footer" id="shopping-cart-footer">
                </div>
            </div>
        </div>
    </main>

    <script type="text/x-handlebars-template" id="shopping-cart-items">
        {{#each Result}}
        <!-- PRODUCT -->
        <div class="row">
            <div class="col-12 col-sm-12 col-md-2 text-center">
                <img class="img-responsive" src="../Images/ForzaUltra/Upload/{{ProductCatalog.ImageName}}" style="object-fit: contain!important" alt="prewiew" width="120" height="80">
            </div>
            <div class="col-12 text-sm-center col-sm-12 text-md-left col-md-4">
                <h4 class="product-name"><strong>{{ProductCatalog.ShortName}}</strong></h4>
                <h4>
                    <small>{{ProductCatalog.Description}}</small>
                </h4>
            </div>
            <div class="col-12 col-sm-12 col-md-2">
                <select class="custom-select dropdown-sizes-for" data-cart-identifier="{{Identifier}}" data-customer-identifier="{{Customer.Identifier}}" data-product-identifier="{{ProductCatalog.Identifier}}" data-category-identifier="{{ProductCategory.Identifier}}">
                    {{#each ProductCatalog.Sizes}}
                            <option value="{{Identifier}}">{{Abreviature}}</option>
                    {{/each}}
                </select>
            </div>
            <div class="col-12 col-sm-12 text-sm-center col-md-4 text-md-right row">
                <div class="col-2 col-sm-2 col-md-5 text-md-right" style="padding-top: 5px">
                    <h6><strong>{{numberFormat ProductCatalog.Price}} <span class="text-muted">x</span></strong></h6>
                </div>
                <div class="col-3 col-sm-3 col-md-3">
                    <div class="quantity">
                        <input type="button" value="+" class="plus plus-size" data-cart-identifier="{{Identifier}}" data-customer-identifier="{{Customer.Identifier}}" data-product-identifier="{{ProductCatalog.Identifier}}" data-category-identifier="{{ProductCategory.Identifier}}">
                        <input type="number" step="1" max="99" min="1" value="{{Quantity}}" title="Qty" class="qty" readonly id="quantity_for_{{ProductCatalog.Identifier}}">
                        <input type="button" value="-" class="minus minus-size" data-cart-identifier="{{Identifier}}" data-customer-identifier="{{Customer.Identifier}}" data-product-identifier="{{ProductCatalog.Identifier}}" data-category-identifier="{{ProductCategory.Identifier}}">
                    </div>
                </div>
                <div class="col-2 col-sm-2 col-md-2 text-center">
                    <button type="button" class="btn btn-sm btn-danger delete-cart-item" data-cart-identifier="{{Identifier}}" data-customer-identifier="{{Customer.Identifier}}" data-product-identifier="{{ProductCatalog.Identifier}}" data-category-identifier="{{ProductCategory.Identifier}}">
                        <i class="fa fa-trash" aria-hidden="true"></i>
                    </button>
                </div>
            </div>
        </div>
        <hr>
        {{/each}}        
    </script>
    <script type="text/x-handlebars-template" id="shopping-cart-footer-content">
        <div class="pull-right" style="margin-left: 10px;">
            <a class="d-inline btn paypal-button pay-order-forza" data-payment-type="0" style="min-height: 30px!important">
                <span class="paypal-button-title">Pagar con
                </span>
                <span class="paypal-logo">
                    <i>Pay</i><i>Pal</i>
                </span>
            </a>
            <a class="d-inline btn btn-lg btn-success  payorder-button pay-order-forza" data-payment-type="1" style="margin-left: 5px!important; margin-top: 12px!important">
                <span>
                    <i></i>
                    <i></i>
                </span>
                <i class="fa fa-money" aria-hidden="true"></i>&nbsp;Generar Orden
            </a>
            <div class="pull-right" style="margin: 5px">
                Total: <b>${{numberFormat Total}}</b>
            </div>
        </div>
    </script>
    <script type="text/x-handlebars-template" id="paypal-response-message">
        <div class="row">
            <div class="col-lg-12 col-sm-12 col-md-2 text-center">
                <h4 class="text-danger">{{Title}}</h4>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12 col-sm-12 col-md-2 text-center">
                <h6 class="text-dark">{{Message}}</h6>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12 col-sm-12 col-md-2 text-center">
                <a class="btn btn-danger" href="/Store.aspx">Volver a la página principal</a>
            </div>
        </div>
    </script>
    <script type="text/javascript" src='<%:ResolveUrl("~/Scripts/ForzaUltra/CartCheckout.js") %>'></script>
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

    </script>
</asp:Content>
