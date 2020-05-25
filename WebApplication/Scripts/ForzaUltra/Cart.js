$(function () {

    var cart = (function () {

        var $cart_lements_count = $('#cart-elements-count');

        function cart_elements_onChange(event) {
            $.ajax({
                type: "POST",
                url: "Store.aspx/CartGetFilteredList",
                data: "",
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                async: false,
                success: function (ressult)
                {
                    var response = ressult.d;
                    if (response.Success) {
                        $cart_lements_count.html(response.Result.length);
                    }
                },
                failure: function () {
                    toastr.error("Error al consultar los elementos del carrito de compras.");
                }
            });
        }

        function setSubscriptions() {
            $.subscribe('cart-elements-count:onChange', cart_elements_onChange);
        }

        return {
            initialize: function () {
                setSubscriptions();
            }
        };
    })();

    cart.initialize();

});