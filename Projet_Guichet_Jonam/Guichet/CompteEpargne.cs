namespace Guichet
{
    class CompteEpargne : CompteClient
    {
        public CompteEpargne(string numerocompte, decimal soldeCompte) : base(numerocompte, soldeCompte)
        {
            this.Numerocompte = numerocompte;
            this.SoldeCompte = soldeCompte;
        }
    }
}
