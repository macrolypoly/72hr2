using _72hr.Data;
using _72hr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _72hr.Services
{
    public class ReplyService
    {
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
