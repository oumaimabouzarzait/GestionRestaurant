using System.ComponentModel.DataAnnotations;

namespace ProjetASI.Models
{
    public class Caissier
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        public ICollection<Facture>? Factures { get; set; }
        public override string ToString()
        {
            return Prenom + " " + Nom;
        }
    }
}
