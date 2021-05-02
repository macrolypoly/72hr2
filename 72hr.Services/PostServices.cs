using _72hr.Data;
using _72hr.Models.PostModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _72hr.Services
{
    public class PostServices
    {
        private Guid _UserID;
        public PostServices(Guid userid)
        {
            _UserID = userid;
        }
        public bool CreatePost(PostCreate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var postentity = new Post();
                postentity.AuthorId = _UserID;
                postentity.Title = model.Title;
                postentity.Text = model.Text;

                ctx.Posts.Add(postentity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<PostListItem> GetAllPost()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var postquery = ctx
                                    .Posts
                                    .Where(p=>p.AuthorId==_UserID)
                                    .Select(p => new PostListItem()
                                    {
                                        PostId = p.Id,
                                        Title = p.Title
                                    });
                return postquery.ToArray();
            }
        }

        public IEnumerable<PostDetail> GetPostByPostId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {

                var post = ctx
                                .Posts
                                .Where(p => p.AuthorId == _UserID && p.Id == id).
                                Select(p =>
                                new PostDetail
                                {
                                    Id = p.Id,
                                    Title = p.Title,
                                    postCommentDetail = ctx
                                                            .Comments
                                                            .Where(c => c.PostId == p.Id)
                                                            .Select(c => new PostCommentDetail
                                                            {
                                                                Text = c.Text,
                                                                CreatedUtc = c.CreatedUtc
                                                            }).ToList()
                                });



                return post;
            }
        }

    }
}
