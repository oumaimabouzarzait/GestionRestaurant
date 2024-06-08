using System.ComponentModel.DataAnnotations;

namespace ProjetASI.Models
{
    public enum EtatTable
    {
        [Display(Name = "Vide")]
        VIDE,
        [Display(Name = "En attente")]
        EN_ATTENTE,
        [Display(Name = "Commandé")]
        COMMANDE
    }
    public class Table
    {
        [Key]
        [Display(Name = "Numéro")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nombre de places")]
        public int NbPlaces { get; set; }
        [Required]
        [Display(Name = "Nombre de clients")]
        public int NbClients { get; set; }
        [Required]
        [Display(Name = "État")]
        public EtatTable Etat { get; set; }
        public ICollection<Commande>? Commandes { get; set; }
    }

}
