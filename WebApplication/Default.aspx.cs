namespace WebApplication
{
    using ECommerce;
    using ECommerceDataModel;
    using ECommerceDataModel.Shared;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Drawing;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using PayPal.Api;
    using System.Globalization;

    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //testLogic();
                //testPayPal();
                //Response.Redirect("~/ForzaUltra/Store.aspx");
            }
        }

        private void testPayPal()
        {
            var apiContext = Utils.Configuration.GetAPIContext();
            string payerId = Request.Params["PayerID"];
            if (string.IsNullOrEmpty(payerId))
            {
                //Obtenemos el detalle de la orden
                var order = new OrderLogic().orderGetFilteredList(new RequestDTO<OrderDTO>
                {
                    Item = new OrderDTO
                    {
                        Identifier = 10
                    }
                });

                //Generamos el listado de items por cobrar en paypal
                var itemList = new ItemList();
                if (order.Success && order.Result.CartItems.Any())
                {
                    //Inicializamos la lista de items
                    itemList.items = new List<Item>();

                    //Iteramos los registros del carrito de compras relacionado a la orden
                    foreach(var cartItem in order.Result.CartItems)
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
                            price = Math.Round(cartItem.ProductCatalog.Price, 2).ToString(new CultureInfo("es-MX")),
                            quantity = cartItem.Quantity.ToString(),
                            sku = string.Format("SKU-{0}{1}", cartItem.ProductCategory.Identifier, cartItem.ProductCatalog.Identifier)
                        });
                    }

                    //Establecemos el tipo de pago
                    var payer = new Payer() { payment_method = "paypal" };

                    //Establecemos las urls de cancelacion de pago y regreso de pago
                    var baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Default.aspx?";
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
                        subtotal = Math.Round(order.Result.TotalAmount, 2).ToString(new CultureInfo("es-MX"))
                    };

                    //Establecemos el monto total de la compra, moneda y el detalle de la compra
                    var amount = new Amount()
                    {
                        currency = "MXN",
                        total = Math.Round(order.Result.TotalAmount, 2).ToString(new CultureInfo("es-MX")),
                        details = details
                    };

                    //Se requiere incluir una transaccion
                    var transactionList = new List<Transaction>
                    {
                        new Transaction
                        {
                            description = "Transaction description.",
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
                    try
                    {
                        var createdPayment = payment.Create(apiContext);
                        
                        //Se crean los links para que el cliente pueda aceptar o rechazar la compra
                        var links = createdPayment.links.GetEnumerator();
                        while (links.MoveNext())
                        {
                            var link = links.Current;
                            if (link.rel.ToLower().Trim().Equals("approval_url"))
                            {
                                //this.flow.RecordRedirectUrl("Redirect to PayPal to approve the payment...", link.href);
                            }
                        }
                        Session.Add(guid, createdPayment.id);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            else
            {

            }
        }

        private void testLogic()
        {
            //new OrderLogic().sendCustomerEmail(1);

            //var order = new OrderLogic().orderGetFilteredList(new RequestDTO<OrderDTO>
            //{
            //    Item = new OrderDTO
            //    {
            //        Identifier = 1
            //    }
            //});

            //var orderResult = new OrderLogic().OrderExecute(new RequestDTO<OrderDTO>
            //{
            //    Item = new OrderDTO
            //    {
            //        Customer = new CustomerDTO
            //        {
            //            Identifier = 1
            //        },
            //        CartItems = new List<CartDTO>
            //        {
            //            new CartDTO
            //            {
            //                Identifier = "90509998-30F7-412C-8644-6EEDB76F31BA"
            //            }
            //        }
            //    }
            //});

            //var productList = new ProductCatalogLogic().ProductCatalogGetListByCategory(new RequestDTO<ProductCatalogDTO>
            //{
            //    Item = new ProductCatalogDTO
            //    {
            //        ProductCategoryIdentifier = 1
            //    },
            //    Paging = new PagingDTO
            //    {
            //        PageSize = 10,
            //        PageNumber = 1
            //    }
            //});

            //bool loginOk = new LoginLogic().ValidatePassword("Hola123", "1000:hcZKnVJpuaOQdupclRpdYUsKp2e0vTll:+OTRoXqjMrq4XO87tC0YzUHa1aM=");
            //bool changePasswordOk = new LoginLogic().CustomerChangePassword(new CustomerDTO
            //{
            //    EncryptedPassword = "1000:q6fher+Uz7wAmR9T7fUcLw1MhAbzZZ9m:4tdSJ8Zs/d4Yx7a1sjgAF04vvbo=",
            //    Password = "Hola123",
            //    Email = "jgallegosledon@gmail.com"
            //});

            //Prueba de CategoryListForMainPage
            //var categoryResponse = new ProductCategoryLogic().CategoryListForMainPage(4);

            //var productSizeResponse = new ProductSizeLogic().ProductSizeMerge(new RequestDTO<ProductCatalogDTO>
            //{
            //    Item = new ProductCatalogDTO
            //    {
            //        Identifier = 1,
            //        ProductCategoryIdentifier = 1,
            //        Sizes = new List<SizesDTO>
            //        {
            //            new SizesDTO { Identifier = 1 },
            //            new SizesDTO { Identifier = 2 },
            //            new SizesDTO { Identifier = 3 },
            //            new SizesDTO { Identifier = 4 },
            //        }
            //    }
            //});

            //var productItem = new ProductCatalogLogic().ProductCatalogGetItem(1);

            //var x = new ProductCatalogLogic().ProductCatalogGetFilteredList
            //(
            //    new ECommerceDataModel.Shared.RequestDTO<ECommerceDataModel.ProductCatalogDTO>
            //    {
            //        Item = new ECommerceDataModel.ProductCatalogDTO
            //        {
            //            Identifier = 1
            //        },
            //        WordFilter = ""
            //    }
            // );

            //var cartResponse = new ResponseDTO<CartDTO>();
            //cartResponse = new CartLogic().CartItemExecute(new ECommerceDataModel.Shared.RequestDTO<ECommerceDataModel.CartDTO>
            //{
            //    Item = new ECommerceDataModel.CartDTO
            //    {
            //        Identifier = string.Empty,
            //        Customer = new ECommerceDataModel.CustomerDTO { Identifier = 1 },
            //        ProductCategory = new ECommerceDataModel.ProductCategoryDTO { Identifier = 1 },
            //        ProductCatalog = new ECommerceDataModel.ProductCatalogDTO 
            //        { 
            //            Identifier = 2,
            //            Price = 700,
            //            Sizes = new List<SizesDTO>
            //            {
            //                new SizesDTO
            //                {
            //                    Identifier = 2
            //                }
            //            }
            //        },
            //        Quantity = 15
            //    },
            //    OperationType = ECommerceDataModel.Shared.OperationType.Insert
            //});

            //var cartListResponse = new ResponseListDTO<CartDTO>();
            //cartListResponse = new CartLogic().CartGetFilteredList(new RequestDTO<CartDTO>
            //{
            //    Item = new CartDTO
            //    {
            //        Identifier = "4EB95B6C-6E88-4261-852B-BA85B7708CB9",
            //        Customer = new CustomerDTO { Identifier = 1 }
            //    }
            //});

            //cartResponse = new CartLogic().CartItemExecute(new ECommerceDataModel.Shared.RequestDTO<ECommerceDataModel.CartDTO>
            //{
            //    Item = new ECommerceDataModel.CartDTO
            //    {
            //        Identifier = "B80DAAC4-686D-4337-BCE8-6234325EBBFC",
            //        Customer = new ECommerceDataModel.CustomerDTO { Identifier = 1 },
            //        ProductCategory = new ECommerceDataModel.ProductCategoryDTO { Identifier = 1 },
            //        ProductCatalog = new ECommerceDataModel.ProductCatalogDTO
            //        {
            //            Identifier = 3
            //        },
            //        Quantity = 8
            //    },
            //    OperationType = ECommerceDataModel.Shared.OperationType.Update
            //});

            //cartResponse = new CartLogic().CartItemExecute(new ECommerceDataModel.Shared.RequestDTO<ECommerceDataModel.CartDTO>
            //{
            //    Item = new ECommerceDataModel.CartDTO
            //   
            //{
            //        Identifier = "B80DAAC4-686D-4337-BCE8-6234325EBBFC",
            //        Customer = new ECommerceDataModel.CustomerDTO { Identifier = 1 },
            //        ProductCategory = new ECommerceDataModel.ProductCategoryDTO { Identifier = 1 },
            //        ProductCatalog = new ECommerceDataModel.ProductCatalogDTO
            //        {
            //            Identifier = 3
            //        }
            //    },
            //    OperationType = ECommerceDataModel.Shared.OperationType.Delete
            //});
        }
    }
}