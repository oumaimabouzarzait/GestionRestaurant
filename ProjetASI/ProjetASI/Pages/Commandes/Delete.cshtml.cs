using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Models;

namespace ProjetASI.Pages.Commandes
{
    public class DeleteModel : PageModel
    {
        private readonly ProjetASI.Data.ApplicationDbContext _context;

        public DeleteModel(ProjetASI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Commande Commande { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Commande == null)
            {
                return NotFound();
            }

            var commande = await _context.Commande.FirstOrDefaultAsync(m => m.Id == id);

            if (commande == null)
            {
                return NotFound();
            }
            else
            {
                Commande = commande;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Commande == null)
            {
                return NotFound();
            }
            var commande = await _context.Commande
                .Include(c => c.LesProduitsCommandes)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (commande != null)
            {
                Commande = commande;
                _context.Commande.Remove(Commande);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
