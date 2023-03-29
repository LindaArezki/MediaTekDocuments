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
    public class SuiviTests
    {
        private const int idetape = 5;
        private const string libelle = "Autre";

        private static readonly Suivi suivi = new Suivi(idetape, libelle);
        [TestMethod()]
        public void SuiviTest()
        {
            Assert.AreEqual(idetape, suivi.Idetape, "devrait réussir : id valorisé");
            Assert.AreEqual(libelle, suivi.Libelle, "devrait réussir : libellé valorisé");
        }
    }
}