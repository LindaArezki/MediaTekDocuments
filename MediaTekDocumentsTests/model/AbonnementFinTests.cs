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
    public class AbonnementFinTests
    {
       
        private static readonly DateTime dateFinAbonnement = new DateTime(2023, 3, 20);
        private const string idRevue = "10002";
        private const string titre = "Alternatives Economiques";
        private static readonly AbonnementFin abonnementFin = new AbonnementFin(dateFinAbonnement, idRevue, titre);

        [TestMethod()]
        public void AbonnementFinTest()
        {
            Assert.AreEqual(dateFinAbonnement, abonnementFin.DateFinAbonnement, "devrait réussir : date de fin d'abonnement valorisée");
            Assert.AreEqual(idRevue, abonnementFin.IdRevue, "devrait réussir : idRevue valorisé");
            Assert.AreEqual(titre, abonnementFin.Titre, "devrait réussir : titre valorisé");
        }
    }
}