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
    public class ExemplaireTests
    {
        private const int numero = 17;
        private static readonly DateTime dateAchat = new DateTime(2023, 3, 18);
        private const string photo = "";
        private const string idEtat = "00002";
        private const string id = "10004";

         private static readonly Exemplaire exemplaire = new Exemplaire(numero, dateAchat, photo, idEtat, id);
        [TestMethod()]
        public void ExemplaireTest()
        {
            Assert.AreEqual(numero, exemplaire.Numero, "devrait réussir : numéro valorisé");
            Assert.AreEqual(dateAchat, exemplaire.DateAchat, "devrait réussir : date d'achat valorisée");
            Assert.AreEqual(photo, exemplaire.Photo, "devrait réussir : photo valorisée");
            Assert.AreEqual(idEtat, exemplaire.IdEtat, "devrait réussir : idEtat valorisé");
            Assert.AreEqual(id, exemplaire.Id, "devrait réussir : id du document valorisé");
        }
    }
}