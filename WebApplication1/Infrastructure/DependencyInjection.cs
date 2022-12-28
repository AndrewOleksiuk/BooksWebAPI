using Application.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence {
  public static class DependencyInjection
    {
        public static void AddInfrastrucuture(this IServiceCollection services, IConfiguration configuration)
        {
      services.AddDbContext<BooksContext>(options =>
          options.UseInMemoryDatabase("BooksDB"));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<BooksContext>());
        }
    }
}
