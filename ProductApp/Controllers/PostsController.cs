using ProductApp.JsonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;



namespace ProductApp.Controllers
{
    public class PostsController : ApiController
    {
        public object Get(int pageSize = 10, int page = 0)
        {
            PostRep postRep = new PostRep();
            var result = postRep.GetAll(pageSize, page).Select(post => ModelFactory.Create(post, Url));
            

            return new
            {
                prev = Url.Link("PostApi", new { page = page - 1 }),
                next = Url.Link("PostApi", new { page = page + 1 }),
                results = result
            };
        }
    
        public IHttpActionResult GetPost(int id)
        {
            PostRep postRep = new PostRep(); 
            var post = postRep.find(id);
            if (post == null)
            {
                
                return NotFound();
            }
            return Ok(ModelFactory.Create(post, Url));
        }
    }
}
