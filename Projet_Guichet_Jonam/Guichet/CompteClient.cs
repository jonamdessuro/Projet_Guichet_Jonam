namespace Guichet
{
    public abstract class CompteClient
    {
        private string numerocompte;
        private decimal soldeCompte;

        public string Numerocompte { get => numerocompte; set => numerocompte = value; }
        public decimal SoldeCompte { get => soldeCompte; set => soldeCompte = value; }
        public CompteClient(string numerocompte, decimal soldeCompte)
        {

        }
        // Ajouter fonctions qui permet de retirer, déposer virer pour simplifier les fonctions du guichet (DeposerMontant, RetirerMontant et Virer)

    }
}
