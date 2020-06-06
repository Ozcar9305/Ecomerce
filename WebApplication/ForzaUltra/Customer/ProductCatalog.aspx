<%@ Page Title="Productor" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductCatalog.aspx.cs" Inherits="WebApplication.ForzaUltra.Customer.ProductCatalog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="margin-top: 10%!important">
        <div class="row">
            <div class="col-lg-3">
                <h2 class="my-4">Forza Ultra</h2>
                <div class="list-group" id="category-list-content">
                </div>
            </div>

            <div class="col-lg-9">
                <div class="row" id="product-list-content">
                </div>
            </div>
        </div>
    </div>

    <script type="text/x-handlebars-template" id="category-list-template">
        {{#each Result}}
        <a class="list-group-item text-dark" data-category-identifier="{{Identifier}}">{{Name}}</a>
        {{/each}}
    </script>

    <script type="text/x-handlebars-template" id="product-list-template">
        {{#each Result}}
        <div class="col-lg-4 col-md-6 mb-4">
            <div class="card h-100">
                <!--Card image-->
                <div class="view overlay">
                    <img src="/Images/ForzaUltra/Upload/{{ImageName}}" class="card-img-top" style="object-fit: contain!important" height="200" width="150"
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
                        <h6>{{ShortName}}</h6>
                    </a>
                    <h6>
                        <strong>
                            <a class="dark-grey-text">{{Description}}
                            </a>
                        </strong>
                    </h6>

                    <h5 class="font-weight-bold blue-text">
                        <strong>${{numberFormat Price}}</strong>
                    </h5>
                    <a class="btn btn-sm btn-danger product-identifier" data-identifier="{{Identifier}}" data-category="{{ProductCategoryIdentifier}}" data-price="{{Price}}">
                        <i class="fa fa-cart-plus"></i>
                        <span class="clearfix d-none d-sm-inline-block">&nbsp;Agregar</span>
                    </a>
                </div>
                <!--Card content-->
            </div>
        </div>
        {{/each}}
    </script>
    <script type="text/javascript" src='<%:ResolveUrl("~/Scripts/ForzaUltra/Customer/ProductCatalog.js") %>'></script>
</asp:Content>

