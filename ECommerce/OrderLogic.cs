 namespace ECommerce
{
    using ECommerce.Helpers;
    using ECommerceDataLayer;
    using ECommerceDataModel;
    using ECommerceDataModel.Shared;
    using System;

    public class OrderLogic
    {
        /// <summary>
        /// Acceso a datos de orden
        /// </summary>
        private readonly OrderDataLayer orderDataLayer = new OrderDataLayer();

        /// <summary>
        /// Agregar nueva orden de pedido
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public ResponseDTO<OrderDTO> OrderExecute(RequestDTO<OrderDTO> order)
        {
            var orderResponse = new ResponseDTO<OrderDTO>();
            try
            {
                orderResponse.Result = orderDataLayer.OrderInsert(order.Item);
                orderResponse.Success = orderResponse.Result.Identifier > default(long);
            }
            catch (Exception exception)
            {
                exception.LogException();
            }
            return orderResponse;
        }
    }
}
