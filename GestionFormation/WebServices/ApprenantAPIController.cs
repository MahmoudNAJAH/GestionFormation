﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GestionFormation.WebServices
{
    public class ApprenantAPIController : ApiController
    {
        // GET: api/ApprenantAPI
        //on va passer par le User 
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ApprenantAPI/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ApprenantAPI
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ApprenantAPI/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApprenantAPI/5
        public void Delete(int id)
        {
        }
    }
}
