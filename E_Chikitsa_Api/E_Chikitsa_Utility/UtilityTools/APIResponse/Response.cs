using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Chikitsa_Utility.UtilityTools.APIResponse
{
    public class Response
    {
        public string? Result { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
    }

    public class ListResponse<TModel> : Response
    {
        public List<TModel>? Data { get; set; }
        public long TotalRecords { get; set; }
    }

    public class SingleResponse<TModel> : Response
    {
        public TModel? Data { get; set; }
        public string? Token { get; set; }
    }

    public class SingleResponse : Response
    {
        public string? Data { get; set; }
        public string? Token { get; set; }
    }



    public class SingleResponseForByteData : Response
    {
        public byte[]? Data { get; set; }
       
    }


}
