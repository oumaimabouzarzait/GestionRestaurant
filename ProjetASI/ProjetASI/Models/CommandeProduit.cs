using System.ComponentModel.DataAnnotations;

namespace ProjetASI.Models
{
    public class CommandeProduit
    {
        [Key]
        public int ID { get; set; }
        public int? LaCommandeId { get; set; }
        public Commande LaCommande { get; set; }
        public int LeProduitId { get; set; }
        public Produit LeProduit { get; set; }
        public int QuantiteProduit { get; set; }
    }
}
