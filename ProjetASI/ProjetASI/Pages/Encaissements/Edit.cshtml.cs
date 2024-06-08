using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Models;

namespace ProjetASI.Pages.Encaissements
{
    public class EditModel : PageModel
    {
        private readonly ProjetASI.Data.ApplicationDbContext _context;

        public EditModel(ProjetASI.Data.ApplicationDbContext context)
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
            Encaissement = encaissement;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Encaissement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EncaissementExists(Encaissement.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EncaissementExists(int id)
        {
            return (_context.Encaissement?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
