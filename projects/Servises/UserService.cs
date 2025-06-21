using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using projects.Models;

namespace projects.Servises
{
    public class UserService
    {
        private readonly ApplicationDbContext context;
        private readonly IMemoryCache cache;

        public UserService(ApplicationDbContext context, IMemoryCache cache)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }
    }
}
