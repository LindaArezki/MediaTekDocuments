using System;
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Commande (réunit les commandes communes à tous les documents : Livre, Revue, Dvd)
    /// </summary>
     public class Commande
    {
        public string Id { get; }
        public DateTime DateCommande { get; }
        public double Montant{ get; }


        public Commande(string id, DateTime dateCommande,double montant)
        {
            this.Id = id;
            this.DateCommande = dateCommande;
            this.Montant = montant;
        }
    }
}
