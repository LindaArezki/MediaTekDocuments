using System;

namespace MediaTekDocuments.model
{
    public class CommandesDocument : Commande

    {
        public int NbExemplaire { get; }
        public string IdLivreDvd { get; }
        public int Suivi { get; }
        public string Libelle { get; }

        

        public CommandesDocument(string id, DateTime dateCommande, double montant, int nbExemplaire, string idLivreDvd, int suivi, string libelle ) 
            : base(id, dateCommande, montant)
        {
            this.NbExemplaire = nbExemplaire;
            this.IdLivreDvd = idLivreDvd;
            this.Suivi = suivi;
            this.Libelle = libelle;
            
        }

      
    }
}
