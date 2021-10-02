using Basket.API.Consumers;
using Basket.Core.Configuration.Mappings;
using Basket.Data.Data.Abstract;
using Basket.Data.Data.Concrete;
using Basket.Data.Repository.Abstract;
using Basket.Data.Repository.Concrete;
using Basket.Services.Abstract;
using Basket.Services.Concrete;
using Basket.Services.ValidationRules.FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Shared.Constants;
using StackExchange.Redis;

namespace Basket.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<BasketPriceChangedEventConsumer>();
                x.AddConsumer<BasketCheckoutEventConsumer>();
                
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(Configuration["RabbitMQUrl"], "/", host =>
                    {
                        host.Username("guest");
                        host.Password("guest");
                    });

                    cfg.ReceiveEndpoint(RabbitMqContants.BasketPriceChangedQueueName, e =>
                    {
                        e.ConfigureConsumer<BasketPriceChangedEventConsumer>(context);
                    });

                    cfg.ReceiveEndpoint(RabbitMqContants.BasketCheckoutEventQueueName, e =>
                    {
                        e.ConfigureConsumer<BasketCheckoutEventConsumer>(context);
                    });

                });
            });

            services.AddMassTransitHostedService();

            services.AddSingleton<ConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(Configuration.GetConnectionString("Redis")));
            
            services.AddScoped<IBasketContext, BasketContext>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IBasketService, BasketService>();
            
            services.AddAutoMapper(typeof(GeneralMapping));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Basket.API", Version = "v1" });
            });

            services.AddControllersWithViews()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<BasketDtoValidator>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
