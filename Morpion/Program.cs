using System;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text.RegularExpressions;

namespace Morpion
{
    class Program
    {
        public static int[,] grille = new int[3, 3]; // matrice pour stocker les coups joués

        // Fonction permettant l'affichage du Morpion
        public static void AfficherMorpion(int j, int k)
        {
            // Dessiner une grille
            for ( j = 0; j < grille.GetLength(0); j++)
            {
                Console.Write("\n|====|====|====|\n");
                Console.Write("|");
                for (int i = 0; i < grille.GetLength(1); i++)
                {
                    Console.Write(" -- ");
                    Console.Write("|");
                }

            }
            Console.Write("\n|====|====|====|");
        }

        // Fonction permettant de changer
        // dans le tableau qu'elle est le 
        // joueur qui à jouer
        // Bien vérifier que le joueur ne sort
        // pas du tableau et que la position
        // n'est pas déjà jouée
        public static bool AJouer(int j, int k, int joueur, bool bonnePosition)
        {
            while (j > 3 || j < 0 || k > 3 || k < 0)
            {
                Console.WriteLine("Vous êtes en dehors du tableau, rejouez à l'intérieur du tableau.");
            }
            while (bonnePosition == false)
            {
                Console.WriteLine("La position a déjà été jouée, rejouez dans une autre position.");
            }
            
            if (joueur == 1)
            {
                joueur = 2;
                Console.WriteLine("C'est au joueur 2 de jouer.");
            }
            else
            {
                joueur = 1;
                Console.WriteLine("C'est au joueur 1 de jouer.");
            }

            // A compléter au dessus de "return false;"
            return false;
        }

        // Fonction permettant de vérifier
        // si un joueur à gagner
        public static bool Gagner(int l, int c, int joueur)
        {
            // A compléter au dessus de "return false;"
            return false;
        }

        // Programme principal
        static void Main(string[] args)
        {
            //--- Déclarations et initialisations --
            int LigneDébut = Console.CursorTop;     // par rapport au sommet de la fenêtre
            int ColonneDébut = Console.CursorLeft; // par rapport au sommet de la fenêtre

            int essais = 0;    // compteur d'essais
	        int joueur = 1 ;   // 1 pour la premier joueur, 2 pour le second
	        int l, c = 0;      // numéro de ligne et de colonne
            int j, k = 0;      // Parcourir le tableau en 2 dimensions
            bool gagner = false; // Permet de vérifier si un joueur à gagné 
            bool bonnePosition = false; // Permet de vérifier si la position souhaité est disponible

	        //--- initialisation de la grille ---
            // On met chaque valeur du tableau à 10
	        for (j=0; j < grille.GetLength(0); j++)
		        for (k=0; k < grille.GetLength(1); k++)
			        grille[j,k] = 10;
            while(!gagner && essais != 9)
            {
                AfficherMorpion(j, k);
                // A compléter 
                try
                {
                    Console.WriteLine("Ligne   =    ");
                    Console.WriteLine("Colonne =    ");
                    // Peut changer en fonction de comment vous avez fait votre tableau.
                    Console.SetCursorPosition(LigneDébut + 10, ColonneDébut + 9); // Permet de manipuler le curseur dans la fenêtre 
                    l = int.Parse(Console.ReadLine()) - 1; 
                    // Peut changer en fonction de comment vous avez fait votre tableau.
                    Console.SetCursorPosition(LigneDébut + 10, ColonneDébut + 10); // Permet de manipuler le curseur dans la fenêtre 
                    c = int.Parse(Console.ReadLine()) - 1;

                    // A compléter 
                    AfficherMorpion(j, k);

                    essais++; // L'essais étant terminé, on le compte rajoute au compteur
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

                // Changement de joueur
                // A compléter 
                AJouer(j, k, joueur, bonnePosition);

            }; // Fin TQ

            // Fin de la partie
            // A compléter 

            Console.ReadKey();
        }
    }
}
