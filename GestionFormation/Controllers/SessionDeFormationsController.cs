using GestionFormation.DTO;
using GestionFormation.Filters;
using GestionFormation.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GestionDeFormation.Controllers
{
    public class SessionDeFormationsController : Controller
    {
        // GET: SessionDeFormation
        public ActionResult Index()
        {
            return View(SessionDeFormationService.GetAllSessionDeFormation());
        }

        // GET: SessionDeFormation/Details/5
        public ActionResult Details(int id)
        {
            return View(SessionDeFormationService.GetSessionDeFormation(id));
        }

        // GET: SessionDeFormation/Create
        public ActionResult Create()
        {
            return View(new SessionDeFormationDTO());
        }

        // POST: SessionDeFormation/Create
        [HttpPost]
        public ActionResult Create(SessionDeFormationDTO f)
        {
            try
            {
                SessionDeFormationService.Create(f);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SessionDeFormation/Edit/5
        public ActionResult Edit(int id)
        {
            return View(SessionDeFormationService.GetSessionDeFormation(id));
        }

        // POST: SessionDeFormation/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, SessionDeFormationDTO sdf)
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

        // GET: SessionDeFormation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SessionDeFormation/Delete/5
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
        // GET: SessionDeFormation/AddUpdateFormateur/5

        public ActionResult AddUpdateFormation (int  idSdf = 0)
        {
            ViewBag.SdfId = idSdf;

            return View(FormationService.GetAllFormation());
        }

        // POST: SessionDeFormation/AddUpdateFormateur/5
        [HttpPost]

        public ActionResult AddUpdateFormation(int idSdf, FormCollection collection)
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
