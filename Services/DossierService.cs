using Microsoft.EntityFrameworkCore;
using SimulationRetraite0.Data;
using SimulationRetraite0.Models;

namespace SimulationRetraite0.Services;

public class DossierService
{
    private readonly RetraiteSimulationsContext context;

    public DossierService(RetraiteSimulationsContext context)
    {
        this.context = context;
    }

    public async Task<List<Dossier>> GetDossiersAsync()
    {
        return await context.Dossiers
            .OrderBy(d => d.TdNomClient)
            .ThenBy(d => d.TdPrenomClient)
            .ToListAsync();
    }

    public async Task<Dossier?> GetDossierByIdAsync(int id)
    {
        return await context.Dossiers.FindAsync(id);
    }

    public async Task<Dossier> CreateDossierAsync(Dossier dossier)
    {
        context.Dossiers.Add(dossier);
        await context.SaveChangesAsync();
        return dossier;
    }

    public async Task<Dossier> UpdateDossierAsync(Dossier dossier)
    {
        // Détacher l'entité existante si elle est déjà trackée
        var existingEntity = context.Dossiers.Local.FirstOrDefault(d => d.TdId == dossier.TdId);
        if (existingEntity != null)
        {
            context.Entry(existingEntity).State = EntityState.Detached;
        }

        context.Entry(dossier).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return dossier;
    }

    public async Task DeleteDossierAsync(int id)
    {
        var dossier = await context.Dossiers.FindAsync(id);
        if (dossier != null)
        {
            context.Dossiers.Remove(dossier);
            await context.SaveChangesAsync();
        }
    }
}
