using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _72hr.Models
{
    public class CommentCreate
    {
        [Required]
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
