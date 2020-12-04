using GestionFormation.Entities;
using GestionFormation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GestionFormation.WebServices
{
    public class CursusAPIController : ApiController
    {
        // GET: api/CursusAPI
        //[Route ("/cursus")]
        public List<Cursus> Get()
        {
            return CursusService.FindAll();
        }

        // GET: api/CursusAPI/5
        public Cursus Get(int id)
        {
            return CursusService.FindById(id);
        }

        // POST: api/CursusAPI
        public void Post([FromBody] Cursus value)
        {
            CursusService.Create(value);

        }

        // PUT: api/CursusAPI/5
        public void Put(int id, [FromBody] Cursus value)
        {
            if (id == value.CursusId) CursusService.Update(value);
        }

        // DELETE: api/CursusAPI/5
        public void Delete(int id)
        {
        }
    }
}
