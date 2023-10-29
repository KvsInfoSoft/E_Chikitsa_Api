using E_Chikitsa_Api.Configuration;
using E_Chikitsa_Api.Middleware;
using E_Chikitsa_Api.Services;
using E_Chikitsa_DBConfiguration.DatabaseContext;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AdoConnectionProvider(builder.Configuration); //This database connection
builder.Services.Add(new ServiceDescriptor(typeof(AdoContext), new AdoContext(builder.Configuration["ConnectionStrings:connection"])));
builder.Services.ConfigureRepositeries(); //for Interface and services
builder.Services.ConfigureServices(); // dependency injection 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "E_Chikitsa_Api", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Please Enter Token",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
    {
        {
             new OpenApiSecurityScheme
             {
                 Reference = new OpenApiReference
                 {
                     Type = ReferenceType.SecurityScheme,
                     Id= "Bearer"
                 },
                 Scheme = "oauth2",
                 Name ="Bearer",
                 In = ParameterLocation.Header,
             },
             new List<string>()
        }
    });
    
});

//builder.Services.AddAuthentication("BearerAuthentication")
//    .AddScheme<AuthenticationSchemeOptions, BearerAuthenticationHandler>("BearerAuthentication", null);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
