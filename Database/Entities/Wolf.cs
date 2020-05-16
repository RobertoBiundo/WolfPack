using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Common;

namespace Database.Entities
{
    public class Wolf
    {
        public const int FirstNameMaxLength = 64;
        public const int InsertionNameMaxLength = 64;
        public const int LastNameMaxLength = 64;
        public const int LatitudeScale = 6;
        public const int LatitudePrecision = 9;
        public const int LongitudeScale = 6;
        public const int LongitudePrecision = 9;
        
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; }
        
        [MaxLength(InsertionNameMaxLength)]
        public string InsertionName { get; set; }
        
        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; }
        
        [Required]
        [Column(TypeName = "TINYINT")]
        public Enums.Gender Gender { get; set; }
        
        [Column(TypeName = "DATE")]
        public DateTime? BirthDate { get; set; }
        
        // We use Decimal(9,6) - Length 9 with 6 decimal digits
        // Using double will cause the system to accept LAT and LONG values that are not useful or will not be stored properly
        [Column(TypeName = "DECIMAL(9,6)")]
        public decimal? Latitude { get; set; }
        
        // We use Decimal(9,6) - Length 9 with 6 decimal digits
        // Using double will cause the system to accept LAT and LONG values that are not useful or will not be stored properly
        [Column(TypeName = "DECIMAL(9,6)")]
        public decimal? Longitude { get; set; }
        
        public DateTimeOffset CreatedOnDateTimeOffsetUtc { get; set; }
        
        public DateTimeOffset LastModifiedDateTimeOffsetUtc { get; set; }
        
        // NAVIGATIONAL PROPERTIES
        public ICollection<WolfPack> WolfPack { get; set; }
    }
}