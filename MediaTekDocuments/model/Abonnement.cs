﻿using System;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe Abonnement hérite de commande
    /// </summary>
    public class Abonnement : Commande
    {
       /// <summary>
       /// Date de la fin de l'abonnement
       /// </summary>
        public DateTime DateFinAbonnement { get; }
        /// <summary>
        /// L'id de la revue
        /// </summary>
        public string IdRevue { get; }

        /// <summary>
        /// Les composants de la classe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dateCommande"></param>
        /// <param name="montant"></param>
        /// <param name="dateFinAbonnement"></param>
        /// <param name="idRevue"></param>
        public Abonnement(string id, DateTime dateCommande, double montant, DateTime dateFinAbonnement, string idRevue) : base(id, dateCommande, montant)
        {
            this.DateFinAbonnement = dateFinAbonnement;
            this.IdRevue = idRevue;
      
        }

    }
}
