using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LodgingReservation_BE.Models
{
    [Table("EXTRA_SERVICE")]
    public class ExtraService
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("EXTRA_SERVICE_NAME")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column("PRICE",TypeName = "decimal(12,2)")]
        public decimal Price { get; set; }

        [Required]
        [Column("UNIT_TYPE")]
        public string UnitType { get; set; } = string.Empty;
    }
}
