using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Buffers;
using System;
using Newtonsoft.Json.Converters;

namespace InfiniteCreativity.Attributes
{
    public class ReferenceLoopAttribute:ActionFilterAttribute
    {
        public async override Task OnResultExecutionAsync(ResultExecutingContext ctx, ResultExecutionDelegate next)
        {
            if (ctx.Result is ObjectResult objectResult)
            {
                var settings = new JsonSerializerSettings
                {
                    PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All,
                    TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,

                    /*ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    }*/
                };

                settings.Converters.Add(new StringEnumConverter
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                });
                objectResult.Formatters.Add(new NewtonsoftJsonOutputFormatter(
                    settings,
                    ctx.HttpContext.RequestServices.GetRequiredService<ArrayPool<char>>(),
                    ctx.HttpContext.RequestServices.GetRequiredService<IOptions<MvcOptions>>().Value,
                    new MvcNewtonsoftJsonOptions()));
            }
            await base.OnResultExecutionAsync(ctx, next);
        }
    }
}
