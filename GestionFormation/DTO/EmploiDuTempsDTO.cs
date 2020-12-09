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
    /// <summary>
    /// Contient :
    ///     - User
    ///     - ListDates : 
    ///         -> pour chaque date, on a la formation, le formateur
    /// </summary>
    public class EmploiDuTempsDTO
    {
        private UserDTO User { get; set; }

        public List<JourneeDTO> ListDates = new List<JourneeDTO>(); //Pour ne pas avoir besoin de l'initialiser quand on utilisare l'EDT

        //Constructeur
        public EmploiDuTempsDTO(UserDTO user)
        {
            User = user;
            FillListDates();
        }

        //Nous permet de remplir l'emploi du temps
        public void FillListDates()
        {
            //A partir du user, on récupère toutes les SessionDeFormation associées
            List<SessionDeFormation> SessionDeFormations = User.GetSessionDeFormations();

            //On boucle sur chaque session de formation
            //Pour le moment, on ne connait que la date de début de Session et sa durée en nombre de jours
            //On doit déterminer toutes les dates/jours de la formation
            foreach(SessionDeFormation SessionForm in SessionDeFormations)
            {
                //On initialise notre variable à la date de début de la SessionDeFormation
                //Elle servira à passer au jours suivant etc ...
                DateTime DateTraitement = SessionForm.DateDebut;

                //Si une information n'est pas renseigné dans la BDD, on sort de la boucle, on ne traite pas cette SessionDeFormation
                if (DateTraitement == null) continue;
                if (SessionForm.Formation?.Dure == null) continue;

                //On va remplir l'EDT
                //On boucle sur la durée de la FormationSession
                for (int i_dure = 0; i_dure < SessionForm.Formation.Dure; i_dure++)
                {
                    //Quand on passe à i_dure++, on passe au jours suivant en ajoutant DateTraitement+1days
                    //Mais le premier jours de la formation est pour SessionForm.DateDebut => i_dure = 0
                    if (i_dure != 0) DateTraitement = DateTraitement.AddDays(1);

                    //Pour que DateTraitement soit un jours ouvrable
                    while (!EmploiDuTempsService.EstJoursOuvrable(DateTraitement))
                        DateTraitement = DateTraitement.AddDays(1);

                    //Notre DateTraitement est un jours ouvrable 
                    //On l'ajoute à notre list de Dates pour l'EDT
                    //On regroupe dans un même objet la Date, la Formation et le Formateur
                    ListDates.Add(new JourneeDTO { Date = DateTraitement, Formation = SessionForm.Formation, Formateur = SessionForm.Formateur });
                }
            }
        }
    }
}