using System.Web.Http;

namespace API.Controllers
{
    using API.Models;

    [Route("home")]
    public class HomeController : ApiController
    {
        public IHttpActionResult Get()
        {
            return this.Ok();
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]PostModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }
    }
}
