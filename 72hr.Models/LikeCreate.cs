using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _72hr.Models
{
    public class LikeCreate
    {
        [Required]
        public int PostId { get; set; }

    }
}
