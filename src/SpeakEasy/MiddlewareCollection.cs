using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpeakEasy
{
    public class MiddlewareCollection
    {
        private readonly List<IHttpMiddleware> middlewares = new List<IHttpMiddleware>();

        public int MiddlewareCount => middlewares.Count;

        public void Append(IHttpMiddleware middleware)
        {
            middlewares.Add(middleware);
        }

        public void Prepend(IHttpMiddleware middleware)
        {
            middlewares.Insert(0, middleware);
        }

        public bool Has<TMiddleware>()
            where TMiddleware : IHttpMiddleware
        {
            return middlewares.Any(t => t is TMiddleware);
        }

        public void Replace<TMiddleware>(TMiddleware replacement)
            where TMiddleware : IHttpMiddleware
        {
            var index = Remove<TMiddleware>();
            middlewares.Insert(index, replacement);
        }

        public int Remove<TMiddleware>()
            where TMiddleware : IHttpMiddleware
        {
            if (!Has<TMiddleware>())
            {
                throw new ArgumentException();
            }

            var index = middlewares.FindIndex(t => t is TMiddleware);
            middlewares.RemoveAt(index);
            return index;
        }

        public Func<IHttpRequest, CancellationToken, Task<IHttpResponse>> BuildMiddlewareChain()
        {
            var head = middlewares[0];

            for (var i = 0; i < middlewares.Count; ++i)
            {
                var current = middlewares[i];

                if (i < middlewares.Count - 1)
                {
                    current.Next = middlewares[i + 1];
                }
            }

            return head.Invoke;
        }
    }
}
