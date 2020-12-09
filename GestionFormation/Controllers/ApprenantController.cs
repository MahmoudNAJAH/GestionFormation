using GestionFormation.DAO;
using GestionFormation.DTO;
using GestionFormation.Filters;
using GestionFormation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionFormation.Controllers
{
    public class ApprenantController : Controller
    {
        // GET: Apprenant
        [LoginRequiredFilter]
        public ActionResult Index()
        {
            if (TempData["emailAEnvoyerDTO"] != null)
            {
                ViewBag.emailAEnvoyerDTO = (EmailAEnvoyerDTO)TempData["emailAEnvoyerDTO"];
            }

            if (TempData["emailMessage"] != null)
            {
                ViewBag.EmailMessage = TempData["emailMessage"];
            }

            return View();
        }

        // GET: Apprenant/Details/5
        //public ActionResult Details(int id)
        //{
            
        //    return View((UserDTO)Session["userConnected"]);
        //}

        //// GET: Apprenant/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Apprenant/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Apprenant/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Apprenant/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Apprenant/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Apprenant/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
