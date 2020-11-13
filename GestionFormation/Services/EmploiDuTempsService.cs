using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace GestionFormation.Services
{
    public class EmploiDuTempsService
    {

        private void FillEdt(List<FormationSession> listForm)
        {
            foreach (FormationSession f in listForm)
            {
                DateTime DateTraitement = f.DateDebut;

                //On va remplir le planning, on boucle sur la durée de la FormationSession
                for (int i_dure = 0; i_dure < f.Dure; i_dure++)
                {
                    //Le premier jours de la formation est pour f.DateDebut => i_dure = 0
                    if (i_dure != 0) DateTraitement.AddDays(1);

                    //Pour que DateTraitement soit un jours ouvrable
                    while (EstFerie(DateTraitement) || EstWeekEnd(DateTraitement))
                    {
                        //Si ce jours est férié, on passe au suivant
                        while (EstFerie(DateTraitement)) DateTraitement.AddDays(1);
                        //Si week end, on se met au lundi suivant
                        while (EstWeekEnd(DateTraitement)) DateTraitement.AddDays(1);
                    }

                    //On récupère les coordonnées
                    //La semaine
                    DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                    Calendar cal = dfi.Calendar;
                    int Semaine = cal.GetWeekOfYear(DateTraitement, dfi.CalendarWeekRule, dfi.FirstDayOfWeek) - 1;
                    //Le jours de la semaine
                    //Dimanche = 0, Lundi = 1 ......, Samedi = 6 
                    //On passe à Lundi = 0 ..... Dimanche = 6
                    int Jours = (int)DateTraitement.DayOfWeek - 1 == -1 ? 6 : (int)DateTraitement.DayOfWeek - 1;

                    //On regarde s'il y a déjà une formation ce jours là.
                    int indexListPlanning = 0;
                    while (Planning[Semaine, Jours][indexListPlanning] != null) indexListPlanning++;

                    //On fill le planning
                    Planning[Semaine, Jours][indexListPlanning] = new Tuple<DateTime, Formateur, Formation>(DateTraitement, FormateurDAO.FindById(f.FormateurId), FormationDAO.FindById(f.FormationId));
                }
            }
        }


        private bool EstFerie(DateTime date)
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
                if (date.Day == d.Day && date.Month == d.Month) return false;

            return true;
        }

        private bool EstWeekEnd(DateTime date)
        {
            //Dimanche = 0, Lundi = 1 ......, Samedi = 6 
            if ((int)date.DayOfWeek == 0 || (int)date.DayOfWeek == 6) return true;
            else return false;
        }

        private List<DateTime> CalculPaque(DateTime date)
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