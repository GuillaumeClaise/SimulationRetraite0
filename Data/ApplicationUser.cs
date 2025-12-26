using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using SimulationRetraite0.Models;

namespace SimulationRetraite0.Data;

public class ApplicationUser : IdentityUser
{
    [JsonIgnore]
    public override string? PasswordHash { get; set; }

    [NotMapped]
    public string? Password { get; set; }

    [NotMapped]
    public string? ConfirmPassword { get; set; }

    [JsonIgnore, NotMapped]
    public string? Name
    {
        get => UserName;
        set => UserName = value;
    }

    public ICollection<ApplicationRole>? Roles { get; set; }
}

