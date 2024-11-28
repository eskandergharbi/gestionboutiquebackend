using GestionBoutiqueBack.services;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;
using GestionBoutiqueBack.model;
using GestionBoutiqueBack.Services;
using GestionBoutiqueBack.Helpers; // Replace with your actual namespace

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .WithOrigins("http://localhost:8080") // Replace with your frontend origin(s)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});
//builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
//.AddNegotiate();
builder.Services.AddDbContext<HRManagementContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<EmployeeDataService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddLogging();

// Ajouter les services nécessaires pour les sessions
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});
/*builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});*/

var app = builder.Build();
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // custom jwt auth middleware

    app.MapControllers();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSession();

app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();

app.UseCors("CorsPolicy"); // Ensure CORS is still applied

app.MapControllers();

app.Run();
