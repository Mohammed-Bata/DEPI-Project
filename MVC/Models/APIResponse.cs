﻿using System.Net;

namespace MVC.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public List<string> Errors { get; set; }
        public object Data { get; set; }
    }
}
