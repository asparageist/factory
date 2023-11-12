using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Factory.Controllers
{
  public class EngineerController : Controller
  {
    private readonly FactoryContext _db;

    public EngineerController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      ViewBag.PageTitle = "All Engineers";
      return View(_db.Engineers.ToList());
    }

    public ActionResult New()
    {
      ViewBag.PageTitle = "Add an Engineer";
      return View();
    }

    [HttpPost]
    public ActionResult New(Engineer engineer)
    {
      if (!ModelState.IsValid)
      {
        return View(engineer);
      }
      else
      {
        _db.Engineers.Add(engineer);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }
    public ActionResult Details(int id)
    {
      Engineer thisEngineer = _db.Engineers
          .Include(engineer => engineer.JoinEntities)
          .ThenInclude(join => join.Machine)
          .FirstOrDefault(engineer => engineer.EngineerID == id);
      return View(thisEngineer);
    }

    public ActionResult AddMachine(int id)
    {
      Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineers => engineers.EngineerID == id);
      ViewBag.MachineID = new SelectList(_db.Machines, "MachineID", "MachineName");
      return View(thisEngineer);
    }

    [HttpPost]
    public ActionResult AddMachine(Engineer engineer, int machineID)
    {
#nullable enable
      EngineerMachine? joinEntity = _db.EngineerMachines.FirstOrDefault(join => (join.MachineID == machineID && join.EngineerID == engineer.EngineerID));
#nullable disable
      if (joinEntity == null && machineID != 0)
      {
        _db.EngineerMachines.Add(new EngineerMachine() { MachineID = machineID, EngineerID = engineer.EngineerID });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = engineer.EngineerID });
    }


    public ActionResult Edit(int id)
    {
      Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineers => engineers.EngineerID == id);
      return View(thisEngineer);
    }

    [HttpPost]
    public ActionResult Edit(Engineer engineer)
    {
      _db.Engineers.Update(engineer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineers => engineers.EngineerID == id);
      return View(thisEngineer);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineers => engineers.EngineerID == id);
      _db.Engineers.Remove(thisEngineer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpGet]
    public ActionResult RemoveMachine(int id)
    {
      Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineers => engineers.EngineerID == id);
      var onlyMachineIDs = _db.EngineerMachines
                              .Where(em => em.EngineerID == id)
                              .Select(em => em.MachineID)
                              .ToList();

      var onlyMachines = _db.Machines
                              .Where(m => onlyMachineIDs.Contains(m.MachineID))
                              .ToList();

      ViewBag.MachineID = new SelectList(onlyMachines, "MachineID", "MachineName");
      return View(thisEngineer);
    }


    [HttpPost]
    public ActionResult RemoveMachine(Engineer engineer, int machineID)
    {
      EngineerMachine joinEntity = _db.EngineerMachines
          .FirstOrDefault(join => join.MachineID == machineID && join.EngineerID == engineer.EngineerID);

      if (joinEntity != null)
      {
        _db.EngineerMachines.Remove(joinEntity);
        _db.SaveChanges();
      }

      return RedirectToAction("Details", new { id = engineer.EngineerID });
    }



  }
}