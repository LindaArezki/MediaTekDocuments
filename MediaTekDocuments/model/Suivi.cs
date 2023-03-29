
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier du Suivi
    /// </summary>
    public class Suivi
    {
        /// <summary>
        /// Id de l'étape
        /// </summary>
        public int Idetape { get; }
        /// <summary>
        /// libelle du suivi
        /// </summary>
        public string Libelle { get; }

        /// <summary>
        /// Composants de la classe
        /// </summary>
        /// <param name="idetape"></param>
        /// <param name="libelle"></param>
        public Suivi(int idetape, string libelle)
        {
            this.Idetape = idetape;
            this.Libelle = libelle;
        }

    }
}
