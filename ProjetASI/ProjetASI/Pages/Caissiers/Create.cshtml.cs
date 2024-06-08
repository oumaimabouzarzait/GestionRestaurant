using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetASI.Models;

namespace ProjetASI.Pages.Caissiers
{
    public class CreateModel : PageModel
    {
        private readonly ProjetASI.Data.ApplicationDbContext _context;

        public CreateModel(ProjetASI.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Caissier Caissier { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Caissier == null || Caissier == null)
            {
                return Page();
            }

            _context.Caissier.Add(Caissier);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
