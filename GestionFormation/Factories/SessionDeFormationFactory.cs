using Gestionformation.DAO;
using GestionFormation.DAO;
using GestionFormation.DTO;
using GestionFormation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionFormation.Factories
{
    public class SessionDeFormationFactory
    {
        public static List<SessionDeFormationDTO> GetSessionDeFormationList(List<SessionDeFormation> lists)
        {
            List<SessionDeFormationDTO> listsdfDTO = new List<SessionDeFormationDTO>();
            lists.ForEach(sdf => listsdfDTO.Add(GetSessionDeFormation(sdf)) );
            return listsdfDTO;
        }

        public static List<SessionDeFormation> GetSessionDeFormationList(List<SessionDeFormationDTO> lists)
        {
            List<SessionDeFormation> listsdf = new List<SessionDeFormation>();
            lists.ForEach(sdf => listsdf.Add(GetSessionDeFormation(sdf)));
            return listsdf;
        }

        private static SessionDeFormation GetSessionDeFormation(SessionDeFormationDTO sdf)
        {
            return new SessionDeFormation
            {
                SessionDeFormationId = sdf.SessionDeFormationId,
                //Formateur = FormateurDAO.getbyId(sdf.SessionDeFormationId),
                //Formation = sdf.FormationId,
                //SessionDeCursusId = sdf.SessionDeCursusId

            };
        }

        public static SessionDeFormationDTO GetSessionDeFormation(SessionDeFormation sdf)
        {

            return new SessionDeFormationDTO
            {
                SessionDeFormationId = sdf.SessionDeFormationId,
                //FormateurId = sdf.FormateurId,
                //FormationId = sdf.FormationId,

                //ATTENTION
                SessionDeCursusId = (sdf.SessionDeCursus == null) ? 0:sdf.SessionDeCursus.SessionDeCursusId,


                NomFormateur = sdf.Formateur?.Nom + " " + sdf.Formateur?.Prenom,
                NomFormation = sdf.Formation?.Nom

            };
        }
    }
}