using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WEB_953506_KONDRASHOV.Data;
using WEB_953506_KONDRASHOV.Entities;

namespace WEB_953506_KONDRASHOV.Areas.Admin.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly WEB_953506_KONDRASHOV.Data.ApplicationDbContext _context;

        public DetailsModel(WEB_953506_KONDRASHOV.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Engine Engine { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Engine = await _context.Engines.FirstOrDefaultAsync(m => m.EngineId == id);

            if (Engine == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
