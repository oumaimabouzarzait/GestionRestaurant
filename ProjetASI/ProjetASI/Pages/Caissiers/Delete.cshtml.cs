using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Models;

namespace ProjetASI.Pages.Caissiers
{
    public class DeleteModel : PageModel
    {
        private readonly ProjetASI.Data.ApplicationDbContext _context;

        public DeleteModel(ProjetASI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Caissier Caissier { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Caissier == null)
            {
                return NotFound();
            }

            var caissier = await _context.Caissier.FirstOrDefaultAsync(m => m.Id == id);

            if (caissier == null)
            {
                return NotFound();
            }
            else
            {
                Caissier = caissier;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Caissier == null)
            {
                return NotFound();
            }
            var caissier = await _context.Caissier.FindAsync(id);

            if (caissier != null)
            {
                Caissier = caissier;
                _context.Caissier.Remove(Caissier);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
