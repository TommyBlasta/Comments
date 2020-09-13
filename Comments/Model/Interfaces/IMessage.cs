using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comments.Model.Interfaces
{
    public interface IMessage
    {
        string Id { get; set; }
        string Text { get; set; }
        Page ParentPage { get; set; }
        User Poster { get; set; }
    }
}
