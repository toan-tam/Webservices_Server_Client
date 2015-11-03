using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerClient.Models
{
    public class Result<T>
    {
        public T data { get; set; }  
        public ErrorCode errorCode { get; set; }
        public String errorInfor { get; set; }  

        public Result()
        {
            errorCode = ErrorCode.NaN;
            errorInfor = "";
        }
    }

}