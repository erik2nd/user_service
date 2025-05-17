using FluentValidation;
using FluentValidation.AspNetCore;
using UsersService.BLL.Domain.Implementation;
using UsersService.BLL.Interfaces;
using UsersService.DAL.Extensions;
using UsersService.PL.Middleware;
using UsersService.PL.Validators;

namespace UsersService;

public class Startup
{
    private readonly IConfigurationRoot _configuration;
    
    public Startup(IConfigurationRoot configuration)
    {
        _configuration = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen();
        services.AddFluentValidationAutoValidation();
        
        services.AddValidatorsFromAssemblyContaining(typeof(UserDtoValidator));
        
        services.AddMvc(options =>
            {
                options.Filters.Add(typeof(BusinessExceptionFilter));
            });
        
        services.AddScoped<IUserService, UserService>();
        
        services
            .AddDalInfrastructure(_configuration)
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