using DevCodeOverflow.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DevCodeOverflow.Controllers
{
    public class PostsController : ApiController
    {
        private ApplicationDbContext db;
        public PostsController()
        {
            this.db = new ApplicationDbContext();
        }
        // GET api/<controller>
        public IEnumerable<Post> Get()
        {
            return this.db.Posts.OrderBy(x => x.DatePosted).ToList();
        }

        // GET api/<controller>/5
        public JArray Get(int id)
        {
            var result = new JArray();
            result.Add(id.ToString());
            return result;
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
