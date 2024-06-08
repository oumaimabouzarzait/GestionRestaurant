using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetASI.Models
{
    public class Produit
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        [Column(TypeName = "Money")]
        public decimal Prix { get; set; }
        [Required]
        public int Stock { get; set; }
        public ICollection<CommandeProduit>? LesCommandesProduits { get; set; }
    }
}
