using GestionFormation.DTO;
using GestionFormation.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace GestionFormation.Services
{
    public class EmploiDuTempsService
    {
        public static List<JourneeDTO> GetWeek(List<JourneeDTO> allDays, DateTime dateReference)
        {
            List<JourneeDTO> result = new List<JourneeDTO>();

            result.AddRange(allDays.FindAll(it => it.Date.ToString("dd/MM/yyyy") == dateReference.ToString("dd/MM/yyyy")));

            // -- On veut envoyer Les journee qui sont la même semaine que ma dateReference

            //On récupère la semaine
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;
            //int WeekNumber = cal.GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule, dfi.FirstDayOfWeek) - 1;

            //on ne connait pas la place de DateTime.Now dans la semaine
            //En partant de dateReference, on regarde jusqu'a +7 jours et -7 jours pour être sur d'avoir toute la semaine

            //On initialise les compteurs
            DateTime nowMinus = dateReference;
            DateTime nowAdd = dateReference;

            //On lance la boucle
            for (int i_jours = 0; i_jours < 7; i_jours++)
            {
                //On change de jours
                nowMinus = nowMinus.AddDays(-1);
                nowAdd = nowAdd.AddDays(1);

                //Si DateTime.now et DateTime-i_jours sont la même semaine, on ajoute
                if (cal.GetWeekOfYear(dateReference, dfi.CalendarWeekRule, dfi.FirstDayOfWeek) == cal.GetWeekOfYear(nowMinus, dfi.CalendarWeekRule, dfi.FirstDayOfWeek))
                {
                    result.AddRange(allDays.FindAll(it => it.Date.ToString("dd/MM/yyyy") == nowMinus.ToString("dd/MM/yyyy")));
                }

                //Si DateTime.now et DateTime+i_jours sont la même semaine, on ajoute
                if (cal.GetWeekOfYear(dateReference, dfi.CalendarWeekRule, dfi.FirstDayOfWeek) == cal.GetWeekOfYear(nowAdd, dfi.CalendarWeekRule, dfi.FirstDayOfWeek))
                {
                    result.AddRange(allDays.FindAll(it => it.Date.ToString("dd/MM/yyyy") == nowAdd.ToString("dd/MM/yyyy")));
                }
            }
            return TrierParDate(result);
        }

        public static List<JourneeDTO> TrierParDate(List<JourneeDTO> allDays)
        {
            List<JourneeDTO> result = new List<JourneeDTO>();

            //Tant qu'on a pas traiter toutes les dates
            while(allDays.Count >0)
            {
                //Pour stocker la plus petite date
                DateTime date_Min = allDays[0].Date;

                //On cherche la plus petite date
                for(int index = 0; index < allDays.Count; index++ )
                    if(allDays[index].Date < date_Min)
                        date_Min = allDays[index].Date;

                //On traite les journees correspondantes
                for (int index = 0; index < allDays.Count; index++)
                    if (allDays[index].Date == date_Min)
                    {
                        result.Add(allDays[index]);
                        allDays.RemoveAt(index);
                    }
            }

            return result;
        }

        public static DateTime DatePreviousMonday(DateTime dateReference)
        {
            switch(dateReference.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return dateReference;

                case DayOfWeek.Tuesday:
                    return dateReference.AddDays(-1);

                case DayOfWeek.Wednesday:
                    return dateReference.AddDays(-2);

                case DayOfWeek.Thursday:
                    return dateReference.AddDays(-3);

                case DayOfWeek.Friday:
                    return dateReference.AddDays(-4);

                case DayOfWeek.Saturday:
                    return dateReference.AddDays(-5);

                case DayOfWeek.Sunday:
                    return dateReference.AddDays(-6);

                default:
                    return dateReference;
            }
        }

        public static bool EstJoursOuvrable(DateTime date)
        {
            return !(EstFerie(date) || EstWeekEnd(date));
        }

        public static bool EstFerie(DateTime date)
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