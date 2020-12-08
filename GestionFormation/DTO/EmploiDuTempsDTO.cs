using Gestionformation.DAO;
using GestionFormation.DAO;
using GestionFormation.Entities;
using GestionFormation.Filters;
using GestionFormation.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace GestionFormation.DTO
{
    public class EmploiDuTempsDTO
    {
        private UserDTO User { get; set; }
        public List<JourneeDTO> ListDates = new List<JourneeDTO>();

        public EmploiDuTempsDTO(UserDTO user)
        {
            User = user;
            FillListDates();
        }

        public void FillListDates()
        {
            //List<SessionDeFormation> SessionDeFormations = GetSessionDeFormations();
            List<SessionDeFormation> SessionDeFormations = User.GetSessionDeFormations();

            foreach(SessionDeFormation SessionForm in SessionDeFormations)
            {
                DateTime DateTraitement = SessionForm.DateDebut;

                if (DateTraitement == null) continue;
                if (SessionForm.Formation?.Dure == null) continue;

                //On va remplir l'EDT, on boucle sur la durée de la FormationSession
                for (int i_dure = 0; i_dure < SessionForm.Formation.Dure; i_dure++)
                {
                    //Quand on passe à i_dure++, on passe au jours suivant en ajoutant DateTraitement+1days
                    //Mais le premier jours de la formation est pour SessionForm.DateDebut => i_dure = 0
                    if (i_dure != 0) 
                        DateTraitement = DateTraitement.AddDays(1);
                    
                    //Pour que DateTraitement soit un jours ouvrable
                    while (!EmploiDuTempsService.EstJoursOuvrable(DateTraitement))
                        DateTraitement = DateTraitement.AddDays(1);

                    ListDates.Add(new JourneeDTO { Date = DateTraitement, Formation = SessionForm.Formation, Formateur = SessionForm.Formateur });
                }
            }
        }
    }
}