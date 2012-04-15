using System;

namespace SpeakEasy.Extensions
{
    public static class RaiseExtensions
    {
        public static void Raise<T>(this EventHandler<T> handler, object sender, T args)
            where T : EventArgs
        {
            var copy = handler;
            if (copy != null)
            {
                copy(sender, args);
            }
        }
    }
}
