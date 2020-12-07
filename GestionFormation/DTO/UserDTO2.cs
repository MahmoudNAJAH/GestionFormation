using GestionFormation.Entities;
using GestionFormation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionFormation.DTO
{
    public class UserDTO2 
    {
       
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }

        public string Email { get; set; }

        public string  MotDePasse { get; set; }
        public virtual List<SessionDeCursus> SessionDeCursus { get; set; }
        public virtual List<Message> Messages { get; set; }




        public   static byte[] transformeEnBytes(string maChaine)
        {
            byte[] bytes = new byte[maChaine.Length * sizeof(char)];
            System.Buffer.BlockCopy(maChaine.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }


    }
   


}
    //        Mon mot de passe est déclaré ainsi dans ma classe User :

    //  public byte[] Password { get; set; }

    //        J'ai voulu insérer un utilisateur dans une migration Code First : notez la conversion de la chaîne en tableau de bytes + l'ajout de "0x" dans la requête.
    //        Ca, c'est uniquement si vous avez besoin de faire une requête SQL "en dur"

    


   
   