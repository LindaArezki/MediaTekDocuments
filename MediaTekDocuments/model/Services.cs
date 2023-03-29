using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier des services
    /// </summary>
    public class Services
    {
        /// <summary>
        /// id du service
        /// </summary>
        public int Id { get; }
        /// <summary>
        /// libelle du service
        /// </summary>
        public string Libelle { get; }

        /// <summary>
        /// Composants de la classe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="libelle"></param>
        public Services(int id, string libelle)
        {
            this.Id = id;
            this.Libelle = libelle;
        }

    }
}
