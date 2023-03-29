using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model.Tests
{
    [TestClass()]
    public class UtilisateurTests
    {
        private const string login = "mediatek";
        private const string pwd = "mediatekmdp";
        private const int idService = 1;
        private const string libelle = "Administrateur";

        private static readonly Utilisateur utilisateur = new Utilisateur(login, pwd, idService, libelle);
        [TestMethod()]
        public void UtilisateurTest()
        {
            Assert.AreEqual(login, utilisateur.Login, "devrait réussir : user valorisé");
            Assert.AreEqual(pwd, utilisateur.Pwd, "devrait réussir : pwd valorisé");
            Assert.AreEqual(idService, utilisateur.IdService, "devrait réussir : idService valorisé");
            Assert.AreEqual(libelle, utilisateur.Libelle, "devrait réussir : libellé valorisé");
        }
    }
}