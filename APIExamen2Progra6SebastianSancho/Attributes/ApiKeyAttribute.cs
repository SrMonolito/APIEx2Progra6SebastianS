﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIExamen2Progra6SebastianSancho.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.All)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {

        private readonly string _apiKey = "ApiKey";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            if (context.HttpContext.Request.Headers.TryGetValue(_apiKey, out var ApiSalida))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Llamada a Api no contiene informacion de seguridad"
                };
                return;
            }

            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var ApiKeyValue = appSettings.GetValue<string>(_apiKey);

            if (ApiKeyValue != null && !ApiKeyValue.Equals(ApiSalida))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "El valor de llave de seguridad es Incorrecto"

                };

                return;
            }
            await next();
        }

    }
}
