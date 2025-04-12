using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class Utility
    {
        public enum ApiType
        {
            Get,
            Post,
            Put,
            Delete
        }
        public static string AccessToken = "JWTToken";
        public static string RefreshToken = "RefreshToken";

        public enum ContentType
        {
            Json,
            MultipartFormData,
        }
    }
}
