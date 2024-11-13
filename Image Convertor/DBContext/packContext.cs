using Image_Convertor.Models;
using Microsoft.EntityFrameworkCore;
using SkiaSharp;

namespace Image_Convertor.DBContext
{
    public class packContext : DbContext
    {
        public packContext(DbContextOptions<packContext> options) : base(options)
        {
        }
        public DbSet<IconPack> Pack { get; set; }
        public DbSet<Icon> Icons { get; set; }
    }
}
