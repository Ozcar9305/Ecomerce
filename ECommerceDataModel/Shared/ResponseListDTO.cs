namespace ECommerceDataModel.Shared
{
    using System.Collections.Generic;

    public class ResponseListDTO<T>
    {
        public bool Success { get; set; }

        public List<T> Result { get; set; }

        public PagingDTO Paging { get; set;}
    }
}
