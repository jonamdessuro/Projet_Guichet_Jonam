using System;
using System.Collections.Generic;

namespace Guichet
{
    public class Guichet
    { //Devoir ajouter la condition : si un virement est plus de 1000$, le systeme doit demander le mdp. Aussi, le client n'a seulement que 3 tentatives
        private List<Client> listeClients;
        private decimal soldeGuichet;
        private string adminLogin;
        private string adminPassword;
        private bool etatGuichet;
        private int compteurLoginAdminInvalide = 0;

        // Cette variable permet de savoir si un Client est connecté ou non. Modifié  dans  SeConnecterClient() et FermerSession()
        private Client clientActuel;

        // Cette variable permet de savoir si un Admin est connecté ou non. Utilisé dans SeConnecterAdmin()
        private string adminActuel = "";

        // Le constructeur ci-dessous contient des valeurs par défaut, cela nous évite de réécrire les valeurs dans le Guichet guichet = new Guichet(Clients)
        public Guichet(List<Client> listeClients, decimal soldeGuichet = 10000, string adminLogin = "admin", string adminPassword = "123456")
        {
            this.listeClients = listeClients;
            this.soldeGuichet = soldeGuichet;
            this.adminLogin = adminLogin;
            this.adminPassword = adminPassword;
            this.etatGuichet = true;
        }

        public void runMenu()
        {
            do
            {
                Console.Clear();
                if (this.clientActuel != null)
                    MenuPersonnel();
                else if (this.adminActuel != "")
                    MenuAdmin();
                else
                    MenuPrincipal();
            }
            while (true);
        }

        public void MenuPrincipal()
        {
            Console.WriteLine("Veuillez choisir l'une des options suivantes: ");
            Console.WriteLine("1- Se connecter à votre compte");
            Console.WriteLine("2- Se connecter comme administrateur");
            Console.WriteLine("3- Quitter");
            string ChoixPrincipal = Console.ReadLine();
            ChoixMenuPrincipal(ChoixPrincipal);
        }
        public void ChoixMenuPrincipal(string choix)
        {
            switch (choix)
            {
                case "1":
                    SeConnecterClient();
                    break;

                case "2":
                    SeConnecterAdmin();
                    break;

                case "3":
                    System.Environment.Exit(0);
                    break;
            }
        }

        public void SeConnecterClient()
        {
            if (etatGuichet == false)
            {
                Console.WriteLine("Guichet en panne");
                AppuyerEntrer();
            }
            else
            {
                int compteurLogin = 0;
                Console.WriteLine("Veuillez entrer vos identifiants: ");
                Console.Write("Nom: ");
                string nomClient = Console.ReadLine();
                Console.Write("Mot de passe: ");
                string motDePasseClient = Console.ReadLine();

                foreach (Client compteClient in listeClients)
                {
                    if (nomClient == compteClient.NomClient)
                    {
                        if (motDePasseClient == compteClient.NipClient & compteClient.Actif == true)
                        {
                            // MenuPersonnel();
                            this.clientActuel = compteClient;
                            compteurLogin++;
                            break;
                        }
                        else
                        {
                            compteClient.NombreLoginInvalide++;
                            if (compteClient.NombreLoginInvalide >= 3)
                            {
                                compteClient.VerrouillerCompte();
                                Console.WriteLine("Votre compte est verrouillé.");
                            }
                        }
                    }


                }
                if (compteurLogin == 0)
                {
                    Console.Write("Votre mot de passe ou votre login est incorrect ");
                    AppuyerEntrer();
                }
            }
        }
        public void MenuPersonnel()
        {
            Console.WriteLine("Veuillez choisir l'une des options suivantes: ");
            Console.WriteLine("1- Changer de mot de passe");
            Console.WriteLine("2- Déposer un montant dans un compte");
            Console.WriteLine("3- Retirer un montant dans un compte");
            Console.WriteLine("4- Afficher le solde d'un compte");
            Console.WriteLine("5- Effectuer un virement entre les comptes");
            Console.WriteLine("6- Payer une facture");
            Console.WriteLine("7- Fermer la session");
            string ChoixMenuPerso = Console.ReadLine();
            ChoixMenuPersonnel(ChoixMenuPerso);
        }

