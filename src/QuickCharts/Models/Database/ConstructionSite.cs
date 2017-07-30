using System;
using System.Collections.Generic;

namespace QuickCharts.Models.Database
{
    public class ConstructionSite
    {
        public Guid ConstructionSiteId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Building> Buildings { get; set; }
    }
}