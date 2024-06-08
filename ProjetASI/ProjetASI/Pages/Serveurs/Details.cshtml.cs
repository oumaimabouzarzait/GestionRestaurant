using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Models;

namespace ProjetASI.Pages.Serveurs
{
    public class DetailsModel : PageModel
    {
        private readonly ProjetASI.Data.ApplicationDbContext _context;

        public DetailsModel(ProjetASI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Serveur Serveur { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Serveur == null)
            {
                return NotFound();
            }

            var serveur = await _context.Serveur.FirstOrDefaultAsync(m => m.Id == id);
            if (serveur == null)
            {
                return NotFound();
            }
            else
            {
                Serveur = serveur;
            }
            return Page();
        }
    }
}
