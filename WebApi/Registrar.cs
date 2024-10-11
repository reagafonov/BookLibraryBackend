using Domain.Entities;
using Infrastructure.EntityFramework;
using Infrastructure.Repositories.Implementations;
using Infrastructure.Repositories.Implementations.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions;
using Services.Contracts;
using Services.Implementations;
using Services.Implementations.Validation;
using Services.Repositories.Abstractions;
using WebApi.Settings;

namespace WebApi;

/// <summary>
/// Регистратор сервиса
/// </summary>
public static class Registrar
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        var applicationSettings = configuration.Get<ApplicationSettings>()!;
        services.AddSingleton(applicationSettings);
        services.AddSingleton((IConfigurationRoot)configuration)
            .InstallServices()
            .ConfigureContext(applicationSettings.ConnectionString)
            .InstallRepositories();
    }

    private static IServiceCollection InstallServices(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddTransient<IBookService, BookService>()
            .AddTransient<IAuthorService, AuthorService>()
            .AddTransient<IValidateDto<AuthorDto>, AuthorValidate>()
            .AddTransient<IValidateDto<BookDto>, BookValidation>();


        return serviceCollection;
    }

    private static void InstallRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddTransient<IBookRepository, BookRepository>()
            .AddTransient<IAuthorRepository, AuthorRepository>()
            .AddTransient<ISimpleFilterQuery<Author, AuthorFilter>, AuthorSimpleFilterQuery>()
            .AddTransient<ISimpleFilterQuery<Book, BookFilter>, BookSimpleFilterQuery>();
    }
}