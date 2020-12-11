using System;
using System.Collections.Generic;
using System.Linq;
//using GestionFormation.DAO;
//using GestionFormation.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace Tests_GestionFormation.DAO
//{
    //[TestClass]
    //public class Test_SessionDeFormationDAO
    //{
    //    [TestMethod]
        //public void Test_Create()
        //{
        //    SessionDeFormation app = new SessionDeFormation
        //    {
        //        SessionDeFormationId = 9999999,
        //        DateDebut = new DateTime(2020, 11, 14)
        //    };

        //    using (BDDContext context = new BDDContext())
        //        //On vérifie que l'SessionDeFormation n'existe pas avant de le créer
        //        Assert.IsNull(context.SessionDeFormations.FirstOrDefault(ap => ap.SessionDeFormationId == app.SessionDeFormationId));

        //    SessionDeFormationDAO.Create(app);

        //    using (BDDContext context = new BDDContext())
        //    {
        //        SessionDeFormation appContext = context.SessionDeFormations.FirstOrDefault(ap => ap.SessionDeFormationId == app.SessionDeFormationId);

        //        Assert.IsNotNull(appContext);

        //        Assert.AreEqual(new DateTime(2020, 11, 14), appContext.DateDebut);

        //        //On le supprime pour ne pas poluer la database
        //        context.SessionDeFormations.Remove(appContext);
        //        context.SaveChanges();
        //    }
        //}


        //[TestMethod]
        //public void Test_Delete()
        //{
        //    SessionDeFormation app = new SessionDeFormation
        //    {
        //        SessionDeFormationId = 99999999,
        //        DateDebut = new DateTime(2020, 11, 14)
        //    };

        //    using (BDDContext context = new BDDContext())
        //        Assert.IsNull(context.SessionDeFormations.FirstOrDefault(ap => ap.SessionDeFormationId == app.SessionDeFormationId));

        //    SessionDeFormationDAO.Create(app);

        //    using (BDDContext context = new BDDContext())
        //        Assert.IsNotNull(context.SessionDeFormations.FirstOrDefault(ap => ap.SessionDeFormationId == app.SessionDeFormationId));

        //    SessionDeFormationDAO.Delete(app.SessionDeFormationId);

        //    using (BDDContext context = new BDDContext())
        //        Assert.IsNull(context.SessionDeFormations.FirstOrDefault(ap => ap.SessionDeFormationId == app.SessionDeFormationId));
        //}

        //[TestMethod]
        //public void Test_Update()
        //{
        //    SessionDeFormation app = new SessionDeFormation
        //    {
        //        SessionDeFormationId = 99999999,
        //        DateDebut = new DateTime(2020, 11, 14)
        //    };

        //    SessionDeFormationDAO.Create(app);

        //    SessionDeFormation app2 = new SessionDeFormation
        //    {
        //        SessionDeFormationId = app.SessionDeFormationId,
        //        DateDebut = new DateTime(2020, 12, 14)
        //    };

        //    SessionDeFormationDAO.Update(app2);

        //    SessionDeFormation app3 = SessionDeFormationDAO.FindById(app2.SessionDeFormationId);

        //    Assert.IsNotNull(app3);

        //    Assert.AreEqual(new DateTime(2020, 12, 14), app3.DateDebut);

        //    SessionDeFormationDAO.Delete(app.SessionDeFormationId);

        //}

        //[TestMethod]
        //public void Test_FindById()
        //{
        //    //INSERT INTO SessionDeFormations (SessionDeCursus_SessionDeCursusId, Formation_FormationId, Formateur_FormateurId, DateDebut) VALUES (1, 1, 1, '01-11-20')

        //    SessionDeFormation app = SessionDeFormationDAO.FindById(1);

        //    Assert.IsNotNull(app);

        //    Assert.AreEqual(1, app.SessionDeFormationId);
        //    Assert.AreEqual(new DateTime(2020, 11, 01), app.DateDebut);

        //    //INSERT INTO Formations(Nom, Dure) VALUES('Formation C# Initiation : Programmer en objet (5 jours)', 5)
        //    Assert.IsNotNull(app.Formation);
        //    Assert.AreEqual(1, app.Formation.FormationId);
        //    Assert.AreEqual("Formation C# Initiation : Programmer en objet (5 jours)", app.Formation.Nom);
        //    Assert.AreEqual(5, app.Formation.Dure);

        //    //INSERT INTO Formateurs(Prenom, Nom, Email, MotDePasse) VALUES('Albus', 'Dumbeldore', 'Albus.Dumbeldore@dawan.com', '1234')
        //    Assert.IsNotNull(app.Formateur);
        //    Assert.AreEqual(1, app.Formateur.FormateurId);
        //    Assert.AreEqual("Albus", app.Formateur.Prenom);
        //    Assert.AreEqual("Dumbeldore", app.Formateur.Nom);
        //    Assert.AreEqual("Albus.Dumbeldore@dawan.com", app.Formateur.Email);
        //    Assert.AreEqual("1234", app.Formateur.MotDePasse);

        //    //INSERT INTO SessionDeCursus(Cursus_CursusId) VALUES(1)
        //    Assert.IsNotNull(app.SessionDeCursus);
        //    Assert.AreEqual(1, app.SessionDeCursus.SessionDeCursusId);

        //}

        //[TestMethod]
        //public void Test_FindAll()
        //{
        //    int nbSessionDeFormation;

        //    using (BDDContext context = new BDDContext())
        //        nbSessionDeFormation = context.SessionDeFormations.Count();

        //    List<SessionDeFormation> SessionDeFormations = SessionDeFormationDAO.FindAll();

        //    Assert.IsNotNull(SessionDeFormations);
        //    Assert.AreEqual(nbSessionDeFormation, SessionDeFormations.Count);

        //    foreach (SessionDeFormation app in SessionDeFormations)
        //    {
        //        Assert.IsNotNull(app);

        //        //On check si tout va bien pour les deux premiers

        //        //INSERT INTO SessionDeFormations (SessionDeCursus_SessionDeCursusId, Formation_FormationId, Formateur_FormateurId, DateDebut) VALUES (1, 1, 1, '01-11-20')
        //        if (app.SessionDeFormationId == 1)
        //        {
        //            Assert.AreEqual(new DateTime(2020, 11, 01), app.DateDebut);

        //            //INSERT INTO Formations(Nom, Dure) VALUES('Formation C# Initiation : Programmer en objet (5 jours)', 5)
        //            Assert.IsNotNull(app.Formation);
        //            Assert.AreEqual(1, app.Formation.FormationId);
        //            Assert.AreEqual("Formation C# Initiation : Programmer en objet (5 jours)", app.Formation.Nom);
        //            Assert.AreEqual(5, app.Formation.Dure);

        //            //INSERT INTO Formateurs(Prenom, Nom, Email, MotDePasse) VALUES('Albus', 'Dumbeldore', 'Albus.Dumbeldore@dawan.com', '1234')
        //            Assert.IsNotNull(app.Formateur);
        //            Assert.AreEqual(1, app.Formateur.FormateurId);
        //            Assert.AreEqual("Albus", app.Formateur.Prenom);
        //            Assert.AreEqual("Dumbeldore", app.Formateur.Nom);
        //            Assert.AreEqual("Albus.Dumbeldore@dawan.com", app.Formateur.Email);
        //            Assert.AreEqual("1234", app.Formateur.MotDePasse);

        //            //INSERT INTO SessionDeCursus(Cursus_CursusId) VALUES(1)
        //            Assert.IsNotNull(app.SessionDeCursus);
        //            Assert.AreEqual(1, app.SessionDeCursus.SessionDeCursusId);
        //        }

        //        //INSERT INTO SessionDeFormations(SessionDeCursus_SessionDeCursusId, Formation_FormationId, Formateur_FormateurId, DateDebut) VALUES(1, 2, 2, '08-11-20')
        //        if (app.SessionDeFormationId == 2)
        //        {
        //            Assert.AreEqual(new DateTime(2020, 11, 08), app.DateDebut);

        //            //INSERT INTO Formations (Nom ,Dure) VALUES ('Formation C# Approfondissement : Développer une application de bureau (5 jours)', 5)
        //            Assert.IsNotNull(app.Formation);
        //            Assert.AreEqual(2, app.Formation.FormationId);
        //            Assert.AreEqual("Formation C# Approfondissement : Développer une application de bureau (5 jours)", app.Formation.Nom);
        //            Assert.AreEqual(5, app.Formation.Dure);

        //            //INSERT INTO Formateurs(Prenom, Nom, Email, MotDePasse) VALUES('Renee', 'Bibine', 'Renee.Bibine@dawan.com', '1234')
        //            Assert.IsNotNull(app.Formateur);
        //            Assert.AreEqual(2, app.Formateur.FormateurId);
        //            Assert.AreEqual("Renee", app.Formateur.Prenom);
        //            Assert.AreEqual("Bibine", app.Formateur.Nom);
        //            Assert.AreEqual("Renee.Bibine@dawan.com", app.Formateur.Email);
        //            Assert.AreEqual("1234", app.Formateur.MotDePasse);

        //            //INSERT INTO SessionDeCursus(Cursus_CursusId) VALUES(1)
        //            Assert.IsNotNull(app.SessionDeCursus);
        //            Assert.AreEqual(1, app.SessionDeCursus.SessionDeCursusId);
        //        }
        //    }
        //}

   /* } *//*}*/
//}
