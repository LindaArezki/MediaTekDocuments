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
    public class CommandesDocumentTests
    {
        private const string id = "17";
        private static readonly DateTime dateCommande = new DateTime(2023, 3, 18);
        private const double montant = 85;
        private const int nbExemplaire = 12;
        private const string idLivreDvd = "20001";
        private const int suivi = 1;
        private const string libelle = "Luc est entraîné par Yoda pendant que Han et Leia tentent de se cacher dans la cité des nuages.";
        private static readonly CommandesDocument commandesDocument = new CommandesDocument(id,dateCommande,montant,nbExemplaire, idLivreDvd, suivi, libelle);
 
         [TestMethod()]
        public void CommandesDocumentTest()
        {
            Assert.AreEqual(id, commandesDocument.Id, "devrait réussir : id valorisé");
            Assert.AreEqual(dateCommande, commandesDocument.DateCommande, "devrait réussir : date de commande valorisée");
            Assert.AreEqual(montant, commandesDocument.Montant, "devrait réussir : montant valorisé");
            Assert.AreEqual(nbExemplaire, commandesDocument.NbExemplaire, "devrait réussir : nombre d'exemplaires valorisé");
            Assert.AreEqual(idLivreDvd, commandesDocument.IdLivreDvd, "devrait réussir : idLivreDvd valorisé");
            Assert.AreEqual(suivi, commandesDocument.Suivi, "devrait réussir : idSuivi valorisé");
            Assert.AreEqual(libelle, commandesDocument.Libelle, "devrait réussir : libellé valorisé");
        }
    }
}