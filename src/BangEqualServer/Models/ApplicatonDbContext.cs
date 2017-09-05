using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BangEqualServer.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        //object name must be same as table name defined in Migrations
        public DbSet<Article> Article{ get; set; }

        public DbSet<ArticleInfo> ArticleInfo{ get; set; }
    }
}
