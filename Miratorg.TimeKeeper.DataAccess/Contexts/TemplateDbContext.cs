using Microsoft.EntityFrameworkCore;
using Miratorg.TimeKeeper.DataAccess.Entities;

namespace Miratorg.TimeKeeper.DataAccess.Contexts;

public class TemplateDbContext : DbContext
{
    public TemplateDbContext(DbContextOptions<TemplateDbContext> options) : base(options)
    {
        Database.Migrate();
    }

    public virtual DbSet<SimpleEntity> Simples { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Model.SetCollation("Cyrillic_General_100_CI_AI"); // Note: возможно будет необходимо

        base.OnModelCreating(modelBuilder);
    }
}