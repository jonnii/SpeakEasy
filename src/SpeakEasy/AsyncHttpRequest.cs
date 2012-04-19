using System;
using System.Threading.Tasks;

namespace SpeakEasy
{
    public class AsyncHttpRequest<T> : IAsyncHttpRequest
        where T : IHttpRequest
    {
        private readonly IRequestRunner runner;

        private readonly T request;

        private Action<IHttpResponse> completeHandler;

        public AsyncHttpRequest(IRequestRunner runner, T request)
        {
            this.runner = runner;
            this.request = request;
        }

        public IAsyncHttpRequest OnComplete(Action<IHttpResponse> handler)
        {
            completeHandler = handler;

            return this;
        }

        public Task Start()
        {
            return runner.RunAsync(request).ContinueWith(r => completeHandler(r.Result));
        }
    }
}