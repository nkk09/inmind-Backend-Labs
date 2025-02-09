using System.Threading.RateLimiting;
using lab1_nour_kassem.Filters;
using lab1_nour_kassem.Models;
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
builder.Services.AddControllers(options => { options.Filters.Add<LoggingActionFilter>(); });
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

builder.Services.AddSingleton<ObjectMapperService>();

// Enable static file serving
builder.Services.AddDirectoryBrowser();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

//test objectmapperservice
var student = new Student { firstname = "Nour", lastname = "Kassem", age = 20, studentID = 202300000};
var mapper = app.Services.GetRequiredService<ObjectMapperService>();
var person = mapper.Map<Student, Person>(student);
Console.WriteLine(person.FirstName + " " + person.LastName + " " + person.Age + " ");
Console.WriteLine(student.firstname + " " + student.lastname + " " + student.age + " " + student.studentID);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//this is where the middleware are listed, put exception handling at the top!
//and now we add logging just before exception handling because we want requests to be logged before being processed
//we call next and then log the response (which could be an error raised by exception handler
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapControllers();

app.Run();