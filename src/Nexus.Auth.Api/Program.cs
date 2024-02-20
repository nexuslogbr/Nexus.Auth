using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Nexus.Auth.Api.Helpers;
using Nexus.Auth.API.Dependency;
using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Infra.Context;
using Nexus.Auth.Repository.Utils;
using System.Dynamic;
using System.Text;
using System.Text.Json;
using Yarp.ReverseProxy.Transforms;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add the reverse proxy capability to the server
builder.Services.AddReverseProxy()
    // Initialize the reverse proxy from the "ReverseProxy" section of configuration
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddTransforms(yarp =>
    {
        yarp.AddResponseTransform(async context =>
        {
            if (context.ProxyResponse != null && context.ProxyResponse?.Content != null)
            {
                var status = (int)context.ProxyResponse!.StatusCode;
                var mediaType = context.ProxyResponse.Content.Headers.ContentType?.MediaType;

                if (mediaType == "application/json")
                {
                    var stream = await context.ProxyResponse.Content.ReadAsStreamAsync();
                    using var reader = new StreamReader(stream);
                    var content = await reader.ReadToEndAsync();

                    context.SuppressResponseBody = true;
                    var obj = JsonSerializer.Deserialize<object?>(content);


                    if (context.ProxyResponse?.IsSuccessStatusCode == true)
                    {
                        var result = new GenericCommandResult<object?>(true, "Success", obj, status);
                        await context.HttpContext.Response.WriteAsJsonAsync(result);
                    }
                    else
                    {
                        var result = new GenericCommandResult<object?>(true, "Error", obj, status);
                        await context.HttpContext.Response.WriteAsJsonAsync(result);
                    }

                }
            }

        });
    });

builder.Services.AddEndpointsApiExplorer();

var connectionString = builder.Configuration.GetConnectionString("NexusAuthConnection");

builder.Services.AddDbContext<NexusAuthContext>(
    x => x.UseSqlServer(connectionString,
    b => b.MigrationsAssembly("Nexus.Auth.Infra"))
    .EnableSensitiveDataLogging());

// Manage required characters in generate password to user
IdentityBuilder identityBuilder = builder.Services.AddIdentityCore<User>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
});

identityBuilder = new IdentityBuilder(identityBuilder.UserType, typeof(Role), builder.Services);
identityBuilder.AddEntityFrameworkStores<NexusAuthContext>();
identityBuilder.AddRoleValidator<RoleValidator<Role>>();
identityBuilder.AddRoleManager<RoleManager<Role>>();
identityBuilder.AddSignInManager<SignInManager<User>>();
builder.Services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<NexusAuthContext>()
    .AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
            .GetBytes(builder.Configuration.GetSection("AppSettings:Key").Value!)),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

    //.AddCookie("CookieAuthScheme", options =>
    //{
    //    options.Cookie.Name = "NexusAuthCookie";
    //    options.LoginPath = "/api/v1/Auth/Login";
    //    options.LogoutPath = "/api/v1/Auth/Logout";
    //}); ;

// Injection Dependency Configuration 
builder.Services.RegisterDependencies(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("Total",
        builder =>
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
});
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nexus.Auth.Api v1");
        //c.DocExpansion(DocExpansion.None);
    });
//}

app.UseCors("Total");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.MapReverseProxy();

app.Run();
