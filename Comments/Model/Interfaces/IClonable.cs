using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comments.Model.Interfaces
{
    public interface IClonable<T>
    {
        public T Clone();
    }
}
