using System;
using System.Collections.Generic;
//using GestionFormation.DTO;
//using GestionFormation.Entities;
//using GestionFormation.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests_GestionFormation.DTO
{
    [TestClass]
    public class Test_EmploiDuTempsDTO
    {
        //ATTENTION 
        //Les fonction étant private static etc ...
        //Quelques changements sont nécessaire pour faire fonctionner les tests
        //Les tests sont commentés pour éviter les erreures de compilation

        /*
        [TestMethod]
        public void Test_FillListDates_Formateur()
        {
            //INSERT INTO Formateurs (Prenom, Nom, Email, MotDePasse) VALUES ('Albus', 'Dumbeldore', 'Albus.Dumbeldore@dawan.com', '1234')
            UserDTO user = new UserDTO
            {
                Id = 1,
                Login = "Albus.Dumbeldore@dawan.com",
                Password = "1234",
                Role = UserRole.FORMATEUR
            };
            EmploiDuTempsDTO edt = new EmploiDuTempsDTO(user);

            List<JourneeDTO> res = new List<JourneeDTO>();

            res = edt.FillListDates();

            Assert.IsNotNull(res);

            //INSERT INTO SessionDeFormations(SessionDeCursus_SessionDeCursusId, Formation_FormationId, Formateur_FormateurId, DateDebut) VALUES(1, 1, 1, '01-11-20')
                // => SessionFormations
            //INSERT INTO Formations(Nom, Dure) VALUES('Formation C# Initiation : Programmer en objet (5 jours)', 5)
                // => Formation correspondante : 5 jours

            Assert.AreEqual(5, res.Count);

            foreach(JourneeDTO journee in res)
            {
                Assert.IsNotNull(journee.Date);
                Assert.IsNotNull(journee.Formation);
                Assert.IsNotNull(journee.Formateur);
            }
            
            Assert.AreEqual( new DateTime(2020, 11, 2), res[0].Date);
            Assert.AreEqual( new DateTime(2020, 11, 3), res[1].Date);
            Assert.AreEqual( new DateTime(2020, 11, 4), res[2].Date);
            Assert.AreEqual( new DateTime(2020, 11, 5), res[3].Date);
            Assert.AreEqual( new DateTime(2020, 11, 6), res[4].Date);
            
        }
        */

        /*
        [TestMethod]
        public void Test_GetSessionDeFormations_Formateur()
        {
            //INSERT INTO Formateurs (Prenom, Nom, Email, MotDePasse) VALUES ('Albus', 'Dumbeldore', 'Albus.Dumbeldore@dawan.com', '1234')
            UserDTO user = new UserDTO
            {
                Id = 1,
                Login = "Albus.Dumbeldore@dawan.com",
                Password = "1234",
                Role = UserRole.FORMATEUR
            };
            EmploiDuTempsDTO edt = new EmploiDuTempsDTO(user);

            List<SessionDeFormation> listSessionFormation = edt.GetSessionDeFormations();

            // => SessionFormation de Albus
            //INSERT INTO SessionDeFormations(SessionDeCursus_SessionDeCursusId, Formation_FormationId, Formateur_FormateurId, DateDebut) VALUES(1, 1, 1, '01-11-20')

            Assert.IsNotNull(listSessionFormation);
            Assert.AreEqual(1, listSessionFormation.Count);

            Assert.IsNotNull(listSessionFormation[0].SessionDeCursus);
            Assert.IsNotNull(listSessionFormation[0].Formation);
            Assert.IsNotNull(listSessionFormation[0].Formateur);

            Assert.AreEqual(1, listSessionFormation[0].SessionDeCursus.SessionDeCursusId);
            Assert.AreEqual(1, listSessionFormation[0].Formation.FormationId);
            Assert.AreEqual(1, listSessionFormation[0].Formateur.FormateurId);
            Assert.AreEqual(new DateTime(2020, 11, 1), listSessionFormation[0].DateDebut);
        }
        */

        /*
        [TestMethod]
        public void Test_GetSessionDeFormations_Attendant()
        {
            //INSERT INTO apprenants (Prenom, Nom, Email, MotDePasse) VALUES ('Hermione', 'Granger', 'Hermione.Granger@dawan.com', '1234')
            UserDTO user = new UserDTO
            {
                Id = 1,
                Login = "Hermione.Granger@dawan.com",
                Password = "1234",
                Role = UserRole.ATTENDANT
            };
            EmploiDuTempsDTO edt = new EmploiDuTempsDTO(user);

            List<SessionDeFormation> listSessionFormation = edt.GetSessionDeFormations();

            //INSERT INTO SessionDeCursusApprenants(SessionDeCursus_SessionDeCursusId, Apprenant_ApprenantId) VALUES(1, 1)
            // => user est attache a la session de cursus 1 

            // => List des SessionFormation attaché a la session de cursus 1
            //INSERT INTO SessionDeFormations(SessionDeCursus_SessionDeCursusId, Formation_FormationId, Formateur_FormateurId, DateDebut) VALUES(1, 1, 1, '01-11-20')
            //INSERT INTO SessionDeFormations(SessionDeCursus_SessionDeCursusId, Formation_FormationId, Formateur_FormateurId, DateDebut) VALUES(1, 2, 2, '08-11-20')
            //INSERT INTO SessionDeFormations(SessionDeCursus_SessionDeCursusId, Formation_FormationId, Formateur_FormateurId, DateDebut) VALUES(1, 3, 3, '15-11-20')
            //INSERT INTO SessionDeFormations(SessionDeCursus_SessionDeCursusId, Formation_FormationId, Formateur_FormateurId, DateDebut) VALUES(1, 1, 4, '20-11-20')
            //INSERT INTO SessionDeFormations(SessionDeCursus_SessionDeCursusId, Formation_FormationId, Formateur_FormateurId, DateDebut) VALUES(1, 1, 5, '30-11-20')
            //INSERT INTO SessionDeFormations(SessionDeCursus_SessionDeCursusId, Formation_FormationId, Formateur_FormateurId, DateDebut) VALUES(1, 2, 6, '02-12-20')
            //INSERT INTO SessionDeFormations(SessionDeCursus_SessionDeCursusId, Formation_FormationId, Formateur_FormateurId, DateDebut) VALUES(1, 2, 7, '07-12-20')
            //INSERT INTO SessionDeFormations(SessionDeCursus_SessionDeCursusId, Formation_FormationId, Formateur_FormateurId, DateDebut) VALUES(1, 1, 8, '12-12-20')
            //INSERT INTO SessionDeFormations(SessionDeCursus_SessionDeCursusId, Formation_FormationId, Formateur_FormateurId, DateDebut) VALUES(1, 3, 9, '17-12-20')
            //INSERT INTO SessionDeFormations(SessionDeCursus_SessionDeCursusId, Formation_FormationId, Formateur_FormateurId, DateDebut) VALUES(1, 1, 10, '25-12-20')
            //INSERT INTO SessionDeFormations(SessionDeCursus_SessionDeCursusId, Formation_FormationId, Formateur_FormateurId, DateDebut) VALUES(1, 7, 11, '27-12-20')
            //INSERT INTO SessionDeFormations(SessionDeCursus_SessionDeCursusId, Formation_FormationId, Formateur_FormateurId, DateDebut) VALUES(1, 4, 12, '04-01-21')
            //INSERT INTO SessionDeFormations(SessionDeCursus_SessionDeCursusId, Formation_FormationId, Formateur_FormateurId, DateDebut) VALUES(1, 1, 13, '08-01-21')

            Assert.IsNotNull(listSessionFormation);
            Assert.AreEqual(13, listSessionFormation.Count);

            //On vérifie les deux premier
            foreach(SessionDeFormation ses in listSessionFormation)
            {
                if(ses.SessionDeFormationId == 1)
                {
                    Assert.IsNotNull(ses.SessionDeCursus);
                    Assert.IsNotNull(ses.Formation);
                    Assert.IsNotNull(ses.Formateur);

                    Assert.AreEqual(1, ses.SessionDeCursus.SessionDeCursusId);
                    Assert.AreEqual(1, ses.Formation.FormationId);
                    Assert.AreEqual(1, ses.Formateur.FormateurId);
                    Assert.AreEqual(new DateTime(2020, 11, 1), ses.DateDebut);
                }
                if (ses.SessionDeFormationId == 2)
                {
                    Assert.IsNotNull(ses.SessionDeCursus);
                    Assert.IsNotNull(ses.Formation);
                    Assert.IsNotNull(ses.Formateur);

                    Assert.AreEqual(1, ses.SessionDeCursus.SessionDeCursusId);
                    Assert.AreEqual(2, ses.Formation.FormationId);
                    Assert.AreEqual(2, ses.Formateur.FormateurId);
                    Assert.AreEqual(new DateTime(2020, 11, 8), ses.DateDebut);
                }
            }
        }
        */

        /*
        [TestMethod]
        public void Test_EstFerie()
        {
           
            //Dates fixes tous les ans
            Assert.IsTrue(EmploiDuTempsDTO.EstFerie(new DateTime(2020, 1, 1)));
            Assert.IsTrue(EmploiDuTempsDTO.EstFerie(new DateTime(2020, 5, 1)));
            Assert.IsTrue(EmploiDuTempsDTO.EstFerie(new DateTime(2020, 5, 8)));
            Assert.IsTrue(EmploiDuTempsDTO.EstFerie(new DateTime(2020, 7, 14)));
            Assert.IsTrue(EmploiDuTempsDTO.EstFerie(new DateTime(2020, 8, 15)));
            Assert.IsTrue(EmploiDuTempsDTO.EstFerie(new DateTime(2020, 11, 1)));
            Assert.IsTrue(EmploiDuTempsDTO.EstFerie(new DateTime(2020, 11, 11)));
            Assert.IsTrue(EmploiDuTempsDTO.EstFerie(new DateTime(2020, 12, 25)));

            //Paque
            Assert.IsTrue(EmploiDuTempsDTO.EstFerie(new DateTime(2020, 4, 13)));
            //Ascencion
            Assert.IsTrue(EmploiDuTempsDTO.EstFerie(new DateTime(2020, 5, 21)));
            //Lundi Pentecote
            Assert.IsTrue(EmploiDuTempsDTO.EstFerie(new DateTime(2020, 6, 1)));

            //Jours non férié
            Assert.IsFalse(EmploiDuTempsDTO.EstFerie(new DateTime(2020, 7, 3)));
            Assert.IsFalse(EmploiDuTempsDTO.EstFerie(new DateTime(2020, 4, 15)));
            Assert.IsFalse(EmploiDuTempsDTO.EstFerie(new DateTime(2020, 8, 19)));
            Assert.IsFalse(EmploiDuTempsDTO.EstFerie(new DateTime(2020, 2, 17)));
            Assert.IsFalse(EmploiDuTempsDTO.EstFerie(new DateTime(2020, 10, 16)));
            Assert.IsFalse(EmploiDuTempsDTO.EstFerie(new DateTime(2020, 9, 7)));
        }
        */

        /*
        [TestMethod]
        public void Test_EstWeekEnd()
        {
            Assert.IsTrue(EmploiDuTempsDTO.EstWeekEnd(new DateTime(2020, 1, 5)));
            Assert.IsTrue(EmploiDuTempsDTO.EstWeekEnd(new DateTime(2020, 1, 4)));
            Assert.IsTrue(EmploiDuTempsDTO.EstWeekEnd(new DateTime(2020, 11, 28)));
            Assert.IsTrue(EmploiDuTempsDTO.EstWeekEnd(new DateTime(2020, 11, 29)));
            Assert.IsTrue(EmploiDuTempsDTO.EstWeekEnd(new DateTime(2020, 3, 14)));
            Assert.IsTrue(EmploiDuTempsDTO.EstWeekEnd(new DateTime(2020, 3, 15)));

            Assert.IsFalse(EmploiDuTempsDTO.EstWeekEnd(new DateTime(2020, 7, 3)));
            Assert.IsFalse(EmploiDuTempsDTO.EstWeekEnd(new DateTime(2020, 4, 15)));
            Assert.IsFalse(EmploiDuTempsDTO.EstWeekEnd(new DateTime(2020, 8, 19)));
            Assert.IsFalse(EmploiDuTempsDTO.EstWeekEnd(new DateTime(2020, 2, 17)));
            Assert.IsFalse(EmploiDuTempsDTO.EstWeekEnd(new DateTime(2020, 10, 16)));
            Assert.IsFalse(EmploiDuTempsDTO.EstWeekEnd(new DateTime(2020, 9, 7)));
        }
        */
    }
}
