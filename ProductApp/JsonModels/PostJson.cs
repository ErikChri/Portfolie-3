using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductApp.JsonModels
{
    public class PostJson
    {
        public string url { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public string comment { get; set; }
        public int viewCount { get; set; }
        public int votes { get; set; }
    }

}