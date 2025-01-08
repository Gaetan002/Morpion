using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Text;
using System.Xml.Linq;

namespace Morpion
{
    class Program
    {
        public static int[,] grille = new int[3, 3]; // Matrice pour stocker les coups joués

        // Fonction permettant l'affichage du Morpion
        public static void AfficherMorpion(int l, int c)
        {
            //--- Construction de la grille en vérifiant ce qui doit être mi dans chaque case ---

            for (int a = 0; a < grille.GetLength(0); a++)
            {
                Console.Write("\n|===|===|===|\n"); // Séparation des lignes de la grille
                Console.Write("|");
                for (int b = 0; b < grille.GetLength(1); b++) // Vérification de ce qui doit être mi dans chaque case
                {
                    if (grille[a, b] == 0) // Si il n'y a rien dans la case
                    {
                        Console.Write(" - "); // On affiche un simple -
                        Console.Write("|");   // On referme la case
                    }
                    else if (grille[a, b] == 1) // Si le joueur 1 a déjà joué la case
                    {
                        Console.Write(" X "); // On affiche le signe du joueur 1
                        Console.Write("|");   // On referme la case
                    }
                    else if (grille[a, b] == 2) // Si le joueur 2 a déjà joué la case
                    {
                        Console.Write(" O "); // On affiche le signe du joueur 2
                        Console.Write("|");   // On referme la case
                    }
                    else if (a == l && b == c && grille[l, c] == 1) // Ajoute le signe du joueur 1 sur la position qu'il vient de jouer
                    {
                        Console.Write(" X "); // On affiche le signe du joueur 1
                        Console.Write("|");   // On referme la case
                    }
                    else if (a == l && b == c && grille[l, c] == 2) // Ajoute le signe du joueur 2 sur la position qu'il vient de jouer
                    {
                        Console.Write(" O "); // On affiche le signe du joueur 2
                        Console.Write("|");   // On referme la case
                    }
                }
            }
            Console.Write("\n|===|===|===|\n"); // Bas de la grille
        }

        // Fonction permettant de changer
        // dans le tableau quel est le 
        // joueur qui à jouer
        // Vérifie également que le joueur ne sort
        // pas du tableau et que la position
        // n'est pas déjà jouée
        public static bool AJouer(int l, int c, int joueur)
        {
            if (l > 3 || l < 0 || c > 3 || c < 0) // Vérifie si la position jouée est bien dans le tableau
            {
                // Le message d'erreur correspondant
                Console.WriteLine("Vous êtes en dehors du tableau." + "\n" + "Appuyez sur une touche pour rejouer à l'intérieur du tableau.");
                Console.ReadKey(); // Le joueur doit appuyer sur une touche
                Console.Clear(); // Supprime tout le contenu actuellement affiché dans la console
                // Cela retourne false afin de ne pas jouer la position demandée dans le programme principal
                return false;
            }
            else if (grille[l, c] == 1 || grille[l, c] == 2) // Vérifie si la position jouée a déjà été jouée ou non
            {
                // Le message d'erreur correspondant
                Console.WriteLine("La position choisie a déjà été jouée." + "\n" + "Appuyez sur une touche pour rejouer dans une position libre.");
                Console.ReadKey(); // Le joueur doit appuyer sur une touche
                Console.Clear(); // Supprime tout le contenu actuellement affiché dans la console
                // Cela retourne false afin de ne pas jouer la position demandée dans le programme principal
                return false;
            }
            else
            {
                // La position jouée est valable, donc on retourne true pour le programme principal
                return true;
            }
        }

        // Fonction permettant de vérifier si un joueur à gagner
        public static bool Gagner(int l, int c, int joueur)
        {
            // Vérifier les lignes
            for (int i = 0; i < 3; i++)
            {
                if (grille[i, 0] == joueur && grille[i, 1] == joueur && grille[i, 2] == joueur)
                {
                    // Le joueur a bien aligné son signe sur une ligne, donc on retourne true pour le programme principal
                    return true;
                }
            }

            // Vérifier les colonnes
            for (int i = 0; i < 3; i++)
            {
                if (grille[0, i] == joueur && grille[1, i] == joueur && grille[2, i] == joueur)
                {
                    // Le joueur a bien aligné son signe sur une colonne, donc on retourne true pour le programme principal
                    return true;
                }
            }

            // Vérifier les diagonales
            if (grille[0, 0] == joueur && grille[1, 1] == joueur && grille[2, 2] == joueur)
            {
                // Le joueur a bien aligné son signe sur une diagonale, donc on retourne true pour le programme principal
                return true;
            }
            if (grille[0, 2] == joueur && grille[1, 1] == joueur && grille[2, 0] == joueur)
            {
                // Le joueur a bien aligné son signe sur une diagonale, donc on retourne true pour le programme principal
                return true;
            }

            return false;
        }

