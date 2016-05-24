using System;

namespace SpeakEasy.Extensions
{
    internal static class RaiseExtensions
    {
        public static void Raise<T>(this EventHandler<T> handler, object sender, T args)
            where T : EventArgs
        {
            handler?.Invoke(sender, args);
        }
    }
}
