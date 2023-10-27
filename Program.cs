using MatysProjekt.Entity;
using MatysProjekt.service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IGameInfoService, GameInfoService>();


builder.Services.AddDbContext<EntityDbContext>(options => 
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
    });



var MyAllowSpecificOrigins = "AngularAppProj";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                      });
});


builder.Services.AddSignalR();



builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.ConsentCookie.IsEssential = true;
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => false;
    options.MinimumSameSitePolicy = SameSiteMode.None;
    options.Secure = CookieSecurePolicy.None;
});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.IsEssential = true;
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
        options.Cookie.HttpOnly = false;
        options.Events.OnRedirectToLogin = (context) =>
        {
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        };
    });




var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseAuthentication();
}
app.UseSession();
app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
var webSocketOptions = new WebSocketOptions();
webSocketOptions.AllowedOrigins.Add("http://localhost:4200");

app.UseWebSockets(webSocketOptions);
app.Use(async (httpContext, next) =>
{
    if (httpContext.Request.Path == "/ws")
    {
        if (httpContext.WebSockets.IsWebSocketRequest && httpContext.User.Identity.IsAuthenticated)
        {
            var socket = await httpContext.WebSockets.AcceptWebSocketAsync();
            var gameInfoService = (GameInfoService)app.Services.GetService(typeof(IGameInfoService));
        
            await gameInfoService.AddUser(socket, httpContext.User?.Identity?.Name);
        }
        else
        {
            httpContext.Response.StatusCode = 400;
        }
    }
    else
    {
        await next();
    }
});
app.Run();
