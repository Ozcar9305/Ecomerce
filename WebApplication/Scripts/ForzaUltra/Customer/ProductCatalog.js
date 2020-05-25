$(function () {

    var customer_product_catalog = (function () {

        var $category_list_template = $('#category-list-template').html(),
            $category_list_content = $('#category-list-content'),
            $product_list_template = $('#product-list-template').html(),
            $product_list_content = $('#product-list-content');

        var request_product = {
            productId: 0,
            wordFilter: '',
            pageNumber: 1,
            pageSize: 12,
            all: false
        };

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
                    if (response.Success) {
                        
                        var compile = Handlebars.compile($product_list_template);
                        $product_list_content.empty();
                        $product_list_content.append(compile(response));
                    }
                },
                failure: function () {
                    toastr.error("Error al consultar los productos.");
                }
            });
        }

        function category_get_list() {
            $.ajax({
                type: "POST",
                url: "ProductCatalog.aspx/CategoryGetList",
                data: "",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                async: false,
                success: function (ressult) {
                    var response = ressult.d;
                    if (response.Success) {
                        product_get_list();                                                
                        var compile = Handlebars.compile($category_list_template);
                        $category_list_content.empty();
                        $category_list_content.append(compile(response));
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
            var value = parseFloat(value);

            // The regex
            var re = '\\d(?=(\\d{3})+' + (dl > 0 ? '\\D' : '$') + ')';

            // Formats the number with the decimals
            var num = value.toFixed(Math.max(0, ~~dl));

            // Returns the formatted number
            return (ds ? num.replace('.', ds) : num).replace(new RegExp(re, 'g'), '$&' + ts);
        });

        return {
            initialize: function () {
                category_get_list();
                setSubscriptions();
            }
        };
    })();

    customer_product_catalog.initialize();

});