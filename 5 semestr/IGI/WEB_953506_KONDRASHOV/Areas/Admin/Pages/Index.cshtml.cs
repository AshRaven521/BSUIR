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
    public class IndexModel : PageModel
    {
        private readonly WEB_953506_KONDRASHOV.Data.ApplicationDbContext _context;

        public IndexModel(WEB_953506_KONDRASHOV.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Engine> Engine { get;set; }

        public async Task OnGetAsync()
        {
            Engine = await _context.Engines.ToListAsync();
        }
    }
}
