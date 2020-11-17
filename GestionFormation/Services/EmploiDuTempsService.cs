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

            result.AddRange(allDays.FindAll(it => it.Date == dateReference));

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
            return result;
        }
    }
}