using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HttpSpeak.IntegrationTests.Controllers
{
    public class ReservationsController : ApiController
    {
        public HttpResponseMessage Post(int productId)
        {
            if (productId == 1)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }
    }
}
