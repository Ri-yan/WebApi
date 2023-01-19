using System.ComponentModel.DataAnnotations;

namespace WebApplicationOne.Model
{
    public class VillaModel
    {
        public int intId { get; set; }
        [Required]
        [MaxLength(50)]
        public string strName { get; set; }
    }
}
