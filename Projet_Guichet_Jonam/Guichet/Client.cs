namespace Guichet
{
    public class Client
    {
        private string nomClient;
        private string nipClient;
        private bool actif;
        private CompteClient compteEpargne;
        private CompteClient compteCheque;
        private int nombreLoginInvalide;


        public Client(string nomClient, string nipClient, CompteClient compteEpargne, CompteClient compteCheque)
        {
            this.nomClient = nomClient;
            this.nipClient = nipClient;
            this.actif = true;
            this.CompteEpargne = compteEpargne;
            this.CompteCheque = compteCheque;
            this.NombreLoginInvalide = 0;
        }

        public string NomClient { get => nomClient; set => nomClient = value; }
        public string NipClient { get => nipClient; set => nipClient = value; }
        public int NombreLoginInvalide { get => nombreLoginInvalide; set => nombreLoginInvalide = value; }

        public void VerrouillerCompte()
        {
            this.actif = false;
        }

        public void DeverrouillerCompte()
        {
            this.actif = true;
        }

        public bool IsCompteVerrouille()
        {
            return this.actif;
        }
        public bool Actif { get => actif; set => actif = value; }
        public CompteClient CompteEpargne { get => compteEpargne; set => compteEpargne = value; }
        public CompteClient CompteCheque { get => compteCheque; set => compteCheque = value; }

    }
}
