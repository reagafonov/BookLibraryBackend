using System;
using System.IO;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Services.Abstractions.Exceptions;
using Services.Repositories.Abstractions.Exceptions;
using WebApi.Mapping;

namespace WebApi;

public class Startup(IConfiguration configuration)
{
    // ReSharper disable once MemberCanBePrivate.Global
    public IConfiguration Configuration { get; } = configuration;

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        InstallAutomapper(services);
        services.AddServices(Configuration);
        services.AddControllers();
        // Register the Swagger generator, defining 1 or more Swagger documents
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Библиотека книг",

                Contact = new OpenApiContact
                {
                    Name = "Roman Agafnonov",
                    Url = new Uri("https://t.me/roman_telegr"),
                    Email = "reagafonov@yandex.ru"
                }
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
        services.AddEndpointsApiExplorer();
    }


    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler(exceptionHandlerApp =>
            {
                exceptionHandlerApp.Run(async context =>
                {
                    var responseBuilder = new StringBuilder();
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                    // using static System.Net.Mime.MediaTypeNames;
                    context.Response.ContentType = MediaTypeNames.Text.Plain;

                    responseBuilder.Append("Ошибка:");

                    var exceptionHandlerPathFeature =
                        context.Features.Get<IExceptionHandlerPathFeature>();

                    switch (exceptionHandlerPathFeature?.Error)
                    {
                        case CrudUpdateException crud:
                            context.Response.StatusCode = StatusCodes.Status400BadRequest;
                            responseBuilder.AppendLine(crud.Message);
                            break;
                        case DtoValidationException dtoValidation:
                            context.Response.StatusCode = StatusCodes.Status400BadRequest;
                            responseBuilder.AppendLine(dtoValidation.Message);
                            break;
                        case ObjectNotFoundException notFoundException:
                            context.Response.StatusCode = StatusCodes.Status404NotFound;
                            responseBuilder.AppendLine(notFoundException.Message);
                            break;
                    }

                    await context.Response.WriteAsync(responseBuilder.ToString());
                });
            });
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        //включено для демонстрации
        //if (!env.IsProduction())
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Каталог книг V1");
                c.RoutePrefix = string.Empty;
            });
        }

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }

    private static void InstallAutomapper(IServiceCollection services)
    {
        services.AddSingleton<IMapper>(new Mapper(GetMapperConfiguration()));
    }

    private static MapperConfiguration GetMapperConfiguration()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<BookMappingsProfile>();
            cfg.AddProfile<AuthorMappingsProfile>();
            cfg.AddProfile<Services.Implementations.Mapping.BookMappingsProfile>();
            cfg.AddProfile<Services.Implementations.Mapping.AuthorMappingsProfile>();
        });
        configuration.AssertConfigurationIsValid();
        return configuration;
    }
}