using System;

namespace Resticle
{
    public class RestResponseHandler : IRestResponseHandler
    {
        public T Unwrap<T>()
        {
            throw new NotImplementedException();
        }
    }
}