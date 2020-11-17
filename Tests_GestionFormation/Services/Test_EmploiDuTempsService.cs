using System;
using System.Collections.Generic;
using GestionFormation.DTO;
using GestionFormation.Entities;
using GestionFormation.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests_GestionFormation.Services
{
    [TestClass]
    public class Test_EmploiDuTempsService
    {
        [TestMethod]
        public void Test_GetWeek()
        {
            List<JourneeDTO> days = new List<JourneeDTO>();
            days.Add(new JourneeDTO { Date = new DateTime(2020, 11, 13), Formateur = new Formateur{Nom = "Formateur 1"}, Formation = new Formation { Nom = "Formation 6"} });
            days.Add(new JourneeDTO { Date = new DateTime(2020, 11, 14), Formateur = new Formateur{Nom = "Formateur 1"}, Formation = new Formation { Nom = "Formation 6"} });
            days.Add(new JourneeDTO { Date = new DateTime(2020, 11, 15), Formateur = new Formateur{Nom = "Formateur 1"}, Formation = new Formation { Nom = "Formation 1"} });
            days.Add(new JourneeDTO { Date = new DateTime(2020, 11, 16), Formateur = new Formateur{Nom = "Formateur 2"}, Formation = new Formation { Nom = "Formation 1"} });
            days.Add(new JourneeDTO { Date = new DateTime(2020, 11, 17), Formateur = new Formateur{Nom = "Formateur 2"}, Formation = new Formation { Nom = "Formation 1"} });
            days.Add(new JourneeDTO { Date = new DateTime(2020, 11, 18), Formateur = new Formateur{Nom = "Formateur 2"}, Formation = new Formation { Nom = "Formation 5"} });
            days.Add(new JourneeDTO { Date = new DateTime(2020, 11, 19), Formateur = new Formateur{Nom = "Formateur 3"}, Formation = new Formation { Nom = "Formation 5"} });
            days.Add(new JourneeDTO { Date = new DateTime(2020, 11, 20), Formateur = new Formateur{Nom = "Formateur 3"}, Formation = new Formation { Nom = "Formation 5"} });
            days.Add(new JourneeDTO { Date = new DateTime(2020, 11, 21), Formateur = new Formateur{Nom = "Formateur 3"}, Formation = new Formation { Nom = "Formation 1"} });
            days.Add(new JourneeDTO { Date = new DateTime(2020, 11, 22), Formateur = new Formateur{Nom = "Formateur 3"}, Formation = new Formation { Nom = "Formation 4"} });
            days.Add(new JourneeDTO { Date = new DateTime(2020, 11, 23), Formateur = new Formateur{Nom = "Formateur 3"}, Formation = new Formation { Nom = "Formation 4"} });

            List<JourneeDTO> result = EmploiDuTempsService.GetWeek(days, new DateTime(2020, 11, 17));

            Assert.IsNotNull(result);

            Assert.AreEqual(7, result.Count);
            foreach(JourneeDTO element in result)
            {
                Assert.IsNotNull(element);
                /*
                switch(element.Date)
                {
                    case DateTime(2020, 11, 16):
                        break;
                }*/
            }
            
        }
    }
}
