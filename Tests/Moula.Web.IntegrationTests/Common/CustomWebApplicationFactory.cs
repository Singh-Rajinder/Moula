using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moula.Application.Contracts;
using Moula.Persistence;

namespace Moula.Web.IntegrationTests.Common
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddDbContext<MoulaContext>(options =>
                {
                    options.UseInMemoryDatabase(Guid.NewGuid().ToString());
                    options.UseInternalServiceProvider(serviceProvider);
                    options.ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));

                });

                services.AddScoped<IMoulaContext>(provider => provider.GetService<MoulaContext>());
                var sp = services.BuildServiceProvider();

                services.AddScoped<ICurrentUserService, CurrentUserService>();


                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var context = scopedServices.GetRequiredService<MoulaContext>();
                var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory>>();



                // Ensure the database is created.
                context.Database.EnsureCreated();
                if (!context.Customers.Any())
                    Helper.InitDatabaseForTest(context);

            }).UseEnvironment("Test");
        }
    }
}
