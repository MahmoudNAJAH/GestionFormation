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
    public class CursusApprenantController : ApiController
    {
        // GET: api/CursusApprenant
       
        // POST: api/CursusApprenant
        public void Post([FromBody] Cursus c , int id )
        {
            Apprenant ap1 = ApprenantDAO.FindById(id);
            foreach (SessionDeCursus Cs in ap1.SessionDeCursus)
            {
                List<SessionDeCursus> sessionCursus = new List<SessionDeCursus>();
                sessionCursus.Add(Cs);
                ap1.SessionDeCursus = sessionCursus;
            }
        }

      
    }
}
