using System;

namespace Resticle
{
	public interface IRestClient
	{
		IRestResponse Get(string url, object segments = null);
	}
	
	public interface IRestResponse{
		void On(int code, Action action);
	}
	
	public class RestResponse : IRestResponse{
		public void On(int code, Action action){
			action();
		}
	}
	
	public class RestClient : IRestClient
	{
		public RestClient(string root)
		{
			Root = root;
		}
		
		public string Root{ get; set; }
		
		public IRestResponse Get(string url, object segments = null){
			return new RestResponse();
		}
	}
}

