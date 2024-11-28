using System.ComponentModel.DataAnnotations;

namespace GestionBoutiqueBack.model
{
    public class Vacation
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public bool IsApproved { get; set; }
        public int? EmployeeId { get; set; }

    }

}
