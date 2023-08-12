using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PSST.Models
{
    public class Flavor
    {
        public int FlavorId { get; set; }

        [Required(ErrorMessage = "The Flavor must be named!")]
        public string FlavorName { get; set; }
        public List<TreatFlavor> JoinEntities { get; }
        public ApplicationUser User { get; set; }
    }
}