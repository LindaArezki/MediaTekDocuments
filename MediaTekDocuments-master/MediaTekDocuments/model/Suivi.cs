
namespace MediaTekDocuments.model
{
    public class Suivi
    {
        public int Idetape { get; }
        public string Libelle { get; }


        public Suivi(int idetape, string libelle)
        {
            this.Idetape = idetape;
            this.Libelle = libelle;
        }

    }
}
