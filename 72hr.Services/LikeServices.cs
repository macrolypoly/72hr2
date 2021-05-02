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
        private readonly Guid _userId;
        public LikeService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateLikeByPostId(LikeCreate model, int id)
        {
            var entity =
                new Like()
                {
                    PostId = model.PostId,
                    OwnerId = _userId
                };
            using(var ctx = new ApplicationDbContext())
            {
                var post = ctx.Posts.Single(p => p.Id == id);
                ctx.Likes.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        /*public LikeDetail CreateLikeByPostId(int postId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                     .Posts.
                     SingleOrDefault(e => e.Id == postId);
                ctx.Likes.Add(entity);
                return new LikeDetail
                {
                    Id = entity.Id,
                    PostId = entity.Id
                };
                
            }
        }*/

        public IEnumerable<LikeDetail> GetLikes()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx.Likes.Select(
                        e => new LikeDetail
                        {
                            PostId = e.PostId,
                            Id = e.Id
                        }
                        );
                return query.ToArray();
            }
        }


        public LikeDetail GetLikesByPostId(int postId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Posts.Single(e => e.Id == postId);
                return new LikeDetail
                {
                    Id = entity.Id,
                    PostId = entity.Id
                };
            }
        }


    }
}
