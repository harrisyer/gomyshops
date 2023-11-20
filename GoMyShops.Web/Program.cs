
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Security.Claims;
using GoMyShops.Data;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using AutoMapper;
using GoMyShops.Mappings;
using GoMyShops.Commons;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using GoMyShops.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using GoMyShops.Models.WebAPI;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

/// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

///JSON options to MVC since this is useful in displaying JSON data
///ad antiforgery
services.AddMvc(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
}).AddJsonOptions(options => {
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});


//Testing
//builder.Services.AddControllersWithViews().AddRazorPagesOptions(options => {
//    options.Conventions.AddPageRoute("Account", "/Account/Login");
//});
builder.Services.AddRazorPages();



services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

#region SimpleInjector (1) Assign container to SimpleInjector
var container = new SimpleInjector.Container();
#endregion

///add extention from Microsoft.Extensions.DependencyInjection
container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
services.AddControllers();
services.AddLocalization();
services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
///for IHelperFactory
services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
///for IhtmlHelper
services.AddTransient<IHtmlHelper, HtmlHelper>();
services.AddTransient(typeof(IHtmlHelper<>), typeof(HtmlHelper<>));

#region SimpleInjector (2) Assign container to SimpleInjector
GoMyShops.DependencySetup.SimplerInjectorSetup.IOCSetup(container);
services.UseSimpleInjectorAspNetRequestScoping(container);
services.AddSimpleInjector(container, options =>
{
    /// AddAspNetCore() wraps web requests in a Simple Injector scope and
    /// allows request-scoped framework services to be resolved.
    options.AddAspNetCore()
       /// Ensure activation of a specific framework type to be created by
       /// Simple Injector instead of the built-in configuration system.
       /// All calls are optional. You can enable what you need. For instance,
       /// ViewComponents, PageModels, and TagHelpers are not needed when you
      /// build a Web API.
       .AddControllerActivation()
      .AddViewComponentActivation();
    //.AddPageModelActivation()
    //.AddTagHelperActivation();

    /// Optionally, allow application components to depend on the non-generic
    /// ILogger (Microsoft.Extensions.Logging) or IStringLocalizer
    /// (Microsoft.Extensions.Localization) abstractions.
    options.AddLogging();
    options.AddLocalization();
    //options.AddLogging();   

});
#endregion

builder.Logging
    .ClearProviders()
    .AddSimpleConsole()
    .AddDebug();

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
    ///MigrationsAssembly mention ef core is in other project.
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        m => m.MigrationsAssembly("GoMyShops.Data"));
});

#region Authentication And Authorization (1-last) Setup to AutoMapper

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    //options.Password.RequireDigit = true;
    //options.Password.RequireLowercase = true;
    //options.Password.RequireUppercase = true;
    //options.Password.RequireNonAlphanumeric = true;
    //options.Password.RequiredLength = 10;

})
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();
//.AddSignInManager();

// Force Identity's security stamp to be validated every minute.
//builder.Services.Configure<SecurityStampValidatorOptions>(o =>
//                   o.ValidationInterval = TimeSpan.FromMinutes(1));

///try put all configuration file parameter here
var gConfigurationParameters = new ConfigurationParameters
{

    tokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        RequireExpirationTime = true,
        ValidateLifetime = true, //manual check skip default  
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
        System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])),
        ClockSkew = TimeSpan.Zero//FromMinutes(1)//
    },
    tokenExpiredHour = Convert.ToInt16(builder.Configuration["JWT:TokenExpiredHour"]),
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
    options.DefaultSignOutScheme = IdentityConstants.ApplicationScheme;
    //JwtBearerDefaults.AuthenticationScheme;

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

    //options.Events = new JwtBearerEvents
    //{
    //    OnTokenValidated = context =>
    //    {
    //        var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
    //        var user = userManager.GetUserAsync(context.HttpContext.User);
    //        if (user == null) context.Fail("Unauthorized");
    //        return Task.CompletedTask;
    //    }
    //};
    //options.RequireHttpsMetadata = false;
    //options.SaveToken = true;
    //options.TokenValidationParameters = new TokenValidationParameters
    //{
    //    ValidateIssuer = true,
    //    ValidateAudience = true,
    //    ValidateIssuerSigningKey = true,
    //    RequireExpirationTime = true,
    //    ValidIssuer = builder.Configuration["JWT:Issuer"],
    //    ValidAudience = builder.Configuration["JWT:Audience"],
    //    IssuerSigningKey = new SymmetricSecurityKey(
    //      System.Text.Encoding.UTF8.GetBytes(
    //          builder.Configuration["JWT:SigningKey"])
    //  )
    //};
});

builder.Services.AddAuthorization(options =>
{
    ///the fallback authorization policy requires all users to be authenticated. 
    ///Endpoints such as controllers, Razor Pages, etc that specify their own authorization requirements don't use the fallback authorization policy. 
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
       .RequireAuthenticatedUser()
       .Build();
    //options.AddPolicy("ModeratorWithMobilePhone", policy =>
    //    policy
    //        .RequireClaim(ClaimTypes.Role, RoleNames.Moderator)
    //        .RequireClaim(ClaimTypes.MobilePhone));

    //options.AddPolicy("MinAge18", policy =>
    //    policy
    //        .RequireAssertion(ctx =>
    //            ctx.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth)
    //            && DateTime.ParseExact(
    //                "yyyyMMdd",
    //                ctx.User.Claims.First(c =>
    //                    c.Type == ClaimTypes.DateOfBirth).Value,
    //                System.Globalization.CultureInfo.InvariantCulture)
    //                >= DateTime.Now.AddYears(-18)));
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

/// SQL Server Distributed Cache
/// --------------------------------------
builder.Services.AddDistributedSqlServerCache(options =>
{
    options.ConnectionString =
        builder.Configuration.GetConnectionString("DefaultConnection");
    options.SchemaName = "dbo";
    options.TableName = "AppCache";
});

///add session
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".GoMyShops.Session";
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.IsEssential = true;
});
builder.Services.AddApplicationInsightsTelemetry();

