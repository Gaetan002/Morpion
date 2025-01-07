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
        public static int[,] grille = new int[3, 3]; // matrice pour stocker les coups joués

        // Fonction permettant l'affichage du Morpion
        public static void AfficherMorpion(int l, int c)
        {
            for (int a = 0; a < grille.GetLength(0); a++)
            {
                Console.Write("\n|===|===|===|\n");
                Console.Write("|");
                for (int b = 0; b < grille.GetLength(1); b++)
                {
                    if (grille[a, b] == 0)
                    {
                        Console.Write(" - ");
                        Console.Write("|");
                    }
                    else if (grille[a, b] == 1)
                    {
                        Console.Write(" X ");
                        Console.Write("|");
                    }
                    else if (grille[a, b] == 2)
                    {
                        Console.Write(" O ");
                        Console.Write("|");
                    }
                    else if (a == l && b == c && grille[l, c] == 1)
                    {
                        Console.Write(" X ");
                        Console.Write("|");
                    }
                    else if (a == l && b == c && grille[l, c] == 2)
                    {
                        Console.Write(" O ");
                        Console.Write("|");
                    }
                }
            }
            Console.Write("\n|===|===|===|\n");
        }
        // Fonction permettant de changer
        // dans le tableau quel est le 
        // joueur qui à jouer
        // Bien vérifier que le joueur ne sort
        // pas du tableau et que la position
        // n'est pas déjà jouée
        public static bool AJouer(int l, int c, int joueur)
        {
            // A compléter

            if (l > 3 || l < 0 || c > 3 || c < 0)
            {
                Console.WriteLine("Vous êtes en dehors du tableau." + "\n" + "Appuyez sur une touche pour rejouer à l'intérieur du tableau.");
                Console.ReadKey();
                Console.Clear();
                return false;
            }
            else if (grille[l, c] == 1 || grille[l, c] == 2)
            {
                Console.WriteLine("La position choisie a déjà été jouée." + "\n" + "Appuyez sur une touche pour rejouer dans une position libre.");
                Console.ReadKey();
                Console.Clear();
                return false;
            }
            else
            {
                return true;
            }
        }

        // Fonction permettant de vérifier si un joueur à gagner
        public static bool Gagner(int l, int c, int joueur)
        {
            // A compléter

            // Vérifier les lignes
            for (int i = 0; i < 3; i++)
            {
                if (grille[i, 0] == joueur && grille[i, 1] == joueur && grille[i, 2] == joueur)
                {
                    return true;
                }
            }

            // Vérifier les colonnes
            for (int i = 0; i < 3; i++)
            {
                if (grille[0, i] == joueur && grille[1, i] == joueur && grille[2, i] == joueur)
                {
                    return true;
                }
            }

            // Vérifier les diagonales
            if (grille[0, 0] == joueur && grille[1, 1] == joueur && grille[2, 2] == joueur)
            {
                return true;
            }
            if (grille[0, 2] == joueur && grille[1, 1] == joueur && grille[2, 0] == joueur)
            {
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
            while (!gagner && essais != 9)
            {
                Console.WriteLine($"Joueur {joueur}, c'est à votre tour.");

                AfficherMorpion(l, c);

                // A compléter 
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

                    if (AJouer(l, c, joueur) == true)
                    {
                        grille[l, c] = joueur;
                        AfficherMorpion(l, c);
                        essais++; // L'essais étant terminé, on le compte rajoute au compteur
                        if (Gagner(l, c, joueur) == true)
                        {
                            string signe;
                            if (joueur == 1)
                            {
                                signe = "X";
                            }
                            else
                            {
                                signe = "O";
                            }

                            Console.Clear();
                            AfficherMorpion(l, c);
                            Console.WriteLine($"Le joueur {joueur} a gagné la partie de morpion en alignant 3 {signe} !");
                            break;
                        }

                        if (essais == 9)
                        {
                            break;
                        }

                        if (joueur == 1)
                        {
                            joueur = 2;
                            Console.Clear();
                            Console.WriteLine("Vous avez effectué votre essais, c'est maintenant au joueur 2 de jouer." + "\n" + "Appuyez sur une touche pour continuer.");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else
                        {
                            joueur = 1;
                            Console.Clear();
                            Console.WriteLine("Vous avez effectué votre essais, c'est maintenant au joueur 1 de jouer." + "\n" + "Appuyez sur une touche pour continuer.");
                            Console.ReadKey();
                            Console.Clear();
                        }
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("La saisie n'est pas conforme." + "\n" + "Appuyez sur une touche pour recommencer la saisie des valeurs.");
                    Console.ReadKey();
                    Console.Clear();
                }

                // Changement de joueur
                // A compléter 

            }; // Fin TQ

            // Fin de la partie
            // A compléter 

            if (Gagner(l, c, joueur) == true)
            {
                Console.WriteLine($"La partie est donc maintenant terminée.");
            }
            else
            {
                Console.WriteLine("La partie est maintenant terminée sans joueur gagant.");
            }

            Console.ReadKey();
        }
    }
}