namespace Api.Service
{
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;
    using System.Web.Http.ModelBinding;

    public sealed class ValidateActionParametersFilter : ActionFilterAttribute
    {
        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            if (actionContext.ActionDescriptor is ReflectedHttpActionDescriptor descriptor)
            {
                var parameters = descriptor.MethodInfo.GetParameters();

                foreach (var parameter in parameters)
                {
                    var argument = actionContext.ActionArguments.ContainsKey(parameter.Name) ?
                        actionContext.ActionArguments[parameter.Name] : null;

                    EvaluateValidationAttributes(parameter, argument, actionContext.ModelState);
                }
            }

            return base.OnActionExecutingAsync(actionContext, cancellationToken);
        }

        private static void EvaluateValidationAttributes(ParameterInfo parameter, object argument, ModelStateDictionary modelState)
        {
            var attributes = parameter.CustomAttributes;

            foreach (var attributeData in attributes)
            {
                var attributeInstance = parameter.GetCustomAttribute(attributeData.AttributeType);

                if (attributeInstance is ValidationAttribute validationAttribute)
                {
                    var isValid = validationAttribute.IsValid(argument);
                    if (!isValid)
                    {
                        modelState.AddModelError(parameter.Name, validationAttribute.FormatErrorMessage(parameter.Name));
                    }
                }
            }
        }
    }
}
