using _72hr.Data;
using _72hr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _72hr.Services
{
    public class LikeService
    {
        public LikeDetail CreateLikeByPostId(int postId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                     .Likes.
                     Single(e => e.PostId == postId);
                return new LikeDetail
                {
                    NoteId = entity.Id,
                    PostId = entity.PostId,
                };
            }
        }

        public IEnumerable<LikeDetail> GetLikes()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx.Likes.Select(
                        e => new LikeDetail
                        {
                            PostId = e.PostId,
                            NoteId = e.Id
                        }
                        );
                return query.ToArray();
            }
        }


        public LikeDetail GetLikesByPostId(int postId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Likes.Single(e => e.PostId == postId);
                return new LikeDetail
                {
                    NoteId = entity.Id,
                    PostId = entity.PostId
                };
            }
        }


    }
}
