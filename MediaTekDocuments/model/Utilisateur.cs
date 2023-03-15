using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    public class Utilisateur
    {

        public string Login { get; }
        public string Pwd { get; }
        public int IdService { get; }
        public string Libelle { get; }

        public Utilisateur(string login, string pwd, int idService, string libelle)
        {
            this.Login = login;
            this.Pwd = pwd;
            this.IdService = idService;
            this.Libelle = libelle;
        }
    }
}
