using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Hubs;
using ProjetASI.Models;

namespace ProjetASI.Pages.Factures
{
    public class DeleteModel : PageModel
    {
        private readonly ProjetASI.Data.ApplicationDbContext _context;
        private readonly IHubContext<ProjectHub> _hubContext;


        public DeleteModel(ProjetASI.Data.ApplicationDbContext context, IHubContext<ProjectHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        [BindProperty]
        public Facture Facture { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Facture == null)
            {
                return NotFound();
            }

            var facture = await _context.Facture
                .Include(f => f.Caissier)
                .Include(f => f.Commande)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (facture == null)
            {
                return NotFound();
            }
            else
            {
                Facture = facture;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Facture == null)
            {
                return NotFound();
            }
            var facture = await _context.Facture
                .Include(f => f.Caissier)
                .Include(f => f.Commande)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (facture != null)
            {
                Facture = facture;
                _context.Facture.Remove(Facture);
                await _context.SaveChangesAsync();
                await _hubContext.Clients.All.SendAsync("updateFactureIndex");
            }

            return RedirectToPage("./Index");
        }
    }
}
