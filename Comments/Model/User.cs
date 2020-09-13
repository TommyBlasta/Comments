using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comments.Model
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int CommentScore { get; set; }
    }
}
