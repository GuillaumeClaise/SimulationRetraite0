using Microsoft.AspNetCore.Identity;
using SimulationRetraite0.Data;

namespace SimulationRetraite0.Models;

public class ApplicationRole : IdentityRole
{
    public ICollection<ApplicationUser>? Users { get; set; }
}
