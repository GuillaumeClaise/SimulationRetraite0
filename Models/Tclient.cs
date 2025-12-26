using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimulationRetraite0.Models;

[Table("TClients", Schema = "dbo")]
public class Tclient
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("TC_Id")]
    public int TcId { get; set; }

    [Column("TC_Nom")]
    [Required(ErrorMessage = "Le nom est requis")]
    public string TcNom { get; set; } = string.Empty;

    [Column("TC_Prenom")]
    [Required(ErrorMessage = "Le prénom est requis")]
    public string TcPrenom { get; set; } = string.Empty;

    [Column("TC_Email")]
    [EmailAddress(ErrorMessage = "Email invalide")]
    public string? TcEmail { get; set; }

    [Column("TC_NomDossier")]
    public string? TcNomDossier { get; set; }
}
