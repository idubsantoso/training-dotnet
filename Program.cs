using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Authorization;
using WebApi.Data;
using WebApi.Dto;
using WebApi.Exceptions;
using WebApi.Helpers;
using WebApi.Queue;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// add services to DI container
{
    var services = builder.Services;
    services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    services.AddCors();
    services.AddControllers();

    builder.Services.AddHangfire(configuration => configuration
       .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
       .UseSimpleAssemblyNameTypeSerializer()
       .UseRecommendedSerializerSettings()
       .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
       {
           CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
           SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
           QueuePollInterval = TimeSpan.Zero,
           UseRecommendedIsolationLevel = true,
           DisableGlobalLocks = true
       }));
    builder.Services.AddHangfireServer();

    // configure strongly typed settings object
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    services.AddAutoMapper(typeof(Program).Assembly);
    // configure DI for application services
    services.AddScoped<IJwtUtils, JwtUtils>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IBookService, BookService>();
    services.AddScoped<IAuthorService, AuthorService>();
    services.AddScoped<IBookAuthorService, BookAuthorService>();
    services.AddScoped<ICinemaService, CinemaService>();
    services.AddScoped<IMovieService, MovieService>();
    services.AddResponseCaching();
    services.AddHttpClient();
    services.Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

    services
        .AddHostedService<QueuedHostedService>()
        .AddSingleton<IBackgroundTaskQueue<BookDto>, DefaultBackgroundTaskQueue<BookDto>>();
}

var app = builder.Build();

// configure HTTP request pipeline
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();

    app.MapControllers();
}

app.Run("http://localhost:5000");