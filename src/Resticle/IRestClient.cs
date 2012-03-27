namespace Resticle
{
	public interface IRestClient
	{
		IRestResponse Get(string url, object segments = null);
	}
}

