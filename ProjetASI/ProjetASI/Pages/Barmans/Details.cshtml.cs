using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Models;

namespace ProjetASI.Pages.Barmans
{
    public class DetailsModel : PageModel
    {
        private readonly ProjetASI.Data.ApplicationDbContext _context;

        public DetailsModel(ProjetASI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
