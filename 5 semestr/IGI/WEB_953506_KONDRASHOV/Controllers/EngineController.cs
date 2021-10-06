using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_953506_KONDRASHOV.Entities;
using WEB_953506_KONDRASHOV.Extensions;
using WEB_953506_KONDRASHOV.Models;
using WEB_953506_KONDRASHOV.Data;
using Microsoft.Extensions.Logging;

namespace WEB_953506_KONDRASHOV.Controllers
{
    public class EngineController : Controller
    {
        private ApplicationDbContext context;
        private int pageSize;
        private ILogger logger;

        [Route("Catalog")]
        [Route("Catalog/Page_{pageNo}")]
        public IActionResult Index(int? group, int pageNo = 1)
        {
            logger.LogInformation($"info: group={group}, page={pageNo}");


            var enginesFiltered = context.Engines.Where(d => !group.HasValue || d.ClassId == group.Value);

            var model = ListViewModel<Engine>.GetModel(enginesFiltered, pageNo, pageSize);

            ViewData["Groups"] = context.EngineClasses;
            ViewData["CurrentGroup"] = group ?? 0;

            if (Request.IsAjaxRequest())
            {
                return PartialView("listpartial", model);
            }
            else
            {
                return View(model);
            }

        }

        public EngineController(ApplicationDbContext context, ILogger<EngineController> logger)
        {
            pageSize = 3;
            this.context = context;
            this.logger = logger;
        }

        
    }
}
