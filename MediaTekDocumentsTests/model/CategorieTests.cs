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
    public class CategorieTests
    {
        private const string idcategorie = "10020";
        private const string libelle = "Horreur";
        private static readonly Categorie categorie = new Categorie(idcategorie, libelle);

        [TestMethod()]
        public void CategorieTest()
        {
            Assert.AreEqual(idcategorie, categorie.Id, "devrait reussir: id valorisé");
            Assert.AreEqual(libelle, categorie.Libelle, "devrait reussir : libelle vaorisé");
        }

       
        [TestMethod()]
        public void ToStringTest1()
        {
            Assert.AreEqual(libelle, categorie.Libelle, "devrait reussir : libelle vaorisé");
        }
    }
}