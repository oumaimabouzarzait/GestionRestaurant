using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Models;

namespace ProjetASI.Pages.Barmans
{
    public class EditModel : PageModel
    {
        private readonly ProjetASI.Data.ApplicationDbContext _context;

        public EditModel(ProjetASI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Barman Barman { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Barman == null)
            {
                return NotFound();
            }

            var barman = await _context.Barman.FirstOrDefaultAsync(m => m.Id == id);
            if (barman == null)
            {
                return NotFound();
            }
            Barman = barman;
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

            _context.Attach(Barman).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BarmanExists(Barman.Id))
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

        private bool BarmanExists(int id)
        {
            return (_context.Barman?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
