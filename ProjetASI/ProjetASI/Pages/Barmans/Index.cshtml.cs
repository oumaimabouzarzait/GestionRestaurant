using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Models;

namespace ProjetASI.Pages.Barmans
{
    public class IndexModel : PageModel
    {
        private readonly ProjetASI.Data.ApplicationDbContext _context;

        public IndexModel(ProjetASI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Barman> Barman { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Barman != null)
            {
                Barman = await _context.Barman.ToListAsync();
            }
        }
    }
}
