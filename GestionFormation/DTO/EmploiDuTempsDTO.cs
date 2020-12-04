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
                    while (EstFerie(DateTraitement) || EstWeekEnd(DateTraitement))
                        DateTraitement = DateTraitement.AddDays(1);

                    ListDates.Add(new JourneeDTO { Date = DateTraitement, Formation = SessionForm.Formation, Formateur = SessionForm.Formateur });
                }
            }
        }

        //Avec la maj sur UserDTO, fonction plus nécessaire
        /*
        private List<SessionDeFormation> GetSessionDeFormations()
        {
            List<SessionDeFormation> listForm = new List<SessionDeFormation>();
            

            switch (User.Role)
            {
                case UserRole.ATTENDANT:

                    List<SessionDeCursus> listSessionDeCursus = (ApprenantDAO.FindById(User.Id)).SessionDeCursus;

                    foreach(SessionDeCursus SessionCursus in listSessionDeCursus)
                    {
                        //on récupère les sessions Formations, mais leurs objets sont vides (Formation, Formateur etc..)
                        List<SessionDeFormation> listSessionFormations = new List<SessionDeFormation>();
                        listSessionFormations.AddRange(SessionDeCursusDAO.FindById(SessionCursus.SessionDeCursusId).SessionsDeFormations);

                        //On les recharge en récupérant leurs objets à l'aide de FindById
                        foreach(SessionDeFormation ses in listSessionFormations)
                            listForm.Add(SessionDeFormationDAO.FindById(ses.SessionDeFormationId));
                    }

                    break;

                case UserRole.FORMATEUR:

                    List<SessionDeFormation> listSessionFormation = new List<SessionDeFormation>();
                    listSessionFormation = FormateurDAO.FindById(User.Id).SessionDeFormations;

                    foreach (SessionDeFormation ses in listSessionFormation)
                        listForm.Add(SessionDeFormationDAO.FindById(ses.SessionDeFormationId));
                        //listForm.Add(ses);

                    break;

                case UserRole.ADMIN:
                    break;

                default:
                    throw new Exception("Erreur : l'utilisateur n'est ni un Formateur ni un Attendant");
            }

            return listForm;
        }
        */

        private static bool EstFerie(DateTime date)
        {
            List<DateTime> JoursFeries = new List<DateTime>
            {
                new DateTime(date.Year,1,1),        // 01 Janvier
                new DateTime(date.Year, 5, 1),      // 01 Mai
                new DateTime(date.Year, 5, 8),      // 08 Mai
                new DateTime(date.Year, 7, 14),     // 14 Juillet
                new DateTime(date.Year, 8, 15),     // 15 Aout
                new DateTime(date.Year, 11, 1),     // 01 Novembre
                new DateTime(date.Year, 11, 11),    // 11 Novembre
                new DateTime(date.Year, 12, 25),    // Noël
            };

            //  --  Calcul de pâque
            //  --  Trouver sur internet : 
            JoursFeries.AddRange(CalculPaque(date));

            foreach (DateTime d in JoursFeries)
                //Je ne sais pas si (date == d) fonctionne correctement, alors dans le doute ...
                if (date.Day == d.Day && date.Month == d.Month) return true;

            return false;
        }

        private static bool EstWeekEnd(DateTime date)
        {
            //Dimanche = 0, Lundi = 1 ......, Samedi = 6 
            if ((int)date.DayOfWeek == 0 || (int)date.DayOfWeek == 6) return true;
            else return false;
        }

        private static List<DateTime> CalculPaque(DateTime date)
        {
            // Calcul du jour de pâques (algorithme de Oudin (1940))
            //Calcul du nombre d'or - 1
            int intGoldNumber = (int)(date.Year % 19);
            // Année divisé par cent
            int intAnneeDiv100 = (int)(date.Year / 100);
            // intEpacte est = 23 - Epacte (modulo 30)
            int intEpacte = (int)((intAnneeDiv100 - intAnneeDiv100 / 4 - (8 * intAnneeDiv100 + 13) / 25 + (
            19 * intGoldNumber) + 15) % 30);
            //Le nombre de jours à partir du 21 mars pour atteindre la pleine lune Pascale
            int intDaysEquinoxeToMoonFull = (int)(intEpacte - (intEpacte / 28) * (1 - (intEpacte / 28) * (29 / (intEpacte + 1)) * ((21 - intGoldNumber) / 11)));
            //Jour de la semaine pour la pleine lune Pascale (0=dimanche)
            int intWeekDayMoonFull = (int)((date.Year + date.Year / 4 + intDaysEquinoxeToMoonFull +
                  2 - intAnneeDiv100 + intAnneeDiv100 / 4) % 7);
            // Nombre de jours du 21 mars jusqu'au dimanche de ou 
            // avant la pleine lune Pascale (un nombre entre -6 et 28)
            int intDaysEquinoxeBeforeFullMoon = intDaysEquinoxeToMoonFull - intWeekDayMoonFull;
            // mois de pâques
            int intMonthPaques = (int)(3 + (intDaysEquinoxeBeforeFullMoon + 40) / 44);
            // jour de pâques
            int intDayPaques = (int)(intDaysEquinoxeBeforeFullMoon + 28 - 31 * (intMonthPaques / 4));
            // lundi de pâques
            DateTime dtMondayPaques = new DateTime(date.Year, intMonthPaques, intDayPaques + 1);
            // Ascension
            DateTime dtAscension = dtMondayPaques.AddDays(38);
            //Pentecote
            DateTime dtMondayPentecote = dtMondayPaques.AddDays(49);

            List<DateTime> JoursPaque = new List<DateTime>
            {
                dtMondayPaques,
                dtAscension,
                dtMondayPentecote
            };

            return JoursPaque;
        }
    }
}