using Microsoft.EntityFrameworkCore;

namespace SampleApp.Web.DAL
{
    public class WordContext: DbContext
    {
        public WordContext(DbContextOptions<WordContext> options): base(options))
        {
        }
        
        public DbSet<Word> Words { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var wordBuilder = modelBuilder.Entity<Word>();
            wordBuilder.HasKey(d => d.Id);
            wordBuilder.Property(d => d.Value).IsRequired().HasMaxLength(1000);
        }
    }
}
