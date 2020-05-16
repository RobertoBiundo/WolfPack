using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Database.Entities
{
    public class Pack
    {
        public const int NameMaxLength = 64;
        public const int DescriptionMaxLength = 3000;
        
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }
        
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }
        
        public DateTimeOffset CreatedOnDateTimeOffsetUtc { get; set; }
        
        public DateTimeOffset LastModifiedDateTimeOffsetUtc { get; set; }
        
        // NAVIGATIONAL PROPERTIES
        public ICollection<WolfPack> WolfPack { get; set; }
    }
}