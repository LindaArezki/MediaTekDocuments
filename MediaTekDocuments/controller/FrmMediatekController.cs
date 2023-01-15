using System.Collections.Generic;
using MediaTekDocuments.model;
using MediaTekDocuments.dal;

namespace MediaTekDocuments.controller
{
    /// <summary>
    /// Contrôleur lié à FrmMediatek
    /// </summary>
    class FrmMediatekController
    {
        /// <summary>
        /// Objet d'accès aux données
        /// </summary>
        private readonly Access access;

        /// <summary>
        /// Récupération de l'instance unique d'accès aux données
        /// </summary>
        public FrmMediatekController()
        {
            access = Access.GetInstance();
        }

        /// <summary>
        /// getter sur la liste des genres
        /// </summary>
        /// <returns>Liste d'objets Genre</returns>
        public List<Categorie> GetAllGenres()
        {
            return access.GetAllGenres();
        }

        /// <summary>
        /// getter sur la liste des livres
        /// </summary>
        /// <returns>Liste d'objets Livre</returns>
        public List<Livre> GetAllLivres()
        {
            return access.GetAllLivres();
        }

        /// <summary>
        /// getter sur la liste des Dvd
        /// </summary>
        /// <returns>Liste d'objets dvd</returns>
        public List<Dvd> GetAllDvd()
        {
            return access.GetAllDvd();
        }

        /// <summary>
        /// getter sur la liste des revues
        /// </summary>
        /// <returns>Liste d'objets Revue</returns>
        public List<Revue> GetAllRevues()
        {
            return access.GetAllRevues();
        }

        /// <summary>
        /// getter sur les rayons
        /// </summary>
        /// <returns>Liste d'objets Rayon</returns>
        public List<Categorie> GetAllRayons()
        {
            return access.GetAllRayons();
        }

        /// <summary>
        /// getter sur les publics
        /// </summary>
        /// <returns>Liste d'objets Public</returns>
        public List<Categorie> GetAllPublics()
        {
            return access.GetAllPublics();
        }

        /// <summary>
        /// getter sur la liste des Suivis
        /// </summary>
        /// <returns>Liste d'objets suivi</returns>
        public List<Suivi> GetAllSuivis()
        {
            return access.GetAllSuivis();
        }

        /// <summary>
        /// récupère les exemplaires d'une revue
        /// </summary>
        /// <param name="idDocument">id de la revue concernée</param>
        /// <returns>Liste d'objets Exemplaire</returns>
        public List<Exemplaire> GetExemplairesRevue(string idDocument)
        {
            return access.GetExemplairesRevue(idDocument);
        }

        /// <summary>
        /// Crée un exemplaire d'une revue dans la bdd
        /// </summary>
        /// <param name="exemplaire">L'objet Exemplaire concerné</param>
        /// <returns>True si la création a pu se faire</returns>
        public bool CreerExemplaire(Exemplaire exemplaire)
        {
            return access.CreerExemplaire(exemplaire);
        }


        /// <summary>
        /// récupère les commandes d'un document
        /// </summary
        /// <param name="idDocument">id du document concerné</param>
        /// <returns>Liste d'objets Commandesdocument</returns>
        public List<CommandesDocument> GetCommandesDocument(string idDocument)
        {
            return access.GetCommandesDocument(idDocument);
        }

        /// <summary>
        /// Crée un exemplaire d'une revue dans la bdd
        /// </summary>
        /// <param name="Id">L'objet Commandedocuments concerné</param>
        /// <param name="NbExemplaire">L'objet Commandedocuments concerné</param>
        /// <param name="IdLivreDvd">L'objet Commandedocuments concerné</param>
        /// <param name="Suivi">L'objet Commandedocuments concerné</param>
        /// <returns>True si la création a pu se faire</returns>
        public bool CreerCommandeDocuments(string Id, int NbExemplaire, string IdLivreDvd, int Suivi)
        {
            return access.CreerCommandesDocument( Id,  NbExemplaire,  IdLivreDvd,  Suivi);
        }

        /// <summary>
        /// Crée un exemplaire d'une revue dans la bdd
        /// </summary>
        /// <param name="commande">L'objet Commande concerné</param>
        /// <returns>True si la création a pu se faire</returns>
        public bool CreerCommandes(Commande commande)
        {
            return access.CreerCommandes(commande);
        }

        /// <summary>
        /// supprime une commande 
        /// </summary
        /// <param name="Id">id du document concerné</param>
        /// <returns>Liste d'objets Commandesdocument</returns>
        public bool SupprimerCommande(string Id)
        {
            
            return access.SupprimerCommandesDocument(Id);
        }

        /// <summary>
        /// modifie une commande 
        /// </summary
        /// <param name="Id">le suivi à modifier</param>
        /// <param name="nbExemplaire"> le nombre d'exemplaire à insérer</param>
        /// <param name="idLivreDvd">l'id du document à insérer</param>
        /// <param name="suivi">commande à modifier</param>
        /// <returns>Liste d'objets Commandesdocument</returns>
        public bool ModifierCommande(string Id, int nbExemplaire, string idLivreDvd, int suivi)
        {

            return access.ModifierCommandesDocument(Id,nbExemplaire,idLivreDvd,suivi);
        }





    }
}
