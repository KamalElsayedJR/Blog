using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTOs
{
    public class ListResponse<T> : BaseResponse
    {
        public ListResponse(bool isSuccess,string message,List<T> data):base(isSuccess, message)
        {
            Data = data;
        }
        public List<T> Data { get; set; }
    }
}
