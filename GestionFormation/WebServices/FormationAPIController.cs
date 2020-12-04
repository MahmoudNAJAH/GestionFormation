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
            List <Formation> result = FormationDAO.FindAll();

            return result;
        }

        // GET: api/FormationAPI/5
        public Formation Get(int id)
        {
            return FormationDAO.FindById(id);
        }

        // POST: api/FormationAPI
        public void Post([FromBody] Formation value)
        {
            if (value?.FormationId == null || value.FormationId == 0)
                value.FormationId = FormationDAO.FindAll().Select(m => m.FormationId).Max() + 1;

            FormationDAO.Create(value);
        }

        // PUT: api/FormationAPI/5
        public void Put(int id, [FromBody] Formation value)
        {
            Formation form = FormationDAO.FindById(id);

            if(form != null)
                FormationDAO.Update(form);
        }

        // DELETE: api/FormationAPI/5
        public void Delete(int id)
        {
            Formation form = FormationDAO.FindById(id);

            if (FormationDAO.FindById(id) != null)
                FormationDAO.Delete(id);
        }

    }
}
