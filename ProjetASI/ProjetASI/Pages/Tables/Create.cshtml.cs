using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using ProjetASI.Hubs;
using ProjetASI.Models;

namespace ProjetASI.Pages.Tables
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

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Table Table { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Table == null || Table == null)
            {
                return Page();
            }

            _context.Table.Add(Table);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("updateTableIndex");

            return RedirectToPage("./Index");
        }
    }
}