        public void ChoixMenuPersonnel(string choix)
        {
            switch (choix)
            {
                case "1":
                    ChangerMotDePasse();
                    break;

                case "2":
                    DeposerMontant();
                    break;

                case "3":
                    RetirerMontant();
                    break;

                case "4":
                    AfficherSoldeCompte();
                    break;

                case "5":
                    EffectuerVirement();
                    break;

                case "6":
                    PayerFacture();
                    break;

                case "7":
                    FermerSession();
                    break;

                default:
                    Console.WriteLine("Veuillez entrer un choix valide");
                    AppuyerEntrer();
                    break;
            }
        }
        public void ChangerMotDePasse()
        {
            Console.Write("Veuillez choisir un nouveau mot de passe: ");
            string newPassword = Console.ReadLine();
            int intNewPassword;
            Console.Write("Veuillez confirmer votre nouveau mot de passe: ");
            string confirmPassword = Console.ReadLine();
            bool parseSuccess = int.TryParse(newPassword, out intNewPassword);

            if (newPassword == confirmPassword)
            {
                if (newPassword.Length > 4 || newPassword.Length < 4)
                {
                    Console.Write("Veuillez entrer un mot de passe de 4 caractères. ");
                }
                else if (parseSuccess)
                {
                    clientActuel.NipClient = newPassword;
                    Console.Write("Mot de passe changé avec succès. ");
                }
                else
                {
                    Console.Write("Votre mot de passe ne peut être composé que de chiffres. ");
                }
            }
            else
            {
                Console.WriteLine("Veuillez entrer deux mots de passe identiques");

            }
            AppuyerEntrer();
        }
        public void DeposerMontant()
        {
            Console.Write("Veuillez entrer le montant à déposer: ");
            decimal decimalChoixMontant;
            string choixMontant = Console.ReadLine();
            bool parseSuccess = decimal.TryParse(choixMontant, out decimalChoixMontant);
            if (parseSuccess)
            {
                if (decimalChoixMontant > 0)
                {
                    string compte = ChoisirCompte();
                    if (compte == "1")
                    {
                        clientActuel.CompteCheque.SoldeCompte = clientActuel.CompteCheque.SoldeCompte + decimalChoixMontant;
                        //  même chose que : clientActuel.CompteCheque.SoldeCompte += decimalChoixMontant;
                        Console.Write("Nouveau solde: ");
                        Console.WriteLine(clientActuel.CompteCheque.SoldeCompte);
                        AppuyerEntrer();
                    }
                    else if (compte == "2")
                    {
                        clientActuel.CompteEpargne.SoldeCompte = clientActuel.CompteEpargne.SoldeCompte + decimalChoixMontant;
                        Console.Write("Nouveau solde: ");
                        Console.WriteLine(clientActuel.CompteEpargne.SoldeCompte);
                        AppuyerEntrer();
                    }
                    else
                    {
                        Console.WriteLine("Veuillez choisir un compte valide");
                        AppuyerEntrer();
                    }
                }
                else
                {
                    Console.WriteLine("Un montant ne peut être inférieur à 0");
                    AppuyerEntrer();
                }
            }
            else
            {
                Console.WriteLine("Veuillez choisir un montant en decimal.");
                AppuyerEntrer();
            }
        }


