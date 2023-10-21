using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
  public class Machine
  {

    public int MachineID { get; set; }

    [Required(ErrorMessage = "Machine must have name: please add a name.")]
    public string MachineName { get; set; }

    public List<EngineerMachine> JoinEntities { get; set; }

  }
}