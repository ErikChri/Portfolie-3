using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductApp.JsonModels
{
    public class Post
    {
        internal string comment;

        public int _Qi_ID { get; set; }
        public string _title { get; set; }
        public string _body { get; set; }
        public string _comment { get; set; }
        public int _viewCount { get; set; }
        public int _votes { get; set; }
    }
}