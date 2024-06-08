using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Models;

namespace ProjetASI.Pages.Caissiers
{
    public class IndexModel : PageModel
    {
        private readonly ProjetASI.Data.ApplicationDbContext _context;

        public IndexModel(ProjetASI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Caissier> Caissier { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Caissier != null)
            {
                Caissier = await _context.Caissier.ToListAsync();
            }
        }
    }
}
