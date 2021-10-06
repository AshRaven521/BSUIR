using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_953506_KONDRASHOV.Entities
{
    public class Engine
    {

        public int EngineId { get; set; }
        public string EngineName { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int ClassId { get; set; }
        public string Image { get; set; }
    }
}
