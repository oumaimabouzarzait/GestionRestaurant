using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Models;

namespace ProjetASI.Pages.Factures
{
    public class DetailsModel : PageModel
    {
        private readonly ProjetASI.Data.ApplicationDbContext _context;

        public DetailsModel(ProjetASI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Facture Facture { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Facture == null)
            {
                return NotFound();
            }

            var facture = await _context.Facture
                .Include(f => f.Caissier)
                .Include(f => f.Commande)
                    .ThenInclude(fc => fc.LesProduitsCommandes)
                    .ThenInclude(fc => fc.LeProduit)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (facture == null)
            {
                return NotFound();
            }
            else
            {
                Facture = facture;
            }



            return Page();
        }
    }
}
