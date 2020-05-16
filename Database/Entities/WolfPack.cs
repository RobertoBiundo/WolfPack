using System;

namespace Database.Entities
{
    public class WolfPack
    {
        public Guid WolfId { get; set; }
        
        public Guid PackId { get; set; }
        
        // NAVIGATIONAL PROPERTIES
        public Wolf Wolf { get; set; }
        public Pack Pack { get; set; }
    }
}