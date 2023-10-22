using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Factory.Controllers
{
  public class MachineController : Controller
  {
    private readonly FactoryContext _db;

    public MachineController(FactoryContext db)
    {
      _db = db;
    }
    public ActionResult Index()
    {
      ViewBag.PageTitle = "All Machines";
      return View(_db.Machines.ToList());
    }

    public ActionResult New()
    {
      ViewBag.PageTitle = "Add a Machine";
      return View();
    }

    [HttpPost]
    public ActionResult New(Machine machine)
    {
      _db.Machines.Add(machine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Machine thisMachine = _db.Machines
          .Include(machine => machine.JoinEntities)
          .ThenInclude(join => join.Engineer)
          .FirstOrDefault(machine => machine.MachineID == id);
      return View(thisMachine);
    }

    public ActionResult AddEngineer(int id)
    {
      Machine thisMachine = _db.Machines.FirstOrDefault(machines => machines.MachineID == id);
      ViewBag.EngineerID = new SelectList(_db.Engineers, "EngineerID", "EngineerName");
      return View(thisMachine);
    }

    [HttpPost]
    public ActionResult AddEngineer(Machine machine, int engineerID)
    {
#nullable enable
      EngineerMachine? joinEntity = _db.EngineerMachines.FirstOrDefault(join => (join.EngineerID == engineerID && join.MachineID == machine.MachineID));
#nullable disable
      if (joinEntity == null && engineerID != 0)
      {
        _db.EngineerMachines.Add(new EngineerMachine() { EngineerID = engineerID, MachineID = machine.MachineID });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = machine.MachineID });
    }


  }
}