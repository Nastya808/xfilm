using DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Infrastructure
{
    public static class XfilmcontextExtensions
    {
        public static void AddXfilmsContext(this IServiceCollection service, string connect)
        {
            service.AddDbContext<XFilmContext>(option=>option.UseSqlServer(connect, x => x.UseDateOnlyTimeOnly()));
        }
    }
}
