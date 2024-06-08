using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Models;

namespace ProjetASI.Pages.Commandes
{
    public class IndexModel : PageModel
    {
        private readonly ProjetASI.Data.ApplicationDbContext _context;

        public IndexModel(ProjetASI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Commande> Commande { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Commande != null)
            {
                Commande = await _context.Commande
                .Include(c => c.Barman)
                .Include(c => c.Serveur)
                .Include(c => c.Table)
                .Include(c => c.Facture)
                .ToListAsync();
            }
        }
    }
}
