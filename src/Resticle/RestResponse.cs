using System;

namespace Resticle
{
    public class RestResponse : IRestResponse
    {
        public void On(int code, Action action)
        {
            action();
        }
    }
}