        // Programme principal
        static void Main(string[] args)
        {
            //--- Déclarations et initialisations ---
            int LigneDébut = Console.CursorTop;     // par rapport au sommet de la fenêtre
            int ColonneDébut = Console.CursorLeft; // par rapport au sommet de la fenêtre

            int essais = 0;    // compteur d'essais
            int joueur = 1;    // 1 pour la premier joueur, 2 pour le second
            int l = 0;       // numéro de ligne
            int c = 0;       // numéro de colonne
            int j, k = 0;      // Parcourir le tableau en 2 dimensions
            bool gagner = false; // Permet de vérifier si un joueur a gagné 

            //--- initialisation de la grille ---
            // On met chaque valeur du tableau à 0, signifiant qu'il n'y a pas de coup joué à cet emplacement
            for (j = 0; j < grille.GetLength(0); j++)
            {
                for (k = 0; k < grille.GetLength(1); k++)
                {
                    grille[j, k] = 0;
                }
            }
            // Tant que aucun joueur n'a gagné et que toutes les cases ne sont pas remplit, la partie continue
            while (!gagner && essais != 9)
            {
                // Permettant de toujours afficher le joueur qui doit jouer
                Console.WriteLine($"Joueur {joueur}, c'est à votre tour.");

                AfficherMorpion(l, c); // Affiche le morpion actualisé

                try
                {
                    Console.WriteLine("Numéro de ligne   =    ");
                    Console.WriteLine("Numéro de colonne =    ");
                    
                    // Peut changer en fonction de comment vous avez fait votre tableau.
                    Console.SetCursorPosition(LigneDébut + 20, ColonneDébut + 9); // Permet de manipuler le curseur dans la fenêtre 
                    l = int.Parse(Console.ReadLine()) - 1;

                    // Peut changer en fonction de comment vous avez fait votre tableau.
                    Console.SetCursorPosition(LigneDébut + 20, ColonneDébut + 10); // Permet de manipuler le curseur dans la fenêtre
                    c = int.Parse(Console.ReadLine()) - 1;

                    if (AJouer(l, c, joueur) == true) // Test si la position jouée est bien disponible
                    {
                        grille[l, c] = joueur; // Mets la position jouée au joueur qui l'a joué, dans le tableau à 2 dimensions
                        AfficherMorpion(l, c); // Affiche le morpion actualisé
                        essais++; // L'essais étant terminé, on le compte rajoute au compteur
                        if (Gagner(l, c, joueur) == true) // Vérifie si le joueur a gagné la partie
                        {
                            string signe;
                            if (joueur == 1) // Associe le joueur au signe qui lui correspond pour le message ci-après
                            {
                                signe = "X";
                            }
                            else
                            {
                                signe = "O";
                            }

                            Console.Clear(); // Supprime tout le contenu actuellement affiché dans la console
                            AfficherMorpion(l, c); // Affiche le morpion actualisé
                            Console.WriteLine($"Le joueur {joueur} a gagné la partie de morpion en alignant 3 {signe} !"); // Annonce le gagnant de la partie
                            break;
                        }

                        // Si toutes les cases sont remplit et qu'il n'y a donc pas eu de joueur gagnant, la partie s'arrête
                        if (essais == 9)
                        {
                            Console.Clear(); // Supprime tout le contenu actuellement affiché dans la console
                            AfficherMorpion(l, c); // Affiche le morpion actualisé
                            break; // Arrête la boucle while en cours
                        }

                        if (joueur == 1) // Permettant d'actualiser le prochain joueur qui va jouer
                        {
                            joueur = 2;
                            Console.Clear(); // Supprime tout le contenu actuellement affiché dans la console
                            Console.WriteLine("Vous avez effectué votre essais, c'est maintenant au joueur 2 de jouer." + "\n" + "Appuyez sur une touche pour continuer.");
                            Console.ReadKey(); // Le joueur doit appuyer sur une touche
                            Console.Clear(); // Supprime tout le contenu actuellement affiché dans la console
                        }
                        else
                        {
                            joueur = 1;
                            Console.Clear(); // Supprime tout le contenu actuellement affiché dans la console
                            Console.WriteLine("Vous avez effectué votre essais, c'est maintenant au joueur 1 de jouer." + "\n" + "Appuyez sur une touche pour continuer.");
                            Console.ReadKey(); // Le joueur doit appuyer sur une touche
                            Console.Clear(); // Supprime tout le contenu actuellement affiché dans la console
                        }
                    }
                }
                catch
                {
                    Console.Clear(); // Supprime tout le contenu actuellement affiché dans la console
                    Console.WriteLine("La saisie n'est pas conforme." + "\n" + "Appuyez sur une touche pour recommencer la saisie des valeurs.");
                    Console.ReadKey(); // Le joueur doit appuyer sur une touche
                    Console.Clear(); // Supprime tout le contenu actuellement affiché dans la console
                }
            }; // Fin Tant que

            // Fin de la partie

            // Permettant d'afficher un message de fin de partie selon si il y a eu un gagnant ou une égalité
            if (Gagner(l, c, joueur) == true)
            {
                Console.WriteLine($"La partie est donc maintenant terminée.");
            }
            else
            {
                Console.WriteLine("La partie est maintenant terminée sans joueur gagnant, à cause d'une égalité.");
            }

            Console.ReadKey();
        }
    }
}