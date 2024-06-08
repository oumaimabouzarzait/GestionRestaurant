using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Models;

namespace ProjetASI.Pages.Barmans
{
    public class DeleteModel : PageModel
    {
        private readonly ProjetASI.Data.ApplicationDbContext _context;

        public DeleteModel(ProjetASI.Data.ApplicationDbContext context)
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
            else
            {
                Barman = barman;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Barman == null)
            {
                return NotFound();
            }
            var barman = await _context.Barman.FindAsync(id);

            if (barman != null)
            {
                Barman = barman;
                _context.Barman.Remove(Barman);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
