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

    public async Task UpdateTableauAsync(Tableau tableau)
    {
        var existingTableau = await context.Tableaux.FindAsync(tableau.TabId);
        if (existingTableau != null)
        {
            existingTableau.TabNomTableau = tableau.TabNomTableau;
            existingTableau.TabDescription = tableau.TabDescription;
            await context.SaveChangesAsync();
        }
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

    // Méthodes pour la gestion des lignes de tableau
    public async Task UpdateLigneTableauAsync(LigneTableau ligne)
    {
        var existingLigne = await context.LignesTableau.FindAsync(ligne.LtId);
        if (existingLigne != null)
        {
            existingLigne.LtNumeroLigne = ligne.LtNumeroLigne;
            existingLigne.LtLibelle = ligne.LtLibelle;
            existingLigne.LtValeur1 = ligne.LtValeur1;
            existingLigne.LtValeur2 = ligne.LtValeur2;
            existingLigne.LtValeur3 = ligne.LtValeur3;
            existingLigne.LtValeur4 = ligne.LtValeur4;
            existingLigne.LtValeur5 = ligne.LtValeur5;
            existingLigne.LtValeur6 = ligne.LtValeur6;
            existingLigne.LtValeur7 = ligne.LtValeur7;
            existingLigne.LtValeur8 = ligne.LtValeur8;
            existingLigne.LtValeur9 = ligne.LtValeur9;
            existingLigne.LtValeur10 = ligne.LtValeur10;
            await context.SaveChangesAsync();
        }
    }

    public async Task<LigneTableau> CreateLigneTableauAsync(LigneTableau ligne)
    {
        context.LignesTableau.Add(ligne);
        await context.SaveChangesAsync();
        return ligne;
    }

    public async Task DeleteLigneTableauAsync(int ligneId)
    {
        var ligne = await context.LignesTableau.FindAsync(ligneId);
        if (ligne != null)
        {
            context.LignesTableau.Remove(ligne);
            await context.SaveChangesAsync();
        }
    }
}
