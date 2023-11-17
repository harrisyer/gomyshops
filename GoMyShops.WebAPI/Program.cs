using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using GoMyShops.Commons;
using GoMyShops.Models;
using MyBGList.Swagger;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Security.Claims;
using GoMyShops.Data;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using AutoMapper;
using GoMyShops.Mappings;
//using GoMyShops.WebAPI.Services;
using GoMyShops.BAL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using GoMyShops.BAL.WebAPI;
using GoMyShops.Models.WebAPI;
using Microsoft.AspNetCore.DataProtection;
using System.Configuration;
//using GoMyShops.Data.Entity;
var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
// ADD services to the container.
//Assign container to SimpleInjector

#region SimpleInjector (1) Assign container to SimpleInjector
var container = new SimpleInjector.Container();
#endregion

//add extention from Microsoft.Extensions.DependencyInjection
container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

services.AddControllers();
services.AddLocalization();
services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

#region SimpleInjector (2) Assign container to SimpleInjector
GoMyShops.DependencySetup.SimplerInjectorSetup.IOCSetup(container);
services.UseSimpleInjectorAspNetRequestScoping(container);
services.AddSimpleInjector(container, options =>
{
    // AddAspNetCore() wraps web requests in a Simple Injector scope and
    // allows request-scoped framework services to be resolved.
    options.AddAspNetCore()

        // Ensure activation of a specific framework type to be created by
        // Simple Injector instead of the built-in configuration system.
        // All calls are optional. You can enable what you need. For instance,
        // ViewComponents, PageModels, and TagHelpers are not needed when you
        // build a Web API.

        .AddControllerActivation();
    //.AddViewComponentActivation()
    //.AddPageModelActivation()
    //.AddTagHelperActivation();

    // Optionally, allow application components to depend on the non-generic
    // ILogger (Microsoft.Extensions.Logging) or IStringLocalizer
    // (Microsoft.Extensions.Localization) abstractions.
    options.AddLogging();
    options.AddLocalization();
    //options.AddLogging();   

});
#endregion

//ValidateAntiForgeryTokenAttribute
//builder.Services.AddControllersWithViews(options =>
//{
//    options.Filters.Add(new Microsoft.AspNetCore.Mvc.ValidateAntiForgeryTokenAttribute());
//});

builder.Logging
    .ClearProviders()
    .AddSimpleConsole()
    .AddDebug();
//.AddApplicationInsights(telemetry => telemetry
//    .ConnectionString = builder
//        .Configuration["Azure:ApplicationInsights:ConnectionString"],
//    loggerOptions => { });

#region Serilog (1-last) Assign Serilog Configuration
builder.Host.UseSerilog((ctx, lc) =>
{
    lc.ReadFrom.Configuration(ctx.Configuration);
    lc.Enrich.WithMachineName();
    lc.Enrich.WithThreadId();
    lc.WriteTo.File("Logs/log.txt",
        outputTemplate:
            "{Timestamp:HH:mm:ss} [{Level:u3}] " +
            "[{MachineName} #{ThreadId}] " +
            "{Message:lj}{NewLine}{Exception}",
        rollingInterval: RollingInterval.Day);
    lc.WriteTo.MSSqlServer(
        connectionString:
            ctx.Configuration.GetConnectionString("DefaultConnection"),
        sinkOptions: new MSSqlServerSinkOptions
        {
            TableName = "LogEvents",
            AutoCreateSqlTable = true
        },
        columnOptions: new ColumnOptions()
        {
            AdditionalColumns = new SqlColumn[]
            {
                new SqlColumn()
                {
                    ColumnName = "SourceContext",
                    PropertyName = "SourceContext",
                    DataType = System.Data.SqlDbType.NVarChar
                }
            }
        }
        );
},
    writeToProviders: true);
#endregion


// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(cfg =>
    {
        cfg.WithOrigins(builder.Configuration["AllowedOrigins"]);
        cfg.AllowAnyHeader();
        cfg.AllowAnyMethod();
    });
    options.AddPolicy(name: "AnyOrigin",
        cfg =>
        {
            cfg.AllowAnyOrigin();
            cfg.AllowAnyHeader();
            cfg.AllowAnyMethod();
        });
});

