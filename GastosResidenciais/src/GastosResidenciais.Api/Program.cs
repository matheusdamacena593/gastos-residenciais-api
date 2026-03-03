
using GastosResidenciais.Api.Filters;
using GastosResidenciais.Application;
using GastosResidenciais.Infrastructure;

namespace GastosResidenciais.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddRouting(options => options.LowercaseUrls = true);

            builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

            builder.Services.AddInfrastruture(builder.Configuration);
            builder.Services.AddApplication();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("FrontendLocal", policy =>
                {
                    policy
                        .WithOrigins(
                            "http://localhost:3000",
                            "https://localhost:3000",
                            "http://127.0.0.1:3000"
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    // .AllowCredentials();
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("FrontendLocal");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
