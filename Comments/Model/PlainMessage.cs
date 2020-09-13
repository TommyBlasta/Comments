using Comments.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comments.Model
{
    public class PlainMessage : IMessage, IClonable<PlainMessage>
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public Page ParentPage { get; set; }
        public User Poster { get; set; }

        public PlainMessage Clone()
        {
            return new PlainMessage()
            {
                Id = this.Id,
                Text = this.Text,
                ParentPage = this.ParentPage,
                Poster = this.Poster
            };
        }
    }
}
