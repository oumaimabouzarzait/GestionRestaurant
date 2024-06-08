using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Models;

namespace ProjetASI.Pages.Encaissements
{
    public class DeleteModel : PageModel
    {
        private readonly ProjetASI.Data.ApplicationDbContext _context;

        public DeleteModel(ProjetASI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Encaissement Encaissement { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Encaissement == null)
            {
                return NotFound();
            }

            var encaissement = await _context.Encaissement.FirstOrDefaultAsync(m => m.ID == id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Encaissement == null)
            {
                return NotFound();
            }
            var encaissement = await _context.Encaissement.FindAsync(id);

            if (encaissement != null)
            {
                Encaissement = encaissement;
                _context.Encaissement.Remove(Encaissement);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
