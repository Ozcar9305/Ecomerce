namespace WebApplication
{
    using ECommerce;
    using ECommerceDataModel;
    using ECommerceDataModel.Shared;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Response.Redirect("~/ForzaUltra/Store.aspx");
            }
        }

        private void testLogic()
        {
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