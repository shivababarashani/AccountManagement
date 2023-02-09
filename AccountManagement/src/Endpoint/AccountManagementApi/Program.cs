using AccountManagement.Persistence.Implimentation;
using Microsoft.EntityFrameworkCore;

using AccountManagementApi.Helper;
using Microsoft.AspNetCore.Mvc.Versioning;
using AccountManagement.Contract.Dto.Setting;
using Microsoft.OpenApi.Models;
using System.Reflection;
using AccountManagement.Framework.Exceptions;
using AccountManagement.Framework.Logger;
using Microsoft.Extensions.Configuration;
using AccountManagement.Framework.Common;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "FrontEndPolicy";

//fill configs from appsetting.json
builder.Services.AddOptions();
builder.Services.Configure<SiteSetting>(options => builder.Configuration.Bind(options));

builder.Services.AddMiniProfiler(options => options.RouteBasePath = "/profiler").AddEntityFramework();


//Add Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://192.168.0.141:7074",
                                             "http://192.168.1.178:3000");
                      });
});


// Add services to the container.
builder.Services.AddDbContext<AccountManagementDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
/*Options*/
//ToDo:اینو باید ببرم تو فریمورک
builder.Services.AddOptions();
builder.Services.Configure<SiteSettingBaseViewModel>(options => builder.Configuration.Bind(options));

builder.Services.AddControllers();
builder.Services.AddCustomServices();
builder.Services.AddRepositories();
builder.Services.AddUnitOfWork();
builder.Services.AddInfra();
builder.Services.AddFreamwork();
builder.Services.AddValidators();
builder.Services.AddHttpClientFactory();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Register the Swagger generator, defining 1 or more Swagger documents
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AccountManagementApi",
        Version = "v1",
        Description = "An API to perform Account operations",
    });
    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});


//api versioning
builder.Services.AddApiVersioning(o =>
{
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    o.ReportApiVersions = true;
    o.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("X-Version"),
        new MediaTypeApiVersionReader("ver"));

});


var app = builder.Build();
app.UseMiniProfiler();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseCustomRequestResponseLog();
app.UseCustomExceptionHandler();


app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();


app.Run();
