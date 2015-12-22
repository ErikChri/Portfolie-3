using ProductApp.JsonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProductApp.Controllers
{
    public class PostingsController : ApiController
    {
       

        // POST: api/Postings
        public IHttpActionResult Post([FromUri]string title, [FromUri]string body)
        {
            PostRep postRep = new PostRep(); 
            var post = postRep.Post(title, body);
            if (post == null)
            {
                return  NotFound();
            }
            return Ok(ModelFactory.Create(post, Url));
        }

        // PUT: api/Postings/5
        // Method for updating database is to be added
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Postings/5
        // Method for deleting a row is to be added
        public void Delete(int id)
        {
        }
    }
}
