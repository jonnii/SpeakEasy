using System;

namespace SpeakEasy
{
    public class BeforeRequestEventArgs : EventArgs
    {
        public BeforeRequestEventArgs(IHttpRequest request)
        {
            Request = request;
        }

        public IHttpRequest Request { get; private set; }
    }
}