using _72hr.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _72hr.Controllers
{
    public class LikeController : ApiController
    {
        public IHttpActionResult Get(int id)
        {
            var service = new LikeService();
            var like = service.GetLikesByPostId(id);
            return Ok(like);
        }

        public IHttpActionResult Post(int postId)
        {
            //if (!ModelState.IsValid)
            //return BadRequest(ModelState);
            var service = new LikeService();
            service.CreateLikeByPostId(postId);
            return Ok();

        }
    }
}