builder.Services.AddControllers(options =>
{
    options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(
        (x) => $"The value '{x}' is invalid.");
    options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(
        (x) => $"The field {x} must be a number.");
    options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor(
        (x, y) => $"The value '{x}' is not valid for {y}.");
    options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(
        () => $"A value is required.");

    options.CacheProfiles.Add("NoCache",
        new CacheProfile() { NoStore = true });
    options.CacheProfiles.Add("Any-60",
        new CacheProfile() { Location = ResponseCacheLocation.Any, Duration = 60 });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.ParameterFilter<SortColumnFilter>();
    options.ParameterFilter<SortOrderFilter>();

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

#region AutoMapper (1-last) Setup to AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new ModelsMappingProfile());
});
services.AddSingleton<IMapper>(s => config.CreateMapper());
#endregion

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        m => m.MigrationsAssembly("GoMyShops.Data"));
});

//// Add a DbContext to store your Database Keys
//services.AddDbContext<MyKeysContext>(options =>
//    options.UseSqlServer(
//        Configuration.GetConnectionString("MyKeysConnection")));

//// using Microsoft.AspNetCore.DataProtection;
//services.AddDataProtection()
//    .PersistKeysToDbContext<MyKeysContext>();


#region Authentication And Authorization (1-last) Setup to AutoMapper
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 10;
})
    .AddEntityFrameworkStores<DataContext>();

///Some internal Services
//builder.Services.AddScoped<ITokenService, TokenService>();
//container.Register<ITokenServiceBAL, TokenServiceBAL>(AsyncScopedLifestyle.Transient);
///try put all configuration file parameter here
//container.Register<IConfigurationParameters, ConfigurationParameters>(AsyncScopedLifestyle.Singleton);
///services.AddSingleton<IConfigurationParameters, ConfigurationParameters>();
var gConfigurationParameters = new ConfigurationParameters {

    tokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        RequireExpirationTime = true,
        ValidateLifetime=true, //manual check skip default  
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
        System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])),        
        ClockSkew = TimeSpan.Zero//FromMinutes(1)//
    },
    tokenExpiredHour =Convert.ToInt16( builder.Configuration["JWT:TokenExpiredHour"]),
    refreshTokenExpiryDay = Convert.ToInt16(builder.Configuration["JWT:RefreshTokenExpiryDay"]),
};

    container.RegisterSingleton<IConfigurationParameters>(() => gConfigurationParameters);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
    options.DefaultChallengeScheme =
    options.DefaultForbidScheme =
    options.DefaultScheme =
    options.DefaultSignInScheme =
    options.DefaultSignOutScheme =
        JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = gConfigurationParameters.tokenValidationParameters;
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;    
    options.Events = new JwtBearerEvents()
    {   
        OnMessageReceived = context =>
        {
            var a = context.Request;
            if (context.Request.Cookies.Count <= 0)
            {
                return Task.CompletedTask;
            }

            ///ASP.NET Core still waits the token from Authorization Header. Therefore, we have to set the token from the cookies
            if (context.Request.Cookies.ContainsKey("X-Access-Token"))
            {
                context.Token = context.Request.Cookies["X-Access-Token"];
                //var aaaa=context.HttpContext.User.Identity.IsAuthenticated;
            }           

            return Task.CompletedTask;
        }
    };
    //options.Events.OnMessageReceived = context => {
       
    //};
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ModeratorWithMobilePhone", policy =>
        policy
            .RequireClaim(ClaimTypes.Role, RoleNames.Moderator)
            .RequireClaim(ClaimTypes.MobilePhone));

    options.AddPolicy("MinAge18", policy =>
        policy
            .RequireAssertion(ctx =>
                ctx.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth)
                && DateTime.ParseExact(
                    "yyyyMMdd",
                    ctx.User.Claims.First(c =>
                        c.Type == ClaimTypes.DateOfBirth).Value,
                    System.Globalization.CultureInfo.InvariantCulture)
                    >= DateTime.Now.AddYears(-18)));
});

// Code replaced by the [ManualValidationFilter] attribute
// builder.Services.Configure<ApiBehaviorOptions>(options =>
//    options.SuppressModelStateInvalidFilter = true);

#endregion 

