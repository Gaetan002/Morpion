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
        public static void AfficherMorpion(int j, int k)
        {
            string hautGrille =
              "   L1  L2  L3    " +
            "\n  |===|===|===|\n";
            string colonne1Grille =
             $"C1| {grille[0, 0]} | {grille[0, 1]} | {grille[0, 2]} |\n"; /*+
              "  |===|===|===|\n" +
             "C2| {grille[1, 0]} | {grille[1, 1]} | {grille[2, 2]} |\n" +
              "  |===|===|===|\n" +
             "C3| {grille[2, 0]} | {grille[2, 1]} | {grille[2, 2]} |\n" +
              "  |===|===|===|\n";*/


            colonne1Grille.Replace($"{grille[0, 0]}", "-");
            Console.WriteLine(colonne1Grille);

            // Dessiner une grille
            /* if (grille[j, k] == 0)
            {
                Console.Write(
                    "   L1  L2  L3    " +
                  "\n  |===|===|===|\n" +
                    "C1| - | - | - |\n" +
                    "  |===|===|===|\n" +
                    "C2| - | - | - |\n" +
                    "  |===|===|===|\n" +
                    "C3| - | - | - |\n" +
                    "  |===|===|===|\n"
                );
            }
            else
            {
                Console.WriteLine(dessinGrille); // Affiche la grille actualisée
            }
            */
        }
        // Fonction permettant de changer
        // dans le tableau quel est le 
        // joueur qui à jouer
        // Bien vérifier que le joueur ne sort
        // pas du tableau et que la position
        // n'est pas déjà jouée
        public static bool AJouer(int j, int k, int joueur)
        {
            // A compléter

            if (j > 3 || j < 0 || k > 3 || k < 0)
            {
                Console.Clear();
                Console.WriteLine("Vous êtes en dehors du tableau." + "\n" + "Appuyez sur une touche pour rejouer à l'intérieur du tableau.");
                Console.ReadKey();
                return false;
            }
            else if (grille[j, k] == 1 || grille[j, k] == 2)
            {
                Console.Clear();
                Console.WriteLine("La position choisie a déjà été jouée." + "\n" + "Appuyez sur une touche pour rejouer dans une position libre.");
                Console.ReadKey();
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
            // A compléter au dessus de "return false;"
            return false;
        }

        // Programme principal
        static void Main(string[] args)
        {
            //--- Déclarations et initialisations ---
            int LigneDébut = Console.CursorTop;     // par rapport au sommet de la fenêtre
            int ColonneDébut = Console.CursorLeft; // par rapport au sommet de la fenêtre

            int essais = 0;    // compteur d'essais
	        int joueur = 1 ;   // 1 pour la premier joueur, 2 pour le second
	        int l, c = 0;      // numéro de ligne et de colonne
            int j, k = 0;      // Parcourir le tableau en 2 dimensions
            bool gagner = false; // Permet de vérifier si un joueur à gagné 
            bool bonnePosition = false; // Permet de vérifier si la position souhaité est disponible

	        //--- initialisation de la grille ---
            // On met chaque valeur du tableau à 0, signifiant qu'il n'y a pas de coup joué à cet emplacement
	        for (j=0; j < grille.GetLength(0); j++)
            {
                for (k = 0; k < grille.GetLength(1); k++)
                {
                    grille[j, k] = 0;
                }
            }

            j = 0;
            k = 0;
            while(!gagner && essais != 9)
            {
                AfficherMorpion(j, k);

                // A compléter 
                try
                {
                    Console.WriteLine("Numéro de ligne   =    ");
                    Console.WriteLine("Numéro de colonne =    ");
                    try
                    {
                        if (essais == 0)
                        {
                            // Peut changer en fonction de comment vous avez fait votre tableau.
                            Console.SetCursorPosition(LigneDébut + 20, ColonneDébut + 8); // Permet de manipuler le curseur dans la fenêtre 
                            l = int.Parse(Console.ReadLine()) - 1;

                            // Peut changer en fonction de comment vous avez fait votre tableau.
                            Console.SetCursorPosition(LigneDébut + 20, ColonneDébut + 9); // Permet de manipuler le curseur dans la fenêtre
                            c = int.Parse(Console.ReadLine()) - 1;
                        }
                        else
                        {
                            Console.SetCursorPosition(LigneDébut + 20, ColonneDébut + 9);
                            l = int.Parse(Console.ReadLine()) - 1;

                            Console.SetCursorPosition(LigneDébut + 20, ColonneDébut + 10);
                            c = int.Parse(Console.ReadLine()) - 1;
                        }
                        j = l;
                        k = c;
                        if (AJouer(j, k, joueur) == true)
                        {
                            /*if (grille[c, l] == 0)
                            {
                                AfficherMorpion(j, k);
                                string dessinGrille = dessinGrille.Replace($"{grille[j, k]}", "-");
                            }
                            else if (grille[j, k] == 1)
                            {
                                AfficherMorpion(j, k);
                                string dessinGrille = dessinGrille.Replace($"{grille[j, k]}", "X");
                            }
                            else if (grille[j, k] == 2)
                            {
                                AfficherMorpion(j, k);
                                string dessinGrille = dessinGrille.Replace($"{grille[j, k]}", "O");
                            }
                            */


                            essais++; // L'essais étant terminé, on le compte rajoute au compteur
                        }
                        Console.Clear();

                        // A compléter

                        if (joueur == 1)
                        {
                            joueur = 2;
                            Console.Clear();
                            Console.WriteLine("Vous avez effectué votre essais, c'est maintenant au joueur 2 de jouer." + "\n" + "Appuyez sur une touche pour continuer.");
                            Console.ReadKey();
                        }
                        else
                        {
                            joueur = 1;
                            Console.Clear();
                            Console.WriteLine("Vous avez effectué votre essais, c'est maintenant au joueur 1 de jouer." + "\n" + "Appuyez sur une touche pour continuer.");
                            Console.ReadKey();
                        }
                    }
                    catch
                    {
                        Console.WriteLine("La saisie n'est pas conforme." + "\n" + "Appuyez sur une touche pour ressaisir la valeur.");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

                // Changement de joueur
                // A compléter 

            }; // Fin TQ

            // Fin de la partie
            // A compléter 

            Console.ReadKey();
        }
    }
}
