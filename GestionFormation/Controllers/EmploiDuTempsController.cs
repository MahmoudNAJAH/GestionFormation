﻿using GestionFormation.DTO;
using GestionFormation.Entities;
using GestionFormation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionFormation.Controllers
{
    public class EmploiDuTempsController : Controller
    {
        //L'emploi du temps de mon utilisateur
        private EmploiDuTempsDTO EDT { get; set; }


        public EmploiDuTempsController()
        {
            //On charge toutes les dates + formations + formateur dans mon controller
            EDT = new EmploiDuTempsDTO(new UserDTO { Id = 1, Role = UserRole.ATTENDANT });
        }

        // GET: EmploiDuTemps
        public ActionResult Index()
        {
            DateTime now = DateTime.Now;

            int WeekNumber = now.DayOfWeek;


            return View();
        }
    }
}