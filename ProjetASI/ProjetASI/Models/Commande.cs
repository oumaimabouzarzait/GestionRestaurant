using System.ComponentModel.DataAnnotations;

namespace ProjetASI.Models
{
    public enum EtatCommande
    {
        [Display(Name = "En cours")]
        EN_COURS,
        [Display(Name = "Prête")]
        PRETE,
        [Display(Name = "Servie")]
        SERVIE
    }

    public class Commande
    {
        [Key]
        [Display(Name = "Numéro")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Date et heure")]
        public DateTime DateHeure { get; set; }
        [Required]
        [Display(Name = "État")]
        public EtatCommande Etat { get; set; }
        [Required]
        [Display(Name = "Encaissée")]
        public bool Encaissee { get; set; }
        [Display(Name = "Table")]
        public int TableId { get; set; }
        public Table? Table { get; set; }
        [Display(Name = "Serveur")]
        public int ServeurId { get; set; }
        public Serveur? Serveur { get; set; }
        [Display(Name = "Barman")]
        public int? BarmanId { get; set; }
        public Barman? Barman { get; set; }
        public Facture? Facture { get; set; }
        public ICollection<CommandeProduit>? LesProduitsCommandes { get; set; }
    }
}
