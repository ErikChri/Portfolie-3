using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace ProductApp.JsonModels
{

   
    public class ModelFactory
    {



        public static PostJson Create(Post post, UrlHelper urlHelper)
        {
            return new PostJson
            {
                url = urlHelper.Link("PostApi", new { id = post._Qi_ID }),
                title = post._title,
                body = post._body,
                comment = post._comment,
                viewCount = post._viewCount,
                votes = post._votes
            };
        }

    }
}