namespace Resticle
{
    public interface IRestResponseHandler
    {
        T Unwrap<T>();
    }
}