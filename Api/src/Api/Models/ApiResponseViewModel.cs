using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinode.Api.Models
{
    public class ApiResponseViewModel<T>
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }
    }
}
