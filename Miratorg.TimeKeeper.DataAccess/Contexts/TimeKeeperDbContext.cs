namespace Miratorg.TimeKeeper.DataAccess.Contexts;

public class TimeKeeperDbContext : DbContext
{
    public TimeKeeperDbContext(DbContextOptions<TimeKeeperDbContext> options) : base(options)
    {
        Database.Migrate();
    }

    public virtual DbSet<EmployeeEntity> Employees { get; set; }
    public virtual DbSet<StoreEntity> Stores { get; set; }
    public virtual DbSet<ScheduleEntity> Schedules { get; set; }
    public virtual DbSet<ScheduleDateEntity> ScheduleDates { get; set; }
    public virtual DbSet<PlanEntity> Plans { get; set; }
    public virtual DbSet<ScudInfo> ScudInfos { get; set; }
    public virtual DbSet<CustomTypeWorkEntity> CustomTypeWorks { get; set; }
    public virtual DbSet<TypeOverWorkEntity> TypeOverWorks { get; set; }
    public virtual DbSet<TypeTimeZupEntity> TypeTimeZups { get; set; }
    public virtual DbSet<AbsenceEntity> Absences { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Model.SetCollation("Cyrillic_General_100_CI_AI"); // Note: возможно будет необходимо

        base.OnModelCreating(modelBuilder);
    }
}