using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _72hr.Models.PostModel
{
    public class PostCommentDetail
    {
        public string Text { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
