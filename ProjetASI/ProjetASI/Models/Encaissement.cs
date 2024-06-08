using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ProjetASI.Models
{
    public enum TypeEncaissement
    {
        [Display(Name = "Carte bancaire")]
        CARTE_BANCAIRE,
        [Display(Name = "Espèce")]
        ESPECE,
        [Display(Name = "Titre restaurant")]
        TITRE_RESTAURANT
    }
    public class Encaissement
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public DateTime DateHeure { get; set; }
        [Required]
        [Column(TypeName = "Money")]
        public decimal Montant { get; set; }
        [Required]
        public TypeEncaissement Type { get; set; }
        [Display(Name = "Facture")]
        public int FactureId { get; set; }
        public Facture? Facture { get; set; }
    }
}
