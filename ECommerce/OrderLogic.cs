 namespace ECommerce
{
    using ECommerce.Helpers;
    using ECommerceDataLayer;
    using ECommerceDataModel;
    using ECommerceDataModel.Shared;
    using System;
    using System.Data.Odbc;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
                if (orderResponse.Success)
                {
                    Task taskCustomerEmail = Task.Run(() => sendCustomerEmail(orderResponse.Result.Identifier));
                    Task taskAdminEmail = Task.Run(() => sendAdminEmail(orderResponse.Result.Identifier));
                }
            }
            catch (Exception exception)
            {
                exception.LogException();
            }
            return orderResponse;
        }

        /// <summary>
        /// Envia el email de la orden de compra al administrador
        /// </summary>
        private void sendAdminEmail(long orderIdentifier)
        {
            try
            {
                var order = this.orderGetFilteredList(new RequestDTO<OrderDTO>
                {
                    Item = new OrderDTO
                    {
                        Identifier = orderIdentifier
                    }
                });

                if (order.Success)
                {
                    //Armado dinamico del cuerpo del correo
                    StringBuilder productRowsBuilder = new StringBuilder();
                    for (int i = 0; i < order.Result.CartItems.Count; i++)
                    {
                        var cartItem = order.Result.CartItems[i];
                        string productName = string.Format("{0} {1}",
                                                          cartItem.ProductCategory.Name.Remove(order.Result.CartItems[i].ProductCategory.Name.Length-1, 1),
                                                          cartItem.ProductCatalog.ShortName);

                        productRowsBuilder.AppendFormat(Resources.PurchaseOrderEmail.ProductRow,
                                                        cartItem.Quantity,
                                                        productName,
                                                        cartItem.ProductCatalog.Sizes.FirstOrDefault().Abreviature,
                                                        cartItem.ProductCatalog.Price.ToString("C"));
                    }

                    string adminEmailBody = string.Format(Resources.PurchaseOrderEmail.PurchaseOrderEmailBodyForAdmin,
                                                          "Detalle",
                                                          order.Result.Identifier,
                                                          productRowsBuilder.ToString(),
                                                          order.Result.TotalAmount.ToString("C"),
                                                          string.Format("{0} {1}", order.Result.Customer.FirstName, order.Result.Customer.LastName),
                                                          string.IsNullOrEmpty(order.Result.Customer.PhoneNumber) ? "N/A" : order.Result.Customer.PhoneNumber,
                                                          string.IsNullOrEmpty(order.Result.Customer.BillingInformation.RFC) ? "N/A" : order.Result.Customer.BillingInformation.RFC,
                                                          order.Result.Customer.Email,
                                                          string.IsNullOrEmpty(order.Result.Customer.ShippingAddress) ? "N/A" : order.Result.Customer.ShippingAddress);

                    //Envio de correo electronico
                    new EmailHelper().SendEmail(new MailDTO
                    {
                        EmailBody = adminEmailBody,
                        EmailSubject = "Nueva orden de Compra Forza Ultra",
                        EmailTo = "jgallegosledon@gmail.com" //order.Result.Customer.Email
                    });
                }
            }
            catch (Exception exception)
            {
                exception.LogException();
            }
        }

        /// <summary>
        /// Envia el email de la orden de compra al cliente
        /// </summary>
        private void sendCustomerEmail(long orderIdentifier)
        {
            try
            {
                var order = this.orderGetFilteredList(new RequestDTO<OrderDTO>
                {
                    Item = new OrderDTO
                    {
                        Identifier = orderIdentifier
                    }
                });

                if (order.Success)
                {
                    //Armado dinamico del cuerpo del correo
                    StringBuilder productRowsBuilder = new StringBuilder();
                    for (int i = 0; i < order.Result.CartItems.Count; i++)
                    {
                        var cartItem = order.Result.CartItems[i];
                        string productName = string.Format("{0} {1}",
                                                          cartItem.ProductCategory.Name.Remove(order.Result.CartItems[i].ProductCategory.Name.Length - 1, 1),
                                                          cartItem.ProductCatalog.ShortName);

                        productRowsBuilder.AppendFormat(Resources.PurchaseOrderEmail.ProductRow,
                                                        cartItem.Quantity,
                                                        productName,
                                                        cartItem.ProductCatalog.Sizes.FirstOrDefault().Abreviature,
                                                        cartItem.ProductCatalog.Price.ToString("C"));
                    }

                    string customerEmailBody = string.Format(Resources.PurchaseOrderEmail.PurchaseOrderEmailBodyForCustomer,
                                                          string.Format("!Gracias por tu compra {0}!", order.Result.Customer.FirstName),
                                                          order.Result.Identifier,
                                                          productRowsBuilder.ToString(),
                                                          order.Result.TotalAmount.ToString("C"));

                    //Envio de correo electronico
                    new EmailHelper().SendEmail(new MailDTO
                    {
                        EmailBody = customerEmailBody,
                        EmailSubject = "Tú recibo de Compra en Forza Ultra",
                        EmailTo = order.Result.Customer.Email
                    });
                }
            }
            catch (Exception exception)
            {
                exception.LogException();
            }
        }

        /// <summary>
        /// Obtiene el detalle de la orden generada
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private ResponseDTO<OrderDTO> orderGetFilteredList(RequestDTO<OrderDTO> order)
        {
            var orderResponse = new ResponseDTO<OrderDTO>();
            try
            {
                orderResponse.Result = orderDataLayer.OrderGetFilteredList(order);
                orderResponse.Success = orderResponse.Result.Identifier > default(long) && orderResponse.Result.CartItems.Any();
            }
            catch (Exception exception)
            {
                exception.LogException();
            }
            return orderResponse;
        }

    }
}
