using GestionFormation.DTO;
using GestionFormation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionFormation.Factories
{
    public class FormationFactory
    {
        public static List<FormationDTO> GetFormationList(List<Formation> lists)
        {
            List<FormationDTO> listfnDTO = new List<FormationDTO>();
            lists.ForEach(fn => listfnDTO.Add(GetFormation(fn)) );
            return listfnDTO;
        }

        public static List<Formation> GetFormationList(List<FormationDTO> lists)
        {
            List<Formation> listfn = new List<Formation>();
            lists.ForEach(fn => listfn.Add(GetFormation(fn)));
            return listfn;
        }

        private static Formation GetFormation(FormationDTO fn)
        {
            return new Formation
            {
                FormationId = fn.FormationId,
                Nom = fn.Nom,
                Description = fn.Description
            };
        }

        public static FormationDTO GetFormation(Formation fn)
        {

            return new FormationDTO
            {
                FormationId = fn.FormationId,
                Nom = fn.Nom,
                Description = fn.Description
            };
        }
    }
}