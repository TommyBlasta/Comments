using Comments.Model;
using Comments.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comments.Factory
{
    public class MultiMediaMessageFactory : BaseMessageFactory
    {
        private string text;
        private byte[][] multimedia;
        private Page parentPage;
        private User poster;

        public MultiMediaMessageFactory(string text, byte[][] multimedia, User poster, Page parentPage)
        {
            this.text = text;
            this.multimedia = multimedia;
            this.poster = poster;
            this.parentPage = parentPage;
        }
        protected override IMessage MakeMessage()
        {
            return new MultimediaMessage()
            {
                Id = IdGenerator(),
                Text = text,
                Poster = poster,
                ParentPage = parentPage,
                Multimedia = multimedia
            };
        }
    }
}
