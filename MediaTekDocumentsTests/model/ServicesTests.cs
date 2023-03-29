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
    public class ServicesTests
    {
        private const int id = 1;
        private const string libelle = "Administratif";

        private static readonly Services services = new Services(id, libelle);

        [TestMethod()]
        public void ServicesTest()
        {
            Assert.AreEqual(id, services.Id, "devrait réussir : id valorisé");
            Assert.AreEqual(libelle, services.Libelle, "devrait réussir : libellé valorisé");
        }
    }
}