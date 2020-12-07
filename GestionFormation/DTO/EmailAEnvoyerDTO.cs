using GestionFormation.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionFormation.DTO
{
    public class EmailAEnvoyerDTO
    {
        public int FromId { get; set; }
        [DisplayName("Destinataire")]
        public string ToId { get; set; }
        public IEnumerable<SelectListItem> ListDestinataires { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("Contenu")]

        public string Content { get; set; }
        [DisplayName("Objet")]

        public string Subject { get; set; }
        [DisplayName("Joindre un fichier")]

        public string AttachementPath { get; set; }

        public EmailAEnvoyerDTO(int AppId)
        {
            ListDestinataires = EmailService.GetDestinatairesPossibles(AppId).Select(x =>
                              new SelectListItem()
                              {
                                  Text = $"{x.Prenom} {x.Nom} {x.Role}",
                                  Value = x.Role.ToString().Substring(0,1) + x.Id.ToString()

                              });
        }
        public EmailAEnvoyerDTO() { }

    }
}