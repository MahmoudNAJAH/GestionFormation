using GestionFormation.DAO;
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
    public class PasswordController : ApiController
    {
       
        // PUT: api/Password/5
        public void Put(int id, [FromBody] string nouveauMotDepaase)
        {
            Apprenant ap1 = ApprenantDAO.FindById(id);
          

            CryptageMotDePasse password = new CryptageMotDePasse(nouveauMotDepaase);
            ap1.MotDePasse = password.ToArray();
            ApprenantDAO.Update(ap1);
        }

       
    }
}
