using Comments.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comments.Model
{
    public class MultimediaMessage : IMessage, IClonable<MultimediaMessage>
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public Page ParentPage { get; set; }
        public User Poster { get; set; }
        public byte[][] Multimedia { get; set; }

        public MultimediaMessage Clone()
        {
            var copiedMultimedia = new byte[Multimedia.Length][];
            for(int i = 0; i < Multimedia.Length ; i++)
            {
                copiedMultimedia[i] = Multimedia[i];
            }
            return new MultimediaMessage()
            {
                Id = this.Id,
                Text = this.Text,
                ParentPage = this.ParentPage,
                Poster = this.Poster,
                Multimedia = copiedMultimedia
            };
        }
    }
}
