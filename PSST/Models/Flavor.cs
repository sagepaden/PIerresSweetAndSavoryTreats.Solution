using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PSST.Models
{
  public class Flavor
  {
    public int FlavorId { get; set; }
    [Required(ErrorMessage = "THE FLAVOR MUST BE NAMED!!!")]
    public string FlavorDescription { get; set; }
    public List<FlavorTreat> JoinEntities { get; }
    public ApplicationUser User { get; set; }
  }
}