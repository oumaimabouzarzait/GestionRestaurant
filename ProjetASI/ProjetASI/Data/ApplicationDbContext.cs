using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Models;

namespace ProjetASI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Barman>? Barman { get; set; }
        public DbSet<Caissier>? Caissier { get; set; }
        public DbSet<Commande>? Commande { get; set; }
        public DbSet<CommandeProduit>? CommandeProduit { get; set; }
        public DbSet<Encaissement>? Encaissement { get; set; }
        public DbSet<Facture>? Facture { get; set; }
        public DbSet<Produit>? Produit { get; set; }
        public DbSet<Serveur>? Serveur { get; set; }
        public DbSet<Table>? Table { get; set; }
    }
}