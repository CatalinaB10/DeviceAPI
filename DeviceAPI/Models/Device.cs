using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeviceAPI.Models
{
    [System.ComponentModel.DataAnnotations.Schema.Table("Devices")]
    public class Device
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; } = new Guid();

        [Required]
        [MaxLength(500)]
        [Column("Description")]
        public string Description { get; set; }
        [MaxLength(500)]
        [Column("Address")]
        public string? Address { get; set; }
        [Column("MaxEnergyConsumption")]
        public double MaxEnergyConsumption { get; set; }

        [Column("UserId")]
        public virtual Guid? UserId { get; set; } 
    }
}
