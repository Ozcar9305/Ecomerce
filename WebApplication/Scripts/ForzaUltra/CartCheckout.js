$(function () {

    var cart_checkout = (function () {

        var $shopping_cart_body = $('#shopping-cart-body'),
            $shopping_cart_items = $('#shopping-cart-items').html(),
            $shopping_cart_footer = $('#shopping-cart-footer'),
            $shopping_cart_footer_content = $('#shopping-cart-footer-content').html(),
            $cart_item_count = 0;

        function delete_cart_item_onClick(e) {
            var cart_identifier = $(this).attr('data-cart-identifier');
            var customer_identifier = $(this).attr('data-customer-identifier');
            var product_identifier = $(this).attr('data-product-identifier');
            var category_identifier = $(this).attr('data-category-identifier');

            $.ajax({
                type: "POST",
                url: "Cart.aspx/DeleteCartItem",
                data: JSON.stringify({ "cartId": cart_identifier, "customerId": customer_identifier, "productId": product_identifier, "categoryId": category_identifier }),
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                async: false,
                success: function (result) {
                    var response = result.d;
                    if (response.Success) {
                        toastr.success("El producto se ha removido de tu carrito de compras.");
                        get_cart_items();
                    } else {
                        toastr.error("Error al eliminar el producto del carrito.");
                    }
                },
                failure: function () {
                    toastr.error("Error al eliminar el producto del carrito.");
                }
            });
        }

        function update_cart_item(cartId, customerId, productId, categoryId, quantity, size) {
            $.ajax({
                type: "POST",
                url: "Cart.aspx/UpdateCartItem",
                data: JSON.stringify({ "cartId": cartId, "customerId": customerId, "productId": productId, "categoryId": categoryId, "quantity": quantity, "size": size }),
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                async: false,
                success: function (result) {
                    var response = result.d;
                    if (response.Success) {
                        get_cart_items();
                    } else {
                        toastr.error("Error al actualizar el producto del carrito.");
                    }
                },
                failure: function () {
                    toastr.error("Error al actualizar el producto del carrito.");
                }
            });
        }

        function plus_size_onClick() {
            var cart_identifier = $(this).attr('data-cart-identifier');
            var customer_identifier = $(this).attr('data-customer-identifier');
            var product_identifier = $(this).attr('data-product-identifier');
            var category_identifier = $(this).attr('data-category-identifier');
            var product_size = $('.dropdown-sizes-for[data-product-identifier="' + product_identifier + '"]').val();

            var quantity = parseInt($('#quantity_for_' + product_identifier).val());

            quantity += 1;

            update_cart_item(cart_identifier, customer_identifier, product_identifier, category_identifier, quantity, product_size);
        }

        function minus_size_onClick() {
            var cart_identifier = $(this).attr('data-cart-identifier');
            var customer_identifier = $(this).attr('data-customer-identifier');
            var product_identifier = $(this).attr('data-product-identifier');
            var category_identifier = $(this).attr('data-category-identifier');
            var product_size = $('.dropdown-sizes-for[data-product-identifier="' + product_identifier + '"]').val();

            var quantity = parseInt($('#quantity_for_' + product_identifier).val());

            if (quantity > 1) {
                quantity -= 1;
                update_cart_item(cart_identifier, customer_identifier, product_identifier, category_identifier, quantity, product_size);
            } else {
                toastr.info("La cantidad minima es 1.");
            }
        }

        function dropdown_sizes_onChange() {
            var cart_identifier = $(this).attr('data-cart-identifier');
            var customer_identifier = $(this).attr('data-customer-identifier');
            var product_identifier = $(this).attr('data-product-identifier');
            var category_identifier = $(this).attr('data-category-identifier');
            var product_size = $('.dropdown-sizes-for[data-product-identifier="' + product_identifier + '"]').val();
            var quantity = parseInt($('#quantity_for_' + product_identifier).val());

            update_cart_item(cart_identifier, customer_identifier, product_identifier, category_identifier, quantity, product_size);
        }

        function pay_order_onClick() {

            if ($cart_item_count > 0) {

                let paymentType = parseInt($(this).attr('data-payment-type'));

                $.ajax({
                    type: "POST",
                    url: "Cart.aspx/OrderExecute",
                    data: JSON.stringify({ "paymentType": paymentType }),
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (result)
                    {
                        console.log('result', result);

                        var response = result.d;
                        if (response.Success) {
                            if (paymentType === 0) {
                                let paypal_href = response.Result.PayPalUrlList.find(element => element.Rel === 'approval_url');
                                console.log(paypal_href);
                                window.location.href = paypal_href.HReference;
                            } else {
                                toastr.success("La orden se genero correctamente, se le ha envíado un correo y pronto un asesor de ventas se pondrá en contacto con usted.")
                                get_cart_items();
                            }
                        } else {
                            toastr.error("Error al generar la orden.");
                        }
                    },
                    failure: function () {
                        toastr.error("Error al generar la orden.");
                    }
                });
            } else {
                toastr.error("Es necesario agregar productos al carrito.");
            }
        }

        function bindEvents(result) {
            $('.delete-cart-item').bind('click', delete_cart_item_onClick);
            $('.plus-size').bind('click', plus_size_onClick);
            $('.minus-size').bind('click', minus_size_onClick);
            $('.dropdown-sizes-for').bind('change', dropdown_sizes_onChange);
            if (result.length > 0) {
                $.each(result, function (key, value) {
                    var sizeIdentifier = (value.ProductCatalog.Sizes !== null && value.ProductCatalog.Sizes.length > 0) ? value.ProductCatalog.Sizes[0].Identifier : 5;
                    $('.dropdown-sizes-for[data-product-identifier=' + value.ProductCatalog.Identifier + ']').children("option[value=" + sizeIdentifier + "]").attr('selected', 'selected');
                });
            }
            $('.pay-order-forza').bind('click', pay_order_onClick);
        }

        function get_cart_items() {
            $.ajax({
                type: "POST",
                url: "Cart.aspx/CartItemGetList",
                data: "",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                async: false,
                success: function (result) {
                    var response = result.d;
                    response.Total = 0;

                    var compile = Handlebars.compile($shopping_cart_items);
                    var compileFooter = Handlebars.compile($shopping_cart_footer_content);
                    $shopping_cart_body.empty();
                    $shopping_cart_footer.empty();
                    if (response.Result.length === 0) {
                        toastr.info("Tu carrito de compras esta vacío!");
                    }
                    else {
                        $cart_item_count = response.Result.length;
                        $.each(response.Result, function (key, value) {
                            response.Total += value.ProductCatalog.Price * value.Quantity;
                            $.each(value.ProductCatalog.Sizes, function (key, value2) {
                                if (value.SizeId === value2.Identifier) {
                                    value2.Selected = true;
                                } else {
                                    value2.Selected = false;
                                }
                            });
                        });
                    }

                    $shopping_cart_body.append(compile(response));
                    console.log(response);
                    $shopping_cart_footer.append(compileFooter({ Total: response.Total }));

                    bindEvents(response.Result);
                    $.each(response.Result, function (key, value) {
                        var dll = $('.dropdown-sizes-for[data-product-identifier="' + value.ProductCatalog.Identifier + '"]');
                        dll.val(value.SizeId);
                    });
                },
                failure: function () {
                    toastr.error("Error al consultar los elementos del carrito de compras.");
                }
            });
        }

        function setSubscriptions() {

        }

        return {
            initialize: function () {
                $.publish('cart-elements-count:onChange');
                get_cart_items();
                setSubscriptions();
            }
        };
    })();

    cart_checkout.initialize();

});