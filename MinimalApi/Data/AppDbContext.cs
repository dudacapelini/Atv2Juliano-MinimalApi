using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
    
    {
        if (Database.GetPendingMigrations().Any())
            Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        #region nota

        modelBuilder.Entity<Nota>().HasKey(t => t.Id);
        modelBuilder.Entity<Nota>().Property(t => t.Aluno).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Nota>().Property(t => t.Disciplina).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Nota>().Property(t => t.Valor).IsRequired().HasPrecision(4, 2);
        modelBuilder.Entity<Nota>().Property(t => t.DataLancamento).IsRequired();

        #endregion nota
    }

    public DbSet<Nota> Notas { get; set; }
}