///add for web call web api
builder.Services.AddHttpClient();//.SetHandlerLifetime(TimeSpan.FromMinutes(3)); ;

var app = builder.Build();

#region SimpleInjector (3-last) UseSimpleInjector() finalizes the integration process.
app.Services.UseSimpleInjector(container);
#endregion

/// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("Error");
    /// The default HSTS value is 30 days. You may want to change this for production scenarios, 
    /// see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

/////config files extra from www root
//var cacheMaxAgeOneWeek = (60 * 60 * 24 * 7).ToString();
//app.UseStaticFiles(new StaticFileOptions
//{
//    ///set /StaticFiles is access path than www root
//    FileProvider = new PhysicalFileProvider(
//           Path.Combine(builder.Environment.ContentRootPath, "StaticFiles")),
//    RequestPath = "/StaticFiles",

//    ///makes static files publicly available in the local cache for one week (604800 seconds)
//    OnPrepareResponse = ctx =>
//    {
//        ctx.Context.Response.Headers.Append(
//             "Cache-Control", $"public, max-age={cacheMaxAgeOneWeek}");
//    }

//});

app.UseRouting();

///add session
app.UseSession();

app.UseCors();

app.UseResponseCaching();

app.UseAuthentication();
app.UseAuthorization();

#region Adds a default cache-control (no cache, overwrite by [RequestCache])
app.Use(async (context, next) =>
{
    ///The X-Frame-Options header prevents framing — i.e., 
    ///it prevents browsers from rendering your web page within another web page, 
    ///and thus prevents other websites from using your content
    context.Response.Headers.Append("X-Frame-Options", "SAMEORIGIN"); //DENY
    ///The X-Xss-Protection header will cause modern-day browsers to stop loading the web page 
    ///when they detect a cross-site scripting attack
    context.Response.Headers.Append("X-Xss-Protection", "1; mode=block");
    ///When you click on a link in the website you’re currently browsing, 
    ///the control is transferred to the linked site. 
    ///In addition, referrer data such as the URL could also be passed.
    ///If this URL includes the path and query string, 
    ///then user privacy or security could be compromised
    context.Response.Headers.Append("Referrer-Policy", "no-referrer");
    ///The X-Content-Type-Options header is used to indicate that the MIME types specified in the 
    ///Content-Type headers are deliberately configured and should not be changed by the browser. 
    ///This header prevents MIME sniffing, which can be used by attackers to turn non-executable MIME types
    ///into executable ones.
    context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
    ///This header can be used to indicate if Adobe products are allowed to render the web page 
    ///from a different domain than yours. In other words, like X-Frame-Options above, 
    ///this header protects you against website spoofing or unauthorized use of your content. 
    ///As an example, if you’re using Flash in your application, 
    ///you can prevent clients from making cross-site requests using the X-Permitted-Cross-Domain-Policies header
    ///using the following code snippet.
    context.Response.Headers.Append("X-Permitted-Cross-Domain-Policies", "none");
    ///The Feature-Policy header is used to specify all of the features your application needs.
    context.Response.Headers.Append("Feature-Policy", "camera none; geolocation none; microphone  none ; usb  none ");
    ///Content Security Policy is a security policy that is used to control the resources that a web page
    ///is allowed to load. It represents an extra layer of security that is implemented 
    ///via a Content-Security-Policy header in an HTTP response.
    ///Content-Security-Policy is used to detect and mitigate certain types of attacks 
    ///such as cross-site scripting attacks and data injection attacks.
    //context.Response.Headers.Append("Content-Security-Policy",
    //"default-src 'self'; report-uri /idgreport");


    string path = context.Request.Path;

    if (path.EndsWith(".css") || path.EndsWith(".js"))
    {

        ///Set css and js files to be cached for 7 days
        //TimeSpan maxAge = new TimeSpan(7, 0, 0, 0);     //7 days
        //context.Response.Headers.Append("Cache-Control", "max-age=" + maxAge.TotalSeconds.ToString("0"));

    }
    else if (path.EndsWith(".gif") || path.EndsWith(".jpg") || path.EndsWith(".png"))
    {
        //context.Response.GetTypedHeaders().CacheControl =
        //       new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
        //       {
        //           NoCache = true,
        //           NoStore = true
        //       };
    }
    else
    {
        //Request for views fall here.
        //context.Response.Headers.Append("Cache-Control", "no-cache");
        //context.Response.Headers.Append("Cache-Control", "private, no-store");
        context.Response.GetTypedHeaders().CacheControl =
                new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
                {
                    NoCache = true,
                    NoStore = true
                };
    }
    await next();
});
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


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

// Controllers
app.MapControllers().RequireCors("AnyOrigin");
app.MapRazorPages();
app.Run();
