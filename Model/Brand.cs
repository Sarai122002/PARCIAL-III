using System.ComponentModel.DataAnnotations;

namespace ParcialComputo3.Models
{
    public class Brand
    {
        [Key]
        public int idBrand { get; set; }
        public string nameBrand { get; set; }
    }
}