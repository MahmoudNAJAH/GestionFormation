using GestionFormation.DTO;
using GestionFormation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace GestionFormation.Controllers
{
    public class FormationController : Controller
    {
        // GET: Formation
        public ActionResult Index()
        {
            return View(FormationService.GetAllFormation());
        }

        // GET: Formation/Details/5
        public ActionResult Details(int id)
        {
            return View(FormationService.GetFormation(id));
        }

        // GET: Formation/Create
        public ActionResult Create()
        {
            return View(new FormationDTO());
        }

        // POST: Formation/Create
        [HttpPost]
        public ActionResult Create(FormationDTO f)
        {
            try
            {

                FormationService.Create(f);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Formation/Edit/5
        public ActionResult Edit(int id)
        {
            return View(FormationService.GetFormation(id));
        }

        // POST: Formation/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormationDTO f)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Formation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Formation/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
