namespace ECommerce.Helpers
{
    using ECommerceDataLayer;
    using System;

    public static class ExceptionClassExtension
    {
        private static ExceptionDataLayer dataLayer = new ExceptionDataLayer();

        public static void LogException(this Exception exception)
        {
            dataLayer.LogExceptionToDataBase(exception);    
        }
    }
}
