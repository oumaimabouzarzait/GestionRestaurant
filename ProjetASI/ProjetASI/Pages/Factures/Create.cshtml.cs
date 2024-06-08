using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Hubs;
using ProjetASI.Models;

namespace ProjetASI.Pages.Factures
{
    public class CreateModel : PageModel
    {
        private readonly ProjetASI.Data.ApplicationDbContext _context;
        private readonly IHubContext<ProjectHub> _hubContext;

        public CreateModel(ProjetASI.Data.ApplicationDbContext context, IHubContext<ProjectHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var Commande = await _context.Commande.FirstOrDefaultAsync(m => m.Id == id);
            var Caissiers = new SelectList(_context.Caissier, "Id", null);
            if (Commande == null || !Caissiers.Any())
            {
                return RedirectToPage("/Commandes/Index");
            }
            ViewData["Commande"] = Commande;
            ViewData["Caissiers"] = Caissiers;
            return Page();
        }

        [BindProperty]
        public Facture Facture { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (_context.Facture == null || Facture == null)
            {
                return Page();
            }

            Facture.DateHeure = DateTime.Now;

            var Commande = await _context.Commande
                .Include(c => c.LesProduitsCommandes)
                    .ThenInclude(cp => cp.LeProduit)
                .FirstOrDefaultAsync(m => m.Id == Facture.CommandeId);

            if (Commande == null)
            {
                return RedirectToPage("/Tables/Index");
            }

            ViewData["Commande"] = Commande;

            foreach (var produit in Commande.LesProduitsCommandes)
            {
                Facture.MontantTotal += produit.LeProduit.Prix * produit.QuantiteProduit;
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Facture.Add(Facture);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("updateCommandeIndex");
            await _hubContext.Clients.All.SendAsync("updateFactureIndex");

            return RedirectToPage("./Index");
        }
    }
}
