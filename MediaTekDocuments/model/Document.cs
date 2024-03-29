﻿
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Document (réunit les infomations communes à tous les documents : Livre, Revue, Dvd)
    /// </summary>
    public class Document
    {
        /// <summary>
        /// id du document
        /// </summary>
        public string Id { get; }
        /// <summary>
        /// titre du document
        /// </summary>
        public string Titre { get; }
        /// <summary>
        /// Image du document
        /// </summary>
        public string Image { get; }
        /// <summary>
        /// Id du genre du document
        /// </summary>
        public string IdGenre { get; }
        /// <summary>
        /// genre du document
        /// </summary>
        public string Genre { get; }
        /// <summary>
        /// Id du public du document
        /// </summary>
        public string IdPublic { get; }
        /// <summary>
        /// public du document
        /// </summary>
        public string Public { get; }
        /// <summary>
        /// Id du rayon du document
        /// </summary>
        public string IdRayon { get; }
        /// <summary>
        /// rayon  du document
        /// </summary>
        public string Rayon { get; }
      
        /// <summary>
        /// Composants de la classe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="titre"></param>
        /// <param name="image"></param>
        /// <param name="idGenre"></param>
        /// <param name="genre"></param>
        /// <param name="idPublic"></param>
        /// <param name="lePublic"></param>
        /// <param name="idRayon"></param>
        /// <param name="rayon"></param>
#pragma warning disable S107 // Methods should not have too many parameters
        public Document(string id, string titre, string image, string idGenre, string genre, string idPublic, string lePublic, string idRayon, string rayon)
#pragma warning restore S107 // Methods should not have too many parameters
        {
            Id = id;
            Titre = titre;
            Image = image;
            IdGenre = idGenre;
            Genre = genre;
            IdPublic = idPublic;
            Public = lePublic;
            IdRayon = idRayon;
            Rayon = rayon;
        }
    }
}
