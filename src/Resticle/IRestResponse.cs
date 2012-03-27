using System;

namespace Resticle
{
    public interface IRestResponse
    {
        void On(int code, Action action);
    }
}