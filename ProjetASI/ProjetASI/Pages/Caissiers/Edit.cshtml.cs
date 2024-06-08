using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Models;

namespace ProjetASI.Pages.Caissiers
{
    public class EditModel : PageModel
    {
        private readonly ProjetASI.Data.ApplicationDbContext _context;

        public EditModel(ProjetASI.Data.ApplicationDbContext context)
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
            Caissier = caissier;
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

            _context.Attach(Caissier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaissierExists(Caissier.Id))
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

        private bool CaissierExists(int id)
        {
            return (_context.Caissier?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
