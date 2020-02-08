namespace Api.Service.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Http;
    using Api.Service.Models;

    [Route("home")]
    public class HomeController : ApiController
    {
        public IHttpActionResult Get()
        {
            return this.Ok();
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody][Required]PostModel model)
        {
            return this.Ok();
        }
    }
}
