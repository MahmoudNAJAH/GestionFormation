using GestionFormation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace GestionFormation.DAO
{
    public class AdminDAO
    {
        public static void Create(Admin ad)
        {
            using (BDDContext context = new BDDContext())
            {
                context.Admins.Add(ad);
                context.SaveChanges();
            }
        }
        public static Admin FindById(int adminId)
        {
            using (BDDContext context = new BDDContext())
            {
                return context.Admins.FirstOrDefault(ad => ad.AdminId == adminId);
            }
        }

        public static Admin FindByLgMD(string mail)
        {
            using (BDDContext context = new BDDContext())
            {
                return context.Admins.FirstOrDefault(a => a.Email == mail);
            }       
        }

        public static List<Admin> FindAll()
        {
            using (BDDContext context = new BDDContext())
            {
                return context.Admins.ToList();
            }
        }

        public static void Update(Admin ad)
        {
            using (BDDContext context = new BDDContext())
            {
                Admin adDansDB = FindById(ad.AdminId);
                if (ad.Nom != null) adDansDB.Nom = ad.Nom;
                if (ad.Prenom != null) adDansDB.Prenom = ad.Prenom;
                if (ad.Email != null) adDansDB.Email = ad.Email;

                context.SaveChanges();
            }
        }

        public static void Delete(Admin ad)
        {
            using (BDDContext context = new BDDContext())
            {
                context.Admins.Remove(ad);
                context.SaveChanges();
            }
        }
    }
}