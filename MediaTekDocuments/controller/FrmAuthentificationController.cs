using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaTekDocuments.model;
using MediaTekDocuments.dal;
using MediaTekDocuments.view;
using System.Security.Cryptography;

namespace MediaTekDocuments.controller
{
    public class FrmAuthentificationController
    {
        /// <summary>
        /// Objet d'accès aux données
        /// </summary>
        private readonly Access access;

        public FrmAuthentification frmAuthentification;

        /// <summary>
        /// Récupération de l'instance unique d'accès aux données
        /// </summary>
        public FrmAuthentificationController()
        {
            access = Access.GetInstance();

        }

        /// <summary>
        /// récupere l'id et le mot de passe d'un admin
        /// </summary>
        /// <param name="login"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool GetAuthentification(string login, string pwd)
        {

            Utilisateur utilisateur = access.GetAuthentification(login);
            if(utilisateur == null)
            {
                return false;
            }
            // retourne vrai si le pwd est correct
            if (utilisateur.Pwd.Equals(sha256(pwd)))
            {
                Service.Id = utilisateur.IdService;
                Service.Libelle = utilisateur.libelle;
                return true;
            }
            return false;

        }

        /// <summary>
        /// Hash le mot de passe
        /// </summary>
        /// <param name="randomString"></param>
        /// <returns></returns>
        static string sha256(string randomString)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

    }
}
