using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Models;

namespace ProjetASI.Pages.Encaissements
{
    public class IndexModel : PageModel
    {
        private readonly ProjetASI.Data.ApplicationDbContext _context;

        public IndexModel(ProjetASI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Encaissement> Encaissement { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Encaissement != null)
            {
                Encaissement = await _context.Encaissement
                .Include(e => e.Facture).ToListAsync();
            }
        }
    }
}
