using ECommerce.Helpers;
using ECommerceDataModel;
using ECommerceDataModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce
{
    public class OrderLogic
    {
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

            }
            catch (Exception exception)
            {
                exception.LogException();
            }
            return orderResponse;
        }
    }
}
