using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimulationRetraite0.Models;

[Table("Tableaux", Schema = "dbo")]
public class Tableau
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Tab_Id")]
    public int TabId { get; set; }

    [Column("Tab_TdId")]
    public int TabTdId { get; set; }

    [Column("Tab_NumeroTableau")]
    public int TabNumeroTableau { get; set; }

    [Column("Tab_NomTableau")]
    [StringLength(100)]
    public string? TabNomTableau { get; set; }

    [Column("Tab_Description")]
    [StringLength(500)]
    public string? TabDescription { get; set; }

    [Column("Tab_DateCreation")]
    public DateTime TabDateCreation { get; set; } = DateTime.Now;

    // Navigation property
    [ForeignKey("TabTdId")]
    public virtual Dossier? Dossier { get; set; }

    // Collection of rows
    public virtual ICollection<LigneTableau> Lignes { get; set; } = new List<LigneTableau>();
}
