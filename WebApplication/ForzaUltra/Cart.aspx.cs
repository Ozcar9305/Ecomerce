
namespace WebApplication.ForzaUltra
{
    using ECommerce;
    using ECommerce.Helpers;
    using ECommerceDataModel;
    using ECommerceDataModel.Shared;
    using PayPal.Api;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Services;
    using System.Web.SessionState;
    using System.Web.UI;

    public partial class Cart : Page
    {
        private static APIContext apiContext = Utils.Configuration.GetAPIContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string payerId = Request.Params["PayerID"];
                string guid = Request.Params["guid"];
                string cancel = Request.Params["cancel"];

                if (!string.IsNullOrEmpty(payerId) && !string.IsNullOrEmpty(guid) && string.IsNullOrEmpty(cancel))
                {
                    var paymentId = Session[guid] as string;
                    var paymentExecution = new PaymentExecution() { payer_id = payerId };
                    var payment = new Payment() { id = paymentId };
                    var executedPayment = payment.Execute(apiContext, paymentExecution);
                }
            }
        }

        [WebMethod]
        public static ResponseListDTO<CartDTO> CartItemGetList()
        {
            var response = new ResponseListDTO<CartDTO>();
            if (HttpContext.Current.Session["SessionInit"] != null && bool.Parse(HttpContext.Current.Session["SessionInit"].ToString()))
            {
                response = new CartLogic().CartGetFilteredList(new RequestDTO<CartDTO>
                {
                    Item = new CartDTO
                    {
                        Customer = new CustomerDTO
                        {
                            Identifier = int.Parse(HttpContext.Current.Session["SessionCustomerIdentifier"].ToString())
                        },
                        Identifier = HttpContext.Current.Session["SessionCartIdentifier"].ToString()
                    }
                });
                response.SessionInit = true;
            }
            return response;
        }

        [WebMethod]
        public static ResponseDTO<CartDTO> DeleteCartItem(string cartId, int customerId, int productId, int categoryId)
        {
            var response = new CartLogic().CartItemExecute(new RequestDTO<CartDTO>
            {
                OperationType = OperationType.Delete,
                Item = new CartDTO
                {
                    Identifier = cartId,
                    Customer = new CustomerDTO { Identifier = customerId },
                    ProductCatalog = new ProductCatalogDTO { Identifier = productId },
                    ProductCategory = new ProductCategoryDTO { Identifier = categoryId }
                }
            });
            return response;
        }

        [WebMethod]
        public static ResponseDTO<CartDTO> UpdateCartItem(string cartId, int customerId, int productId, int categoryId, int quantity, int size)
        {
            var response = new CartLogic().CartItemExecute(new RequestDTO<CartDTO>
            {
                OperationType = OperationType.Update,
                Item = new CartDTO
                {
                    Identifier = cartId,
                    Customer = new CustomerDTO { Identifier = customerId },
                    ProductCatalog = new ProductCatalogDTO { Identifier = productId, Sizes = new List<SizesDTO> { new SizesDTO { Identifier = size } } },
                    ProductCategory = new ProductCategoryDTO { Identifier = categoryId },
                    Quantity = quantity
                }
            });

            return response;
        }

        [WebMethod(EnableSession=true)]
        public static ResponseDTO<OrderDTO> OrderExecute(ECommerceDataModel.Enum.PaymentType paymentType)
        {
            var orderResponse = new ResponseDTO<OrderDTO>();
            orderResponse = new OrderLogic().OrderExecute(new RequestDTO<OrderDTO>
            {
                Item = new OrderDTO
                {
                    Customer = new CustomerDTO
                    {
                        Identifier = int.Parse(HttpContext.Current.Session["SessionCustomerIdentifier"].ToString())//customerId
                    },
                    CartItems = new List<CartDTO>
                    {
                        new CartDTO
                        {
                            Identifier = HttpContext.Current.Session["SessionCartIdentifier"].ToString()//cartId
                        }
                    }
                }
            });

            //Se valida que se haya ejecutado correctamente la orden y se generan las url's de paypal
            if (orderResponse.Success && orderResponse.Result.CartItems.Any() && paymentType == ECommerceDataModel.Enum.PaymentType.PayPal)
            {
                var payPalUrlList = createPayPalOrder(orderResponse);
                if(payPalUrlList != null && payPalUrlList.Any())
                {
                    orderResponse.Result.PayPalUrlList = payPalUrlList;
                }
            }

            return orderResponse;
        }

        /// <summary>
        /// Generar las rutas de pago paypal
        /// </summary>
        /// <param name="orderResponse"></param>
        /// <returns></returns>
        private static List<UrlDTO> createPayPalOrder(ResponseDTO<OrderDTO> order)
        {
            var payPalUrlsList = new List<UrlDTO>();
            var itemList = new ItemList();
            
            try
            {
                //Inicializamos la lista de items
                itemList.items = new List<Item>();

                //Iteramos los registros del carrito de compras relacionado a la orden
                foreach (var cartItem in order.Result.CartItems)
                {
                    string productSizeDescription = cartItem.ProductCatalog.Sizes.FirstOrDefault().Abreviature.Equals("Unitalla") ? "Unitalla" : string.Format("Talla {0}", cartItem.ProductCatalog.Sizes.FirstOrDefault().Abreviature);
                    string productName = string.Format("{0} '{1}' {2}",
                                                      cartItem.ProductCategory.Name.Remove(cartItem.ProductCategory.Name.Length - 1, 1),
                                                      cartItem.ProductCatalog.ShortName,
                                                      productSizeDescription);

                    itemList.items.Add(new Item
                    {
                        name = productName,
                        currency = "MXN",
                        price = Math.Round(cartItem.ProductCatalog.Price, 2).ToString(),
                        quantity = cartItem.Quantity.ToString(),
                        sku = string.Format("SKU-{0}{1}", cartItem.ProductCategory.Identifier, cartItem.ProductCatalog.Identifier)
                    });
                }

                //Establecemos el tipo de pago
                var payer = new Payer() { payment_method = "paypal" };

                //Establecemos las urls de cancelacion de pago y regreso de pago
                var baseURI = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/Default.aspx?";
                var guid = Convert.ToString((new Random()).Next(100000));
                var redirectUrl = baseURI + "guid=" + guid;
                var redirUrls = new RedirectUrls()
                {
                    cancel_url = redirectUrl + "&cancel=true",
                    return_url = redirectUrl
                };

                //Incluimos detalle de la compra (impuestos, costo de envio, subtotal)
                var details = new Details()
                {
                    tax = "0",
                    shipping = "0",
                    subtotal = Math.Round(order.Result.TotalAmount, 2).ToString()
                };

                //Establecemos el monto total de la compra, moneda y el detalle de la compra
                var amount = new Amount()
                {
                    currency = "MXN",
                    total = Math.Round(order.Result.TotalAmount, 2).ToString(),
                    details = details
                };

                //Se requiere incluir una transaccion
                var transactionList = new List<Transaction>
                {
                    new Transaction
                    {
                        description = string.Format("Compra ForzaUltra"),
                        invoice_number = string.Format("{0}-{1}", guid, order.Result.Identifier),
                        amount = amount,
                        item_list = itemList
                    }
                };

                //Generamos el objeto payment que incluye el tipo de pago, la transaccion y las urls de redireccion
                var payment = new Payment()
                {
                    intent = "sale",
                    payer = payer,
                    transactions = transactionList,
                    redirect_urls = redirUrls
                };

                //Creamos el pago
                var createdPayment = payment.Create(apiContext);

                //Se crean los links para que el cliente pueda aceptar o rechazar la compra
                var links = createdPayment.links.GetEnumerator();
                while (links.MoveNext())
                {
                    var link = links.Current;
                    if (link.rel.ToLower().Trim().Equals("approval_url"))
                    {
                        payPalUrlsList.Add(new UrlDTO
                        {
                            Rel = link.rel.ToLower().Trim(),
                            HReference = link.href
                        });
                    }
                }
                HttpContext.Current.Session.Add(guid, createdPayment.id);
            }
            catch (Exception ex)
            {
                payPalUrlsList = null;
                ex.LogException();
            }
            return payPalUrlsList;
        }
    }
}