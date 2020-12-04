using GestionFormation.DAO;
using GestionFormation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionFormation.Services
{
    public class CursusService
    {

        public static List<Cursus> FindAll()
        {
            return CursusDAO.FindAll();
        }

        public static Cursus FindById(int cursusId)
        {
            return CursusDAO.FindById(cursusId);
        }

        internal static void Create(Cursus c)
        {
            CursusDAO.Create(c);
        }

        internal static void Update(Cursus value)
        {
            throw new NotImplementedException();
        }
    }
}