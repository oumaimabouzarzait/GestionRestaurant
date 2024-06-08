using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ProjetASI.Models
{
    public class Facture
    {
        [Key]
        [Display(Name = "Numéro")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Montant total")]
        [Column(TypeName = "Money")]
        public decimal MontantTotal { get; set; }
        [Required]
        [Display(Name = "Date et heure")]
        public DateTime DateHeure { get; set; }
        [Display(Name = "Caissier")]
        public int CaissierId { get; set; }
        public Caissier? Caissier { get; set; }
        [Display(Name = "Commande")]
        public int CommandeId { get; set; }
        public Commande? Commande { get; set; }
        public ICollection<Encaissement>? Encaissements { get; set; }

    }
}
