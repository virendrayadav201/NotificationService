using NotificationService.Infrastructure;

namespace NotificationService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ServiceRegistration.RegisterService(builder.Services, builder.Configuration);
            // Add services to the container.
            builder.Services.AddCors(option =>
                option.AddPolicy("AllowAll",policy =>
                {
                    policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
                })                
            );

            builder.Services.AddControllers();
             

            var app = builder.Build();
            app.UseCors("AllowAll");
            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            
            app.MapControllers();

            app.Run();
        }
    }
}
