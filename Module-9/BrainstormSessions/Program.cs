using System;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Email;
using Serilog.Sinks.EmailPickup;

namespace BrainstormSessions
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var config = host.Services.GetRequiredService<IConfiguration>();
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.Email(new EmailConnectionInfo
                    {
                        FromEmail = config["Smtp:FromEmail"],
                        ToEmail = config["Smtp:ToEmail"],
                        MailServer = config["Smtp:MailServer"],
                        NetworkCredentials = new NetworkCredential {
                            UserName = config["Smtp:NetworkCredentials:UserName"],
                            Password = config["Smtp:NetworkCredentials:Password"]
                        },
                        EnableSsl = bool.Parse(config["Smtp:EnableSsl"]),
                        Port = int.Parse(config["Smtp:Port"]),
                        EmailSubject = "Brainstorm Sessions Error"
                    },
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}",
                    batchPostingLimit: 10
                    , restrictedToMinimumLevel: LogEventLevel.Error
                    )
                .WriteTo.EmailPickup(
                    fromEmail: config["Smtp:FromEmail"],
                    toEmail: config["Smtp:ToEmail"],
                    pickupDirectory: config["Smtp:PickupDirectory"],
                    subject: "Brainstorm Sessions Error",
                    fileExtension: ".email",
                    restrictedToMinimumLevel: LogEventLevel.Error
                    )
                .CreateLogger();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();
    }
}
