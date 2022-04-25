using System;
using System.IO;
using Azure.Identity;
using FunctionApp4;
using FunctionApp4.Configurations;
using FunctionApp4.Repositories;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace FunctionApp4;

public class Startup : FunctionsStartup
{
    /// <summary>
    /// アプリ構成を設定する
    /// </summary>
    /// <param name="builder"></param>
    public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
    {
        var context = builder.GetContext();

        builder.ConfigurationBuilder
            .AddJsonFile(Path.Combine(context.ApplicationRootPath, "appsettings.json"), optional: true, reloadOnChange: false)
            .AddJsonFile(Path.Combine(context.ApplicationRootPath, $"appsettings.{context.EnvironmentName}.json"), optional: true, reloadOnChange: false)
            .AddEnvironmentVariables();
    }

    /// <summary>
    /// サービスを登録する
    /// </summary>
    /// <param name="builder"></param>
    /// <exception cref="System.NotImplementedException"></exception>
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var context = builder.GetContext();

        // OK
        // builder.Services.AddOptions<BlobStorageOptions>()
        //     .Configure<IConfiguration>((options, configuration) =>
        //     {
        //         configuration.GetSection("BlobStorage").Bind(options);
        //     });

        // OK
        builder.Services.Configure<BlobStorageOptions>(context.Configuration.GetSection("BlobStorage"));

        builder.Services.AddAzureClients(azureClientFactoryBuilder =>
        {
            var endpoint = context.Configuration["BlobStorage:Endpoint"];

            azureClientFactoryBuilder.AddBlobServiceClient(new Uri(endpoint));

            azureClientFactoryBuilder.UseCredential(new DefaultAzureCredential());
        });

        builder.Services.AddTransient<IRepository, Repository>();
    }
}