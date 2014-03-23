using BLL;
using MVCSports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCSports.Controllers
{
    public class EquipmentController : Controller
    {
        static List<Equipment> equipmentList = new List<Equipment>();
        static List<Equipment> finalEquipmentList = new List<Equipment>();
        //
        // GET: /Equipment/
        public ActionResult Index()
        {
            List<Equipment> equipmentIndexList = GetAllEquipment();
            return View(equipmentIndexList);
        }
        public List<Equipment> GetAllEquipment()
        {
            List<Equipment> equipmentList = new List<Equipment>();
            List<EquipmentVM> newEquipmentList = new List<EquipmentVM>();
            Logic logic = new Logic();
            Equipment equipment = new Equipment();
            newEquipmentList = logic.GetAllEquipment();
            foreach (EquipmentVM equipmentVm in newEquipmentList)
            {
                equipment = ConvertEquipmentVmToEquipment(equipmentVm);
                bool x = equipmentList.Contains(equipment);
                if (x == false)
                {
                    equipmentList.Add(equipment);
                }
            }
            return equipmentList;
        }
        public Equipment ConvertEquipmentVmToEquipment(EquipmentVM equipmentVm)
        {
            Equipment equipment = new Equipment();
            if (equipmentVm != null)
            {
                equipment.id = equipmentVm.id;
                equipment.category = equipmentVm.category;
                equipment.type = equipmentVm.type;
            }
            return equipment;
        }

        //
        // GET: /Equipment/Details/5
        public ActionResult Details(Equipment equipment)
        {
            return View(equipment);
        }

        //
        // GET: /Equipment/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Equipment/Create
        [HttpPost]
        public ActionResult Create(Equipment equipment)
        {
            Logic logic = new Logic();
            if (!ModelState.IsValid)
            {
                return View("Create", equipment);
            }
            logic.CreateEquipmentVM(equipment.category, equipment.type);
            equipmentList.Add(equipment);
            return RedirectToAction("Index");
        }

        //
        // GET: /Equipment/Edit/5
        public ActionResult Edit(Equipment equipment)
        {
            return View(equipment);
        }

        //
        // POST: /Equipment/Edit/5
        [HttpPost]
        public ActionResult Edit(Equipment equipment, FormCollection collection)
        {
            List<Equipment> equipmentList = GetAllEquipment();
            Logic logic = new Logic();
            if (!ModelState.IsValid)
            {
                return View("Edit", equipment);
            }
            foreach (Equipment equip in equipmentList)
            {
                if (equip.id == equipment.id)
                {
                    equip.id = equipment.id;
                    equip.category = equipment.category ?? "None";
                    equip.type = equipment.type ?? "None";
                    logic.UpdateEquipment(equip.id, equip.category, equip.type);
                }
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /Equipment/Delete/5
        public ActionResult Delete(Equipment equipment)
        {
            return View(equipment);
        }

        //
        // POST: /Equipment/Delete/5
        [HttpPost]
        public ActionResult Delete(Equipment equipment, FormCollection collection)
        {
            List<EquipmentVM> newPiece = new List<EquipmentVM>();
            Logic logic = new Logic();
            newPiece = logic.GetAllEquipment();
            foreach (EquipmentVM equip in newPiece)
            {
                if (equip.id == equipment.id)
                {
                    logic.DeleteEquipment(equip.id);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
