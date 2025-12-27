using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimulationRetraite0.Models;

[Table("Dossiers", Schema = "dbo")]
public class Dossier
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Td_Id")]
    public int TdId { get; set; }

    [Column("Td_NomClient")]
    [Required(ErrorMessage = "Le nom est requis")]
    [StringLength(70)]
    public string TdNomClient { get; set; } = string.Empty;

    [Column("Td_PrenomClient")]
    [Required(ErrorMessage = "Le prénom est requis")]
    [StringLength(70)]
    public string TdPrenomClient { get; set; } = string.Empty;

    [Column("Td_DateDossier")]
    public DateTime TdDateDossier { get; set; } = DateTime.Now;

    [Column("Td_NomDossier")]
    [StringLength(200)]
    public string? TdNomDossier { get; set; }

    [Column("Td_Description")]
    [StringLength(500)]
    public string? TdDescription { get; set; }
}
