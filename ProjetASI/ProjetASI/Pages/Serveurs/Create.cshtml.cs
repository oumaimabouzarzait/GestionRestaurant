using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetASI.Models;

namespace ProjetASI.Pages.Serveurs
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
        public Serveur Serveur { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Serveur == null || Serveur == null)
            {
                return Page();
            }

            _context.Serveur.Add(Serveur);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
