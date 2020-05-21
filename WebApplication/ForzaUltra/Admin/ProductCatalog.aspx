<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductCatalog.aspx.cs" Inherits="WebApplication.ForzaUltra.ProductCatalog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <br />
    <br />
    <div class="row">
        <div class="container">
            <div class="col-lg-12">
                <div class="row">
                    <div class="col-lg-6">
                        <h2>Administración de Productos</h2>
                        <hr />
                    </div>
                    <div class="col-lg-6">
                        <a class="btn btn-sm btn-danger pull-right" id="btnNewProcut" data-toggle="modal" data-target="#basicExampleModal"><i class="fa fa-plus-circle"></i>&nbsp;Nuevo</a>
                    </div>
                </div>
                <hr />
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
    <div class="row">
        <div class="container">
            <div class="col-lg-12 text-center">
                <div class="pagination">
                    <a href="#" class="first" data-action="first">&laquo;</a>
                    <a href="#" class="previous" data-action="previous">&lsaquo;</a>
                    <input type="text" readonly="readonly" data-max-page="40" />
                    <a href="#" class="next" data-action="next">&rsaquo;</a>
                    <a href="#" class="last" data-action="last">&raquo;</a>
                </div>
            </div>
        </div>
    </div>
    <!-- Button trigger modal -->
    <div class="modal fade" id="mergeProductModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header nav-item-forza">
                    <h5 class="modal-title" id="mergeProductModalTitle">Nuevo producto</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <%--<label for="recipient-name" class="col-form-label">Nombre:</label>--%>
                        <input type="hidden" id="productIdentifier" />
                        <input type="text" class="form-control" id="txtProductName" placeholder="Nombre">
                    </div>
                    <div class="form-group">
                        <%--<label for="recipient-name" class="col-form-label">Descripción:</label>--%>
                        <input type="text" class="form-control" id="txtDescription" placeholder="Descripción">
                    </div>
                    <div class="form-group">
                        <%--<label for="recipient-name" class="col-form-label">Categoría:</label>--%>
                        <select id="ddlCategory" class="custom-select">
                            <option value="" disabled selected>Selecciona una categoría</option>
                        </select>
                    </div>
                    <div class="form-group">
                        <%--<label for="message-text" class="col-form-label">Precio:</label>--%>
                        <input type="number" class="form-control" id="txtProductPrice" placeholder="Precio" />
                    </div>
                    <div class="form-inline">
                        <label for="message-text" class="col-form-label">Seleccionar tallas disponibles:</label>
                    </div>
                    <div class="form-inline" style="width: 100%!important">
                        <div class="form-check">
                            <input type="checkbox" class="form-check-input" value="1">
                            <label class="form-check-label" for="materialUnchecked">CH&nbsp&nbsp</label>
                        </div>
                        <div class="form-check">
                            <input type="checkbox" class="form-check-input" value="2">
                            <label class="form-check-label" for="materialUnchecked">M&nbsp&nbsp</label>
                        </div>
                        <div class="form-check">
                            <input type="checkbox" class="form-check-input" value="3">
                            <label class="form-check-label" for="materialUnchecked">G&nbsp&nbsp</label>
                        </div>
                        <div class="form-check">
                            <input type="checkbox" class="form-check-input" value="4">
                            <label class="form-check-label" for="materialUnchecked">EG&nbsp&nbsp</label>
                        </div>
                        <div class="form-check">
                            <input type="checkbox" class="form-check-input" value="5">
                            <label class="form-check-label" for="materialUnchecked">UNITALLA&nbsp&nbsp</label>
                        </div>
                        <div class="form-check">
                            <input type="checkbox" class="form-check-input" value="6">
                            <label class="form-check-label" for="materialUnchecked" aria-checked="true">NA</label>
                        </div>
                    </div>
                    <br />
                    <div class="custom-file">
                        <input type="file" class="custom-file-input" id="customFileLang" lang="es" accept="image/x-png,image/jpeg">
                        <label class="custom-file-label" for="customFileLang">Seleccionar Archivo</label>
                    </div>
                    <div class="form-group">
                        <img id="imageProductPreview" src="#" hidden />
                        <input type="hidden" id="imageProductPreviewBytes" />
                        <input type="hidden" id="fileNameProductCatalog" />
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-sm btn-outline-primary" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-sm btn-outline-danger" id="btnSaveObjectProduct">Guardar</button>
                </div>
            </div>
        </div>
    </div>

    <script type="text/x-handlebars-template" id="mergeProductTemplate">
    </script>

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
                                    <td class="text-center">
                                        <a>
                                            <i class="fa fa-pencil-square edit-product" style="width: 25px; height: 25px;" data-idproduct="{{Identifier}}" aria-hidden="true"></i>
                                        </a>
                                    </td>
                                </tr>
                                {{/each}}
                            </tbody>
                        </table>

                    </div>
                </div>
            </div>
        </div>

    </script>

    <script type="text/x-handlebars-template" id="ddlCategoryTemplate">
        <option value="0">Seleccionar categoría</option>
        {{#each Result}}
            <option value="{{Identifier}}">{{Name}}
            </option>
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


            var $gridProductCatalogTemplate = $('#gridProductCatalogTemplate').html(),
                $ddlCategoryTemplate = $('#ddlCategoryTemplate').html(),
                $griProductCatalog = $('#griProductCatalog'),
                $modalProductCatalogMerge = $('#mergeProductModal'),
                $btnNewProcut = $('#btnNewProcut'),
                $btnSaveObjectProduct = $('#btnSaveObjectProduct');

            var request = {
                Paging: {
                    PageNumber: 1,
                    PageSize: 10
                },
                WordFilter: '',
                Item: {
                    Identifier: 0
                }
            };

            function PaginatorInit(response) {
                var $estatusPedidoSelectPickerTemplate = $('.pagination');
                var maxPage = (response.PageSize > response.TotalRecords) ? 1 : (Math.floor(response.TotalRecords / response.PageSize)) + (((response.TotalRecords % response.PageSize) > 0) ? 1 : 0);

                $estatusPedidoSelectPickerTemplate.jqPagination({
                    link_string: '/?page={page_number}',
                    max_page: maxPage,
                    paged: function (page) {
                        $('.log').prepend('<li>Requested page ' + page + '</li>');
                        if (page <= maxPage) {
                            $.publish('pagination-component:onChange', { PageNumber: page });
                        }
                    }
                });
            }

            function readURL(input) {
                if (input.files && input.files[0]) {
                    var reader = new FileReader();

                    reader.onload = function (e) {
                        $('#imageProductPreview').attr('src', e.target.result);
                        $('#imageProductPreviewBytes').val(/base64,(.+)/.exec(e.target.result)[1]);
                    }

                    reader.readAsDataURL(input.files[0]); // convert to base64 string
                }
            }

            $('.custom-file-input').bind('change', function (e) {
                readURL(this);
                var fileName = document.getElementById("customFileLang").files[0].name;
                console.log(document.getElementById("customFileLang").files[0]);
                var nextSibling = e.target.nextElementSibling
                nextSibling.innerText = fileName;
                $('#fileNameProductCatalog').val(fileName);
            });

            function btnNewProduct_onClick() {
                $modalProductCatalogMerge.modal('show');
                getListCategory();
                clearProductModal();    
            }

            function clearProductModal() {
                $('#productIdentifier').val('');
                $('#txtProductName').val('');
                $('#txtDescription').val('');
                $('#txtProductPrice').val('');

                $('.form-check-input:checkbox').each(function () {
                    $(this).prop('checked', false);
                });
            }

            function btnSaveObjectProduct_onClick() {
                var $txtProductName = $('#txtProductName'),
                    $txtDescription = $('#txtDescription'),
                    $ddlCategory = $('#ddlCategory'),
                    $txtProductPrice = $('#txtProductPrice');

                if ($txtProductName.val() === "") {
                    toastr.error("El campo Nombre es requerido.");
                    $txtProductName.focus();
                    return false;
                }

                if ($txtDescription.val() === "") {
                    toastr.error("El campo Descripción es requerido.");
                    $txtDescription.focus();
                    return false;
                }

                if ($ddlCategory.val() === 0 || $ddlCategory.val() === '') {
                    toastr.error("El campo Categoría es requerido.");
                    $ddlCategory.focus();
                    return false;
                }

                if ($txtProductPrice.val() === "") {
                    toastr.error("El campo Precio es requerido.");
                    $txtProductPrice.focus();
                    return false;
                }
                if (isNaN($txtProductPrice.val())) {
                    toastr.error("El Precio solo debe contener números.");
                    $txtProductPrice.val('');
                    $txtProductPrice.focus();
                    return false;
                }
                if (parseInt($txtProductPrice.val()) === 0) {
                    toastr.error("El Precio sdebe ser mayor a $0.00.");
                    $txtProductPrice.focus();
                    return false;
                }

                var sizes = [];
                $('.form-check-input:checkbox:checked').each(function () {
                    var sizeIdentifier = $(this).val();
                    var size = {
                        Identifier: sizeIdentifier
                    };
                    sizes.push(size);
                });

                if (sizes.length === 0) {
                    toastr.error("Es necesario seleccionar por lo menos una talla.");
                    return false;
                }

                var imageBase64 = '';
                if ($('#imageProductPreview').attr('src') !== undefined && $('#imageProductPreview').attr('src') !== '') {
                    //imageBase64 = document.getElementById("imageProductPreview").src;
                    imageBase64 = $('#imageProductPreviewBytes').val();
                }

                var hdIdentifier = 0

                if ($('#productIdentifier').val() !== '') {
                    hdIdentifier = parseInt($('#productIdentifier').val());
                }

                var product =
                {
                    Identifier: hdIdentifier,
                    ProductCategoryIdentifier: $ddlCategory.val(),
                    ShortName: $txtProductName.val(),
                    Description: $txtDescription.val(),
                    AditionalDescription: '',
                    Price: $txtProductPrice.val(),
                    ImageName: '',
                    Sizes: sizes,
                    ApplyDiscount: false,
                    DiscountAmount: 0,
                    Status: true,
                    ImageBase64: imageBase64,
                    ImageName: $('#fileNameProductCatalog').val()
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
                            $modalProductCatalogMerge.modal('hide');
                            toastr.success("La información se guardo correctamente.");
                        }
                    },
                    failure: function () {
                        toastr.error("Error al guardar la información.");
                    }
                });
            }

            function bindEvents() {
                $btnNewProcut.bind('click', btnNewProduct_onClick);
                $btnSaveObjectProduct.bind('click', btnSaveObjectProduct_onClick);
            }

            function edit_onClick() {
                var idProduct = $(this).attr('data-idProduct');
                $.ajax({
                    type: "POST",
                    url: "ProductCatalog.aspx/GetItem",
                    data: JSON.stringify({ 'productIdentifier': idProduct }),
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (response) {
                        if (response.d.Success) {
                            $modalProductCatalogMerge.modal('show');
                            getListCategory();
                            console.log(response.d);
                            $('#mergeProductModalTitle').html('Editar');
                            $('#productIdentifier').val(response.d.Result.Identifier);
                            $('#txtProductName').val(response.d.Result.ShortName);
                            $('#txtDescription').val(response.d.Result.Description);
                            $('#txtProductPrice').val(response.d.Result.Price);
                            $('#ddlCategory').val(response.d.Result.ProductCategoryIdentifier);
                        }
                    },
                    failure: function () {
                        toastr.error("Error al consultar los datos del producto");
                    }
                });
            }

            function getData() {
                $.ajax({
                    type: "POST",
                    url: "ProductCatalog.aspx/GetList",
                    data: JSON.stringify({ "request": request }),
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (response) {
                        if (response.d.Success) {
                            PaginatorInit(response.d.Paging);
                            var compile = Handlebars.compile($gridProductCatalogTemplate);
                            $griProductCatalog.empty();
                            $griProductCatalog.append(compile(response.d));
                            $('.edit-product').bind('click', edit_onClick);
                        }
                    },
                    failure: function () {
                        alert("Sorry,there is a error!");
                    }
                });
            }

            function getListCategory() {

                $.ajax({
                    type: "POST",
                    url: "Categories.aspx/CategoryGetList",
                    data: "",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (response) {
                        if (response.d.Success) {
                            console.log(response.d);
                            var compile = Handlebars.compile($ddlCategoryTemplate);
                            $('#ddlCategory').empty();
                            $('#ddlCategory').append(compile(response.d));
                            $('.selectpicker').selectpicker('refresh');
                        }
                    },
                    failure: function () {
                        alert("Sorry,there is a error!");
                    }
                });
            }

            function pagination_onChange(event, response) {
                request.Paging.PageNumber = response.PageNumber;
                getData();
            }

            bindEvents();
            getData();
            $.subscribe('pagination-component:onChange', pagination_onChange);
        })();

    </script>
</asp:Content>