        public void RetirerMontant()
        {
            Console.WriteLine("Veuillez entrer un montant à retirer: ");
            decimal decimalMontantRetrait;
            string montantRetrait = Console.ReadLine();
            bool parseSuccess = decimal.TryParse(montantRetrait, out decimalMontantRetrait);

            if (parseSuccess)
            {
                if (decimalMontantRetrait > 0)
                {
                    if (soldeGuichet >= decimalMontantRetrait)
                    {
                        string compte = ChoisirCompte();//a suivre

                        if (compte == "1")
                        {
                            if (decimalMontantRetrait < clientActuel.CompteCheque.SoldeCompte)
                            {
                                clientActuel.CompteCheque.SoldeCompte = clientActuel.CompteCheque.SoldeCompte - decimalMontantRetrait;
                                soldeGuichet = soldeGuichet - decimalMontantRetrait;
                                Console.Write("Nouveau solde du compte: ");
                                Console.WriteLine(clientActuel.CompteCheque.SoldeCompte);
                                Console.Write("Nouveau solde du guichet: ");
                                Console.WriteLine(soldeGuichet);
                            }
                            else { Console.WriteLine("Solde du compte insuffisant"); }
                        }
                        else if (compte == "2")
                        {
                            if (decimalMontantRetrait < clientActuel.CompteEpargne.SoldeCompte)
                            {
                                clientActuel.CompteEpargne.SoldeCompte = clientActuel.CompteEpargne.SoldeCompte - decimalMontantRetrait;
                                soldeGuichet = soldeGuichet - decimalMontantRetrait;
                                Console.Write("Nouveau solde du compte: ");
                                Console.WriteLine(clientActuel.CompteEpargne.SoldeCompte);
                                Console.Write("Nouveau solde du guichet: ");
                                Console.WriteLine(soldeGuichet);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Veuillez choisir un compte valide");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Désolé, le guichet ne contient pas de fond suffisants.");
                    }
                }
                else
                {
                    Console.WriteLine("Veuillez entrer un montant supérieur à 0");
                }
            }
            else
            {
                Console.WriteLine("Veuillez entrer un montant en décimal");
            }
            VerifierSoldeGuichet();
            AppuyerEntrer();
        }
        public void VerifierSoldeGuichet()
        {
            if (soldeGuichet == 0)
            {
                etatGuichet = false;
                Console.WriteLine("Solde du guichet : 0. GUICHET EN PANNE.");
            }
        }

        public void AfficherSoldeCompte()
        {
            string compte = ChoisirCompte();
            if (compte == "1")
            {
                Console.Write("Solde du compte: ");
                Console.WriteLine(clientActuel.CompteCheque.SoldeCompte);
            }
            else if (compte == "2")
            {
                Console.Write("Solde du compte: ");
                Console.WriteLine(clientActuel.CompteEpargne.SoldeCompte);
            }
            else
            {
                Console.WriteLine("Choix Invalide");

            }
            AppuyerEntrer();
        }

        public void EffectuerVirement()
        {

            Console.WriteLine("Veuillez choisir le montant à virer: ");
            decimal decimalMontantVire;
            string montantVire = Console.ReadLine();
            bool parseSuccess = decimal.TryParse(montantVire, out decimalMontantVire);
            if (parseSuccess)
            {
                if (decimalMontantVire > 0)
                {
                    if (decimalMontantVire > 1000)
                    {
                        int compteurLogin = 0;
                        Console.WriteLine("Veuillez entrer vos identifiants: ");
                        Console.Write("Nom: ");
                        string nomClient = Console.ReadLine();
                        Console.Write("Mot de passe: ");
                        string motDePasseClient = Console.ReadLine();

                            if (nomClient == clientActuel.NomClient)
                            {
                                if (motDePasseClient == clientActuel.NipClient)
                                {
                                    string compte = ChoisirCompte();
                                    if (compte == "1")
                                    {
                                        if (decimalMontantVire < clientActuel.CompteCheque.SoldeCompte)
                                        {
                                            clientActuel.CompteCheque.SoldeCompte = clientActuel.CompteCheque.SoldeCompte - decimalMontantVire;
                                            clientActuel.CompteEpargne.SoldeCompte = clientActuel.CompteEpargne.SoldeCompte + decimalMontantVire;
                                            Console.WriteLine("Transfert effectué: ");
                                            Console.Write("Nouveau solde du compte chèque: ");
                                            Console.WriteLine(clientActuel.CompteCheque.SoldeCompte);
                                            Console.Write("Nouveau solde du compte épargne: ");
                                            Console.WriteLine(clientActuel.CompteEpargne.SoldeCompte);
                                            AppuyerEntrer();
                                            compteurLogin++;
                                    }
                                        else
                                        {
                                            Console.WriteLine("Fonds insuffisants");
                                            AppuyerEntrer();
                                        }
                                    }
                                    else if (compte == "2")
                                    {
                                        if (decimalMontantVire < clientActuel.CompteEpargne.SoldeCompte)
                                        {
                                            clientActuel.CompteEpargne.SoldeCompte = clientActuel.CompteEpargne.SoldeCompte - decimalMontantVire;
                                            clientActuel.CompteCheque.SoldeCompte = clientActuel.CompteCheque.SoldeCompte + decimalMontantVire;
                                            Console.WriteLine("Transfert effectué: ");
                                            Console.Write("Nouveau solde du compte chèque: ");
                                            Console.WriteLine(clientActuel.CompteCheque.SoldeCompte);
                                            Console.Write("Nouveau solde du compte épargne: ");
                                            Console.WriteLine(clientActuel.CompteEpargne.SoldeCompte);
                                            AppuyerEntrer();
                                            compteurLogin++;
                                    }
                                        else
                                        {
                                            Console.WriteLine("Fonds insuffisants");
                                            AppuyerEntrer();
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Choix de compte erroné");
                                        AppuyerEntrer();
                                    }
                                }
                                else
                                {
                                    int NombreLoginInvalide = 0;
                                    NombreLoginInvalide++;
                                    if (NombreLoginInvalide == 3)
                                    {
                                        //compteClient.VerrouillerCompte();
                                        Console.WriteLine("Votre compte est verrouillé.");
                                        AppuyerEntrer();
                                    }
                                }
                            }


                        
                        if (compteurLogin == 0)
                        {
                            Console.Write("Votre mot de passe ou votre login est incorrect ");
                            AppuyerEntrer();
                        }
                    }
                    else
                    {
                        string compte = ChoisirCompte();
                        if (compte == "1")
                        {
                            if (decimalMontantVire < clientActuel.CompteCheque.SoldeCompte)
                            {
                                clientActuel.CompteCheque.SoldeCompte = clientActuel.CompteCheque.SoldeCompte - decimalMontantVire;
                                clientActuel.CompteEpargne.SoldeCompte = clientActuel.CompteEpargne.SoldeCompte + decimalMontantVire;
                                Console.WriteLine("Transfert effectué: ");
                                Console.Write("Nouveau solde du compte chèque: ");
                                Console.WriteLine(clientActuel.CompteCheque.SoldeCompte);
                                Console.Write("Nouveau solde du compte épargne: ");
                                Console.WriteLine(clientActuel.CompteEpargne.SoldeCompte);
                                AppuyerEntrer();
                            }
                            else
                            {
                                Console.WriteLine("Fonds insuffisants");
                                AppuyerEntrer();
                            }
                        }
                        else if (compte == "2")
                        {
                            if (decimalMontantVire < clientActuel.CompteEpargne.SoldeCompte)
                            {
                                clientActuel.CompteEpargne.SoldeCompte = clientActuel.CompteEpargne.SoldeCompte - decimalMontantVire;
                                clientActuel.CompteCheque.SoldeCompte = clientActuel.CompteCheque.SoldeCompte + decimalMontantVire;
                                Console.WriteLine("Transfert effectué: ");
                                Console.Write("Nouveau solde du compte chèque: ");
                                Console.WriteLine(clientActuel.CompteCheque.SoldeCompte);
                                Console.Write("Nouveau solde du compte épargne: ");
                                Console.WriteLine(clientActuel.CompteEpargne.SoldeCompte);
                                AppuyerEntrer();
                            }
                            else
                            {
                                Console.WriteLine("Fonds insuffisants");
                                AppuyerEntrer();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Choix de compte erroné");
                            AppuyerEntrer();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Seul les virements strictement positifs sont acceptés");
                    AppuyerEntrer();
                }
            }
            else
            {
                Console.WriteLine("Veuillez entrer des nombres décimaux uniquement.");
                AppuyerEntrer();
            }


        }

        public void PayerFacture()
        {
            Console.WriteLine("Veuillez choisir un fournisseur: ");
            Console.WriteLine("1- Amazon");
            Console.WriteLine("2- Bell");
            Console.WriteLine("3- Videotron");
            string choixFournisseur = Console.ReadLine();

            Console.WriteLine("Veuillez entrer le montant de la facture: ");
            decimal decimalMontantFacture;
            string montantFacture = Console.ReadLine();
            bool parseSuccess = decimal.TryParse(montantFacture, out decimalMontantFacture);

            if (parseSuccess)
            {

                if (decimalMontantFacture > 0)
                {
                    string compte = ChoisirCompte();//a suivre

                    if (compte == "1")
                    {
                        if (decimalMontantFacture + 2 < clientActuel.CompteCheque.SoldeCompte)
                        {
                            clientActuel.CompteCheque.SoldeCompte = clientActuel.CompteCheque.SoldeCompte - decimalMontantFacture - 2;
                            Console.WriteLine("Facture payée avec succès, des frais de 2$ se sont appliqués");
                            Console.Write("Nouveau solde du compte: ");
                            Console.WriteLine(clientActuel.CompteCheque.SoldeCompte);
                        }
                        else
                        {
                            Console.WriteLine("Solde du compte insuffisant");
                        }
                    }
                    else if (compte == "2")
                    {
                        if (decimalMontantFacture + 2 < clientActuel.CompteEpargne.SoldeCompte)
                        {
                            clientActuel.CompteEpargne.SoldeCompte = clientActuel.CompteEpargne.SoldeCompte - decimalMontantFacture - 2;
                            Console.WriteLine("Facture payée avec succès, des frais de 2$ se sont appliqués");
                            Console.Write("Nouveau solde du compte: ");
                            Console.WriteLine(clientActuel.CompteEpargne.SoldeCompte);
                        }
                        else
                        {
                            Console.WriteLine("Solde du compte insuffisant");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Veuillez choisir un compte valide");
                    }
                }
                else
                {
                    Console.WriteLine("Désolé, une facture ne peut être négative.");
                }
            }
            else
            {
                Console.WriteLine("Veuillez entrer un montant en décimal");
            }
            AppuyerEntrer();
        }

        public void FermerSession()
        {
            clientActuel = null;
        }

        public void SeConnecterAdmin()
        {
            string nomAdmin;
            string motDePasseAdmin;

            Console.WriteLine("Veuillez entrer vos identifiants: ");
            Console.Write("Nom: ");
            nomAdmin = Console.ReadLine();
            Console.Write("Mot de passe: ");
            motDePasseAdmin = Console.ReadLine();

            if ((nomAdmin == adminLogin) & (motDePasseAdmin == adminPassword))
            {
                this.adminActuel = nomAdmin;
                this.compteurLoginAdminInvalide = 0;
            }
            else
            {
                Console.WriteLine("Votre mot de passe ou votre login est incorrect.");
                AppuyerEntrer();
                this.compteurLoginAdminInvalide++;
            }

            if (this.compteurLoginAdminInvalide == 3)
            {
                Console.WriteLine("TROP DE TENTATIVES, SYSTÈME EN PANNE");
                MettreEnPanne();
            }
        }

        public void MenuAdmin()
        {
            Console.WriteLine("Veuillez choisir l'une des options suivantes: ");
            Console.WriteLine("1- Remettre le guichet en fonction");
            Console.WriteLine("2- Déposer de l'argent dans le guichet");
            Console.WriteLine("3- Afficher la liste des comptes");
            Console.WriteLine("4- Retourner au menu principal"); //On peut réutiliser la fonction MenuPrincipal();
            string ChoixAdmin = Console.ReadLine();
            ChoixMenuAdmin(ChoixAdmin);
        }

        public void ChoixMenuAdmin(string choix)
        {
            switch (choix)
            {
                case "1":
                    RemettreGuichetFonction();
                    break;

                case "2":
                    DeposerGuichet();
                    break;

                case "3":
                    AfficherListeCompte();
                    break;

                case "4":
                    this.adminActuel = "";
                    break;

                default:
                    Console.WriteLine("Veuillez entrer un choix valide");
                    break;
            }
        }


        public void RemettreGuichetFonction()
        {
            Console.WriteLine("Voulez-vous remettre le guichet en fonction ? (O/N)");
            string choixFonction = Console.ReadLine();
            if (choixFonction == "O")
            {
                Console.WriteLine("Guichet remit en fonction avec succès");
                this.etatGuichet = true;
            }
            else if (choixFonction == "N")
            {
                Console.WriteLine("Le guichet reste en panne");
            }
            AppuyerEntrer();
        }
        public void MettreEnPanne()
        {
            this.etatGuichet = false;
            System.Environment.Exit(0);
        }

        public void DeposerGuichet()
        {
            Console.WriteLine("Veuillez entrer le montant à déposer: ");
            decimal decimalMontantDepot;
            string montantDepot = Console.ReadLine();
            bool parseSuccess = decimal.TryParse(montantDepot, out decimalMontantDepot);
            if (parseSuccess)
            {
                if (decimalMontantDepot <= 10000)
                {
                    soldeGuichet = soldeGuichet + decimalMontantDepot;
                    Console.WriteLine("Dépôt effectué dans le guichet avec succès.");
                    Console.Write("Nouveau solde du guichet:");
                    Console.WriteLine(soldeGuichet);
                }
                else
                {
                    Console.WriteLine("Dépôt refusé: Les dépôts sont d'un maximum de 10 000$");
                }
            }
            else
            {
                Console.WriteLine("Veuillez entrer un montant décimal");
            }
            AppuyerEntrer();
        }

        public void AfficherListeCompte()
        {
            foreach (Client compteClient in listeClients)
            {
                Console.Write("Nom du client: ");
                Console.WriteLine(compteClient.NomClient);
                Console.Write("Numero du compte chèque: ");
                Console.WriteLine(compteClient.CompteCheque.Numerocompte);
                Console.Write("Solde du compte chèque: ");
                Console.WriteLine(compteClient.CompteCheque.SoldeCompte);
                Console.Write("Numero du compte Épargne: ");
                Console.WriteLine(compteClient.CompteEpargne.Numerocompte);
                Console.Write("Solde du compte Épargne: ");
                Console.WriteLine(compteClient.CompteEpargne.SoldeCompte);
                Console.Write("État du compte: ");
                if (compteClient.Actif == true)
                { Console.WriteLine("Compte actif"); }
                else { Console.WriteLine("Compte Verrouillé"); }
                Console.WriteLine("");
            }
            AppuyerEntrer();
        }

        public string ChoisirCompte()
        {
            Console.WriteLine("Dans quel compte voulez-vous effectuer l'action ?");
            Console.WriteLine("1- Compte chèque");
            Console.WriteLine("2- Compte épargne");
            string choixCompte = Console.ReadLine();
            return choixCompte;
        }

        public void AppuyerEntrer()
        {
            Console.WriteLine("Appuyer sur entrer pour continuer");
            Console.ReadLine();
        }

    }
}
