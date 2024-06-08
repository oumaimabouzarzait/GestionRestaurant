using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ProjetASI.Hubs;
using ProjetASI.Models;

namespace ProjetASI.Pages.Commandes
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
            var Table = await _context.Table.FirstOrDefaultAsync(m => m.Id == id);
            var Serveurs = new SelectList(_context.Serveur, "Id", null);
            var Produits = await _context.Produit.ToListAsync();
            if (Table == null || !Serveurs.Any() || Produits == null)
            {
                return RedirectToPage("/Tables/Index");
            }
            ViewData["Table"] = Table;
            ViewData["Serveurs"] = Serveurs;
            ViewData["Produits"] = Produits;
            return Page();
        }

        [BindProperty]
        public Commande Commande { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (_context.Commande == null || Commande == null)
            {
                return Page();
            }

            Commande.DateHeure = DateTime.Now;

            var Table = await _context.Table.FirstOrDefaultAsync(m => m.Id == Commande.TableId);
            var Serveurs = new SelectList(_context.Serveur, "Id", null);
            var Produits = await _context.Produit.ToListAsync();

            if (Table == null || !Serveurs.Any() || Produits.Count == 0)
            {
                return RedirectToPage("/Tables/Index");
            }

            ViewData["Table"] = Table;
            ViewData["Serveurs"] = Serveurs;
            ViewData["Produits"] = Produits;

            if (Request.Form.TryGetValue("produits", out var produitsId) && Request.Form.TryGetValue("quantites", out var quantites))
            {
                for (int i = 0; i < produitsId.Count; i++)
                {
                    string produitId = produitsId[i];
                    if (!int.TryParse(quantites[i], out int quantite))
                        continue;
                    if (quantite <= 0)
                        continue;
                    var Produit = await _context.Produit.FirstOrDefaultAsync(m => m.Id == int.Parse(produitId));
                    if (Produit == null)
                        continue;
                    CommandeProduit commandeProduit = new()
                    {
                        LeProduitId = Produit.Id,
                        LaCommande = Commande,
                        QuantiteProduit = quantite
                    };
                    _context.CommandeProduit.Add(commandeProduit);
                }
            }

            if (Commande.LesProduitsCommandes == null || !ModelState.IsValid)
            {
                return Page();
            }


            Table.Etat = EtatTable.COMMANDE;

            _context.Attach(Table).State = EntityState.Modified;
            _context.Commande.Add(Commande);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("updateTableIndex");
            await _hubContext.Clients.All.SendAsync("updateCommandeIndex");

            return RedirectToPage("./Index");
        }
    }
}
