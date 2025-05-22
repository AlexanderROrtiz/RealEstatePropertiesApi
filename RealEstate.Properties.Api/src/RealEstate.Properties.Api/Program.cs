using RealEstate.Properties.Application.Commands.PropertiesCommand;
using RealEstate.Properties.Application.Interfaces;
using RealEstate.Properties.Application.Interfaces.Kafka;
using RealEstate.Properties.Application.Kafka.Interfaces;
using RealEstate.Properties.Infrastructure.Kafka.Producer;
using RealEstate.Properties.Infrastructure.Mappings;
using RealEstate.Properties.Infrastructure.Persistence;
using RealEstate.Properties.Infrastructure.SelectorTemplate;
using System.Diagnostics.CodeAnalysis;

namespace RealEstate.Properties.Api;

[ExcludeFromCodeCoverage]
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        // --- MediatR ---
        builder.Services.AddMediatR(cfg =>
        cfg.RegisterServicesFromAssemblyContaining<CreatePropertyCommand>());

        // --- AutoMapper ---
        builder.Services.AddAutoMapper(typeof(PropertyMappings).Assembly);

        // --- MongoDB ---
        var mongoConnStr = builder.Configuration.GetSection("MongoDb:ConnectionString").Value;
        var mongoDbName = builder.Configuration.GetSection("MongoDb:Database").Value;
        builder.Services.AddSingleton(new MongoDbContext(mongoConnStr, mongoDbName));

        // --- Repositorios ---
        builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
        builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
        builder.Services.AddScoped<IPropertyImageRepository, PropertyImageRepository>();
        builder.Services.AddScoped<IPropertyTraceRepository, PropertyTraceRepository>();

        // --- Kafka Producer ---
        builder.Services.AddSingleton<IEventSelector, EventSelector>();
        builder.Services.AddSingleton<IPropertyEventProducer>(sp =>
                new PropertyEventProducer(builder.Configuration.GetSection("Kafka:BootstrapServers").Value,
                                          sp.GetRequiredService<IEventSelector>()));

        // --- Controllers ---
        builder.Services.AddControllers()
            .AddNewtonsoftJson(); // Para mejor serialización/compatibilidad

        // --- Swagger ---
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // --- CORS (opcional, útil para frontend) ---
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
        });

        var app = builder.Build();
        app.UseMiddleware<ErrorHandling.ExceptionMiddleware>();
        // --- Middlewares ---
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("AllowAll");

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}