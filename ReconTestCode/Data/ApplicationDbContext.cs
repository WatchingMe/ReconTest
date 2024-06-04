using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<CPMapping> CPMappings { get; set; }
    public DbSet<ARAPJDE> ARAPJDEs { get; set; }
    public DbSet<Insurance> Insurances { get; set; }
    public DbSet<Reconciliation> Reconciliations { get; set; }
}
