using Microsoft.EntityFrameworkCore;
using SimulationRetraite0.Models;

namespace SimulationRetraite0.Data;

public class RetraiteSimulationsContext : DbContext
{
    public RetraiteSimulationsContext(DbContextOptions<RetraiteSimulationsContext> options)
        : base(options)
    {
    }

    public DbSet<Tclient> Tclients { get; set; }
    public DbSet<Dossier> Dossiers { get; set; }
    public DbSet<Tableau> Tableaux { get; set; }
    public DbSet<LigneTableau> LignesTableau { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
