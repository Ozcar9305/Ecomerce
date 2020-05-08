using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceDataModel.Shared
{
    public class ResponseDTO<T>
    {
        public bool Success { get; set; }

        public T Result { get; set; }
    }
}
