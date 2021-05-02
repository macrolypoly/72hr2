using _72hr.Data;
using _72hr.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace _72hr.Controllers
{
    public class ReplyController : ApiController
    {
        public IHttpActionResult Get(int id)
        {
            ReplyService replyService = CreateReplyService();
            var replies = replyService.GetRepliesByCommentId(id);
            return Ok(replies);
        }

        public IHttpActionResult Post(ReplyCreate reply)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateReplyService();

            if (!service.CreateReply(reply))
                return InternalServerError();

            return Ok();
        }

        private ReplyService CreateReplyService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var replyService = new ReplyService(userId);
            return replyService;
        }


        public class ReplyService
        {
            private readonly Guid _userId;
            public ReplyService(Guid userId)
            {
                _userId = userId;
            }

            public bool CreateReply(ReplyCreate model)
            {
                var entity =
                    new Reply()
                    {
                        Text = model.Text,
                        CommentId = model.CommentId,
                    };
                using (var ctx = new ApplicationDbContext())
                {
                    ctx.Replies.Add(entity);
                    return ctx.SaveChanges() == 1;
                }
            }


            public bool CreateReplyOnComment(int replyId, int commentId)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var reply = ctx.Replies.SingleOrDefault(ca => ca.Id == replyId);

                    var comment = ctx.Comments.Single(co => co.Id == commentId);
                    comment.Replies.Add(reply);
                    return ctx.SaveChanges() == 1;
                }
            }

            public ReplyDetail GetRepliesByCommentId(int commentId)
            {
                // Search for commentid through reply database then after its returned its added to the list of comments
                using (var ctx = new ApplicationDbContext())
                {
                    var entity =
                        ctx
                            .Replies
                            .Single(e => e.CommentId == commentId);
                    return
                        new ReplyDetail
                        {
                            Id = entity.Id,
                            Text = entity.Text,
                        };
                }
            }
        }
    }
}
