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
    }
}