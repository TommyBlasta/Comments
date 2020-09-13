using Comments.Model;
using Comments.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comments.Factory
{
    public class PlainMessageFactory : BaseMessageFactory
    {
        private string text;
        private Page parentPage;
        private User poster;

        public PlainMessageFactory(string text, User poster, Page parentPage)
        {
            this.text = text;
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
            };
        }
    }
}
