using Microsoft.EntityFrameworkCore;
using SimulationRetraite0.Data;
using SimulationRetraite0.Models;

namespace SimulationRetraite0.Services;

public class TableauService
{
    private readonly RetraiteSimulationsContext context;

    public TableauService(RetraiteSimulationsContext context)
    {
        this.context = context;
    }

    public async Task<List<Tableau>> GetTableauxByDossierIdAsync(int dossierId)
    {
        return await context.Tableaux
            .Include(t => t.Lignes)
            .Where(t => t.TabTdId == dossierId)
            .OrderBy(t => t.TabNumeroTableau)
            .ToListAsync();
    }

    public async Task<Tableau?> GetTableauByIdAsync(int id)
    {
        return await context.Tableaux
            .Include(t => t.Lignes.OrderBy(l => l.LtNumeroLigne))
            .FirstOrDefaultAsync(t => t.TabId == id);
    }

    public async Task<Tableau> CreateTableauWithLignesAsync(Tableau tableau, List<LigneTableau> lignes)
    {
        // Ajouter le tableau
        context.Tableaux.Add(tableau);
        await context.SaveChangesAsync();

        // Ajouter les lignes liées au tableau
        foreach (var ligne in lignes)
        {
            ligne.LtTabId = tableau.TabId;
            context.LignesTableau.Add(ligne);
        }

        await context.SaveChangesAsync();
        return tableau;
    }

    public async Task DeleteTableauAsync(int id)
    {
        var tableau = await context.Tableaux.FindAsync(id);
        if (tableau != null)
        {
            // Les lignes seront supprimées automatiquement grâce au CASCADE DELETE
            context.Tableaux.Remove(tableau);
            await context.SaveChangesAsync();
        }
    }

    public async Task<int> GetNextNumeroTableauAsync(int dossierId)
    {
        var maxNumero = await context.Tableaux
            .Where(t => t.TabTdId == dossierId)
            .MaxAsync(t => (int?)t.TabNumeroTableau);

        return (maxNumero ?? 0) + 1;
    }
}
