using System;

namespace SpeakEasy
{
    public class AfterRequestEventArgs : EventArgs
    {
        public AfterRequestEventArgs(IHttpRequest request, IHttpResponse response, long elapsedMs)
        {
            Request = request;
            Response = response;
            ElapsedMs = elapsedMs;
        }

        public IHttpRequest Request { get; }

        public IHttpResponse Response { get; }

        public long ElapsedMs { get; }
    }
}
