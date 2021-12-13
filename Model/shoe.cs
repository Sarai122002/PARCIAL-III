using System.ComponentModel.DataAnnotations;

namespace ParcialComputo3.Models
{
    public class shoe
    {
        [Key]
        public int idshoe { get; set; }
        public string modelshoe { get; set; }
        public string colorshoe { get; set; }
        public string sizeshoe { get; set; }
        public string typeshoe { get; set; }
        
        // Instanciar la Key de la clase Brand.
        public int idBrand { get; set; }

    }
}