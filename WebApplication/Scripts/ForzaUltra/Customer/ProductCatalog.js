$(function () {

    var customer_product_catalog = (function () {

        var $category_list_template = $('#category-list-template').html(),
            $category_list_content = $('#category-list-content'),
            $product_list_template = $('#product-list-template').html(),
            $product_list_content = $('#product-list-content');

        var request_product = {
            productId: 0,
            categoryId: 0,
            wordFilter: '',
            pageNumber: 1,
            pageSize: 100,
            all: false
        };

        function product_onClick(e) {
            var categoryIentifier = $(this).attr('data-category');
            var productIdentifier = $(this).attr('data-identifier');
            var productPrice = $(this).attr('data-price');

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
                url: "ProductCatalog.aspx/CartItemExecute",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ "item": item }),
                async: false,
                success: function (ressult) {
                    var response = ressult.d;
                    if (response.Success) {
                        $.publish('cart-elements-count:onChange');
                        toastr.success("Se agrego un elemento a tu carrito de compras.");
                    } else if (!response.SessionInit) {
                        $("#loginModal").modal();
                    } else {
                        toastr.error("Error al agregar el producto");
                    }
                },
                failure: function () {
                    toastr.error("Error al agregar el producto.");
                }
            });
        }

        function product_get_list() {
            $.ajax({
                type: "POST",
                url: "ProductCatalog.aspx/ProductCatalogGetList",
                data: JSON.stringify(request_product),
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                async: false,
                success: function (ressult) {
                    var response = ressult.d;
                    console.log(response);

                    var compile = Handlebars.compile($product_list_template);
                    $product_list_content.empty();
                    $product_list_content.append(compile(response));
                    $('.product-identifier').bind('click', product_onClick);
                },
                failure: function () {
                    toastr.error("Error al consultar los productos.");
                }
            });
        }

        function category_list_events() {
            $('.list-group-item').bind('click', function () {
                request_product.categoryId = $(this).attr('data-category-identifier');
                product_get_list();
                $('.list-group-item').each(function () {
                    $(this).removeClass('list-group-item-dark');
                });
                $('.list-group-item[data-category-identifier="' + request_product.categoryId + '"]').addClass('list-group-item-dark');
            });
        }

        function category_get_list() {
            $.ajax({
                type: "POST",
                url: "ProductCatalog.aspx/CategoryGetList",
                data: JSON.stringify({ "wordFilter": "" }),
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                async: false,
                success: function (ressult) {
                    var response = ressult.d;
                    if (response.Success) {
                        request_product.categoryId = (response.Result.length > 0) ? response.Result[0].Identifier : 0;
                        product_get_list();
                        var compile = Handlebars.compile($category_list_template);
                        $category_list_content.empty();
                        $category_list_content.append(compile(response));
                        category_list_events();
                        $('.list-group-item[data-category-identifier="' + request_product.categoryId + '"]').addClass('list-group-item-dark');
                    }
                },
                failure: function () {
                    toastr.error("Error al consultar las categorias.");
                }
            });
        }

        function setSubscriptions() {

        }

        Handlebars.registerHelper('numberFormat', function (value, options) {
            // Helper parameters
            var dl = options.hash['decimalLength'] || 2;
            var ts = options.hash['thousandsSep'] || ',';
            var ds = options.hash['decimalSep'] || '.';

            // Parse to float
            var _value = parseFloat(value);

            // The regex
            var re = '\\d(?=(\\d{3})+' + (dl > 0 ? '\\D' : '$') + ')';

            // Formats the number with the decimals
            var num = _value.toFixed(Math.max(0, ~~dl));

            // Returns the formatted number
            return (ds ? num.replace('.', ds) : num).replace(new RegExp(re, 'g'), '$&' + ts);
        });

        return {
            initialize: function () {
                $.publish('cart-elements-count:onChange');
                category_get_list();
                setSubscriptions();
            }
        };
    })();

    customer_product_catalog.initialize();

});