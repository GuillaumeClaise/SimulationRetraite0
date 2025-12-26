using Microsoft.EntityFrameworkCore;
using SimulationRetraite0.Data;
using SimulationRetraite0.Models;

namespace SimulationRetraite0.Services;

public class ClientService
{
    private readonly RetraiteSimulationsContext context;

    public ClientService(RetraiteSimulationsContext context)
    {
        this.context = context;
    }

    public async Task<List<Tclient>> GetClientsAsync()
    {
        return await context.Tclients
            .OrderBy(c => c.TcNom)
            .ThenBy(c => c.TcPrenom)
            .ToListAsync();
    }

    public async Task<Tclient?> GetClientByIdAsync(int id)
    {
        return await context.Tclients.FindAsync(id);
    }

    public async Task<Tclient> CreateClientAsync(Tclient client)
    {
        context.Tclients.Add(client);
        await context.SaveChangesAsync();
        return client;
    }

    public async Task<Tclient> UpdateClientAsync(Tclient client)
    {
        context.Entry(client).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return client;
    }

    public async Task DeleteClientAsync(int id)
    {
        var client = await context.Tclients.FindAsync(id);
        if (client != null)
        {
            context.Tclients.Remove(client);
            await context.SaveChangesAsync();
        }
    }
}
