using FluentValidation;
using FluentValidation.AspNetCore;
using UsersService.BLL.Domain.Implementation;
using UsersService.BLL.Interfaces;
using UsersService.BLL.Validators;
using UsersService.BLL.Validators.Interfaces;
using UsersService.DAL.Extensions;
using UsersService.PL.Middleware;
using UsersService.PL.Validators;

namespace UsersService;

public class Startup
{
    private readonly IConfiguration _configuration;
    
    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen();
        services.AddFluentValidationAutoValidation();
        
        services.AddValidatorsFromAssemblyContaining(typeof(CreateUserRequestValidator));
        services.AddValidatorsFromAssemblyContaining(typeof(GetUserByLoginRequestValidator));
        services.AddValidatorsFromAssemblyContaining(typeof(GetUserByLoginAndPasswordRequestValidator));
        services.AddValidatorsFromAssemblyContaining(typeof(GetUsersByAgeRequestValidator));
        services.AddValidatorsFromAssemblyContaining(typeof(UpdateUserProfileRequestValidator));
        services.AddValidatorsFromAssemblyContaining(typeof(UpdateUserPasswordRequestValidator));
        services.AddValidatorsFromAssemblyContaining(typeof(UpdateUserLoginRequestValidator));
        services.AddValidatorsFromAssemblyContaining(typeof(DeleteUserRequestValidator));
        services.AddValidatorsFromAssemblyContaining(typeof(RestoreUserRequestValidator));
        
        services.AddMvc(options =>
            {
                options.Filters.Add(typeof(BusinessExceptionFilter));
            });
        
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserAccessValidator, UserAccessValidator>();

        
        services
            .AddDalInfrastructure((IConfigurationRoot)_configuration)
            .AddDalRepositories();
    }
    
    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseEndpoints(
            endpoints =>
            {
                endpoints.MapControllers();
            });
    }
}