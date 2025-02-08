using System.Threading.RateLimiting;
using lab1_nour_kassem.Services;
using LearnWebAPI.Middlewares;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    c.OperationFilter<LanguageHeaderFilter>();
});
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();

//make a singleton service injection, because:
//at first I tried the scoped service and I discovered that updating users
//or deleting them wouldn't last until the next request, where they would
//still be available
//since my data is hardcoded I need to keep it "alive" for the whole session
builder.Services.AddSingleton<UserService>();

//however for date we're not storing any data so it's better to use scoped
builder.Services.AddScoped<DateService>();

//finally for image?
builder.Services.AddScoped<ImageService>();

//add the middleware i looked online and we should register the middleware as a service before
//calling it. it should be transient since we want a new instance for every request
builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();

// Enable static file serving
builder.Services.AddDirectoryBrowser();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//this is where the middleware are listed, put exception handling at the top!
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapControllers();

app.Run();