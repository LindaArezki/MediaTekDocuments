using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier des utilisateurs
    /// </summary>
    public class Utilisateur
    {
        /// <summary>
        /// Login
        /// </summary>
        public string Login { get; }
        /// <summary>
        /// mot de passe
        /// </summary>
        public string Pwd { get; }
        /// <summary>
        /// id du service
        /// </summary>
        public int IdService { get; }
        /// <summary>
        /// libelle du service
        /// </summary>
        public string Libelle { get; }

        /// <summary>
        /// Composants de la classe
        /// </summary>
        /// <param name="login"></param>
        /// <param name="pwd"></param>
        /// <param name="idService"></param>
        /// <param name="libelle"></param>
        public Utilisateur(string login, string pwd, int idService, string libelle)
        {
            this.Login = login;
            this.Pwd = pwd;
            this.IdService = idService;
            this.Libelle = libelle;
        }
    }
}
