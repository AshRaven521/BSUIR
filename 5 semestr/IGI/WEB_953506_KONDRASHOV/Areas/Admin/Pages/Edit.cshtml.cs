using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB_953506_KONDRASHOV.Data;
using WEB_953506_KONDRASHOV.Entities;

namespace WEB_953506_KONDRASHOV.Areas.Admin.Pages
{
    public class EditModel : PageModel
    {
        private readonly WEB_953506_KONDRASHOV.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public EditModel(WEB_953506_KONDRASHOV.Data.ApplicationDbContext context, IWebHostEnvironment env)
        {
            _environment = env;
            _context = context;
        }

        [BindProperty]
        public Engine Engine { get; set; }
        [BindProperty]
        public IFormFile Image { get; set; }
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

            ViewData["EngineClassId"] = new SelectList(_context.EngineClasses, "EngineClassId", "ClassName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Image != null)
            {
                var fileName = $"{Engine.EngineId}" +
                Path.GetExtension(Image.FileName);
                Engine.Image = fileName;
                var path = Path.Combine(_environment.WebRootPath, "Images", fileName);
                using (var fStream = new FileStream(path, FileMode.Create))
                {
                    await Image.CopyToAsync(fStream);
                }
            }
            _context.Attach(Engine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EngineExists(Engine.EngineId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EngineExists(int id)
        {
            return _context.Engines.Any(e => e.EngineId == id);
        }
    }
}
