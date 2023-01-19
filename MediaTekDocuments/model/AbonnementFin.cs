using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    public class AbonnementFin
    {

        public DateTime DateFinAbonnement { get; }
        public string IdRevue { get; }
        public string Titre { get;  }


        public AbonnementFin( DateTime dateFinAbonnement, string idRevue, string titre) 
        {
            this.DateFinAbonnement = dateFinAbonnement;
            this.IdRevue = idRevue;
            this.Titre= titre;
            
        }
    }
}
