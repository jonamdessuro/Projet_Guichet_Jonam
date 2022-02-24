namespace Guichet
{
    class CompteCheque : CompteClient
    {
        public CompteCheque(string numerocompte, decimal soldeCompte) : base(numerocompte, soldeCompte)
        {
            this.Numerocompte = numerocompte;
            this.SoldeCompte = soldeCompte;
        }
    }
}
