using _72hr.Models.PostModel;
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
    public class PostController : ApiController
    {
        private PostServices CreatePostServices()
        {
            var userid = Guid.Parse(User.Identity.GetUserId());
            var spost = new PostServices(userid);
            return spost;
        }

        public IHttpActionResult Post(PostCreate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model is null)
            {
                return BadRequest("you cannot have an empty model");
            }
            var post = CreatePostServices();
            if (!post.CreatePost(model))
            {
                return BadRequest("Model could not be addeded");
            }
            return Ok(" Unit succesfully created");
        }
        [HttpGet]
        public IHttpActionResult GetAllPost()
        {
            var spost = CreatePostServices();
            var Postlist = spost.GetAllPost();
            return Ok(Postlist);
        }
        public IHttpActionResult GetPostByPostById(int id)
        {
            var spost = CreatePostServices();
            var postdetail = spost.GetPostByPostId(id);
            if(postdetail is null)
            {
                return InternalServerError();
            }
            return Ok(postdetail);
        }
    }
}
