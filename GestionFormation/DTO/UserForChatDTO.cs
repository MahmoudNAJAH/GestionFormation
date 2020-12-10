using GestionFormation.DAO;
using GestionFormation.Entities;
using GestionFormation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionFormation.DTO
{
    public class UserForChatDTO
    {
        public int Id { get; set; }
        public string Prenom_N_ { get; set; }
        public UserRole Role { get; set; }

        public IEnumerable<SelectListItem> ListeDesSalons { get; set; }
        public string SalonSelectionne { get; set; }


        public UserForChatDTO()
        {
        }
        public UserForChatDTO(Apprenant ap)
        {
            this.Id = ap.ApprenantId;
            this.Prenom_N_ = $"{ap.Prenom}" + " " + $"{ap.Nom.Substring(0, 1)}.";
            List<SessionDeCursus> sdc = ApprenantDAO.GetSessionsDeCursus(ap);
            if (sdc.Count() == 1) SalonSelectionne = sdc.First().SessionDeCursusId.ToString();
            
            this.ListeDesSalons = sdc.Select(s =>
                            new SelectListItem()
                            {
                                Text = $"{s.Cursus.Nom}",
                                Value = $"{s.SessionDeCursusId}"

                            });
            this.Role = UserRole.ATTENDANT;


        }

    }

}