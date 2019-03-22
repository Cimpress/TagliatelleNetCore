using System;

namespace Cimpress.TagliatelleNetCore.Exceptions
{
    public class MalfomedTagException : Exception
    {
        public MalfomedTagException(string message) : base(message)
        {
        }
    }
}
