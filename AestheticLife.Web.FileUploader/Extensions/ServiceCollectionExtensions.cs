using System.Reflection;

namespace AestheticLife.Web.FileUploader.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFileUploaderWebMapper(this IServiceCollection services)
        => services.AddAutoMapper(Assembly.GetExecutingAssembly());
}