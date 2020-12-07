using GestionFormation.DAO;
using GestionFormation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GestionFormation.WebServices
{
    public class FormationAPIController : ApiController
    {
        // GET: api/FormationAPI
        public List<Formation> Get()
        {
            List<Formation> result = new List<Formation>();

            foreach (Formation f in FormationDAO.FindAll())
            {
                Formation form2 = new Formation
                {
                    FormationId = f.FormationId,
                    Nom = f.Nom,
                    Description = f.Description,
                    Dure = f.Dure
                };

                form2.Cursus = new List<Cursus>();
                form2.SessionsDeFormations = new List<SessionDeFormation>();

                foreach (Cursus curs in f.Cursus)
                    form2.Cursus.Add(new Cursus
                    {
                        CursusId = curs.CursusId,
                        Nom = curs.Nom,
                        Description = curs.Description
                    });
                foreach (SessionDeFormation curs in f.SessionsDeFormations)
                    form2.SessionsDeFormations.Add(new SessionDeFormation
                    {
                        SessionDeFormationId = curs.SessionDeFormationId,
                        DateDebut = curs.DateDebut,
                    });

                result.Add(form2);
            }

            return result;
        }

        // GET: api/FormationAPI/5
        public Formation Get(int id)
        {
            Formation form = FormationDAO.FindById(id);

            Formation form2 = new Formation
            {
                FormationId = form.FormationId,
                Nom = form.Nom,
                Description = form.Description,
                Dure = form.Dure
            };

            form2.Cursus = new List<Cursus>();
            form2.SessionsDeFormations = new List<SessionDeFormation>();

            foreach (Cursus curs in form.Cursus)
                form2.Cursus.Add(new Cursus
                {
                    CursusId = curs.CursusId,
                    Nom = curs.Nom,
                    Description = curs.Description
                });
            foreach (SessionDeFormation curs in form.SessionsDeFormations)
                form2.SessionsDeFormations.Add(new SessionDeFormation
                {
                    SessionDeFormationId = curs.SessionDeFormationId,
                    DateDebut = curs.DateDebut,
                });

            return form2;
        }

        // POST: api/FormationAPI
        public void Post([FromBody] Formation value)
        {
            value.FormationId = FormationDAO.FindAll().Select(m => m.FormationId).Max() + 1;

            FormationDAO.Create(value);
        }

        // PUT: api/FormationAPI/5
        public void Put(int id, [FromBody] Formation value)
        {
            Formation form = FormationDAO.FindById(id);

            if (form != null)
                FormationDAO.Update(form);
        }

        // DELETE: api/FormationAPI/5
        public void Delete(int id)
        {
            Formation form = FormationDAO.FindById(id);

            if (form != null)
                FormationDAO.Delete(id);
        }
    }
}
