using System.ComponentModel.DataAnnotations;

namespace ProjetASI.Models
{
    public class Serveur
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        public ICollection<Commande>? Commandes { get; set; }
        public override string ToString()
        {
            return Prenom + " " + Nom;
        }
    }
}
