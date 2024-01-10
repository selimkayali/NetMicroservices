using Discount.Grpc.Extensions;
using Discount.Grpc.Repositories;
using Discount.Grpc.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Discount.Grpc;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddGrpc(opt =>
        {
            opt.EnableDetailedErrors = true;
        });

        //builder.WebHost.ConfigureKestrel(opt =>
        //{
        //    opt.ListenAnyIP(8003, o => o.Protocols = HttpProtocols.Http2);
        //});

        InjectDependencies(builder.Services);


        var app = builder.Build();

        app.MigrateDatabase<Program>();

        // Configure the HTTP request pipeline.
        app.MapGrpcService<DiscountService>();

        app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

        app.Run();


    }

    private static void InjectDependencies(IServiceCollection services)
    {
        services.AddScoped<IDiscountRepository, DiscountRepository>();
        services.AddAutoMapper(typeof(Program));
    }
}