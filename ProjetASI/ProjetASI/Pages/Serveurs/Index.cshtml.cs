using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Models;

namespace ProjetASI.Pages.Serveurs
{
    public class IndexModel : PageModel
    {
        private readonly ProjetASI.Data.ApplicationDbContext _context;

        public IndexModel(ProjetASI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Serveur> Serveur { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Serveur != null)
            {
                Serveur = await _context.Serveur.ToListAsync();
            }
        }
    }
}