builder.Services.AddResponseCaching(options =>
{
    options.MaximumBodySize = 32 * 1024 * 1024;
    options.SizeLimit = 50 * 1024 * 1024;
});

builder.Services.AddMemoryCache();

// SQL Server Distributed Cache
// --------------------------------------
builder.Services.AddDistributedSqlServerCache(options =>
{
    options.ConnectionString =
        builder.Configuration.GetConnectionString("DefaultConnection");
    options.SchemaName = "dbo";
    options.TableName = "AppCache";
});

// Redis Distributed Cache
// --------------------------------------
//builder.Services.AddStackExchangeRedisCache(options =>
//{
//    options.Configuration = builder.Configuration["Redis:Configuration"];
//});

var app = builder.Build();

#region SimpleInjector (3-last) UseSimpleInjector() finalizes the integration process.
app.Services.UseSimpleInjector(container);
#endregion

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

if (app.Configuration.GetValue<bool>("UseDeveloperExceptionPage"))
    app.UseDeveloperExceptionPage();
else
    app.UseExceptionHandler("/Error");

app.UseHttpsRedirection();

app.UseCors();

app.UseResponseCaching();

app.UseAuthentication();
app.UseAuthorization();

#region Adds a default cache-control (no cache, overwrite by [RequestCache])
//app.Use((context, next) =>
//{
//    context.Response.GetTypedHeaders().CacheControl =
//            new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
//            {
//                NoCache = true,
//                NoStore = true
//            };
//    return next.Invoke();
//});
#endregion

// Minimal API
#region Minimal API Testing error,cod,cache
app.MapGet("/error",
    [EnableCors("AnyOrigin")]
[ResponseCache(NoStore = true)] (HttpContext context) =>
    {
        var exceptionHandler =
            context.Features.Get<IExceptionHandlerPathFeature>();

        var details = new ProblemDetails();
        details.Detail = exceptionHandler?.Error.Message;
        details.Extensions["traceId"] =
            System.Diagnostics.Activity.Current?.Id
              ?? context.TraceIdentifier;
        details.Type =
            "https://tools.ietf.org/html/rfc7231#section-6.6.1";
        details.Status = StatusCodes.Status500InternalServerError;

        app.Logger.LogError(
            CustomLogEvents.Error_Get,
            exceptionHandler?.Error,
            "An unhandled exception occurred.");

        return Results.Problem(details);
    });

app.MapGet("/error/test",
    [EnableCors("AnyOrigin")]
[ResponseCache(NoStore = true)] () =>
    { throw new Exception("test"); });

app.MapGet("/cod/test",
    [EnableCors("AnyOrigin")]
[ResponseCache(NoStore = true)] () =>
    Results.Text("<script>" +
        "window.alert('Your client supports JavaScript!" +
        "\\r\\n\\r\\n" +
        $"Server time (UTC): {DateTime.UtcNow.ToString("o")}" +
        "\\r\\n" +
        "Client time (UTC): ' + new Date().toISOString());" +
        "</script>" +
        "<noscript>Your client does not support JavaScript</noscript>",
        "text/html"));

app.MapGet("/cache/test/1",
    [EnableCors("AnyOrigin")]
(HttpContext context) =>
    {
        context.Response.Headers["cache-control"] =
            "no-cache, no-store";
        return Results.Ok();
    });

app.MapGet("/cache/test/2",
    [EnableCors("AnyOrigin")]
(HttpContext context) =>
    {
        return Results.Ok();
    });
#endregion
#region Minimal API Testing authorization
app.MapGet("/auth/test/1",
    [Authorize]
[EnableCors("AnyOrigin")]
[ResponseCache(NoStore = true)] () =>
    {
        return Results.Ok("You are authorized!");
    });

app.MapGet("/auth/test/2",
    [Authorize(Roles = RoleNames.Moderator)]
[EnableCors("AnyOrigin")]
[ResponseCache(NoStore = true)] () =>
    {
        return Results.Ok("You are authorized!");
    });

app.MapGet("/auth/test/3",
    [Authorize(Roles = RoleNames.Administrator)]
[EnableCors("AnyOrigin")]
[ResponseCache(NoStore = true)] () =>
    {
        return Results.Ok("You are authorized!");
    });
#endregion

// Controllers
app.MapControllers().RequireCors("AnyOrigin");

app.Run();

