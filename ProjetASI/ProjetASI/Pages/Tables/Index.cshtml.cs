using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Models;

namespace ProjetASI.Pages.Tables
{
    public class IndexModel : PageModel
    {
        private readonly ProjetASI.Data.ApplicationDbContext _context;

        public IndexModel(ProjetASI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Table> Table { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Table != null)
            {
                Table = await _context.Table.ToListAsync();
            }
        }
    }
}
