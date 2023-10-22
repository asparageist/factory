using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
  public class Engineer
  {
    public int EngineerID { get; set; }
    [Required(ErrorMessage = "Engineer must have name: please add a name.")]
    public string EngineerName { get; set; }
    public List<EngineerMachine> JoinEntities { get; }

  }
}