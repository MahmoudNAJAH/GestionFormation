using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionFormation.Models
{
    public class PartageFichierModel
    {
        public List<SessionDeCursus_PartageFichier_Model> listSessionCursus { get; set; }
        public DirectoryModel DirectoryModel { get; set; }

        public PartageFichierModel()
        {
            listSessionCursus = new List<SessionDeCursus_PartageFichier_Model>();
            DirectoryModel = new DirectoryModel();
        }
    }
}