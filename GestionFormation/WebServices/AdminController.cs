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
    public class AdminController : ApiController
    {
        // GET: api/Admin
        public List<Admin> Get()
        {
            return AdminDAO.FindAll();
        }

        // GET: api/Admin/5
        public Admin Get(int id)
        {
            return AdminDAO.FindById(id);
        }

        // POST: api/Admin
        public void Post([FromBody]Admin value)
        {
            if (value?.AdminId == null || value.AdminId == 0)
                value.AdminId = AdminDAO.FindAll().Select(m => m.AdminId).Max() + 1;

            AdminDAO.Create(value);
        }

        // PUT: api/Admin/5
        public void Put(int id, [FromBody]string value)
        {
            Admin adm = AdminDAO.FindById(id);

            if (adm != null)
                AdminDAO.Update(adm);
        }

        // DELETE: api/Admin/5
        public void Delete(int id)
        {
            Admin form = AdminDAO.FindById(id);

            if (AdminDAO.FindById(id) != null)
                AdminDAO.Delete(id);
        }
    }
}
