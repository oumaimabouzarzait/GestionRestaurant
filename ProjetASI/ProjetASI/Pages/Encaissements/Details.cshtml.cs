using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Models;

namespace ProjetASI.Pages.Encaissements
{
    public class DetailsModel : PageModel
    {
        private readonly ProjetASI.Data.ApplicationDbContext _context;

        public DetailsModel(ProjetASI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Encaissement Encaissement { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Encaissement == null)
            {
                return NotFound();
            }

            var encaissement = await _context.Encaissement
                .Include(e => e.Facture)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (encaissement == null)
            {
                return NotFound();
            }
            else
            {
                Encaissement = encaissement;
            }
            return Page();
        }
    }
}
