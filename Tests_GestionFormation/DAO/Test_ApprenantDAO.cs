using System;
using System.Collections.Generic;
using System.Linq;
using GestionFormation.DAO;
using GestionFormation.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests_GestionFormation.DAO
{
    [TestClass]
    public class Test_ApprenantDAO
    {
        [TestMethod]
        public void Test_Create()
        {
            Apprenant app = new Apprenant
            {
                ApprenantId = 9999999,
                Nom = "Nom",
                Prenom = "Prenom",
                Email = "Email",
                MotDePasse = "MotDePasse"
            };

            using (BDDContext context = new BDDContext())
                //On vérifie que l'apprenant n'existe pas avant de le créer
                Assert.IsNull(context.Apprenants.FirstOrDefault(ap => ap.ApprenantId == app.ApprenantId));

            ApprenantDAO.Create(app);

            using (BDDContext context = new BDDContext())
            {
                Apprenant appContext = context.Apprenants.FirstOrDefault(ap => ap.ApprenantId == app.ApprenantId);

                Assert.IsNotNull(appContext);

                Assert.AreEqual("Nom", appContext.Nom);
                Assert.AreEqual("Prenom", appContext.Prenom);
                Assert.AreEqual("Email", appContext.Email);
                Assert.AreEqual("MotDePasse", appContext.MotDePasse);

                //On le supprime pour ne pas poluer la database
                context.Apprenants.Remove(appContext);
                context.SaveChanges();
            }
        }


        [TestMethod]
        public void Test_Delete()
        {
            Apprenant app = new Apprenant
            {
                ApprenantId = 99999999,
                Nom = "Nom",
                Prenom = "Prenom",
                Email = "Email",
                MotDePasse = "MotDePasse"
            };

            using (BDDContext context = new BDDContext())
                Assert.IsNull(context.Apprenants.FirstOrDefault(ap => ap.ApprenantId == app.ApprenantId));

            ApprenantDAO.Create(app);

            using (BDDContext context = new BDDContext())
                Assert.IsNotNull(context.Apprenants.FirstOrDefault(ap => ap.ApprenantId == app.ApprenantId));

            ApprenantDAO.Delete(app.ApprenantId);

            using (BDDContext context = new BDDContext())
                Assert.IsNull(context.Apprenants.FirstOrDefault(ap => ap.ApprenantId == app.ApprenantId));
        }

        [TestMethod]
        public void Test_Update()
        {
            Apprenant app = new Apprenant
            {
                ApprenantId = 99999999,
                Nom = "Nom",
                Prenom = "Prenom",
                Email = "Email",
                MotDePasse = "MotDePasse"
            };

            ApprenantDAO.Create(app);

            Apprenant app2 = new Apprenant
            {
                ApprenantId = app.ApprenantId,
                Nom = "Nom2",
                Prenom = "Prenom2",
                Email = "Email2",
                MotDePasse = "MotDePasse2"
            };

            ApprenantDAO.Update(app2);

            Apprenant app3 = ApprenantDAO.FindById(app2.ApprenantId);

            Assert.IsNotNull(app3);

            Assert.AreEqual("Nom2", app3.Nom);
            Assert.AreEqual("Prenom2", app3.Prenom);
            Assert.AreEqual("Email2", app3.Email);
            Assert.AreEqual("MotDePasse2", app3.MotDePasse);

            ApprenantDAO.Delete(app.ApprenantId);

        }

        [TestMethod]
        public void Test_FindById()
        {
            //INSERT INTO apprenants (Prenom, Nom, Email, MotDePasse) VALUES ('Hermione', 'Granger', 'Hermione.Granger@dawan.com', '1234')
            Apprenant app = ApprenantDAO.FindById(1);

            Assert.IsNotNull(app);

            Assert.AreEqual(1, app.ApprenantId);
            Assert.AreEqual("Granger", app.Nom);
            Assert.AreEqual("Hermione", app.Prenom);
            Assert.AreEqual("Hermione.Granger@dawan.com", app.Email);
            Assert.AreEqual("1234", app.MotDePasse);

            Assert.IsNotNull(app.SessionDeCursus);
            Assert.AreEqual(1, app.SessionDeCursus[0].SessionDeCursusId);

            //On a pas ajouté de Messages dans la Bdd
            //Mais si ca marche pour Session de Cursus, a priori c'est bon pour Messages

            //Assert.IsNotNull(app.Messages);
            //Assert.AreEqual(1, app.Messages[0].MessageId);
            //Assert.AreEqual(1, app.Messages[0].DateDePublication);
        }

        [TestMethod]
        public void Test_FindAll()
        {
            int nbApprenant;

            using (BDDContext context = new BDDContext())
                nbApprenant = context.Apprenants.Count();

            List<Apprenant> Apprenants = ApprenantDAO.FindAll();

            Assert.IsNotNull(Apprenants);
            Assert.AreEqual(nbApprenant, Apprenants.Count);

            foreach(Apprenant app in Apprenants)
            {
                Assert.IsNotNull(app);

                //On check si tout va bien pour les deux premiers

                //INSERT INTO apprenants (Prenom, Nom, Email, MotDePasse) VALUES ('Hermione', 'Granger', 'Hermione.Granger@dawan.com', '1234')
                if (app.ApprenantId == 1)
                {
                    Assert.AreEqual("Granger", app.Nom);
                    Assert.AreEqual("Hermione", app.Prenom);
                    Assert.AreEqual("Hermione.Granger@dawan.com", app.Email);
                    Assert.AreEqual("1234", app.MotDePasse);

                    Assert.IsNotNull(app.SessionDeCursus);
                    Assert.AreEqual(1, app.SessionDeCursus[0].SessionDeCursusId);
                }

                //INSERT INTO apprenants (Prenom, Nom, Email, MotDePasse) VALUES ('Ron', 'Weasley', 'Ron.Weasley@dawan.com', '1234')
                if (app.ApprenantId == 2)
                {
                    Assert.AreEqual("Weasley", app.Nom);
                    Assert.AreEqual("Ron", app.Prenom);
                    Assert.AreEqual("Ron.Weasley@dawan.com", app.Email);
                    Assert.AreEqual("1234", app.MotDePasse);

                    Assert.IsNotNull(app.SessionDeCursus);
                    Assert.AreEqual(1, app.SessionDeCursus[0].SessionDeCursusId);
                }
            }
        }
    }
}
