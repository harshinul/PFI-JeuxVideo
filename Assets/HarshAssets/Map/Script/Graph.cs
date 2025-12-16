using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding
{
    public class Graph
    {
        int[,] matriceAdjacence;

        public Graph(int[,] matriceAdjacence) 
        {
            this.matriceAdjacence = matriceAdjacence;
        }

        public List<int> DepthFirstSearch(int départ, int arrivé)
        {
            Stack<(int, int)> AExplorer = new Stack<(int, int)>(); // <(où tu vas, où tu es, )>
            List<(int, int)> DéjaVu = new List<(int, int)>(); // même chose

            AExplorer.Push((départ, -1));

            while (AExplorer.Count != 0)
            {
                (int enExploration, int origine) actif = AExplorer.Pop();
                DéjaVu.Add(actif);

                int lenght = matriceAdjacence.GetLength(0);
                for (int i = 0; i < lenght; ++i)
                {
                    if (matriceAdjacence[actif.enExploration, i] != 0)
                    {
                        if (!Contient(DéjaVu, i) && !Contient(AExplorer, i))
                        {
                            AExplorer.Push((i, actif.enExploration));
                        }
                    }
                }
            }
            return invert(ConstruireChemin(DéjaVu, arrivé));

        }
        public List<int> BreadthFirstSearch(int départ, int arrivé)
        {
            Queue<(int, int)> AExplorer = new Queue<(int, int)>();
            List<(int, int)> DéjaVu = new List<(int, int)>();

            AExplorer.Enqueue((départ, -1));

            while (AExplorer.Count != 0)
            {
                (int enExploration, int origine) actif = AExplorer.Dequeue();
                DéjaVu.Add(actif);

                int lenght = matriceAdjacence.GetLength(0);
                for (int i = 0; i < lenght; ++i)
                {
                    if (matriceAdjacence[actif.enExploration, i] != 0)
                    {
                        if (!Contient(DéjaVu, i) && !Contient(AExplorer, i))
                        {
                            AExplorer.Enqueue((i, actif.enExploration));
                        }
                    }
                }
            }
            return invert(ConstruireChemin(DéjaVu, arrivé));

        }
        public List<int> DijkstraSearch(int départ, int arrivé) // recherche pour le moins couteux chemin
        {
            CustomQueue AExplorer = new CustomQueue();
            List<(int, float, int)> DéjaVu = new List<(int, float, int)>();

            AExplorer.Ajouter((départ, 0, -1));

            while (AExplorer.Count != 0)
            {
                (int index, float cout, int origine) actif = AExplorer.Retirer();
                DéjaVu.Add(actif);
                if (actif.index == arrivé)
                {
                    break; //on a trouvé le chemin le plus court
                }

                int lenght = matriceAdjacence.GetLength(0);
                for (int i = 0; i < lenght; ++i)
                {
                    if (matriceAdjacence[actif.index, i] != 0)
                    {
                        if (!Contient(DéjaVu, i))
                        {
                            AExplorer.Ajouter((i, matriceAdjacence[actif.index, i] + actif.cout, actif.index));
                        }
                    }
                }
            }
            return invert(ConstruireChemin(DéjaVu, arrivé));

        }
        public List<int> AStarSearch(int départ, int arrivé)
        {
            CustomQueue AExplorer = new CustomQueue();
            List<(int, float, int)> DéjaVu = new List<(int, float, int)>();

            AExplorer.Ajouter((départ, 0, -1));

            while (AExplorer.Count != 0)
            {
                (int index, float cout, int origine) actif = AExplorer.Retirer();
                DéjaVu.Add(actif);
                if (actif.index == arrivé)
                {
                    break;
                }

                int lenght = matriceAdjacence.GetLength(0);
                for (int i = 0; i < lenght; ++i)
                {
                    if (matriceAdjacence[actif.index, i] != 0)
                    {
                        if (!Contient(DéjaVu, i))
                        {
                            AExplorer.Ajouter((i, matriceAdjacence[actif.index, i] + actif.cout + Heuristique(actif.index, arrivé), actif.index));
                        }
                    }
                }
            }
            return invert(ConstruireChemin(DéjaVu, arrivé));

        }

        private int Heuristique(int index, int arrivé)
        {
            return 0;
        }

        private List<int> ConstruireChemin(List<(int, int)> déjaVu, int arrivé)
        {
            List<int> chemin = new();
            foreach((int node, int origine) i in déjaVu)
            {
                if(i.node == arrivé)
                {
                    chemin.Add(i.node);
                    chemin.AddRange(ConstruireChemin(déjaVu, i.origine));
                }
            }
            return chemin;
        }

        private List<int> ConstruireChemin(List<(int, float, int)> déjaVu, int arrivé)
        {
            List<int> chemin = new();
            foreach ((int node,int cout, int origine) i in déjaVu)
            {
                if (i.node == arrivé)
                {
                    chemin.Add(i.node);
                    chemin.AddRange(ConstruireChemin(déjaVu, i.origine));
                }
            }
            return chemin;
        }

        bool Contient(IEnumerable<(int, int)> list, int value)
        {
            foreach (var val in list)
            {
                if (val.Item1 == value)
                    return true;
            }
            return false;
        }

        bool Contient(IEnumerable<(int, float, int)> list, int value)
        {
            foreach (var val in list)
            {
                if (val.Item1 == value)
                    return true;
            }
            return false;
        }

        List<int> invert(List<int> a)
        {
            List<int> b = new List<int>(a.Count);
            for(int i = a.Count - 1; i >= 0; --i )
            {
                b.Add(a[i]);
            }
            return b;
        }

    }
}
