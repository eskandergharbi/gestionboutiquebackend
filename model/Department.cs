using System.ComponentModel.DataAnnotations;

namespace GestionBoutiqueBack.model
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

    }

}
