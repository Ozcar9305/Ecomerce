namespace ECommerce
{
    using System;
    using ECommerce.Helpers;
    using ECommerceDataLayer;
    using ECommerceDataModel;
    using ECommerceDataModel.Shared;
    using System.Linq;
    using System.Collections.Generic;
    using System.Drawing;

    public class CartLogic
    {
        /// <summary>
        /// Objeto de acceso a capa de datos
        /// </summary>
        private readonly CartDataLayer dataLayer = new CartDataLayer();

        /// <summary>
        /// Objecto de acceso a la capa de datos de la entidad categoria
        /// </summary>
        private readonly ProductSizeDataLayer productSizeDataLayer = new ProductSizeDataLayer();

        /// <summary>
        /// Permite obtener un listado de items guardados en el carrito de compras
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public ResponseListDTO<CartDTO> CartGetFilteredList(RequestDTO<CartDTO> cartRequest)
        {
            var cartListResponse = new ResponseListDTO<CartDTO>();
            try
            {
                cartListResponse.Result = dataLayer.CartGeFilteredtList(cartRequest.Item);
                cartListResponse.Success = cartListResponse.Result.Any();
                if (cartListResponse.Result.Count > 0)
                {
                    foreach (var item in cartListResponse.Result)
                    {
                        var sizes = productSizeDataLayer.ProductSizeGetFilteredList(item.ProductCategory.Identifier, item.ProductCatalog.Identifier) ?? new List<SizesDTO>();                        
                        item.ProductCatalog.Sizes = sizes;
                    }
                }
            }
            catch (Exception exception)
            {
                exception.LogException();
            }
            return cartListResponse;
        }

        /// <summary>
        /// Permite actualizar o insertar registros al carrito de compras
        /// </summary>
        /// <param name="cartRequest"></param>
        /// <returns></returns>
        public ResponseDTO<CartDTO> CartItemExecute(RequestDTO<CartDTO> cartRequest)
        {
            var cartResponse = new ResponseDTO<CartDTO>();
            try
            {
                switch (cartRequest.OperationType)
                {
                    case OperationType.Insert:
                        cartResponse.Result = dataLayer.CartAddItem(cartRequest.Item);
                        cartResponse.Success = !string.IsNullOrEmpty(cartResponse.Result.Identifier);
                        break;
                    case OperationType.Update:
                        cartResponse.Success = dataLayer.CartUpdateItem(cartRequest.Item);
                        break;
                    case OperationType.Delete:
                        cartResponse.Success = dataLayer.CartDeleteItem(cartRequest.Item);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception exception)
            {
                exception.LogException();
            }
            return cartResponse;
        }
    }
}
