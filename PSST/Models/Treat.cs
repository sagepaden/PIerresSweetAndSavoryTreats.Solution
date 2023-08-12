using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PSST.Models
{
    public class Treat
    {
        public int TreatId { get; set; }

        [Required(ErrorMessage = "The Treat must have a name!")]
        public string TreatName { get; set; }

        [Required(ErrorMessage = "The Treat must have a description!")]
        public string TreatDescription { get; set; }
        public List<TreatFlavor> JoinEntities { get; }
        public ApplicationUser User { get; set; }
    }
}