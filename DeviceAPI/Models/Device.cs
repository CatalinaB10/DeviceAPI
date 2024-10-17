using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeviceAPI.Models
{
    [System.ComponentModel.DataAnnotations.Schema.Table("Devices")]
    public class Device
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public long Id { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        [MaxLength(500)]
        public string Address { get; set; }
      
        public double MaxEnergyConsumption { get; set; }
        
        public virtual long UserId { get; set; }
    }
}
