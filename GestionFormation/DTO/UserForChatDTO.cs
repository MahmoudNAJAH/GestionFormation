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
            Id = ap.ApprenantId;
            Prenom_N_ = $"{ap.Prenom}" + " " + $"{ap.Nom.Substring(0, 1)}.";
            ListeDesSalons = ApprenantDAO.GetSessionsDeCursus(ap).Select(s =>
                                  new SelectListItem()
                                  {
                                      Text = $"{s.Cursus.Nom}",
                                      Value = $"{s.SessionDeCursusId}"

                                  });
            Role = UserRole.ATTENDANT;
           
        }

    }

}