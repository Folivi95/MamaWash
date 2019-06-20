using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MamaWash.Models
{
    public static class SeedBanks
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (MamaWashContext context = new MamaWashContext(serviceProvider.GetRequiredService<DbContextOptions<MamaWashContext>>()))
            {

            }
        }
    }
}
