using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimulationRetraite0.Models;

[Table("LignesTableau", Schema = "dbo")]
public class LigneTableau
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Lt_Id")]
    public int LtId { get; set; }

    [Column("Lt_TabId")]
    public int LtTabId { get; set; }

    [Column("Lt_NumeroLigne")]
    public int LtNumeroLigne { get; set; }

    [Column("Lt_Libelle")]
    [StringLength(200)]
    public string? LtLibelle { get; set; }

    [Column("Lt_Valeur1")]
    [StringLength(50)]
    public string? LtValeur1 { get; set; }

    [Column("Lt_Valeur2")]
    [StringLength(50)]
    public string? LtValeur2 { get; set; }

    [Column("Lt_Valeur3")]
    [StringLength(50)]
    public string? LtValeur3 { get; set; }

    [Column("Lt_Valeur4")]
    [StringLength(50)]
    public string? LtValeur4 { get; set; }

    [Column("Lt_Valeur5")]
    [StringLength(50)]
    public string? LtValeur5 { get; set; }

    [Column("Lt_Valeur6")]
    [StringLength(50)]
    public string? LtValeur6 { get; set; }

    [Column("Lt_Valeur7")]
    [StringLength(50)]
    public string? LtValeur7 { get; set; }

    [Column("Lt_Valeur8")]
    [StringLength(50)]
    public string? LtValeur8 { get; set; }

    [Column("Lt_Valeur9")]
    [StringLength(50)]
    public string? LtValeur9 { get; set; }

    [Column("Lt_Valeur10")]
    [StringLength(50)]
    public string? LtValeur10 { get; set; }

    // Navigation property
    [ForeignKey("LtTabId")]
    public virtual Tableau? Tableau { get; set; }
}
