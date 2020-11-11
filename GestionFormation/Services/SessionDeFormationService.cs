using GestionFormation.DAO;
using GestionFormation.DTO;
using GestionFormation.Entities;
using GestionFormation.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionFormation.Services
{
    public class SessionDeFormationService
    {
        public static List<SessionDeFormationDTO> GetAllSessionDeFormation()
        {
            return SessionDeFormationFactory.GetSessionDeFormationList(SessionDeFormationDAO.FindAll());
        }

        public static object GetSessionDeFormation(int id)
        {
            return SessionDeFormationFactory.GetSessionDeFormation(SessionDeFormationDAO.FindById(id));
        }

        public static object EditSessionDeFormation(int id)
        {
            throw new NotImplementedException();
        }

        public static void Create(SessionDeFormationDTO f)
        {
            throw new NotImplementedException();
        }
    }
}