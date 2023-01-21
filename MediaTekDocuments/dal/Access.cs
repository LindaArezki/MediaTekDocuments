using System;
using System.Collections.Generic;
using MediaTekDocuments.model;
using MediaTekDocuments.manager;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Configuration;

namespace MediaTekDocuments.dal
{
    /// <summary>
    /// Classe d'accès aux données
    /// </summary>
    public class Access
    {
        /// <summary>
        /// adresse de l'API
        /// </summary>
        private static readonly string uriApi = "http://localhost/rest_mediatekdocuments/";
        /// <summary>
        /// instance unique de la classe
        /// </summary>
        private static Access instance = null;
        /// <summary>
        /// instance de ApiRest pour envoyer des demandes vers l'api et recevoir la réponse
        /// </summary>
        private readonly ApiRest api = null;
        /// <summary>
        /// méthode HTTP pour select
        /// </summary>
        private const string GET = "GET";
        /// <summary>
        /// méthode HTTP pour insert
        /// </summary>
        private const string POST = "POST";
        /// <summary>
        /// méthode HTTP pour update
        private const string PUT = "PUT";
        /// <summary>
        /// méthode HTTP pour delete
        private const string DELETE = "DELETE";
        /// <summary>
        /// Méthode privée pour créer un singleton
        /// initialise l'accès à l'API
        /// </summary>
        private Access()
        {
            String authenticationString;
            try
            {
                authenticationString = "admin:adminpwd";
                api = ApiRest.GetInstance(uriApi, authenticationString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Création et retour de l'instance unique de la classe
        /// </summary>
        /// <returns>instance unique de la classe</returns>
        public static Access GetInstance()
        {
            if(instance == null)
            {
                instance = new Access();
            }
            return instance;
        }

        /// <summary>
        /// Retourne tous les genres à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Genre</returns>
        public List<Categorie> GetAllGenres()
        {
            IEnumerable<Genre> lesGenres = TraitementRecup<Genre>(GET, "genre");
            return new List<Categorie>(lesGenres);
        }

        /// <summary>
        /// Retourne tous les rayons à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Rayon</returns>
        public List<Categorie> GetAllRayons()
        {
            IEnumerable<Rayon> lesRayons = TraitementRecup<Rayon>(GET, "rayon");
            return new List<Categorie>(lesRayons);
        }

        /// <summary>
        /// Retourne toutes les catégories de public à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Public</returns>
        public List<Categorie> GetAllPublics()
        {
            IEnumerable<Public> lesPublics = TraitementRecup<Public>(GET, "public");
            return new List<Categorie>(lesPublics);
        }

        /// <summary>
        /// Retourne tous les livres à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Livre</returns>
        public List<Livre> GetAllLivres()
        {
            List<Livre> lesLivres = TraitementRecup<Livre>(GET, "livre");
            return lesLivres;
        }

        /// <summary>
        /// Retourne tous les dvd à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Dvd</returns>
        public List<Dvd> GetAllDvd()
        {
            List<Dvd> lesDvd = TraitementRecup<Dvd>(GET, "dvd");
            return lesDvd;
        }

        /// <summary>
        /// Retourne toutes les revues à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Revue</returns>
        public List<Revue> GetAllRevues()
        {
            List<Revue> lesRevues = TraitementRecup<Revue>(GET, "revue");
            return lesRevues;
        }

        /// <summary>
        /// Retourne tous les suivis à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Revue</returns>
        public List<Suivi> GetAllSuivis()
        {
            List<Suivi> lesSuivis = TraitementRecup<Suivi>(GET, "suivi");
            return lesSuivis;
        }


        /// <summary>
        /// Retourne les exemplaires d'une revue
        /// </summary>
        /// <param name="idDocument">id de la revue concernée</param>
        /// <returns>Liste d'objets Exemplaire</returns>
        public List<Exemplaire> GetExemplairesRevue(string idDocument)
        {
            List<Exemplaire> lesExemplaires = TraitementRecup<Exemplaire>(GET, "exemplaire/" + idDocument);
            return lesExemplaires;
        }

        /// <summary>
        /// ecriture d'un exemplaire en base de données
        /// </summary>
        /// <param name="exemplaire">exemplaire à insérer</param>
        /// <returns>true si l'insertion a pu se faire (retour != null)</returns>
        public bool CreerExemplaire(Exemplaire exemplaire)
        {
            String jsonExemplaire = JsonConvert.SerializeObject(exemplaire, new CustomDateTimeConverter());
            try {
                // récupération soit d'une liste vide (requête ok) soit de null (erreur)
                List<Exemplaire> liste = TraitementRecup<Exemplaire>(POST, "exemplaire/" + jsonExemplaire);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false; 
        }

        /// <summary>
        /// Retourne les commandes d'un document
        /// </summary>
        /// <param name="idDocument">id du document concernée</param>
        /// <returns>Liste d'objets CommandesDocument</returns>
        public List<CommandesDocument> GetCommandesDocument(string idDocument)
        {
            List<CommandesDocument> lescommandesdocument = TraitementRecup<CommandesDocument>(GET, "commandedocument/" + idDocument);
            return lescommandesdocument;
        }

        /// <summary>
        /// Retourne les abonnements d'un document
        /// </summary>
        /// <param name="idDocument">id du document concernée</param>
        /// <returns>Liste d'objets CommandesDocument</returns>
        public List<Abonnement> GetAbonnement(string idDocument)
        {
            List<Abonnement> lescommandesabonnements = TraitementRecup<Abonnement>(GET, "abonnement/" + idDocument);
            return lescommandesabonnements;
           
        }

        /// <summary>
        /// Retourne tous les suivis à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Revue</returns>
        public List<AbonnementFin> GetAbonnementFin()
        {
            List<AbonnementFin> lesabonnements = TraitementRecup<AbonnementFin>(GET, "abonnement");
            return lesabonnements;
        }

        
        /// <summary>
        /// ecriture d'une commande en base de données
        /// </summary>
        /// <param name="commande">commande à insérer</param>
        /// <returns>true si l'insertion a pu se faire (retour != null)</returns>
        public bool CreerCommandes(Commande commande)
        {
            String jsonCommande = JsonConvert.SerializeObject(commande, new CustomDateTimeConverter());
            try
            {
                // récupération soit d'une liste vide (requête ok) soit de null (erreur)
                List<Commande> liste = TraitementRecup<Commande>(POST, "commande/" + jsonCommande);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// ecriture d'une commandedocument en base de données
        /// </summary>
        /// <param name="id">commande à insérer</param>
        /// <param name="nbExemplaire"> le nombre d'exemplaire à insérer</param>
        /// <param name="idLivreDvd">l'id du document à insérer</param>
        /// <param name="suivi">le suivi à insérer</param>
        /// <returns>true si l'insertion a pu se faire (retour != null)</returns>
        public bool CreerCommandesDocument(string id, int nbExemplaire, string idLivreDvd, int suivi)
        {
            String jsonCommandeDocument ="{ \"id\" : " + id + ", \"nbExemplaire\" : " + nbExemplaire + ", \"idLivreDvd\" : \""  + idLivreDvd + "\""+", \"suivi\" : " + suivi + "}";
            try
            {
                // récupération soit d'une liste vide (requête ok) soit de null (erreur)
                List<CommandesDocument> liste = TraitementRecup<CommandesDocument>(POST, "commandedocument/" + jsonCommandeDocument);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }


        /// <summary>
        /// ecriture d'une commande de revue en base de données
        /// </summary>
        /// <param name="id">l'id du document à insérer</param>
        /// <param name="dateFinAbonnement">l'id du document à insérer</param>
        /// <param name="idRevue">le suivi à insérer</param>
        /// <returns>true si l'insertion a pu se faire (retour != null)</returns>
        public bool CreerCommandesRevue(string id , DateTime dateFinAbonnement, string idRevue)
        {
            String jsondateCommande = JsonConvert.SerializeObject(dateFinAbonnement, new CustomDateTimeConverter());
            String jsonabonnement = "{  \"id\" : " + id + ", \"dateFinabonnement\" : " + jsondateCommande + ", \"idRevue\" :  \"" + idRevue + "\"" + "}";
 
            try
            {
                // récupération soit d'une liste vide (requête ok) soit de null (erreur)
                List<CommandesDocument> liste = TraitementRecup<CommandesDocument>(POST, "abonnement/" + jsonabonnement);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
       
        /// <summary>
        /// Supression d'une commande en base de données
        /// </summary>
        /// <param name="Id">commande à supprimer</param>
        /// <returns>true si l'insertion a pu se faire (retour != null)</returns>
        public bool SupprimerCommandes(string Id)
        {
            String jsonIdCommande = "{ \"id\" : "+Id+"}";
            try
            {
                // récupération soit d'une liste vide (requête ok) soit de null (erreur)
                List<Commande> liste = TraitementRecup<Commande>(DELETE, "commande/" + jsonIdCommande);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }


        /// <summary>
        /// Récupération de l'utilisateur correspondant au login
        /// </summary>
        /// <param name="login">login de l'utiisateur à chercher</param>
        /// <returns>l'utilisateur trouvé, ou null</returns>
        public Utilisateur GetAuthentification(string login)
        {
            try
            {
                List<Utilisateur> liste = TraitementRecup<Utilisateur>(GET, "utilisateur/" + login);
                if (liste==null || liste.Count == 0)
                {
                    return null;
                }
                // retourne l'utilisateur trouvé
                return (liste[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        /// <summary>
        /// Modification d'une commande en base de données
        /// </summary>
        /// <param name="Id">commande à modifier</param>
        /// <param name="nbExemplaire"> le nombre d'exemplaire à insérer</param>
        /// <param name="idLivreDvd">l'id du document à insérer</param>
        /// <param name="suivi">le suivi à modifie</param>
        /// <returns>true si l'insertion a pu se faire (retour != null)</returns>
        public bool ModifierCommandesDocument( string Id, int nbExemplaire, string idLivreDvd, int suivi)
        {
            String jsonCommandeDocument = "{ \"id\" : " + Id + " , \"nbExemplaire\" : " + nbExemplaire + ", \"idLivreDvd\" : \"" + idLivreDvd + "\"" + ", \"suivi\" : " + suivi + " }";
            try
            {
                // récupération soit d'une liste vide (requête ok) soit de null (erreur)
                List<CommandesDocument> liste = TraitementRecup<CommandesDocument>(PUT, "commandedocument/" + Id + "/" + jsonCommandeDocument);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Traitement de la récupération du retour de l'api, avec conversion du json en liste pour les select (GET)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methode">verbe HTTP (GET, POST, PUT, DELETE)</param>
        /// <param name="message">information envoyée</param>
        /// <returns>liste d'objets récupérés (ou liste vide)</returns>
        private List<T> TraitementRecup<T> (String methode, String message)
        {
            List<T> liste = new List<T>();
            try
            {
                JObject retour = api.RecupDistant(methode, message);
                // extraction du code retourné
                String code = (String)retour["code"];
                if (code.Equals("200"))
                {
                    // dans le cas du GET (select), récupération de la liste d'objets
                    if (methode.Equals(GET))
                    {
                        String resultString = JsonConvert.SerializeObject(retour["result"]);
                        // construction de la liste d'objets à partir du retour de l'api
                        liste = JsonConvert.DeserializeObject<List<T>>(resultString, new CustomBooleanJsonConverter());
                    }
                }
                else
                {
                    Console.WriteLine("code erreur = " + code + " message = " + (String)retour["message"]);
                }
            }catch(Exception e)
            {
                Console.WriteLine("Erreur lors de l'accès à l'API : "+e.Message);
                //Environment.Exit(0);
            }
            return liste;
        }

        /// <summary>
        /// Modification du convertisseur Json pour gérer le format de date
        /// </summary>
        private sealed class CustomDateTimeConverter : IsoDateTimeConverter
        {
            public CustomDateTimeConverter()
            {
                base.DateTimeFormat = "yyyy-MM-dd";
            }
        }

        /// <summary>
        /// Modification du convertisseur Json pour prendre en compte les booléens
        /// classe trouvée sur le site :
        /// https://www.thecodebuzz.com/newtonsoft-jsonreaderexception-could-not-convert-string-to-boolean/
        /// </summary>
        private sealed class CustomBooleanJsonConverter : JsonConverter<bool>
        {
            public override bool ReadJson(JsonReader reader, Type objectType, bool existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                return Convert.ToBoolean(reader.ValueType == typeof(string) ? Convert.ToByte(reader.Value) : reader.Value);
            }

            public override void WriteJson(JsonWriter writer, bool value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }

    }
}
