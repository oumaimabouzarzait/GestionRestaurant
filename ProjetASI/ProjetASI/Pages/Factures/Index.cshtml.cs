using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Models;

namespace ProjetASI.Pages.Factures
{
    public class IndexModel : PageModel
    {
        private readonly ProjetASI.Data.ApplicationDbContext _context;

        public IndexModel(ProjetASI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Facture> Facture { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Facture != null)
            {
                Facture = await _context.Facture
                .Include(f => f.Caissier)
                .Include(f => f.Commande).ToListAsync();
            }
        }
    }
}
