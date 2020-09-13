using Comments.Model.Interfaces;
using System;

namespace Comments.Factory
{
    public abstract class BaseMessageFactory
    {
        protected string IdGenerator()
        {
            return new Guid().ToString();
        }
        protected abstract IMessage MakeMessage();

        public IMessage GetObject()
        {
            return this.MakeMessage();
        }
    }
}
