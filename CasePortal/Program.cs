using NK.Web.CasePortal.ConfigureServices;
using NK.BusinessProcess.Core.Json;
using NK.BusinessProcess.Web.ConfigureViewSearchPathCore;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add a Configure service handler for Dependency injection
// All ConfigureService Handlers inherit from IConfigureServices will automatically be run
foreach (var configureServicesHandler in ConfigureServicesFactory.GetConfigureServicesHandlers())
{
    configureServicesHandler.ConfigureServices(builder.Services);
}

builder.Services.AddMvc(options => options.EnableEndpointRouting = false)
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.IgnoreReadOnlyFields = true;
    options.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
    options.JsonSerializerOptions.Converters.Add(new NorwegianDateTimeJsonConverter());
    options.JsonSerializerOptions.Converters.Add(new NorwegianNullableDateTimeJsonConverter());
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Customize the view search path
// Add path where to find the view components
foreach (var configureServicesHandler in ConfigureViewSearchPathFactory.GetConfigureViewSearchPathHandlers(Assembly.GetExecutingAssembly()))
{
    configureServicesHandler.SetViewSearchPath(builder.Services);
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

const string cacheJSAndCSSMaxAge = "100"; // We want client to check for new version of JS and CSS resources every 100 seconds
const string cacheMaxAge = "2600000"; // Other static resources should be cached 1 month
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        if ((ctx.File.Name.Contains(".js") && !ctx.File.Name.Contains(".min.js"))
            || (ctx.File.Name.Contains(".css") && !ctx.File.Name.Contains(".min.css")))
        {
            ctx.Context.Response.Headers.Add("Cache-Control", $"public, max-age={cacheJSAndCSSMaxAge}");
        }
        else
        {
            ctx.Context.Response.Headers.Add("Cache-Control", $"public, max-age={cacheMaxAge}");
        }
    }
});

app.UseMvc();
// Attribute routing (defined in each controller/action)
app.UseMvc();
Startup.SetDefaultApplicationCulture();
Startup.PopulateProductMockData();
app.Run();
