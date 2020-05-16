using System;
using Common;

namespace Services.Models
{
    public class WolfForModificationModel
    {
        public string FirstName { get; set; }
        
        public string InsertionName { get; set; }
        
        public string LastName { get; set; }
        
        public Enums.Gender Gender { get; set; }
        
        public DateTime? BirthDate { get; set; }
        
        public decimal? Latitude { get; set; }
        
        public decimal? Longitude { get; set; }
    }
}