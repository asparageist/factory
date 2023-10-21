namespace Factory.Models
{
  public class EngineerMachine
  {
    public int EngineerMachineId { get; set; }
    public int EngineerID { get; set; }
    public int MachineID { get; set; }
    public Engineer Engineer { get; set; }
    public Machine Machine { get; set; }


  }
}