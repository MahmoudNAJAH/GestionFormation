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
    public class FormationService
    {
        public static List<FormationDTO> GetAllFormation()
        {
            return FormationFactory.GetFormationList(FormationDAO.FindAll());
        }

        internal static object GetFormation(int id)
        {
            return FormationFactory.GetFormation(FormationDAO.FindById(id));
        }

        internal static object EditFormation(int id)
        {
            throw new NotImplementedException();
        }

        internal static void Create(FormationDTO f)
        {
            throw new NotImplementedException();
        }
    }
}