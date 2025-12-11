using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTOs
{
    public class SingleResponse<T> : BaseResponse
    {
        public T Data { get; set; }
        public SingleResponse(bool isSuccess, string message, T data = default) : base(isSuccess, message)
        {
            Data = data;
        }
    }
}
