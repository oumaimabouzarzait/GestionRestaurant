using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Models;

namespace ProjetASI.Pages.Produits
{
    public class IndexModel : PageModel
    {
        private readonly ProjetASI.Data.ApplicationDbContext _context;

        public IndexModel(ProjetASI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Produit> Produit { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Produit != null)
            {
                Produit = await _context.Produit.ToListAsync();
            }
        }
    }
}
