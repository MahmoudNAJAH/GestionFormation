using System;
using System.Collections.Generic;
using System.Linq;
using Gestionformation.DAO;
using GestionFormation.DAO;
using GestionFormation.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests_GestionFormation.DAO
{
    [TestClass]
    public class Test_SessionDeCursusDAO
{
        [TestMethod]
        public void Test_Create()
        {
            SessionDeCursus app = new SessionDeCursus
            {
                SessionDeCursusId = 9999999,
            };

            using (BDDContext context = new BDDContext())
                //On vérifie que l'SessionDeCursus n'existe pas avant de le créer
                Assert.IsNull(context.SessionDeCursus.FirstOrDefault(ap => ap.SessionDeCursusId == app.SessionDeCursusId));

            SessionDeCursusDAO.Create(app);

            using (BDDContext context = new BDDContext())
            {
                SessionDeCursus appContext = context.SessionDeCursus.FirstOrDefault(ap => ap.SessionDeCursusId == app.SessionDeCursusId);

                Assert.IsNotNull(appContext);

                //On le supprime pour ne pas poluer la database
                context.SessionDeCursus.Remove(appContext);
                context.SaveChanges();
            }
        }


        [TestMethod]
        public void Test_Delete()
        {
            SessionDeCursus app = new SessionDeCursus
            {
                SessionDeCursusId = 99999999,
            };

            using (BDDContext context = new BDDContext())
                Assert.IsNull(context.SessionDeCursus.FirstOrDefault(ap => ap.SessionDeCursusId == app.SessionDeCursusId));

            SessionDeCursusDAO.Create(app);

            using (BDDContext context = new BDDContext())
                Assert.IsNotNull(context.SessionDeCursus.FirstOrDefault(ap => ap.SessionDeCursusId == app.SessionDeCursusId));

            SessionDeCursusDAO.Delete(app.SessionDeCursusId);

            using (BDDContext context = new BDDContext())
                Assert.IsNull(context.SessionDeCursus.FirstOrDefault(ap => ap.SessionDeCursusId == app.SessionDeCursusId));
        }

        [TestMethod]
        public void Test_Update()
        {
            SessionDeCursus app = new SessionDeCursus
            {
                SessionDeCursusId = 99999999,
            };

            app.Cursus = CursusDAO.FindById(1);

            SessionDeCursusDAO.Create(app);

            SessionDeCursus app2 = new SessionDeCursus
            {
                SessionDeCursusId = app.SessionDeCursusId,
            };

            app2.Cursus = CursusDAO.FindById(2);

            SessionDeCursusDAO.Update(app2);

            SessionDeCursus app3 = SessionDeCursusDAO.FindById(app.SessionDeCursusId);

            Assert.IsNotNull(app3);

            Assert.AreEqual(2, app3.Cursus.CursusId);

            SessionDeCursusDAO.Delete(app.SessionDeCursusId);

        }

        [TestMethod]
        public void Test_FindById()
        {
            //INSERT INTO SessionDeCursuss (Prenom, Nom, Email, MotDePasse) VALUES ('Hermione', 'Granger', 'Hermione.Granger@dawan.com', '1234')
            SessionDeCursus app = SessionDeCursusDAO.FindById(1);

            Assert.IsNotNull(app);

            Assert.AreEqual(1, app.SessionDeCursusId);

            int nbApprenant = 10;
            int nbSessionDeFormations = 13;

            //INSERT INTO SessionDeFormations (SessionDeCursus_SessionDeCursusId, Formation_FormationId, Formateur_FormateurId, DateDebut) VALUES (1, 1, 1, '01-11-20')
            Assert.IsNotNull(app.SessionsDeFormations);
            Assert.AreEqual(nbSessionDeFormations, app.SessionsDeFormations.Count());
            Assert.AreEqual(1, app.SessionsDeFormations[0].SessionDeFormationId);
            Assert.AreEqual(new DateTime(2020, 11, 01), app.SessionsDeFormations[0].DateDebut);
            

            //INSERT INTO SessionDeCursusApprenants (SessionDeCursus_SessionDeCursusId, Apprenant_ApprenantId) VALUES (1, 1)
            //INSERT INTO apprenants (Prenom, Nom, Email, MotDePasse) VALUES ('Hermione', 'Granger', 'Hermione.Granger@dawan.com', '1234')
            Assert.IsNotNull(app.Apprenants);
            Assert.AreEqual(nbApprenant, app.Apprenants.Count());
            Assert.AreEqual(1, app.Apprenants[0].ApprenantId);
            Assert.AreEqual("Hermione", app.Apprenants[0].Prenom);
            Assert.AreEqual("Granger", app.Apprenants[0].Nom);
            Assert.AreEqual("Hermione.Granger@dawan.com", app.Apprenants[0].Email);
            Assert.AreEqual("1234", app.Apprenants[0].MotDePasse);
            
        }

        [TestMethod]
        public void Test_FindAll()
        {
            int nbSessionDeCursus;

            using (BDDContext context = new BDDContext())
                nbSessionDeCursus = context.SessionDeCursus.Count();

            List<SessionDeCursus> SessionDeCursuss = SessionDeCursusDAO.FindAll();

            Assert.IsNotNull(SessionDeCursuss);
            Assert.AreEqual(nbSessionDeCursus, SessionDeCursuss.Count);

            foreach (SessionDeCursus app in SessionDeCursuss)
            {
                Assert.IsNotNull(app);

                //On check si tout va bien pour les deux premiers
                
                if (app.SessionDeCursusId == 1)
                {
                    //INSERT INTO SessionDeFormations (SessionDeCursus_SessionDeCursusId, Formation_FormationId, Formateur_FormateurId, DateDebut) VALUES (1, 1, 1, '01-11-20')
                    Assert.IsNotNull(app.SessionsDeFormations);
                    Assert.AreEqual(1, app.SessionsDeFormations[0].SessionDeFormationId);
                    Assert.AreEqual(new DateTime(2020, 11, 01), app.SessionsDeFormations[0].DateDebut);

                    //INSERT INTO SessionDeCursusApprenants (SessionDeCursus_SessionDeCursusId, Apprenant_ApprenantId) VALUES (1, 1)
                    //INSERT INTO apprenants (Prenom, Nom, Email, MotDePasse) VALUES ('Hermione', 'Granger', 'Hermione.Granger@dawan.com', '1234')
                    Assert.IsNotNull(app.Apprenants[0]);
                    Assert.AreEqual(1, app.Apprenants[0].ApprenantId);
                    Assert.AreEqual("Granger", app.Apprenants[0].Nom);
                    Assert.AreEqual("Hermione", app.Apprenants[0].Prenom);
                    Assert.AreEqual("Hermione.Granger@dawan.com", app.Apprenants[0].Email);
                    Assert.AreEqual("1234", app.Apprenants[0].MotDePasse);
                }
            }
        }
    }
}
