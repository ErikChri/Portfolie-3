using ProductApp.JsonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProductApp.Controllers
{
    public class SearchStringsController : ApiController
    {


        public object Get([FromUri]string searchString)
        {
            SearchStringRep searchStringRep = new SearchStringRep();
            var result = searchStringRep.GetAll(searchString).Select(post => ModelFactory.Create(post, Url));

           return new
            {
                results = result
            };
        }
    }
}
