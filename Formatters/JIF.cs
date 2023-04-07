using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;

namespace bank_api.Formatters;

public static class JIF{

    public static NewtonsoftJsonInputFormatter GetJsonPatchInputFormatter(){

        var builder = new ServiceCollection()
            .AddLogging()
            .AddMvc()
            .AddNewtonsoftJson()
            .Services.BuildServiceProvider();

        return builder
                .GetRequiredService<IOptions<MvcOptions>>()
                .Value
                .InputFormatters
                .OfType<NewtonsoftJsonPatchInputFormatter>()
                .First();
    }

}