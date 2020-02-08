namespace PetApi.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;

    [RoutePrefix("home")]
    public class HomeController : ApiController
    {
        [Route("me")]
        public async Task<IHttpActionResult> GetAsync()
        {
            return this.Ok();
        }
    }
}