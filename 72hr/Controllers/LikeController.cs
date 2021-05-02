using _72hr.Models;
using _72hr.Services;
using Microsoft.AspNet.Identity;
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
        private LikeService CreateLikeService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var replyService = new LikeService(userId);
            return replyService;
        }
        public IHttpActionResult Get(int id)
        {
            var service = CreateLikeService();
            var like = service.GetLikesByPostId(id);
            return Ok(like);
        }

        public IHttpActionResult Post(LikeCreate like,int postId)
        {
            //if (!ModelState.IsValid)
            //return BadRequest(ModelState);
            var service = CreateLikeService();
            service.CreateLikeByPostId(like, postId);
            return Ok();

        }
    }
}
