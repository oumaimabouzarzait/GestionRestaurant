using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Hubs;
using ProjetASI.Models;

namespace ProjetASI.Pages.Encaissements
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
            var Facture = await _context.Facture.FirstOrDefaultAsync(m => m.Id == id);
            if (Facture == null)
            {
                return RedirectToPage("/Factures/Index");
            }
            var Encaissements = await _context.Encaissement
                .Where(e => e.FactureId == Facture.Id)
                .ToListAsync();
            var MontantRestant = Facture.MontantTotal;
            foreach (var e in Encaissements)
            {
                MontantRestant -= e.Montant;
            }
            ViewData["Facture"] = Facture;
            ViewData["MontantRestant"] = MontantRestant;
            return Page();
        }

        [BindProperty]
        public Encaissement Encaissement { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (_context.Encaissement == null || Encaissement == null)
            {
                return Page();
            }

            Encaissement.DateHeure = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var Facture = await _context.Facture
                .Include(f => f.Commande)
                    .ThenInclude(fc => fc.Table)
                .FirstOrDefaultAsync(m => m.Id == Encaissement.FactureId);

            if (Facture == null)
            {
                return RedirectToPage("/Factures/Index");
            }

            var Encaissements = await _context.Encaissement
                .Where(e => e.FactureId == Facture.Id)
                .ToListAsync();
            var MontantRestant = Facture.MontantTotal;
            foreach (var e in Encaissements)
            {
                MontantRestant -= e.Montant;
            }
            MontantRestant -= Encaissement.Montant;

            if (MontantRestant <= 0)
            {
                Facture.Commande.Encaissee = true;
                Facture.Commande.Table.NbClients = 0;
                Facture.Commande.Table.Etat = EtatTable.VIDE;
            }

            _context.Encaissement.Add(Encaissement);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("updateEncaissementIndex");

            if (MontantRestant <= 0)
            {
                await _hubContext.Clients.All.SendAsync("updateTableIndex");
                await _hubContext.Clients.All.SendAsync("updateCommandeIndex");
                await _hubContext.Clients.All.SendAsync("updateFactureIndex");
            }

            return RedirectToPage("./Index");
        }
    }
}
