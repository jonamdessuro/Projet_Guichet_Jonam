using System.Collections.Generic;

namespace Guichet
{
    class Controller
    {
        static void Main(string[] args)
        {
            //une fonction static permet d'utiliser une fonction sans avoir à instancier un objet (pas besoin de mettre Chien myDog = new Chien(); -> myDog.fonction, on peut faire directement Chien.fonction)
            CompteEpargne compteEpargneJonam = new CompteEpargne("00000001", 400);
            CompteCheque compteChequeJonam = new CompteCheque("00000001", 401);
            Client Jonam = new Client("JonamDes", "1234", compteEpargneJonam, compteChequeJonam);

            CompteEpargne compteEpargneFirdaous = new CompteEpargne("00000002", 800);
            CompteCheque compteChequeFirdaous = new CompteCheque("00000002", 801);
            Client Firdaous = new Client("Firdaous", "4321", compteEpargneFirdaous, compteChequeFirdaous);

            CompteEpargne compteEpargnePaul = new CompteEpargne("00000003", 600);
            CompteCheque compteChequePaul = new CompteCheque("00000003", 601);
            Client Paul = new Client("PaulFaye", "1122", compteEpargnePaul, compteChequePaul);

            CompteEpargne compteEpargneFelipe = new CompteEpargne("00000004", 700);
            CompteCheque compteChequeFelipe = new CompteCheque("00000004", 701);
            Client Felipe = new Client("FelipeMo", "3344", compteEpargneFelipe, compteChequeFelipe);

            CompteEpargne compteEpargnePatrick = new CompteEpargne("00000005", 500);
            CompteCheque compteChequePatrick = new CompteCheque("00000005", 501);
            Client Patrick = new Client("PatrickR", "0123", compteEpargnePatrick, compteChequePatrick);



            List<Client> Clients = new List<Client>();
            Clients.Add(Jonam);
            Clients.Add(Firdaous);
            Clients.Add(Paul);
            Clients.Add(Felipe);
            Clients.Add(Patrick);


            Guichet guichet = new Guichet(Clients);
            guichet.runMenu();

        }
    }
}
