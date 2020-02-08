namespace Api.Service
{
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    public sealed class ValidationFilter : ActionFilterAttribute
    {
        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var modelState = actionContext.ModelState;

            if (!modelState.IsValid)
            {
                var errors = modelState.Where(o => o.Value.Errors.Count != 0).SelectMany(o => o.Value.Errors, (a, b) => new { a.Key, Error = b }).Select(o => new ServiceError
                {
                    Target = o.Key,
                    Message = !string.IsNullOrWhiteSpace(o.Error.ErrorMessage) ? o.Error.ErrorMessage : o.Error.Exception?.Message
                }).ToArray();

                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, new ServiceError { Message = "Bad request", Errors = errors });
            }

            return base.OnActionExecutingAsync(actionContext, cancellationToken);
        }

        private class ServiceError
        {
            public string Target { get; set; }
            public string Message { get; set; }
            public ServiceError[] Errors { get; set; }

        }
    }
}
