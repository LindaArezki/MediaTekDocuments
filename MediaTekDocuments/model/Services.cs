using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    public class Services
    {
        public int Id { get; }
        public string Libelle { get; }


        public Services(int id, string libelle)
        {
            this.Id = id;
            this.Libelle = libelle;
        }

    }
}
