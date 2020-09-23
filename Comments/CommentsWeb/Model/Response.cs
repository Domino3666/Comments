using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentsWeb.Model
{
    public class Response<T>
    {
        public bool Success { get; set; }
        public string ErrorMsg { get; set; }
        public T Model { get; set; }
    }
}
