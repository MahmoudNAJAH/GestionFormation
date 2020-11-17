using System;
using System.Collections.Generic;
using System.Linq;
using GestionFormation.DAO;
using GestionFormation.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests_GestionFormation.DAO
{
    [TestClass]
    public class Test_FormationDAO
    {
        
        [TestMethod]
        public void Test_Create()
        {
            Formation app = new Formation
            {
                Nom = "Nom",
                Description = "Description",
                Dure = 5,
            };
            /*
            app.SessionsDeFormations = new List<SessionDeFormation>();
            app.SessionsDeFormations.Add(new SessionDeFormation { DateDebut = new DateTime(9999, 02, 05)});
            */
            /*
            app.Cursus = new List<Cursus>();
            app.Cursus.Add(CursusDAO.FindById(1));
            app.Cursus.Add(CursusDAO.FindById(2));
            */
            using (BDDContext context = new BDDContext())
                //On vérifie que la Formation n'existe pas avant de le créer
                Assert.IsNull(context.Formations.FirstOrDefault(ap => ap.FormationId == app.FormationId));

            FormationDAO.Create(app);

            using (BDDContext context = new BDDContext())
            {
                Formation appContext = context.Formations.FirstOrDefault(ap => ap.FormationId == app.FormationId);

                Assert.IsNotNull(appContext);

                Assert.AreEqual("Nom", appContext.Nom);
                Assert.AreEqual("Description", appContext.Description);
                Assert.AreEqual(5, appContext.Dure);

                //On le supprime pour ne pas poluer la database
                context.Formations.Remove(appContext);
                context.SaveChanges();
            }
        }


        [TestMethod]
        public void Test_Delete()
        {
            Formation app = new Formation
            {
                FormationId = 99999999,
                Nom = "Nom",
                Description = "Description",
                Dure = 5,
            };

            using (BDDContext context = new BDDContext())
                Assert.IsNull(context.Formations.FirstOrDefault(ap => ap.FormationId == app.FormationId));

            FormationDAO.Create(app);

            using (BDDContext context = new BDDContext())
                Assert.IsNotNull(context.Formations.FirstOrDefault(ap => ap.FormationId == app.FormationId));

            FormationDAO.Delete(app.FormationId);

            using (BDDContext context = new BDDContext())
                Assert.IsNull(context.Formations.FirstOrDefault(ap => ap.FormationId == app.FormationId));
        }

        [TestMethod]
        public void Test_Update()
        {
            Formation app = new Formation
            {
                FormationId = 99999999,
                Nom = "Nom",
                Description = "Description",
                Dure = 5,
            };

            app.SessionsDeFormations = new List<SessionDeFormation>();
            app.SessionsDeFormations.Add(SessionDeFormationDAO.FindById(1));
            app.SessionsDeFormations.Add(SessionDeFormationDAO.FindById(2));

            FormationDAO.Create(app);

            Formation app2 = new Formation
            {
                FormationId = app.FormationId,
                Nom = "Nom2",
                Description = "Description2",
                Dure = 6,
            };

            app2.SessionsDeFormations = new List<SessionDeFormation>();
            app2.SessionsDeFormations.Add(SessionDeFormationDAO.FindById(1));
            app2.SessionsDeFormations.Add(SessionDeFormationDAO.FindById(3));

            FormationDAO.Update(app2);

            Formation app3 = FormationDAO.FindById(app.FormationId);

            Assert.IsNotNull(app3);

            Assert.AreEqual("Nom2", app3.Nom);
            Assert.AreEqual("Description2", app3.Description);
            Assert.AreEqual(6, app3.Dure);

            Assert.IsNotNull(app3.SessionsDeFormations);
            Assert.AreEqual(2, app3.SessionsDeFormations.Count());
            Assert.AreEqual(1, app3.SessionsDeFormations[0].SessionDeFormationId);
            Assert.AreEqual(3, app3.SessionsDeFormations[1].SessionDeFormationId);

            FormationDAO.Delete(app.FormationId);

        }

        [TestMethod]
        public void Test_FindById()
        {
            //INSERT INTO Formations (Nom ,Dure) VALUES ('Formation C# Initiation : Programmer en objet (5 jours)', 5)
            Formation app = FormationDAO.FindById(1);

            Assert.IsNotNull(app);

            Assert.AreEqual(1, app.FormationId);
            Assert.AreEqual("Formation C# Initiation : Programmer en objet (5 jours)", app.Nom);
            Assert.AreEqual(5, app.Dure);

            //INSERT INTO dbo.FormationCursus(Cursus_CursusId, Formation_FormationId) VALUES(1, 1)
            Assert.IsNotNull(app.Cursus);
            Assert.AreEqual(1, app.Cursus[0].CursusId);

            //INSERT INTO SessionDeFormations (SessionDeCursus_SessionDeCursusId, Formation_FormationId, Formateur_FormateurId, DateDebut) VALUES (1, 1, 1, '01-11-20')
            Assert.IsNotNull(app.SessionsDeFormations);
            Assert.AreEqual(1, app.SessionsDeFormations[0].SessionDeFormationId);
            Assert.AreEqual(new DateTime(2020, 11, 01), app.SessionsDeFormations[0].DateDebut);
        }

        [TestMethod]
        public void Test_FindAll()
        {
            int nbFormation;

            using (BDDContext context = new BDDContext())
                nbFormation = context.Formations.Count();

            List<Formation> Formations = FormationDAO.FindAll();

            Assert.IsNotNull(Formations);
            Assert.AreEqual(nbFormation, Formations.Count);

            foreach (Formation app in Formations)
            {
                Assert.IsNotNull(app);

                //On check si tout va bien pour le premier

                //INSERT INTO Formations (Nom ,Dure) VALUES ('Formation C# Initiation : Programmer en objet (5 jours)', 5)
                if (app.FormationId == 1)
                {
                    Assert.AreEqual(1, app.FormationId);
                    Assert.AreEqual("Formation C# Initiation : Programmer en objet (5 jours)", app.Nom);
                    Assert.AreEqual(5, app.Dure);

                    //INSERT INTO dbo.FormationCursus(Cursus_CursusId, Formation_FormationId) VALUES(1, 1)
                    Assert.IsNotNull(app.Cursus);
                    Assert.AreEqual(1, app.Cursus[0].CursusId);

                    //INSERT INTO SessionDeFormations (SessionDeCursus_SessionDeCursusId, Formation_FormationId, Formateur_FormateurId, DateDebut) VALUES (1, 1, 1, '01-11-20')
                    Assert.IsNotNull(app.SessionsDeFormations);
                    Assert.AreEqual(1, app.SessionsDeFormations[0].SessionDeFormationId);
                    Assert.AreEqual(new DateTime(2020, 11, 01), app.SessionsDeFormations[0].DateDebut);
                }
            }
        }
        
    }
}
